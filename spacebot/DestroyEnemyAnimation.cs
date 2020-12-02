using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace spacebot
{
    public class DestroyEnemyAnimation : DrawableGameComponent
    {
        Texture2D texture;
        readonly Vector2 position;
        SoundEffect sound;
        Rectangle rectangle;
        int elapsedTime;
        uint currentIndex;
        static readonly int FrameWidth = 150;
        static readonly int FrameHeigth = 150;

        public DestroyEnemyAnimation(Game game, Texture2D texture, SoundEffect sound, Vector2 position) : base(game)
        {
            this.texture = texture;
            this.position = position;
            rectangle = new Rectangle(new Point(0, 0), new Point(FrameHeigth, FrameWidth));
            game.Components.Add(this);
            this.sound = sound;
            sound.Play();
        }

        private void ComputeCurrentFrame(GameTime time)
        {
            elapsedTime += time.ElapsedGameTime.Milliseconds;
            if (elapsedTime > 50)
            {
                elapsedTime = 0;
                rectangle.Offset(FrameWidth, 0);
                if (currentIndex++ > 7)
                {
                    Game.Components.Remove(this);
                }
            }    
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch batch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            ComputeCurrentFrame(gameTime);
            batch.Draw(texture, position, rectangle, color: Color.White);
            base.Draw(gameTime);
        }

    }

    public class AnimationFactory
    {
        static Texture2D texture;
        static SoundEffect sound;
        static Game game;

        public static void Initialize(Game game, Texture2D texture, SoundEffect sound)
        {
            AnimationFactory.game = game;
            AnimationFactory.sound = sound;
            AnimationFactory.texture = texture;
        }

        public static void SpawnAnimation(Vector2 position)
        {
            new DestroyEnemyAnimation(game, texture, sound, position);
        }
    }
}
