using DKoFinal.Renderers;
using DKoFinal.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace DKoFinal
{
    public class DkoFinal : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        private MainScene mainScene;
        private HelpScene helpScene;
        private AboutScene aboutScene;
        private GameLevel1 level1;

        List<GameScene> gameScenes;

        public DkoFinal()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 64 * 14;
            graphics.PreferredBackBufferHeight = 64 * 8;
        }

        public void HideAllScenes()
        {
            foreach (GameScene scene in this.Components)
            {
                scene.Hide();
            }
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameScenes = new List<GameScene>();

            mainScene = new MainScene(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(mainScene);
            //mainScene.Display();

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            level1 = new GameLevel1(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(level1);
            level1.Display();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            /*
            if (mainScene.Visible)
            {
                if (ks.IsKeyDown(Keys.Enter))
                {
                    int selectedScene = mainScene.GetSelectedIndex();
                    HideAllScenes();
                    switch (selectedScene)
                    {
                        case 1:
                            helpScene.Display();
                            break;
                        case 2:
                            aboutScene.Display();
                            break;
                        case 3:
                            Exit();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    HideAllScenes();
                    mainScene.Display();
                }
            }
            */

            /*
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            */

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}