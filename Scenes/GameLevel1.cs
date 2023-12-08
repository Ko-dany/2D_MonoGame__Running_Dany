using DKoFinal.GameManager;
using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DKoFinal.Scenes
{
    public class GameLevel1 : GameScene
    {
        SpriteBatch spriteBatch;
        Texture2D mainBackgroundImg;

        Background mainBackground;
        PlayerCharacter player;

        ObstacleAnimation obstacle;
        ObstacleAnimationCollision obstacleCollision;

        CheckpointAnimation checkpoint;
        CheckpointAnimationCollision checkpointCollision;

        Ground ground;
        GroundCollision groundCollision;

        bool gameOver;

        public GameLevel1(Game game, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;
            mainBackgroundImg = dkoFinal.Content.Load<Texture2D>("Level1/Yellow");
            Texture2D obstacleImage = dkoFinal.Content.Load<Texture2D>("Level1/SpikeHead");
            Texture2D groundTexture = dkoFinal.Content.Load<Texture2D>("Level1/Spikes");

            mainBackground = new Background(dkoFinal, spriteBatch, mainBackgroundImg, backgroundWidth,  backgroundHeight);
            this.Components.Add(mainBackground);

            player = new PlayerCharacter(dkoFinal, spriteBatch, backgroundWidth, backgroundHeight);
            this.Components.Add(player);

            Random random = new Random();
            const int stages = 4;
            const int obstacleCount = 3;

            List<ObstacleAnimation> obstacles = new List<ObstacleAnimation>();
            
            for(int k = 1; k <= stages; k++)
            {
                for (int i = 0; i < obstacleCount; i++)
                {
                    Vector2 randomPosition = new Vector2(random.Next(backgroundWidth*k, backgroundWidth*(k+1)), random.Next(0, backgroundHeight-obstacleImage.Height));
                    Vector2 randomSpeed = new Vector2(random.Next(3, 5), 0);

                    obstacle = new ObstacleAnimation(dkoFinal, spriteBatch, obstacleImage, randomPosition, randomSpeed, 4);
                    obstacles.Add(obstacle);
                    this.Components.Add(obstacle);
                }
            }

            obstacleCollision = new ObstacleAnimationCollision(dkoFinal, player, obstacles);
            this.Components.Add(obstacleCollision);

            checkpoint = new CheckpointAnimation(dkoFinal, spriteBatch, new Vector2(backgroundWidth - 30, backgroundHeight/2));
            this.Components.Add(checkpoint);
            checkpointCollision = new CheckpointAnimationCollision(dkoFinal, player, checkpoint);
            this.Components.Add(checkpointCollision);

            ground = new Ground(dkoFinal, spriteBatch, groundTexture, new Vector2(0, -groundTexture.Height), new Vector2(0, backgroundHeight + groundTexture.Height), new Vector2(groundTexture.Width, -groundTexture.Height), new Vector2(groundTexture.Width, backgroundHeight + groundTexture.Height));
            this.Components.Add(ground);
            groundCollision = new GroundCollision(dkoFinal, player, ground);
            this.Components.Add(groundCollision); 

            gameOver = false;
        }

        public override void Update(GameTime gameTime)
        {
            if(obstacleCollision.DetectCollision() || groundCollision.DetectCollision())
            {
                gameOver = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool CheckGameOver()
        {
            return gameOver;
        }
    }
}
