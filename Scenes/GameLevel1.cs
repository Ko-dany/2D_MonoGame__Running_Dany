using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace DKoFinal.Scenes
{
    internal class GameLevel1 : GameScene
    {
        SpriteBatch spriteBatch;
        Texture2D mainBackgroundImg;
        Texture2D playerImage;

        BackgroundRenderer mainBackground;
        PlayerCharacter player;

        public GameLevel1(Game game, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;
            mainBackgroundImg = dkoFinal.Content.Load<Texture2D>("Level1/Yellow");
            mainBackground = new BackgroundRenderer(spriteBatch, mainBackgroundImg, backgroundWidth, backgroundHeight);

            player = new PlayerCharacter(dkoFinal, spriteBatch, new Vector2(10, backgroundHeight/5*4));

            this.Components.Add(player);
        }

        public override void Draw(GameTime gameTime)
        {
            mainBackground.Draw();

            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
