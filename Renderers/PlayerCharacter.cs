using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Renderers
{
    internal class PlayerCharacter : DrawableGameComponent
    {
        const int idle_COL = 11;
        const int run_COL = 12;
        const int doubleJump_COL = 6;

        SpriteBatch spriteBatch;
        Texture2D playerIdle;
        Texture2D playerRun;
        Texture2D playerJump;
        Texture2D playerFall;
        Texture2D playerDoubleJump;

        Vector2 position;
        Color color;
        float rotation;
        Vector2 origin;
        float scale;
        SpriteEffects spriteEffects;
        float layerDepth;

        List<Rectangle> idleRectangles;
        List<Rectangle> runRectangles;
        Rectangle jumpRectangle;
        List<Rectangle> doubleJumpRectangles;

        float jumpForce = 10.0f;
        float gravity = .5f;
        float velocityY = 0f;
        float groundY;

        int currentRun = 0;
        int currentDoubleJump = 0;

        int delay = 20;
        float counter = 0;

        bool isJumping = false;
        bool isGrounded = true;
        int jumps = 0;
        bool isDoubleJumping = false;

        public PlayerCharacter(Game game, SpriteBatch spriteBatch, int backgroundHeight) :base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            playerIdle = dkoFinal.Content.Load<Texture2D>("Level1/Idle");
            playerRun = dkoFinal.Content.Load<Texture2D>("Level1/Run");
            playerJump = dkoFinal.Content.Load<Texture2D>("Level1/Jump");
            playerFall = dkoFinal.Content.Load<Texture2D>("Level1/Fall");
            playerDoubleJump = dkoFinal.Content.Load<Texture2D>("Level1/DoubleJump");

            Texture2D groundTexture = dkoFinal.Content.Load<Texture2D>("Level1/Ground");

            this.spriteBatch = spriteBatch;
            position = new Vector2(30, backgroundHeight - (groundTexture.Height*3.0f));

            color = Color.White;
            rotation = 0.0f;
            origin = new Vector2(0, playerIdle.Height);
            scale = 3f;
            spriteEffects = SpriteEffects.None;
            layerDepth = 0.0f;

            groundY = position.Y;

            idleRectangles = new List<Rectangle>();
            runRectangles = new List<Rectangle>();
            jumpRectangle = new Rectangle(0, 0, playerJump.Width, playerJump.Height);
            doubleJumpRectangles = new List<Rectangle>();

            for (int i=0; i< idle_COL; i++)
            {
                Rectangle rectangle = new Rectangle(i * playerIdle.Width / idle_COL, 0, playerIdle.Width / idle_COL, playerIdle.Height);
                idleRectangles.Add(rectangle);
            }

            for (int i = 0; i < run_COL; i++)
            {
                Rectangle rectangle = new Rectangle(i * playerRun.Width / run_COL, 0, playerRun.Width / run_COL, playerRun.Height);
                runRectangles.Add(rectangle);
            }

            for (int i = 0; i < doubleJump_COL; i++)
            {
                Rectangle rectangle = new Rectangle(i * playerDoubleJump.Width / doubleJump_COL, 0, playerDoubleJump.Width / doubleJump_COL, playerDoubleJump.Height);
                doubleJumpRectangles.Add(rectangle);
            }
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Space) && (isGrounded || jumps < 2))
            {
                if (jumps == 0)
                {
                    velocityY = -jumpForce;
                }
                else
                {
                    velocityY = -(jumpForce * 2f);
                    isDoubleJumping = true;
                }
                
                jumps++;
                isJumping = true;
                isGrounded = false;
            }

            velocityY += gravity;
            position.Y += velocityY;

            if (velocityY > 0)
            {
                isJumping = false;
            }

            if (position.Y >= groundY)
            {
                velocityY = 0;
                position.Y = groundY;
                isGrounded = true;
                isDoubleJumping = false;
                jumps = 0;
            }


            if (isGrounded)
            {
                counter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (counter > delay)
                {
                    counter = 0;
                    currentRun += 1;
                    if (currentRun == runRectangles.Count)
                    {
                        currentRun = 0;
                    }
                }
            }
            else
            {
                counter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (counter > delay)
                {
                    counter = 0;
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
            KeyboardState ks = Keyboard.GetState();

            spriteBatch.Begin();

            if (isGrounded)
            {
                spriteBatch.Draw(playerRun, position, runRectangles[currentRun], color, rotation, origin, scale, spriteEffects, layerDepth);
            }
            else
            {
                if (isDoubleJumping)
                {
                    spriteBatch.Draw(playerDoubleJump, position, doubleJumpRectangles[currentDoubleJump], color, rotation, origin, scale, spriteEffects, layerDepth);
                }
                else if (isJumping)
                {
                    spriteBatch.Draw(playerJump, position, jumpRectangle, color, rotation, origin, scale, spriteEffects, layerDepth);
                }
                else
                {
                    spriteBatch.Draw(playerFall, position, jumpRectangle, color, rotation, origin, scale, spriteEffects, layerDepth);
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
