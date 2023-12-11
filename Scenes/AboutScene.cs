using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace DKoFinal.Scenes
{
    public class AboutScene:GameScene
    {
        SpriteBatch spriteBatch;

        public AboutScene(Game game, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;

            /* ============= Load image & font content ============= */
            Texture2D mainBackgroundImg = dkoFinal.Content.Load<Texture2D>("MainScene/Blue");
            Texture2D mainTitleImg = dkoFinal.Content.Load<Texture2D>("MainScene/GameTitle");
            SpriteFont regular = dkoFinal.Content.Load<SpriteFont>("Fonts/regular");

            /* ============= Add background & title image components ============= */
            Background mainBackground = new Background(dkoFinal, spriteBatch, mainBackgroundImg, backgroundWidth, backgroundHeight);
            this.Components.Add(mainBackground);

            Image menuTitle = new Image(dkoFinal, spriteBatch, mainTitleImg, new Vector2(backgroundWidth / 2, backgroundHeight / 3), new Rectangle(0, 0, mainTitleImg.Width, mainTitleImg.Height), Color.White, new Vector2(mainTitleImg.Width / 2, mainTitleImg.Height / 2), 0.0f, 0.3f, SpriteEffects.None, 0.0f);
            this.Components.Add(menuTitle);

            /* ============= Add text components ============= */
            Text leaderboardTitle = new Text(dkoFinal, "DEVELOPER: DAHYUN KO", spriteBatch, regular, new Vector2(backgroundWidth / 2, backgroundHeight / 3*2), Color.White);
            this.Components.Add(leaderboardTitle);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
