using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Scenes
{
    internal class MainScene:GameScene
    {
        SpriteBatch spriteBatch;
        Texture2D mainBackgroundImg;

        BackgroundRenderer mainBackground;

        Texture2D mainTitle;
        Vector2 titlePosition;
        Rectangle titleRectangle;
        Color color;
        Vector2 titleOrigin;
        float rotation;
        float scale;
        SpriteEffects spriteEffect;
        float layerDepth;

        ButtonRenderer mainButton;
        SpriteFont font;

        public MainScene(Game game, int backgroundWidth, int backgroundHeight, Vector2 titlePosition, Rectangle titleRectangle, Color color,
        Vector2 titleOrigin, float rotation,float scale, SpriteEffects spriteEffect, float layerDepth) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal._spriteBatch;

            mainBackgroundImg = dkoFinal.Content.Load<Texture2D>("MainScene/Blue");
            mainTitle = dkoFinal.Content.Load<Texture2D>("MainScene/GameTitle");
            font = dkoFinal.Content.Load<SpriteFont>("Fonts/classic");

            mainBackground = new BackgroundRenderer(spriteBatch, mainBackgroundImg, backgroundWidth, backgroundHeight);
            mainButton = new ButtonRenderer(font, new Vector2(backgroundWidth / 2, backgroundHeight / 2), "START", Color.White, Color.AliceBlue);

            this.titlePosition = titlePosition;
            this.titleRectangle = titleRectangle;
            this.color = color;
            this.titleOrigin = titleOrigin;
            this.rotation = rotation;
            this.scale = scale;
            this.spriteEffect = spriteEffect;
            this.layerDepth = layerDepth;
        }
        public override void Draw(GameTime gameTime)
        {
            // Drawing the background
            mainBackground.Draw();
            mainButton.Draw(spriteBatch);

            spriteBatch.Begin();
            // Drawing the title
            spriteBatch.Draw(mainTitle, titlePosition, titleRectangle, color, rotation, titleOrigin, scale, spriteEffect, layerDepth);      

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
