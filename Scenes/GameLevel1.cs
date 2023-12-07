using DKoFinal.GameManager;
using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
        GameResultScene gameResultScene;

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

            obstacle = new ObstacleAnimation(dkoFinal, spriteBatch, obstacleImage, new Vector2(700, 300), new Vector2(1,0), 4);
            this.Components.Add(obstacle);

            obstacleCollision = new ObstacleAnimationCollision(dkoFinal, player, obstacle);
            this.Components.Add(obstacleCollision);

            //ground = new GroundRenderer(dkoFinal, spriteBatch, backgroundHeight);
            //this.Components.Add(ground);

            //GroundCollisionManager groundCollision = new GroundCollisionManager(dkoFinal, player, ground);
            //this.Components.Add(groundCollision); 

            gameResultScene = new GameResultScene(game, "Game Over!", backgroundWidth, backgroundHeight);

            gameOver = false;
        }

        public override void Update(GameTime gameTime)
        {

            if (obstacleCollision.DetectCollision())
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
