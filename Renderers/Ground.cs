using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace DKoFinal.Renderers
{
    public class Ground : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D groundTexture;
        Vector2 bottomPosition1, topPosition1, bottomPosition2, topPosition2;
        Rectangle srcRectangle;
        Color color;
        float rotation;
        Vector2 origin;
        float scale;
        SpriteEffects spriteEffects;
        float layerDepth;

        float speed;
        int backgroundHeight;

        public Ground(Game game, SpriteBatch spriteBatch, Texture2D groundTexture, Vector2 topPosition1, Vector2 bottomPosition1, Vector2 topPosition2, Vector2 bottomPosition2):base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            this.groundTexture = groundTexture;
            this.spriteBatch = spriteBatch;
            this.topPosition1 = topPosition1;
            this.bottomPosition1 = bottomPosition1;
            this.topPosition2 = topPosition2;
            this.bottomPosition2 = bottomPosition2;

            srcRectangle = new Rectangle(0,0, groundTexture.Width, groundTexture.Height);
            color = Color.White;
            rotation = 0.0f;
            origin = new Vector2(0, 0);
            scale = 1.0f;
            spriteEffects = SpriteEffects.None;
            layerDepth = 0.0f;

            speed = 2.0f;
        }

        public override void Update(GameTime gameTime)
        {
            bottomPosition1.X -= speed;
            topPosition1.X -= speed;
            bottomPosition2.X -= speed;
            topPosition2.X -= speed;

            if (bottomPosition1.X + groundTexture.Width < 0)
            {
                bottomPosition1.X = topPosition2.X + groundTexture.Width;
            }
            if (topPosition1.X + groundTexture.Width < 0)
            {
                topPosition1.X = bottomPosition2.X + groundTexture.Width;
            }

            if (bottomPosition2.X + groundTexture.Width < 0)
            {
                bottomPosition2.X = topPosition1.X + groundTexture.Width;
            }
            if (topPosition2.X + groundTexture.Width < 0)
            {
                topPosition2.X = bottomPosition1.X + groundTexture.Width;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(groundTexture, bottomPosition1, srcRectangle, color, rotation, origin, scale, spriteEffects, layerDepth);
            spriteBatch.Draw(groundTexture, topPosition1, srcRectangle, color, rotation, origin, scale, SpriteEffects.FlipVertically, layerDepth);

            spriteBatch.Draw(groundTexture, bottomPosition2, srcRectangle, color, rotation, origin, scale, spriteEffects, layerDepth);
            spriteBatch.Draw(groundTexture, topPosition2, srcRectangle, color, rotation, origin, scale, SpriteEffects.FlipVertically, layerDepth);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public List<Rectangle> GetBounds()
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            rectangles.Add(new Rectangle(0, 0, groundTexture.Width, groundTexture.Height));
            rectangles.Add(new Rectangle((int)bottomPosition1.X, (int)bottomPosition1.Y, groundTexture.Width, groundTexture.Height));

            return rectangles;
        }

        public Rectangle GetBottomBound()
        {
            return new Rectangle((int)bottomPosition1.X, (int)bottomPosition1.Y, groundTexture.Width, groundTexture.Height);
        }
    }
}
