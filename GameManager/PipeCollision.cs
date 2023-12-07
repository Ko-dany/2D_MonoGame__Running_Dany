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
    public class PipeCollision : GameComponent
    {
        private PlayerCharacter player;
        private ObstacleRenderer pipe;
        public PipeCollision(Game game, PlayerCharacter player, ObstacleRenderer pipe) : base(game)
        {
            this.player = player;
            this.pipe = pipe;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = player.GetBounds();
            Rectangle pipeRect = pipe.GetBounds();

            if (playerRect.Intersects(pipeRect))
            {
                Debug.WriteLine("PipeHit==============================================");
            }
            base.Update(gameTime);
        }
    }
}
