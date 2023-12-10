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
    public class LearderboardScene : GameScene
    {
        SpriteBatch spriteBatch;

        public LearderboardScene(Game game, string scores, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;

            SpriteFont regular = dkoFinal.Content.Load<SpriteFont>("Fonts/regular");

            Text leaderboardTitle = new Text(dkoFinal, "Leader Board", spriteBatch, regular, new Vector2(backgroundWidth / 2, backgroundHeight / 3));
            this.Components.Add(leaderboardTitle);

            Text scoreText = new Text(dkoFinal, scores, spriteBatch, regular, new Vector2(backgroundWidth / 2, backgroundHeight / 2));
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
