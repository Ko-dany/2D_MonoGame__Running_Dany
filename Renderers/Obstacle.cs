using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Renderers
{
    public class Obstacle : DrawableGameComponent
    {
        int textureColumn;
        SpriteBatch spriteBatch;
        Texture2D obstacleTexture;
        Vector2 position;
        Color color;
        float rotation;
        Vector2 origin;
        float scale;
        SpriteEffects spriteEffects;
        float layerDepth;

        int backgroundWidth;
        int backgroundHeight;

        List<Rectangle> obstacleRectangles;
        int currentRectangleIndex = 0;

        int delay = 200;
        float counter = 0;

        Vector2 speed;
        bool hitBottom = false;

        public Obstacle(Game game, SpriteBatch spriteBatch, Texture2D obstacleTexture, float scale, Vector2 position, Vector2 speed, int textureColumn, int backgroundWidth, int backgroundHeight) : base(game)
        {
            this.textureColumn = textureColumn;
            this.spriteBatch = spriteBatch;
            this.obstacleTexture = obstacleTexture;
            this.position = position;
            this.speed = speed;

            color = Color.White;
            rotation = 0.0f;
            origin = new Vector2(0,0);
            this.scale = scale;
            spriteEffects = SpriteEffects.None;
            layerDepth = 0.0f;

            this.backgroundWidth = backgroundWidth;
            this.backgroundHeight = backgroundHeight;

            obstacleRectangles = new List<Rectangle>();
            for (int i = 0; i < textureColumn; i++)
            {
                Rectangle rectangle = new Rectangle(i * obstacleTexture.Width / textureColumn, 0, obstacleTexture.Width / textureColumn, obstacleTexture.Height);
                obstacleRectangles.Add(rectangle);
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, (obstacleTexture.Width/textureColumn)*(int)scale, (obstacleTexture.Height)*(int)scale);
        }

        public override void Update(GameTime gameTime)
        {
            position.X -= speed.X;

            if (hitBottom)
            {
                position.Y -= speed.Y;
                if(position.Y <= 0) { hitBottom = false; }
            }
            else
            {
                position.Y += speed.Y;
                if (position.Y >= backgroundHeight - obstacleTexture.Height) { hitBottom = true; }
            }

            counter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (counter > delay)
            {
                counter = 0;

                currentRectangleIndex += 1;
                if (currentRectangleIndex == obstacleRectangles.Count)
                {
                    currentRectangleIndex = 0;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(obstacleTexture, position, obstacleRectangles[currentRectangleIndex], color, rotation, origin, scale, spriteEffects, layerDepth);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
