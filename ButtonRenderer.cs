using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;

namespace DKoFinal
{
    internal class ButtonRenderer
    {
        private SpriteFont font;
        private Vector2 position;
        private string text;
        private Color backgroundColor;
        private Color borderColor;

        public ButtonRenderer(SpriteFont font, Vector2 position, string text, Color backgroundColor, Color borderColor)
        {
            this.font = font;
            this.position = position;
            this.text = text;
            this.backgroundColor = backgroundColor;
            this.borderColor = borderColor;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawRectangle(new Rectangle((int)position.X, (int)position.Y, 200, 50), backgroundColor);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 10, position.Y + 10), Color.White);
            spriteBatch.DrawRectangle(new Rectangle((int)position.X, (int)position.Y, 200, 50), borderColor, 2);

            spriteBatch.End();
        }
    }
}
