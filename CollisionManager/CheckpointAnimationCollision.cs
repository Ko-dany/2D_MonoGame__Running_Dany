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
    public class CheckpointAnimationCollision : GameComponent
    {
        PlayerCharacter player;
        CheckpointAnimation checkpoint;

        bool collided = false;

        public CheckpointAnimationCollision(Game game, PlayerCharacter player, CheckpointAnimation checkpoint) : base(game)
        {
            this.player = player;
            this.checkpoint = checkpoint;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = player.GetBounds();
            Rectangle checkpointAnimRect = checkpoint.GetBounds();
            if (playerRect.Intersects(checkpointAnimRect))
            {
                collided = true;
                Debug.WriteLine("Gotta go to the next level :3");
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
