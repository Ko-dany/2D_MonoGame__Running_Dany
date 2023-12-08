using DKoFinal.Renderers;
using DKoFinal.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DKoFinal
{
    public class DkoFinal : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        SpriteFont scoreFont;
        Text scoreText;

        MainScene mainScene;
        HelpScene helpScene;
        AboutScene aboutScene;
        GameLevel1 gameLevel1;
        MenuDuringGameScene menuDuringGame;
        GameResultScene gameResult;

        List<GameScene> gameScenes;

        bool gameStarted;
        bool gamePaused;
        bool gameEnded;
        TimeSpan gamePlayedTime;
        double gameScore;
        string gameTimeResult;

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
            gameStarted = false;
            gamePaused = false;
            gameEnded = false;
            gameScore = 0.00;
            gamePlayedTime = TimeSpan.Zero;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameScenes = new List<GameScene>();
            scoreFont = Content.Load<SpriteFont>("Fonts/regular");

            mainScene = new MainScene(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(mainScene);
            mainScene.Display();

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            gameLevel1 = new GameLevel1(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(gameLevel1);

            menuDuringGame = new MenuDuringGameScene(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(menuDuringGame);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            
            if (ks.IsKeyDown(Keys.I))
            {
                Initialize();
            }
            

            /* ================= Main Menu Scene ================= */
            if (mainScene.Visible)
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

            /* ================= Help/About Page ================= */
            // When Esc is pressed, this will go back to the previous menus (main / during game / game result)
            if (helpScene.Visible || aboutScene.Visible)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    HideAllScenes();
                    if (!gameStarted)
                    {
                        mainScene.Display();
                    }
                    else if (gameStarted && gamePaused)
                    {
                        menuDuringGame.Display();
                    }
                    else if (gameEnded)
                    {
                        gameResult.Display();
                    }
                }
            }

            /* ================= Game Scene ================= */
            if (gameLevel1.Visible)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    HideAllScenes();
                    gamePaused = true;
                    menuDuringGame.Display();
                }

                if (gameLevel1.CheckGameOver())
                {
                    HideAllScenes();
                    gameEnded = true;

                    /* ==== 게임 시간 표기 테스트 (시간/분/초)
                    double totalSeconds = gamePlayedTime.TotalSeconds;
                    if (totalSeconds > 3600)
                    {
                        int hours = (int)(totalSeconds / 3600);
                        int minutes = (int)((totalSeconds % 3600) / 60);
                        int seconds = (int)(totalSeconds % 60);

                        gameTimeResult = $"GAME OVER!\n" +
                            $"You took <{hours} hours, {minutes} minutes, {seconds} seconds> to finish the game.";
                    }
                    else if(totalSeconds > 60)
                    {
                        int minutes = (int)((totalSeconds % 3600) / 60);
                        int seconds = (int)(totalSeconds % 60);

                        gameTimeResult = $"GAME OVER!\n" +
                            $"You took <{minutes} minutes, {seconds} seconds> to finish the game.";
                    }
                    else
                    {
                        gameTimeResult = $"GAME OVER! You took <{totalSeconds} seconds> to finish the game.";
                    }
                    */

                    gameTimeResult = $"GAME OVER\n" +
                        $"Your score: {gameScore}";

                    gameResult = new GameResultScene(this, gameTimeResult, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    this.Components.Add(gameResult);
                    gameResult.Display();
                }
            }

            /* ================= Menu Scene During game is on ================= */
            if (menuDuringGame.Visible)
            {
                if (ks.IsKeyDown(Keys.Enter))
                {
                    int selectedScene = menuDuringGame.GetSelectedIndex();
                    HideAllScenes();
                    switch (selectedScene)
                    {
                        case 0:
                            gameLevel1.Display();
                            gamePaused = false;
                            break;
                        case 1:
                            helpScene.Display();
                            break;
                        case 2:
                            aboutScene.Display();
                            break;
                        case 3:
                            Initialize();
                            break;
                        default:
                            break;
                    }
                }
            }

            /* ================= Game Result Scene ================= */
            if (gameResult != null && gameResult.Visible)
            {
                if (ks.IsKeyDown(Keys.Enter))
                {
                    int selectedScene = gameResult.GetSelectedIndex();
                    HideAllScenes();
                    switch (selectedScene)
                    {
                        case 0:
                            Initialize();
                            break;
                        case 1:
                            aboutScene.Display();
                            break;
                        case 2:
                            Exit();
                            break;
                        default:
                            break;
                    }
                }
            }

            // Recording the elapsed time since the game started.
            if (gameStarted && !gamePaused && !gameEnded)
            { 
                gamePlayedTime += gameTime.ElapsedGameTime;
                gameScore = Math.Round(gamePlayedTime.TotalSeconds, 2);

                /* ==== 게임 시간 스코어 테스트
                scoreText = new Text(this, gameScore.ToString(), spriteBatch, scoreFont, new Vector2(20, 20));
                this.Components.Add(scoreText);
                */
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