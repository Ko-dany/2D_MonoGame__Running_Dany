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
    public class GameClearedScene : GameScene
    {
        SpriteBatch spriteBatch;
        Text playerNameText;

        StringBuilder playerName;
        const int MaxNameLength = 3;

        KeyboardState oldState;

        public GameClearedScene(Game game, string gameResult, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;

            /* ============= Load image & font content ============= */
            SpriteFont regular = dkoFinal.Content.Load<SpriteFont>("Fonts/regular");
            Texture2D resultBackgroundImg = dkoFinal.Content.Load<Texture2D>("Level1/Pink");

            playerName = new StringBuilder();
            Vector2 position = new Vector2(backgroundWidth / 2, backgroundHeight / 3*2);
            oldState = Keyboard.GetState();

            /*============ Add background component ============*/
            Background resultBackground = new Background(dkoFinal, spriteBatch, resultBackgroundImg, backgroundWidth, backgroundHeight);
            this.Components.Add(resultBackground);

            /*============ Add text component ============*/
            Text resultText = new Text(dkoFinal, gameResult, spriteBatch, regular, new Vector2(backgroundWidth/2, backgroundHeight / 3), Color.Black);
            this.Components.Add(resultText);

            playerNameText = new Text(dkoFinal, $"Player Name: {playerName}", spriteBatch, regular, position, Color.Black);
            this.Components.Add(playerNameText);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            /*============ When key(A-Z) is pressed, update the playerName shown on the scene. ============*/
            for (Keys key = Keys.A; key <= Keys.Z; key++)
            {
                if (ks.IsKeyDown(key) && !oldState.IsKeyDown(key))
                {
                    playerName.Append(key.ToString());
                    playerName.Length = Math.Min(playerName.Length, MaxNameLength);
                    playerNameText.gameResult = $"Player Name: {playerName}";
                }
            }

            /*============ When backspace is pressed, remove one chracter from the playerName shown on the scene. ============*/
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

        public string GetPlayerName()
        {
            return playerName.ToString();
        }
    }
}
