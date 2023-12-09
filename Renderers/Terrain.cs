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
    public class Terrain : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D horizontalTexture, verticalTexture;
        Vector2 bottomPosition1, topPosition1, bottomPosition2, topPosition2, startLinePosition, endLinePosition;
        Rectangle horizontalRectangle, verticalRectangle;
        Color color;
        float rotation;
        Vector2 origin;
        float scale;
        float layerDepth;

        Vector2 speed;

        public Terrain(Game game, SpriteBatch spriteBatch, Texture2D horizontalTexture, Vector2 topPosition1, Vector2 bottomPosition1, Vector2 topPosition2, Vector2 bottomPosition2, Texture2D verticalTexture, Vector2 startLinePosition, Vector2 endLinePosition) :base(game)
        {
            this.spriteBatch = spriteBatch;

            this.horizontalTexture = horizontalTexture;
            this.verticalTexture = verticalTexture;

            /*====== Terrain for the ceiling ======*/
            this.topPosition1 = topPosition1;
            this.topPosition2 = topPosition2;

            /*====== Terrain for the ground ======*/
            this.bottomPosition1 = bottomPosition1;
            this.bottomPosition2 = bottomPosition2;

            /*====== Terrain for the start line the game ======*/
            this.startLinePosition = startLinePosition;

            /*====== Terrain for the end line the game ======*/
            this.endLinePosition = endLinePosition;

            horizontalRectangle = new Rectangle(0,0, horizontalTexture.Width, horizontalTexture.Height);
            verticalRectangle = new Rectangle(0, 0, verticalTexture.Width, verticalTexture.Height);
            color = Color.White;
            rotation = 0.0f;
            origin = new Vector2(0, 0);
            scale = 1.0f;
            layerDepth = 0.0f;

            speed = new Vector2(3, 0);
        }

        public override void Update(GameTime gameTime)
        {
            bottomPosition1 -= speed;
            topPosition1 -= speed;
            bottomPosition2 -= speed;
            topPosition2 -= speed;

            if (bottomPosition1.X + horizontalTexture.Width < 0)
            {
                bottomPosition1.X = topPosition2.X + horizontalTexture.Width;
            }
            if (topPosition1.X + horizontalTexture.Width < 0)
            {
                topPosition1.X = bottomPosition2.X + horizontalTexture.Width;
            }

            if (bottomPosition2.X + horizontalTexture.Width < 0)
            {
                bottomPosition2.X = topPosition1.X + horizontalTexture.Width;
            }
            if (topPosition2.X + horizontalTexture.Width < 0)
            {
                topPosition2.X = bottomPosition1.X + horizontalTexture.Width;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(horizontalTexture, bottomPosition1, horizontalRectangle, color, rotation, origin, scale, SpriteEffects.None, layerDepth);
            spriteBatch.Draw(horizontalTexture, topPosition1, horizontalRectangle, color, rotation, origin, scale, SpriteEffects.FlipVertically, layerDepth);

            spriteBatch.Draw(horizontalTexture, bottomPosition2, horizontalRectangle, color, rotation, origin, scale, SpriteEffects.None, layerDepth);
            spriteBatch.Draw(horizontalTexture, topPosition2, horizontalRectangle, color, rotation, origin, scale, SpriteEffects.FlipVertically, layerDepth);

            spriteBatch.Draw(verticalTexture, endLinePosition, verticalRectangle, color, rotation, origin, scale, SpriteEffects.None, layerDepth);
            spriteBatch.Draw(verticalTexture, startLinePosition, verticalRectangle, color, rotation, origin, scale, SpriteEffects.FlipHorizontally, layerDepth);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public List<Rectangle> GetAllBounds()
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            rectangles.Add(new Rectangle((int)topPosition1.X, (int)topPosition1.Y, horizontalTexture.Width, horizontalTexture.Height));
            rectangles.Add(new Rectangle((int)topPosition2.X, (int)topPosition2.Y, horizontalTexture.Width, horizontalTexture.Height));
            rectangles.Add(new Rectangle((int)bottomPosition1.X, (int)bottomPosition1.Y, horizontalTexture.Width, horizontalTexture.Height));
            rectangles.Add(new Rectangle((int)bottomPosition2.X, (int)bottomPosition2.Y, horizontalTexture.Width, horizontalTexture.Height));

            rectangles.Add(new Rectangle((int)startLinePosition.X, (int)startLinePosition.Y, verticalTexture.Width, verticalTexture.Height));
            rectangles.Add(new Rectangle((int)endLinePosition.X, (int)endLinePosition.Y, verticalTexture.Width, verticalTexture.Height));

            return rectangles;
        }

        public List<Rectangle> GetBottomBound()
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            rectangles.Add(new Rectangle((int)bottomPosition1.X, (int)bottomPosition1.Y, horizontalTexture.Width, horizontalTexture.Height));

            return rectangles;
        }

        public Rectangle GetEndLineBound()
        {
            return new Rectangle((int)endLinePosition.X, (int)endLinePosition.Y, verticalTexture.Width, verticalTexture.Height);
        }
    }
}
