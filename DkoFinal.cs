using DKoFinal.Renderers;
using DKoFinal.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace DKoFinal
{
    public class DkoFinal : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        MainScene mainScene;
        HelpScene helpScene;
        AboutScene aboutScene;
        GameLevel1 gameLevel1;
        MenuDuringGameScene menuDuringGame;
        GameResultScene gameResult; 

        List<GameScene> gameScenes;

        bool gameStarted = false;

        public DkoFinal()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(16.666);

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

            gameLevel1 = new GameLevel1(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(gameLevel1);
            gameLevel1.Display();

            menuDuringGame = new MenuDuringGameScene(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(menuDuringGame);

            gameResult = new GameResultScene(this, "GAME OVER!", graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(gameResult);

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (mainScene.Visible || menuDuringGame.Visible)
            {
                if (ks.IsKeyDown(Keys.Enter))
                {
                    int selectedScene = mainScene.GetSelectedIndex();
                    HideAllScenes();
                    switch (selectedScene)
                    {
                        case 0:
                            gameLevel1.Display();
                            gameStarted = true;
                            break;
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
                    if (gameStarted)
                    {
                        menuDuringGame.Display();
                    }
                    else
                    {
                        mainScene.Display();
                    }
                }
            }

            if (gameLevel1.Visible)
            {
                if (gameLevel1.CheckGameOver())
                {
                    HideAllScenes();
                    gameResult.Display();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}