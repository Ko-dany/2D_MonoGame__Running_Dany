using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DKoFinal.Renderers
{
    public class PlayerCharacter : DrawableGameComponent
    {
        const int doubleJump_COL = 6;

        SpriteBatch spriteBatch;
        Texture2D playerFall;
        Texture2D playerDoubleJump;

        Vector2 position;
        Color color;
        float rotation;
        Vector2 origin;
        float scale;
        SpriteEffects spriteEffects;
        float layerDepth;

        List<Rectangle> doubleJumpRectangles;

        const float speed = 5.0f;
        const float jumpForce = 5.0f;
        const float gravity = 0.25f;
        float velocityY = 0f;

        int currentDoubleJump = 0;

        int delay = 30;
        float counter = 0;

        bool isJumping = false;

        public PlayerCharacter(Game game, SpriteBatch spriteBatch, int backgroundWidth, int backgroundHeight) :base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            playerFall = dkoFinal.Content.Load<Texture2D>("Level1/Fall");
            playerDoubleJump = dkoFinal.Content.Load<Texture2D>("Level1/DoubleJump");

            //Texture2D groundTexture = dkoFinal.Content.Load<Texture2D>("Level1/Ground");

            this.spriteBatch = spriteBatch;
            position = new Vector2(backgroundWidth/2, backgroundHeight/2);

            color = Color.White;
            rotation = 0.0f;
            origin = new Vector2(playerFall.Width/2, playerFall.Height/2);
            scale = 2f;
            spriteEffects = SpriteEffects.None;
            layerDepth = 0.0f;

            isJumping = false;

            doubleJumpRectangles = new List<Rectangle>();
            for (int i = 0; i < doubleJump_COL; i++)
            {
                Rectangle rectangle = new Rectangle(i * playerDoubleJump.Width / doubleJump_COL, 0, playerDoubleJump.Width / doubleJump_COL, playerDoubleJump.Height);
                doubleJumpRectangles.Add(rectangle);
            }
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Space) && !isJumping)
            {
                velocityY = -jumpForce;
                isJumping = true;
            }

            if (ks.IsKeyDown(Keys.Left))
            {
                position.X -= speed;
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                position.X += speed;
                spriteEffects = SpriteEffects.None;
            }

            velocityY += gravity;
            position.Y += velocityY;

            if (velocityY > 0)
            {
                isJumping = false;
            }

            if (position.Y < 0)
            {
                velocityY = 0;
                position.Y = 0;
            }

            counter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (counter > delay)
            {
                counter = 0;

                if (isJumping)
                {
                    currentDoubleJump += 1;
                    if (currentDoubleJump == doubleJumpRectangles.Count)
                    {
                        currentDoubleJump = 0;
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (!isJumping)
            {
                spriteBatch.Draw(playerFall, position, new Rectangle(0,0, playerFall.Width, playerFall.Height), color, rotation, origin, scale, spriteEffects, layerDepth);
            }
            else
            {
                spriteBatch.Draw(playerDoubleJump, position, doubleJumpRectangles[currentDoubleJump], color, rotation, origin, scale, spriteEffects, layerDepth);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, playerFall.Width, playerFall.Height);
        }

    }
}
