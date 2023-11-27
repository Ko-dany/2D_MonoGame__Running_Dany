using DKoFinal.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace DKoFinal
{
    public class DkoFinal : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        private MainScene mainScene;

        public DkoFinal()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 64 * 14;
            _graphics.PreferredBackBufferHeight = 64 * 8;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D titleImage = this.Content.Load<Texture2D>("MainScene/GameTitle");
            mainScene = new MainScene(this, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight/3), new Rectangle(0,0, titleImage.Width, titleImage.Height), Color.White, new Vector2(titleImage.Width/2, titleImage.Height/2), 0.0f, 0.3f, SpriteEffects.None, 0.0f);
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