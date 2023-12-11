using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Scenes
{
    public class LeaderBoardScene : GameScene
    {
        SpriteBatch spriteBatch;

        public LeaderBoardScene(Game game, string scores, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;

            /* ============= Load image & font content ============= */
            Texture2D backgroundImg = dkoFinal.Content.Load<Texture2D>("MainScene/Blue");
            SpriteFont regular = dkoFinal.Content.Load<SpriteFont>("Fonts/regular");

            /*============ Add background component ============*/
            Background mainBackground = new Background(dkoFinal, spriteBatch, backgroundImg, backgroundWidth, backgroundHeight);
            this.Components.Add(mainBackground);

            /* ============= Add text components ============= */
            Text leaderboardTitle = new Text(dkoFinal, "Leader Board", spriteBatch, regular, new Vector2(backgroundWidth / 2, backgroundHeight / 3), Color.Black);
            this.Components.Add(leaderboardTitle);

            Text scoreText = new Text(dkoFinal, scores, spriteBatch, regular, new Vector2(backgroundWidth / 2, backgroundHeight / 2), Color.Black);
            this.Components.Add(scoreText);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
