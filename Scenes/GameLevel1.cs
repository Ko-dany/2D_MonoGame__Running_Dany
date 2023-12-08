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

        bool gameOver;

        //GroundRenderer ground;
        public GameLevel1(Game game, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;
            mainBackgroundImg = dkoFinal.Content.Load<Texture2D>("Level1/Yellow");
            Texture2D obstacleImage = dkoFinal.Content.Load<Texture2D>("Level1/SpikeHead");

            mainBackground = new Background(dkoFinal, spriteBatch, mainBackgroundImg, backgroundWidth,  backgroundHeight);
            this.Components.Add(mainBackground);

            player = new PlayerCharacter(dkoFinal, spriteBatch, backgroundWidth, backgroundHeight);
            this.Components.Add(player);

            Random random = new Random();
            int obstacleCount = 15;

            List<ObstacleAnimation> obstacles = new List<ObstacleAnimation>();
            for(int i=0; i< obstacleCount; i++)
            {
                Vector2 randomPosition = new Vector2(random.Next(backgroundWidth, backgroundWidth * 3), random.Next(0, backgroundHeight));
                Vector2 randomSpeed = new Vector2(random.Next(3,5), random.Next(0,1));

                obstacle = new ObstacleAnimation(dkoFinal, spriteBatch, obstacleImage, randomPosition, randomSpeed, 4);
                obstacles.Add(obstacle);
                this.Components.Add(obstacle);
            }

            obstacleCollision = new ObstacleAnimationCollision(dkoFinal, player, obstacles);
            this.Components.Add(obstacleCollision);

            // ===== Level 2 를 위한 지형
            //ground = new GroundRenderer(dkoFinal, spriteBatch, backgroundHeight);
            //this.Components.Add(ground);

            //GroundCollisionManager groundCollision = new GroundCollisionManager(dkoFinal, player, ground);
            //this.Components.Add(groundCollision); 

            gameOver = false;
        }

        public override void Update(GameTime gameTime)
        {
            gameOver = obstacleCollision.DetectCollision();
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
