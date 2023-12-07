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
        SpriteFont spriteFont;
        string gameResult;
        Vector2 position;
        Color color;
        Vector2 origin;
        float rotation;
        float scale;
        SpriteEffects spriteEffect;
        float layerDepth;

        public GameResultScene(Game game, string gameResult, int backgroundWidth, int backgroundHeight) : base(game)
        {
            this.gameResult = gameResult;

            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;
            spriteFont = dkoFinal.Content.Load<SpriteFont>("Fonts/regular");
            position = new Vector2(backgroundWidth/2, backgroundHeight/2);
            color = Color.Black;
            origin = new Vector2(spriteFont.MeasureString(gameResult).X / 2, spriteFont.LineSpacing / 2);
            rotation = 0.0f;
            scale = 1.0f;
            spriteEffect = SpriteEffects.None;
            layerDepth = 0.0f;
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, gameResult, position, color, rotation, origin, scale, spriteEffect, layerDepth);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
