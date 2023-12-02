using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Renderers
{
    internal class ImageRenderer : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D texture;
        Vector2 position;
        Rectangle srcRectangle;
        Color color;
        Vector2 origin;
        float rotation;
        float scale;
        SpriteEffects spriteEffect;
        float layerDepth;

        public ImageRenderer(Game game, SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle srcRectangle, Color color, Vector2 origin, float rotation, float scale, SpriteEffects spriteEffect, float layerDepth): base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.position = position;
            this.srcRectangle = srcRectangle;
            this.color = color;
            this.origin = origin;
            this.rotation = rotation;
            this.scale = scale;
            this.spriteEffect = spriteEffect;
            this.layerDepth = layerDepth;
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, srcRectangle, color, rotation, origin, scale, spriteEffect, layerDepth);
            spriteBatch.End();
        }

    }
}
