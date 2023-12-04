﻿using Microsoft.Xna.Framework;
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

        SpriteBatch spriteBatch;
        Texture2D playerIdle;
        Texture2D playerRun;
        Texture2D playerJump;
        Texture2D playerFall;

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

        float speed = 5.0f;
        float jumpForce = 10.0f;
        float gravity = .5f;

        float velocityY = 0f;
        float groundY;

        int currentImage = 0;
        int delay = 20;
        float counter = 0;

        bool isJumping = false;
        bool isGrounded = true;

        public PlayerCharacter(Game game, SpriteBatch spriteBatch, int backgroundHeight) :base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            playerIdle = dkoFinal.Content.Load<Texture2D>("Level1/Idle");
            playerRun = dkoFinal.Content.Load<Texture2D>("Level1/Run");
            playerJump = dkoFinal.Content.Load<Texture2D>("Level1/Jump");
            playerFall = dkoFinal.Content.Load<Texture2D>("Level1/Fall");

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


        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Space) && isGrounded)
            {
                velocityY = -jumpForce;
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
                position.Y = groundY;
                velocityY = position.Y;
                isGrounded = true;
            }

            counter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (counter > delay)
            {
                counter = 0;
                currentImage += 1;
                if (currentImage == runRectangles.Count)
                {
                    currentImage = 0;
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
                spriteBatch.Draw(playerRun, position, runRectangles[currentImage], color, rotation, origin, scale, spriteEffects, layerDepth);
            }
            else
            {
                if (isJumping)
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
