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

        SpriteBatch spriteBatch;
        Texture2D playerIdle;
        Texture2D playerRun;

        Vector2 position;
        Color color;
        float rotation;
        Vector2 origin;
        float scale;
        SpriteEffects spriteEffects;
        float layerDepth;

        List<Rectangle> idleRectangles;
        List<Rectangle> runRectangles;

        float moveSpeed = 5.0f;

        int currentImage = 0;
        int delay = 20;
        float counter = 0;

        bool isRunning = false;

        public PlayerCharacter(Game game, SpriteBatch spriteBatch, Vector2 position):base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;

            this.spriteBatch = spriteBatch;
            this.position = position;

            playerIdle = dkoFinal.Content.Load<Texture2D>("Level1/Idle");
            playerRun = dkoFinal.Content.Load<Texture2D>("Level1/Run");

            color = Color.White;
            rotation = 0.0f;
            origin = new Vector2(0, playerIdle.Height);
            scale = 1.5f;
            spriteEffects = SpriteEffects.None;
            layerDepth = 0.0f;

            idleRectangles = new List<Rectangle>();
            runRectangles = new List<Rectangle>();

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
            if (ks.IsKeyDown(Keys.Left))
            {
                position.X -= moveSpeed;
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                position.X += moveSpeed;
            }

            if(ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.Right))
            {
                isRunning = true;
            }
            else if(ks.IsKeyUp(Keys.Left) || ks.IsKeyUp(Keys.Right))
            {
                isRunning = false;
            }

            counter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (counter > delay)
            {
                counter = 0;
                currentImage += 1;
                if (isRunning && currentImage == idleRectangles.Count)
                {
                    currentImage = 0;
                }
                else if (!isRunning && currentImage == idleRectangles.Count)
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

            if (isRunning)
            {
                spriteBatch.Draw(playerRun, position, runRectangles[currentImage], color, rotation, origin, scale, spriteEffects, layerDepth);
            }
            else
            {
                spriteBatch.Draw(playerIdle, position, idleRectangles[currentImage], color, rotation, origin, scale, spriteEffects, layerDepth);

            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
