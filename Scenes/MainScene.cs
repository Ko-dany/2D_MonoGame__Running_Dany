using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace DKoFinal.Scenes
{
    public class MainScene:GameScene
    {
        SpriteBatch spriteBatch;
        Texture2D mainBackgroundImg;
        Texture2D mainTitleImg;
        
        SpriteFont regular, selected;

        Background mainBackground;
        MenuSelection menuSelection;
        Image menuTitle;

        public MainScene(Game game, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;

            mainBackgroundImg = dkoFinal.Content.Load<Texture2D>("MainScene/Blue");
            mainTitleImg = dkoFinal.Content.Load<Texture2D>("MainScene/GameTitle");

            regular = dkoFinal.Content.Load<SpriteFont>("Fonts/regular");
            selected = dkoFinal.Content.Load<SpriteFont>("Fonts/selected");

            mainBackground = new Background(dkoFinal, spriteBatch, mainBackgroundImg, backgroundWidth, backgroundHeight);

            menuTitle = new Image(dkoFinal, spriteBatch, mainTitleImg, new Vector2(backgroundWidth / 2, backgroundHeight / 3), new Rectangle(0, 0, mainTitleImg.Width, mainTitleImg.Height), Color.White, new Vector2(mainTitleImg.Width / 2, mainTitleImg.Height / 2), 0.0f, 0.3f, SpriteEffects.None, 0.0f);

            menuSelection = new MenuSelection(dkoFinal, spriteBatch, regular, selected, new Vector2(backgroundWidth/2, backgroundHeight/5*3), Color.White, Color.Black, new string[] { "START", "HELP", "ABOUT", "EXIT" });

            this.Components.Add(mainBackground);
            this.Components.Add(menuTitle);
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
