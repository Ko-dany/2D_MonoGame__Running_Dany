using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Renderers
{
    internal class GroundRenderer : DrawableGameComponent
    {
        SpriteBatch spriteBath;
        Texture2D groundTexture;
        Vector2 position1, position2;
        Rectangle srcRectangle;
        Color color;
        float rotation;
        Vector2 origin;
        float scale;
        SpriteEffects spriteEffects;
        float layerDepth;

        Vector2 speed;

        public GroundRenderer(Game game, SpriteBatch spriteBath,int backgroundHeight):base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;

            this.spriteBath = spriteBath;
            
            groundTexture = dkoFinal.Content.Load<Texture2D>("Level1/Spikes");
            position1 = new Vector2(0, 0);
            position2 = new Vector2(position1.X + srcRectangle.Width, position1.Y);

            srcRectangle = new Rectangle(0,0, groundTexture.Width, groundTexture.Height);
            color = Color.White;
            rotation = 0.0f;
            origin = new Vector2(0, groundTexture.Height);
            scale = 4.0f;
            spriteEffects = SpriteEffects.None;
            layerDepth = 0.0f;

            speed = new Vector2(2,0);
        }

        public override void Update(GameTime gameTime)
        {
            position1 -= speed;
            position2 -= speed;

            if (position1.X < -srcRectangle.Width)
            {
                position1.X = position2.X + srcRectangle.Width;
            }
            if(position2.X < -srcRectangle.Width)
            {
                position2.X = position1.X + srcRectangle.Width;
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBath.Begin();
            spriteBath.Draw(groundTexture, position1, srcRectangle, color, rotation, origin, scale, spriteEffects, layerDepth);
            spriteBath.Draw(groundTexture, position2, srcRectangle, color, rotation, origin, scale, spriteEffects, layerDepth);
            spriteBath.End();
            base.Draw(gameTime);
        }
    }
}
