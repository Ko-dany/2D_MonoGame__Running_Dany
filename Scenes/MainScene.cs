using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Scenes
{
    internal class MainScene:GameScene
    {
        SpriteBatch spriteBatch;
        Texture2D mainBackgroundImg;
        Texture2D mainTitle;

        int backgroundWidth;
        int backgroundHeight;

        public MainScene(Game game, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal._spriteBatch;

            mainBackgroundImg = dkoFinal.Content.Load<Texture2D>("MainScene/Blue");
            mainTitle = dkoFinal.Content.Load<Texture2D>("MainScene/GameTitle");

            this.backgroundWidth = backgroundWidth;
            this.backgroundHeight = backgroundHeight;
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            for(int y=0; y< backgroundHeight/mainBackgroundImg.Height; y++)
            {
                for(int x=0; x<backgroundWidth/mainBackgroundImg.Width; x++)
                {
                    spriteBatch.Draw(mainBackgroundImg, new Vector2(x*mainBackgroundImg.Width, y*mainBackgroundImg.Height), Color.White);
                }
            }
            spriteBatch.Draw(mainTitle, new Vector2((backgroundWidth - mainTitle.Width)/2,(backgroundHeight - mainTitle.Height)/2), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
