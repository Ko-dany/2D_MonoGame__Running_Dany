using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace DKoFinal.Scenes
{
    public class GameResultScene : GameScene
    {
        SpriteBatch spriteBatch;
        MenuSelection menuSelection;

        public GameResultScene(Game game, string gameResult, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;

            /* ============= Load image & font content ============= */
            SpriteFont selected = dkoFinal.Content.Load<SpriteFont>("Fonts/selected");
            SpriteFont regular = dkoFinal.Content.Load<SpriteFont>("Fonts/regular");
            Texture2D resultBackgroundImg = dkoFinal.Content.Load<Texture2D>("MainScene/Blue");
            Background resultBackground = new Background(dkoFinal, spriteBatch, resultBackgroundImg, backgroundWidth, backgroundHeight);
            this.Components.Add(resultBackground);

            /* ============= Add text component ============= */
            Text resultText = new Text(dkoFinal, gameResult, spriteBatch, regular, new Vector2(backgroundWidth / 2, backgroundHeight / 3), Color.Black);
            this.Components.Add(resultText);

            /* ============= Add menu selection component ============= */
            menuSelection = new MenuSelection(dkoFinal, spriteBatch, regular, selected, new Vector2(backgroundWidth / 2, backgroundHeight / 5 * 3), Color.White, Color.Black, new string[] { "BACK TO MAIN", "ABOUT", "EXIT" });
            this.Components.Add(menuSelection);

        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public int GetSelectedIndex()
        {
            return menuSelection.selectedIndex;
        }

    }
}
