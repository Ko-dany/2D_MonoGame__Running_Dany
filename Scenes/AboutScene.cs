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
    public class AboutScene:GameScene
    {
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        Texture2D aboutImage;

        public AboutScene(Game game) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;
            spriteFont = dkoFinal.Content.Load<SpriteFont>("Fonts/regular");

            aboutImage = dkoFinal.Content.Load<Texture2D>("HelpScene/help");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "Developer: Dahyun Ko", new Vector2(300,300), Color.AliceBlue);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
