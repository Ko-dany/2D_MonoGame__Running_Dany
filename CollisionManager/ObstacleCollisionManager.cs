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
    public class ObstacleCollisionManager : GameComponent
    {
        PlayerCharacter player;
        ObstacleAnimationRenderer obstacleAnim;

        Rectangle obstacleImgRect;
        Rectangle obstacleAnimRect;

        bool collided = false;
        public ObstacleCollisionManager(Game game, PlayerCharacter player, ObstacleAnimationRenderer obstacleAnim) : base(game)
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
 
            base.Update(gameTime);
        }

        public bool DetectCollision()
        {
            return collided;
        }
    }
}
