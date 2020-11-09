using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1;
namespace Game1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MovableComponent hero;
        BackgroundImage background;
        EnemyFactory enemyFactory;
        public BulletFactory bulletFactory;
        public List<Enemy> enemies;
        readonly int enemyAmount = 15;

        public SoundEffect machinegunSound;
        public SoundEffect s;
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
            enemyFactory = new EnemyFactory(this);
            bulletFactory = new BulletFactory(this);
            enemies = enemyFactory.enemies;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            machinegunSound = Content.Load<SoundEffect>("m_gun");
            shotgunSound = Content.Load<SoundEffect>("s_gun");
            background = new BackgroundImage(this, Content.Load<Texture2D>("background"));
            hero = new MovableComponent(this, Content.Load<Texture2D>("hero"), new Vector2(12, 650));
            s = Content.Load<SoundEffect>("m_gun");
            for (int i = 0; i < enemyAmount; i++)
                enemyFactory.CreateEnemy();

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
