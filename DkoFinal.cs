﻿using DKoFinal.Renderers;
using DKoFinal.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        GameLevel2 gameLevel2;
        MenuDuringGameScene menuDuringGame;
        GameResultScene gameResult;

        List<GameScene> gameScenes;

        bool gameStarted;
        bool gamePaused;
        bool gameEnded;
        TimeSpan gamePlayedTime;
        double gameScore;

        KeyboardState oldState;

        bool allLevelCleared;

        private Song mainBackgroundMusic;
        bool mainBackgroundMusicPlaying;

        private Song gameBackgroundMusic;
        bool gameBackgroundMusicPlaying;

        public DkoFinal()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //graphics.PreferredBackBufferWidth = 64 * 14;
            //graphics.PreferredBackBufferHeight = 64 * 8;
        }

        protected override void Initialize()
        {
            gameStarted = false;
            gamePaused = false;
            gameEnded = false;
            gameScore = 0.00;
            gamePlayedTime = TimeSpan.Zero;
            oldState = Keyboard.GetState();

            mainBackgroundMusicPlaying = false;

            allLevelCleared = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameScenes = new List<GameScene>();
            scoreFont = Content.Load<SpriteFont>("Fonts/regular");

            mainBackgroundMusic = Content.Load<Song>("Sounds/Lobby_Background");
            gameBackgroundMusic = Content.Load<Song>("Sounds/Game_Background");

            mainScene = new MainScene(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(mainScene);
            //mainScene.Display();

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            gameLevel1 = new GameLevel1(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(gameLevel1);

            gameLevel2 = new GameLevel2(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(gameLevel2);
            gameLevel2.Display();

            menuDuringGame = new MenuDuringGameScene(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(menuDuringGame);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            /* ================= Main Menu Scene ================= */
            if (mainScene.Visible)
            {
                if (!mainBackgroundMusicPlaying)
                {
                    PlayBackgroundMusic(mainBackgroundMusic);
                    mainBackgroundMusicPlaying = true;
                    gameBackgroundMusicPlaying = false;
                }

                if (ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    int selectedScene = mainScene.GetSelectedIndex();
                    HideAllScenes();
                    switch (selectedScene)
                    {
                        case 0:
                            if (!gameBackgroundMusicPlaying)
                            {
                                PlayBackgroundMusic(gameBackgroundMusic);
                                mainBackgroundMusicPlaying = false;
                                gameBackgroundMusicPlaying = true;
                            }
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
                    menuDuringGame.Display();
                    gamePaused = true;
                }

                if (gameLevel1.CheckGameOver())
                {
                    HideAllScenes();
                    gameEnded = true;          

                    gameResult = new GameResultScene(this, GetGameResultMessage(), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    this.Components.Add(gameResult);
                    gameResult.Display();
                }

                if (gameLevel1.CheckGameClear())
                {
                    HideAllScenes();
                    gameLevel2.Display();
                }
            }

            if (gameLevel2.Visible)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    HideAllScenes();
                    menuDuringGame.Display();
                    gamePaused = true;
                }

                if (gameLevel2.CheckGameOver())
                {
                    HideAllScenes();
                    gameEnded = true;

                    gameResult = new GameResultScene(this, GetGameResultMessage(), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    this.Components.Add(gameResult);
                    gameResult.Display();
                }

                if (gameLevel2.CheckGameClear())
                {
                    HideAllScenes();
                    //gameLevel2.Display();
                }
            }

            /* ================= Menu Scene During game is on ================= */
            if (menuDuringGame.Visible)
            {
                if (ks.IsKeyDown(Keys.Enter)&& oldState.IsKeyUp(Keys.Enter))
                {
                    int selectedScene = menuDuringGame.GetSelectedIndex();
                    HideAllScenes();
                    switch (selectedScene)
                    {
                        case 0:
                            gamePaused = false;
                            if (gameLevel1.CheckGameClear()) { gameLevel2.Display(); }
                            else { gameLevel1.Display(); }
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

            oldState = ks;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }

        public void HideAllScenes()
        {
            foreach (GameScene scene in this.Components)
            {
                scene.Hide();
            }
        }

        public string GetGameResultMessage()
        {
            string gameTimeResult = String.Empty;

            if (allLevelCleared)
            {
                gameTimeResult = "ALL LEVELS CLEARED!\n" + $"Your score: {gameScore}";
            }
            else
            {
                gameTimeResult = "GAME OVER\n" + $"Your score: {gameScore}";
            }

            return gameTimeResult;
        }

        public void PlayBackgroundMusic(Song backgroundMusic)
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
        }
    }
}