using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace DKoFinal.Scenes
{
    public class GameCleared : GameScene
    {
        SpriteBatch spriteBatch;
        SpriteFont regular, selected;
        int backgroundWidth;
        int backgroundHeight;

        Texture2D resultBackgroundImg;
        Background resultBackground;

        Text resultText;
        MenuSelection menuSelection;

        Text playerNameText;

        StringBuilder playerName = new StringBuilder();
        const int MaxNameLength = 3;

        KeyboardState oldState;


        public GameCleared(Game game, string gameResult, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;
            selected = dkoFinal.Content.Load<SpriteFont>("Fonts/selected");
            regular = dkoFinal.Content.Load<SpriteFont>("Fonts/regular");
            resultBackgroundImg = dkoFinal.Content.Load<Texture2D>("Level1/Pink");

            Vector2 position = new Vector2(backgroundWidth / 2, backgroundHeight / 2);

            this.backgroundWidth = backgroundWidth;
            this.backgroundHeight = backgroundHeight;

            resultBackground = new Background(dkoFinal, spriteBatch, resultBackgroundImg, backgroundWidth, backgroundHeight);
            this.Components.Add(resultBackground);

            resultText = new Text(dkoFinal, gameResult, spriteBatch, regular, new Vector2(backgroundWidth/2, backgroundHeight / 3));
            this.Components.Add(resultText);

            playerNameText = new Text(dkoFinal, $"Player Name: {playerName}", spriteBatch, regular, position);
            this.Components.Add(playerNameText);

            oldState = Keyboard.GetState();

            //menuSelection = new MenuSelection(dkoFinal, spriteBatch, regular, selected, new Vector2(backgroundWidth / 2, backgroundHeight / 5 * 3), Color.White, Color.Black, new string[] { "BACK TO MAIN", "ABOUT", "EXIT" });
            //this.Components.Add(menuSelection);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            for (Keys key = Keys.A; key <= Keys.Z; key++)
            {
                if (ks.IsKeyDown(key) && !oldState.IsKeyDown(key))
                {
                    playerName.Append(key.ToString());
                    playerName.Length = Math.Min(playerName.Length, MaxNameLength);
                    playerNameText.gameResult = $"Player Name: {playerName}";
                }
            }

            if (ks.IsKeyDown(Keys.Back) && playerName.Length > 0 && !oldState.IsKeyDown(Keys.Back))
            {
                playerName.Length--;
                playerNameText.gameResult = $"Player Name: {playerName}";
            }

            oldState = ks;

            base.Update(gameTime);
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

        public string GetPlayerName()
        {
            return playerName.ToString();
        }
    }
}
