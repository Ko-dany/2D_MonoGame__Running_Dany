/*
 * Program: PROG2370-SEC4
 * Purpose: Final Project
 * Revision History:
 *      created by Dahyun Ko, Dec/10/2023
 */

using DKoFinal.Database;
using DKoFinal.Renderers;
using DKoFinal.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DKoFinal
{
    public class DkoFinal : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        /*======= Game scenes =======*/
        MainScene mainScene;
        HelpScene helpScene;
        AboutScene aboutScene;
        GameLevel1 gameLevel1;
        GameLevel2 gameLevel2;
        GameLevel3 gameLevel3;
        MenuDuringGameScene menuDuringGame;
        GameResultScene gameResult;
        GameClearedScene gameClearedScene;
        LeaderBoardScene leaderBoard;

        /*======= Game states & scores =======*/
        bool isGameStarted;
        bool isGamePaused;
        bool isGameEnded;
        bool isGameCleared;

        double gameScore;
        TimeSpan gamePlayedTime;
        KeyboardState oldState;

        bool hasScores;

        /*======= Background Music =======*/
        private Song mainBackgroundMusic;
        bool mainBackgroundMusicPlaying;
        private Song gameBackgroundMusic;
        bool gameBackgroundMusicPlaying;

        ScoreRecordManager scoreRecordManager;
        List<ScoreRecord> scores;

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
            isGameStarted = false;
            isGamePaused = false;
            isGameEnded = false;
            gameScore = 0.00;
            gamePlayedTime = TimeSpan.Zero;
            oldState = Keyboard.GetState();

            mainBackgroundMusicPlaying = false;

            isGameCleared = false;
            hasScores = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mainBackgroundMusic = Content.Load<Song>("Sounds/Lobby_Background");
            gameBackgroundMusic = Content.Load<Song>("Sounds/Game_Background");

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string filePath = "C:\\Users\\iamgo\\Desktop\\Conestoga\\Semester_3\\3. PROG2370_Game_Programming_with_Data_Structures\\Final Project\\DKoFinal\\game.txt";

            if(File.Exists(filePath)) 
            { 
                hasScores = true;
            }
            scoreRecordManager = new ScoreRecordManager(filePath);
            scores = scoreRecordManager.ReadScores();

            mainScene = new MainScene(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, hasScores);
            this.Components.Add(mainScene);
            mainScene.Display();

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            aboutScene = new AboutScene(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(aboutScene);

            gameLevel1 = new GameLevel1(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(gameLevel1);

            gameLevel2 = new GameLevel2(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(gameLevel2);

            gameLevel3 = new GameLevel3(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.Components.Add(gameLevel3);
            //gameLevel3.Display();

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
                    if (!hasScores)
                    {
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
                                isGameStarted = true;
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
                    else
                    {
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
                                isGameStarted = true;
                                break;
                            case 1:
                                leaderBoard = new LeaderBoardScene(this, ConvertToString(scores), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                                this.Components.Add(leaderBoard);
                                leaderBoard.Display();
                                break;
                            case 2:
                                helpScene.Display();
                                break;
                            case 3:
                                aboutScene.Display();
                                break;
                            case 4:
                                Exit();
                                break;
                            default:
                                break;
                        }
                    }
                    
                }
            }

            if(leaderBoard != null && leaderBoard.Visible)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    HideAllScenes();
                    mainScene.Display();
                }
            }

            /* ================= Help/About Page ================= */
            // When Esc is pressed, this will go back to the previous menus (main / during game / game result)
            if (helpScene.Visible || aboutScene.Visible)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    HideAllScenes();

                    if (isGameCleared)
                    {
                        gameClearedScene.Display();
                    }
                    else if (isGameEnded)
                    {
                        gameResult.Display();
                    }
                    else if(isGameStarted && isGamePaused)
                    {
                        menuDuringGame.Display();
                    }
                    else if (!isGameStarted)
                    {
                        mainScene.Display();
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
                    isGamePaused = true;
                }

                if (gameLevel1.CheckGameOver())
                {
                    HideAllScenes();
                    isGameEnded = true;          

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
                    isGamePaused = true;
                }

                if (gameLevel2.CheckGameOver())
                {
                    HideAllScenes();
                    isGameEnded = true;

                    gameResult = new GameResultScene(this, GetGameResultMessage(), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    this.Components.Add(gameResult);
                    gameResult.Display();
                }

                if (gameLevel2.CheckGameClear())
                {
                    HideAllScenes();
                    gameLevel3.Display();
                }
            }

            if (gameLevel3.Visible)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    HideAllScenes();
                    menuDuringGame.Display();
                    isGamePaused = true;
                }

                if (gameLevel3.CheckGameOver())
                {
                    HideAllScenes();
                    isGameEnded = true;

                    gameResult = new GameResultScene(this, GetGameResultMessage(), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    this.Components.Add(gameResult);
                    gameResult.Display();
                }

                if (gameLevel3.CheckGameClear())
                {
                    HideAllScenes();
                    isGameEnded = true;
                    isGameCleared = true;

                    gameClearedScene = new GameClearedScene(this, GetGameResultMessage(), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    this.Components.Add(gameClearedScene);
                    gameClearedScene.Display();
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
                            isGamePaused = false;
                            if (gameLevel2.CheckGameClear()) { gameLevel3.Display(); }
                            else if (gameLevel1.CheckGameClear()) { gameLevel2.Display(); }
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

            /* ================= Game Cleared Scene ================= */
            if (isGameCleared && gameClearedScene.Visible)
            {
                if (ks.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    string playerName = gameClearedScene.GetPlayerName();

                    if(playerName.Length > 0 && playerName.Length < 4)
                    {
                        Debug.WriteLine("You are here!");

                        ScoreRecord sr = new ScoreRecord(playerName, gameScore);
                        scores.Add(sr);
                        scoreRecordManager.WriteScores(scores);

                        gameResult = new GameResultScene(this, "Thanks for playing!", graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                        this.Components.Add(gameResult);
                        gameResult.Display();
                    }
                }
            }

            // Recording the elapsed time since the game started.
            if (isGameStarted && !isGamePaused && !isGameEnded)
            { 
                gamePlayedTime += gameTime.ElapsedGameTime;
                gameScore = Math.Round(gamePlayedTime.TotalSeconds, 2);
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
            string gameTimeResult;

            if (isGameCleared)
            {
                gameTimeResult = "ALL LEVELS CLEARED\n" + $"Your score: {gameScore}\n" + "Leave your player name to save the score.";
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

        public string ConvertToString(List<ScoreRecord> scores)
        {
            StringBuilder sb = new StringBuilder();

            int count = Math.Min(scores.Count, 5);
            for (int i = 0; i < count; i++)
            {
                sb.AppendLine($"Player: {scores[i].Player} --- Score: {scores[i].Score}");
            }

            return sb.ToString();
        }

    }
}