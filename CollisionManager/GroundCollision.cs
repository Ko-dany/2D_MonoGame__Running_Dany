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
    public class GroundCollision : GameComponent
    {
        PlayerCharacter player;
        Ground ground;

        bool collided = false;

        public GroundCollision(Game game, PlayerCharacter player, Ground ground) : base(game)
        {
            this.player = player;
            this.ground = ground;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRec = player.GetBounds();
            Rectangle groundRec = ground.GetBottomBound();

            if (playerRec.Intersects(groundRec))
            {
                collided = true;
                Debug.WriteLine("Player should die!");
            }
            else
            {
                collided = false;
            }

            /*
            List<Rectangle> groundRectangles = ground.GetBounds();

            foreach(Rectangle groundRec in groundRectangles)
            {
                if (playerRectangles.Intersects(groundRec))
                {
                    collided = true;
                    Debug.WriteLine("Player should die!");
                    break;
                }
                else
                {
                    collided = false;
                }
            }
            */
            base.Update(gameTime);
        }

        public bool DetectCollision()
        {
            return collided;
        }
    }
}
