using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Renderers
{
    public class ObstacleImage : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D obstacleTexture;
        private Vector2 position;
        private Vector2 speed;
        public ObstacleImage(Game game, SpriteBatch spriteBatch, Texture2D obstacleTexture, Vector2 position, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.obstacleTexture = obstacleTexture;
            this.position = position;
            this.speed = speed;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, obstacleTexture.Width, obstacleTexture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            position -= speed;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(obstacleTexture, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
