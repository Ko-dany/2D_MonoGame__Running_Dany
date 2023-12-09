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
    public class GameCleared : GameScene
    {
        SpriteBatch spriteBatch;
        SpriteFont regular, selected;
        string gameResult;
        Vector2 position;

        Texture2D resultBackgroundImg;
        Background resultBackground;

        Text resultText;
        MenuSelection menuSelection;

        public GameCleared(Game game, string gameResult, int backgroundWidth, int backgroundHeight) : base(game)
        {
            this.gameResult = gameResult;

            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;
            selected = dkoFinal.Content.Load<SpriteFont>("Fonts/selected");
            regular = dkoFinal.Content.Load<SpriteFont>("Fonts/regular");
            position = new Vector2(backgroundWidth/2, backgroundHeight/2);

            resultBackgroundImg = dkoFinal.Content.Load<Texture2D>("Level1/Pink");
            resultBackground = new Background(dkoFinal, spriteBatch, resultBackgroundImg, backgroundWidth, backgroundHeight);
            this.Components.Add(resultBackground);

            menuSelection = new MenuSelection(dkoFinal, spriteBatch, regular, selected, new Vector2(backgroundWidth / 2, backgroundHeight / 5 * 3), Color.White, Color.Black, new string[] { "BACK TO MAIN", "ABOUT", "EXIT" });
            this.Components.Add(menuSelection);

            resultText = new Text(dkoFinal, gameResult, spriteBatch, regular, new Vector2(backgroundWidth/2, backgroundHeight / 3));
            this.Components.Add(resultText);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void InputGameResult(string gameResult)
        {
            this.gameResult = gameResult;
        }

        public int GetSelectedIndex()
        {
            return menuSelection.selectedIndex;
        }
    }
}
