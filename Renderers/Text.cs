﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Renderers
{
    public class Text : DrawableGameComponent
    {
        string gameResult;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Vector2 position;
        Rectangle srcRectangle;
        Color color;
        Vector2 origin;
        float rotation;
        float scale;
        SpriteEffects spriteEffect;
        float layerDepth;

        public Text(Game game, string gameResult, SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 position) : base(game)
        {
            this.gameResult = gameResult;
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.position = position;

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