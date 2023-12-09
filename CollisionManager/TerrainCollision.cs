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
    public class TerrainCollision : GameComponent
    {
        PlayerCharacter player;
        Terrain terrain;

        bool collided = false;

        public TerrainCollision(Game game, PlayerCharacter player, Terrain terrain) : base(game)
        {
            this.player = player;
            this.terrain = terrain;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRec = player.GetBounds();
            List<Rectangle> terrainRecs = terrain.GetAllBounds();

            foreach(Rectangle terrainRec in terrainRecs)
            {
                if (playerRec.Intersects(terrainRec))
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
