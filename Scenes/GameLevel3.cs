using DKoFinal.GameManager;
using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace DKoFinal.Scenes
{
    public class GameLevel3 : GameScene
    {
        SpriteBatch spriteBatch;

        ObstacleCollision spikeHeadCollision;
        ObstacleCollision movingSawCollision;
        CheckpointCollision checkpointCollision;
        TerrainCollision terrainCollision;

        bool gameOver;
        bool gameClear;

        public GameLevel3(Game game, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;

            /* ============= Load image & font content ============= */
            Texture2D mainBackgroundImg = dkoFinal.Content.Load<Texture2D>("Level1/Purple");
            Texture2D spikeHeadImage = dkoFinal.Content.Load<Texture2D>("Level1/SpikeHead");
            Texture2D MovingSawImage = dkoFinal.Content.Load<Texture2D>("Level1/MovingSaw");
            Texture2D horizontalTexture = dkoFinal.Content.Load<Texture2D>("Level1/Spikes");
            Texture2D verticalTexture = dkoFinal.Content.Load<Texture2D>("Level1/Spikes_Vertical");

            gameOver = false;
            gameClear = false;

            /*============ Add background component ============*/
            Background mainBackground = new Background(dkoFinal, spriteBatch, mainBackgroundImg, backgroundWidth,  backgroundHeight);
            this.Components.Add(mainBackground);

            /*============ Add player character component ============*/
            PlayerCharacter player = new PlayerCharacter(dkoFinal, spriteBatch, backgroundWidth, backgroundHeight);
            this.Components.Add(player);

            /*============ Generate random obstacle components & add obstacle collision manager ============*/
            Random random = new Random();
            const int stages = 5;
            const int spikeHeadsCount = 4;
            const int movingSawsCount = 5;

            List<Obstacle> spikeHeads = new List<Obstacle>();
            List<Rectangle> spikeHeadBounds = new List<Rectangle>();

            List<Obstacle> movingSaws = new List<Obstacle>();
            List<Rectangle> movingSawBounds = new List<Rectangle>();

            for (int k = 1; k <= stages; k++)
            {
                /*============ Generate Spike Heads ============*/
                for (int i = 0; i < spikeHeadsCount; i++)
                {
                    Rectangle newSpikeHeadsBounds = new Rectangle(random.Next(backgroundWidth * k, backgroundWidth * (k + 1)), random.Next(0, backgroundHeight - spikeHeadImage.Height), spikeHeadImage.Width, spikeHeadImage.Height);

                    while (ObstacleOverlaps(newSpikeHeadsBounds, spikeHeadBounds))
                    {
                        newSpikeHeadsBounds.X = random.Next(backgroundWidth * k, backgroundWidth * (k + 1));
                        newSpikeHeadsBounds.Y = random.Next(0, backgroundHeight - spikeHeadImage.Height);
                    }

                    Vector2 randomPosition = new Vector2(newSpikeHeadsBounds.X, newSpikeHeadsBounds.Y);
                    Vector2 randomSpeed = new Vector2(random.Next(3, 5), 0);

                    Obstacle spikeHead = new Obstacle(dkoFinal, spriteBatch, spikeHeadImage, 1.5f, randomPosition, randomSpeed, 4, backgroundWidth, backgroundHeight);
                    spikeHeads.Add(spikeHead);
                    this.Components.Add(spikeHead);

                    spikeHeadBounds.Add(newSpikeHeadsBounds);
                }

                /*============ Generate Moving saws ============*/
                for (int i = 0; i < movingSawsCount; i++)
                {
                    Rectangle newSawsBounds = new Rectangle(random.Next(backgroundWidth * k, backgroundWidth * (k + 1)), random.Next(0, backgroundHeight - MovingSawImage.Height), MovingSawImage.Width, MovingSawImage.Height);

                    while (ObstacleOverlaps(newSawsBounds, movingSawBounds))
                    {
                        newSawsBounds.X = random.Next(backgroundWidth * k, backgroundWidth * (k + 1));
                        newSawsBounds.Y = random.Next(0, backgroundHeight - MovingSawImage.Height);
                    }

                    Vector2 randomPosition = new Vector2(newSawsBounds.X, newSawsBounds.Y);
                    Vector2 randomSpeed = new Vector2(random.Next(3, 5), random.Next(3, 5));

                    Obstacle movingSaw = new Obstacle(dkoFinal, spriteBatch, MovingSawImage, 1.5f, randomPosition, randomSpeed, 8, backgroundWidth, backgroundHeight);
                    movingSaws.Add(movingSaw);
                    this.Components.Add(movingSaw);

                    movingSawBounds.Add(newSawsBounds);
                }
            }

            spikeHeadCollision = new ObstacleCollision(dkoFinal, player, spikeHeads);
            this.Components.Add(spikeHeadCollision);
            movingSawCollision = new ObstacleCollision(dkoFinal, player, movingSaws);
            this.Components.Add(movingSawCollision);

            /*============ Add terrain component & terrain collision manager ============*/
            Terrain terrain = new Terrain(dkoFinal, spriteBatch, horizontalTexture, new Vector2(0, 0), new Vector2(0, backgroundHeight - horizontalTexture.Height), new Vector2(horizontalTexture.Width,0), new Vector2(horizontalTexture.Width, backgroundHeight - horizontalTexture.Height), verticalTexture, new Vector2(-verticalTexture.Width, 0), new Vector2(backgroundWidth * (stages + 1) + backgroundWidth / 2, 0));
            this.Components.Add(terrain);
            terrainCollision = new TerrainCollision(dkoFinal, player, terrain);
            this.Components.Add(terrainCollision);

            /*============ Add checkpoint component & checkpoint collision manager ============*/
            //checkpoint = new CheckpointAnimation(dkoFinal, spriteBatch, new Vector2(backgroundWidth * (stages + 1) + backgroundWidth / 3, backgroundHeight / 2));
            CheckpointAnimation checkpoint = new CheckpointAnimation(dkoFinal, spriteBatch, new Vector2(backgroundWidth * (stages + 1) + backgroundWidth / 3, backgroundHeight / 2));
            this.Components.Add(checkpoint);
            checkpointCollision = new CheckpointCollision(dkoFinal, player, checkpoint);
            this.Components.Add(checkpointCollision);
        }

        public override void Update(GameTime gameTime)
        {
            if (terrainCollision.DetectCollision() || spikeHeadCollision.DetectCollision() || movingSawCollision.DetectCollision()) { gameOver = true; }
            if (checkpointCollision.DetectCollision()) { gameClear = true; }

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

        public bool CheckGameClear()
        {
            return gameClear;
        }
        bool ObstacleOverlaps(Rectangle newObstacle, List<Rectangle> existingObstacles)
        {
            foreach (var obstacle in existingObstacles)
            {
                if (obstacle.Intersects(newObstacle))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
