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
        ObstacleAnimation obstacleAnim;

        bool collided = false;

        public ObstacleAnimationCollision(Game game, PlayerCharacter player, ObstacleAnimation obstacleAnim) : base(game)
        {
            this.player = player;
            this.obstacleAnim = obstacleAnim;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = player.GetBounds();
            Rectangle obstacleAnimRect = obstacleAnim.GetBounds();

            if (playerRect.Intersects(obstacleAnimRect))
            {
                collided = true;
                Debug.WriteLine("Obstacle just Hit==============================================");
            }
            else
            {
                collided = false;
            }

            base.Update(gameTime);
        }

        public bool DetectCollision()
        {
            return collided;
        }
    }
}
