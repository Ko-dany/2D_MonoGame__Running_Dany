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

            Texture2D titleImage = this.Content.Load<Texture2D>("MainScene/GameTitle");
            mainScene = new MainScene(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            mainScene.Display();
            this.Components.Add(mainScene);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

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