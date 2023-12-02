using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Renderers
{
    internal class ButtonRenderer
    {
        SpriteFont font;
        Vector2 position;
        string text;
        Color textColor;
        Color backgroundColor;
        Color borderColor;


        public ButtonRenderer(SpriteFont font, Vector2 position, string text, Color textColor, Color backgroundColor, Color borderColor)
        {
            this.font = font;
            this.position = position;
            this.text = text;
            this.textColor = textColor;
            this.backgroundColor = backgroundColor;
            this.borderColor = borderColor;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            Vector2 textSize = font.MeasureString(text);
            Vector2 buttonSize = new Vector2(200, 50);
            Vector2 buttonPosition = position - buttonSize / 2;

            //spriteBatch.DrawRectangle(new Rectangle((int)buttonPosition.X, (int)buttonPosition.Y, (int)buttonSize.X, (int)buttonSize.Y), borderColor, 2);
            //spriteBatch.DrawRectangle(new Rectangle((int)buttonPosition.X, (int)buttonPosition.Y, (int)buttonSize.X, (int)buttonSize.Y), backgroundColor);
            spriteBatch.DrawString(font, text, buttonPosition + buttonSize / 2 - textSize / 2, textColor);
            spriteBatch.End();
        }
    }
}
