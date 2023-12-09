using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Renderers
{
    public class CheckpointAnimation : DrawableGameComponent
    {
        const int textureColumn = 10;
        SpriteBatch spriteBatch;
        Texture2D checkpointTexture;
        Vector2 position;
        Color color;
        float rotation;
        Vector2 origin;
        float scale;
        SpriteEffects spriteEffects;
        float layerDepth;

        List<Rectangle> checkpointRectangles;
        int currentRectangleIndex = 0;

        int delay = 50;
        float counter = 0;

        Vector2 speed = new Vector2(3, 0);

        public CheckpointAnimation(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.position = position;

            DkoFinal dkoFinal = (DkoFinal)game;
            checkpointTexture = dkoFinal.Content.Load<Texture2D>("Level1/Checkpoint");

            color = Color.White;
            rotation = 0.0f;
            origin = new Vector2(0,0);
            scale = 1.5f;
            spriteEffects = SpriteEffects.None;
            layerDepth = 0.0f;

            checkpointRectangles = new List<Rectangle>();
            for (int i = 0; i < textureColumn; i++)
            {
                Rectangle rectangle = new Rectangle(i * checkpointTexture.Width / textureColumn, 0, checkpointTexture.Width / textureColumn, checkpointTexture.Height);
                checkpointRectangles.Add(rectangle);
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, (checkpointTexture.Width/textureColumn)*(int)scale, (checkpointTexture.Height)*(int)scale);
        }

        public override void Update(GameTime gameTime)
        {
            position -= speed;

            counter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (counter > delay)
            {
                counter = 0;

                currentRectangleIndex += 1;
                if (currentRectangleIndex == checkpointRectangles.Count)
                {
                    currentRectangleIndex = 0;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(checkpointTexture, position, checkpointRectangles[currentRectangleIndex], color, rotation, origin, scale, spriteEffects, layerDepth);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
