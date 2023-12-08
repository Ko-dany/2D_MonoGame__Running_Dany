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
    public class ObstacleAnimationCollision : GameComponent
    {
        PlayerCharacter player;
        List<ObstacleAnimation> obstacleAnims;
        SoundEffect playerDeathSound;

        bool collided = false;

        public ObstacleAnimationCollision(Game game, PlayerCharacter player, List<ObstacleAnimation> obstacleAnims) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            playerDeathSound = dkoFinal.Content.Load<SoundEffect>("Sounds/Die");

            this.player = player;
            this.obstacleAnims = obstacleAnims;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = player.GetBounds();

            foreach (ObstacleAnimation obstacle in obstacleAnims)
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
