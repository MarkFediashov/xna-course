using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using spacebot.map_service;
using spacebot;
using spacebot.ui;
namespace Game1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Hero hero { get; private set; }
        BackgroundImage background;
        EnemyFactory enemyFactory;
        CometFactory cometFactory;
        MapBuildingService mapBuilder;
        public SpriteFont font;
        List<string> levelList = new List<string>() { "lvl1.txt", "lvl2.txt", "lvl3.txt" };
        public string playerName;
        public List<Weapron> wList = new List<Weapron>();
        public List<float> levelScore = new List<float>();
        HUD hud;
        bool hasEnded = false;
        int currentLevel = 0;

        public BulletFactory bulletFactory;
        public List<IColliding> collidableItems = new List<IColliding>();

        public SoundEffect machinegunSound;
        public SoundEffect shotgunSound;
        public SoundEffect themeMusic;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            themeMusic = Content.Load<SoundEffect>("theme");
            themeMusic.Play(volume: 0.1f, pitch:0, pan:0);   

            font = Content.Load<SpriteFont>("font");

            Menu menu = new Menu(this);
            //Services.AddService(typeof(MapBuildingService), mapBuilder);
        }

        public void Startup()
        {
            machinegunSound = Content.Load<SoundEffect>("m_gun");
            shotgunSound = Content.Load<SoundEffect>("s_gun");
            background = new BackgroundImage(this, Content.Load<Texture2D>("background"));
            enemyFactory = new EnemyFactory(this, collidableItems);
            cometFactory = new CometFactory(this, Content.Load<Texture2D>("meteor"));
            bulletFactory = new BulletFactory(this);
            hud = new HUD(this);

            AnimationFactory.Initialize(this, Content.Load<Texture2D>("explosion"), Content.Load<SoundEffect>("death"));
            hero = new Hero(this, Content.Load<Texture2D>("hero"), new Vector2(12, 650));
            LoadNewLevel();
        }

        public void LoadNewLevel()
        {
            if (currentLevel < levelList.Count)
            {
                StreamReader file = File.OpenText(levelList[currentLevel++]);
                mapBuilder = new MapBuildingService(file, enemyFactory, cometFactory, this);
                mapBuilder.BuildMap(wList);
                file.Close();
                hero.SetPosition(new Vector2(12, 650));
                hero.Refueling();
                hero.Weapron = wList;
            }
            else
            {
                hasEnded = true;
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            base.Draw(gameTime);
            if (hasEnded)
            {
                Vector2 position = new Vector2(30, 30);
                int counter = 1;
                float total = 0f;
                foreach(float s in levelScore)
                {
                    string lineStr = "Level #" + counter.ToString() + "      " + s.ToString();
                    total += s;
                    spriteBatch.DrawString(font, lineStr, position, Color.Azure);
                    position.Y += 30;
                    counter++;
                }
                position.Y += 30;
                string totalStr = "Total            " + total.ToString();
                spriteBatch.DrawString(font, totalStr, position, Color.Azure);
            }
            spriteBatch.End(); 
        }

        public void CompleteLevel()
        {
            this.collidableItems.Clear();
        }
    }
}
