using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.GameManager
{
    public class ObstacleCollision : GameComponent
    {
        PlayerCharacter player;
        List<Obstacle> obstacleAnims;
        SoundEffect playerDeathSound;

        bool collided = false;

        public ObstacleCollision(Game game, PlayerCharacter player, List<Obstacle> obstacleAnims) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            playerDeathSound = dkoFinal.Content.Load<SoundEffect>("Sounds/Die");

            this.player = player;
            this.obstacleAnims = obstacleAnims;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = player.GetBounds();

            foreach (Obstacle obstacle in obstacleAnims)
            {
                Rectangle obstacleAnimRect = obstacle.GetBounds();
                if (playerRect.Intersects(obstacleAnimRect))
                {
                    playerDeathSound.Play();
                    collided = true;
                    break;
                }
                else
                {
                    collided = false;
                }
            }

            base.Update(gameTime);
        }

        public bool DetectCollision()
        {
            return collided;
        }

    }
}
