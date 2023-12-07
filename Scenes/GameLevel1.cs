using DKoFinal.GameManager;
using DKoFinal.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace DKoFinal.Scenes
{
    internal class GameLevel1 : GameScene
    {
        SpriteBatch spriteBatch;
        Texture2D mainBackgroundImg;

        BackgroundRenderer mainBackground;
        GroundRenderer groundRenderer;
        PlayerCharacter player;

        public GameLevel1(Game game, int backgroundWidth, int backgroundHeight) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;
            mainBackgroundImg = dkoFinal.Content.Load<Texture2D>("Level1/Yellow");

            mainBackground = new BackgroundRenderer(dkoFinal, spriteBatch, mainBackgroundImg, backgroundWidth, backgroundHeight);
            groundRenderer = new GroundRenderer(dkoFinal, spriteBatch, backgroundHeight);
            ObstacleRenderer pipe = new ObstacleRenderer(dkoFinal, spriteBatch, dkoFinal.Content.Load<Texture2D>("Level1/Pipe"), new Vector2(backgroundWidth + 500, 300), new Vector2(3, 0));
            player = new PlayerCharacter(dkoFinal, spriteBatch, backgroundWidth, backgroundHeight);

            this.Components.Add(player);
            this.Components.Add(pipe);
            this.Components.Add(groundRenderer);

            PipeCollision collision = new PipeCollision(dkoFinal, player, pipe);
            this.Components.Add(collision);

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
