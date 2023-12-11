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
    public class CheckpointCollision : GameComponent
    {
        PlayerCharacter player;
        CheckpointAnimation checkpoint;
        SoundEffect gameClearSound;

        bool collided = false;

        public CheckpointCollision(Game game, PlayerCharacter player, CheckpointAnimation checkpoint) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            gameClearSound = dkoFinal.Content.Load<SoundEffect>("Sounds/Finish");

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
                gameClearSound.Play();
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
