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
    internal class MainScene:GameScene
    {
        SpriteBatch spriteBatch;
        Texture2D mainBackgroundImg;

        BackgroundRenderer mainBackground;
        MenuRenderer menuSelection;

        Texture2D mainTitle;
        Vector2 titlePosition;
        Rectangle titleRectangle;
        Color color;
        Vector2 titleOrigin;
        float rotation;
        float scale;
        SpriteEffects spriteEffect;
        float layerDepth;

        SpriteFont regular, selected;

        public MainScene(Game game, int backgroundWidth, int backgroundHeight, Vector2 titlePosition, Rectangle titleRectangle, Color color,
        Vector2 titleOrigin, float rotation,float scale, SpriteEffects spriteEffect, float layerDepth) : base(game)
        {
            DkoFinal dkoFinal = (DkoFinal)game;
            spriteBatch = dkoFinal.spriteBatch;

            mainBackgroundImg = dkoFinal.Content.Load<Texture2D>("MainScene/Blue");
            mainTitle = dkoFinal.Content.Load<Texture2D>("MainScene/GameTitle");

            regular = dkoFinal.Content.Load<SpriteFont>("Fonts/regular");
            selected = dkoFinal.Content.Load<SpriteFont>("Fonts/selected");


            mainBackground = new BackgroundRenderer(spriteBatch, mainBackgroundImg, backgroundWidth, backgroundHeight);

            this.titlePosition = titlePosition;
            this.titleRectangle = titleRectangle;
            this.color = color;
            this.titleOrigin = titleOrigin;
            this.rotation = rotation;
            this.scale = scale;
            this.spriteEffect = spriteEffect;
            this.layerDepth = layerDepth;

            menuSelection = new MenuRenderer(dkoFinal, spriteBatch, regular, selected, new Vector2(backgroundWidth/2, backgroundHeight/5*3), Color.White, Color.Black, new string[] { "START", "HELP", "OPTIONS", "EXIT" });
            this.Components.Add(menuSelection);
        }
        public override void Draw(GameTime gameTime)
        {
            // Drawing the background
            mainBackground.Draw();

            spriteBatch.Begin();
            // Drawing the title
            spriteBatch.Draw(mainTitle, titlePosition, titleRectangle, color, rotation, titleOrigin, scale, spriteEffect, layerDepth);      

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
