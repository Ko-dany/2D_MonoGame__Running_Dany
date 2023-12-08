using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
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

        bool collided = false;

        public ObstacleAnimationCollision(Game game, PlayerCharacter player, List<ObstacleAnimation> obstacleAnims) : base(game)
        {
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
