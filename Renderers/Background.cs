using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace DKoFinal.Renderers
{
    public class Background : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D mainBackgroundImg;
        int backgroundWidth;
        int backgroundHeight;

        public Background(Game game, SpriteBatch spriteBatch, Texture2D mainBackgroundImg, int backgroundWidth, int backgroundHeight):base(game)
        {
            this.spriteBatch = spriteBatch;
            this.mainBackgroundImg = mainBackgroundImg;
            this.backgroundWidth = backgroundWidth;
            this.backgroundHeight = backgroundHeight;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            for (int y = 0; y < backgroundHeight / mainBackgroundImg.Height; y++)
            {
                for (int x = 0; x < backgroundWidth / mainBackgroundImg.Width; x++)
                {
                    spriteBatch.Draw(mainBackgroundImg, new Vector2(x * mainBackgroundImg.Width, y * mainBackgroundImg.Height), Color.White);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
