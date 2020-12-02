using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1;
using spacebot.map_service;
using spacebot;
namespace Game1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Hero hero;
        BackgroundImage background;
        EnemyFactory enemyFactory;
        CometFactory cometFactory;
        MapBuildingService mapBuilder;
        uint score;

        public BulletFactory bulletFactory;
        public List<IColliding> collidableItems = new List<IColliding>();
        readonly int enemyAmount = 15;

        public SoundEffect machinegunSound;
        public SoundEffect shotgunSound;

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
            machinegunSound = Content.Load<SoundEffect>("m_gun");
            shotgunSound = Content.Load<SoundEffect>("s_gun");
            background = new BackgroundImage(this, Content.Load<Texture2D>("background"));

            enemyFactory = new EnemyFactory(this, collidableItems);
            cometFactory = new CometFactory(this, Content.Load<Texture2D>("meteor"));
            bulletFactory = new BulletFactory(this);
            hero = new Hero(this, Content.Load<Texture2D>("hero"), new Vector2(12, 650));

            StreamReader file = File.OpenText("lvl1.txt");

            mapBuilder = new MapBuildingService(file, enemyFactory, cometFactory);

            AnimationFactory.Initialize(this, Content.Load<Texture2D>("explosion"), Content.Load<SoundEffect>("death"));

            mapBuilder.BuildMap();

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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            base.Draw(gameTime);
            spriteBatch.End(); 
        }
    }
}
