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
    public class GroundCollisionManager : GameComponent
    {
        PlayerCharacter player;
        GroundRenderer ground;

        bool collided = false;

        public GroundCollisionManager(Game game, PlayerCharacter player, GroundRenderer ground) : base(game)
        {
            this.player = player;
            this.ground = ground;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRectangles = player.GetBounds();
            List<Rectangle> groundRectangles = ground.GetBounds();

            foreach(Rectangle groundRec in groundRectangles)
            {
                if (playerRectangles.Intersects(groundRec))
                {
                    collided = true;
                    Debug.WriteLine("Player should die!!!!!!");
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
