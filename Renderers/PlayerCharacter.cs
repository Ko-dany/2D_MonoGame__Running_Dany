using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Renderers
{
    internal class PlayerCharacter : DrawableGameComponent
    {
        const int COL = 11;

        SpriteBatch spriteBatch;
        Texture2D playerTexture;
        Vector2 position;
        Color color;

        List<Rectangle> srcRectangles;
        float rotation;
        Vector2 origin;
        float scale;
        SpriteEffects spriteEffects;
        float layerDepth;

        float moveSpeed = 5.0f;

        int currentImage = 0;
        int delay = 1;
        float counter = 0;

        public PlayerCharacter(Game game, SpriteBatch spriteBatch, Texture2D playerTexture, Vector2 position, Color color):base(game)
        {
            this.spriteBatch = spriteBatch;
            this.playerTexture = playerTexture;
            this.position = position;
            this.color = color;

            rotation = 0.0f;
            origin = new Vector2(0, playerTexture.Height);
            scale = 1.0f;
            spriteEffects = SpriteEffects.None;
            layerDepth = 0.0f;

            srcRectangles = new List<Rectangle>();
            for (int i=0; i<COL; i++)
            {
                Rectangle rectangle = new Rectangle(i * playerTexture.Width / COL, 0, playerTexture.Width / COL, playerTexture.Height);
                srcRectangles.Add(rectangle);
            }
        }

        public override void Update(GameTime gameTime)
        {
            counter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(counter > delay)
            {
                counter = 0;
                currentImage += 1;
                if(currentImage == srcRectangles.Count())
                {
                    currentImage = 0;
                }
            }

            /*
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
            {
                position.X -= moveSpeed;
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                position.X += moveSpeed;
            }
            */

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(playerTexture, position, srcRectangles[currentImage], color, rotation, origin, scale, spriteEffects, layerDepth);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
