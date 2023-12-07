using DKoFinal.GameManager;
using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Scenes
{
    internal class GameLevel1 : GameScene
    {
        SpriteBatch spriteBatch;
        Texture2D mainBackgroundImg;

        BackgroundRenderer mainBackground;
        PlayerCharacter player;

        //GroundRenderer ground;


        public GameLevel1(Game game, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;
            mainBackgroundImg = dkoFinal.Content.Load<Texture2D>("Level1/Yellow");

            mainBackground = new BackgroundRenderer(dkoFinal, spriteBatch, mainBackgroundImg, backgroundWidth, backgroundHeight);
            this.Components.Add(mainBackground);

            player = new PlayerCharacter(dkoFinal, spriteBatch, backgroundWidth, backgroundHeight);
            this.Components.Add(player);

            ObstacleRenderer obstacle = new ObstacleRenderer(dkoFinal, spriteBatch, dkoFinal.Content.Load<Texture2D>("Level1/Pipe"), new Vector2(backgroundWidth + 500, 300), new Vector2(3, 0));
            this.Components.Add(obstacle);

            CollisionManager obstacleCollision = new CollisionManager(dkoFinal, player, obstacle);
            this.Components.Add(obstacleCollision);

            //ground = new GroundRenderer(dkoFinal, spriteBatch, backgroundHeight);
            //this.Components.Add(ground);

            //GroundCollisionManager groundCollision = new GroundCollisionManager(dkoFinal, player, ground);
            //this.Components.Add(groundCollision); 
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
