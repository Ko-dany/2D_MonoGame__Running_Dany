using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Renderers
{
    internal class MenuRenderer : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SpriteFont regular, selected;
        Vector2 position;
        Color regularColor, selectedColor;
        Vector2 menuOrigin;
        float rotation;
        float scale;
        SpriteEffects spriteEffect;
        float layerDepth;

        string[] menus;
        public int selectedIndex = 0;

        KeyboardState oldState;

        public MenuRenderer(Game game, SpriteBatch spriteBatch, SpriteFont regular, SpriteFont selected, Vector2 position, Color regularColor, Color selectedColor, string[] menus) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regular = regular;
            this.selected = selected;
            this.position = position;
            this.regularColor = regularColor;
            this.selectedColor = selectedColor;

            this.menus = menus;

            menuOrigin = new Vector2(regular.MeasureString(menus[0]).X / 2, regular.LineSpacing / 2);
            rotation = 0.0f;
            scale = 1.0f;
            spriteEffect = SpriteEffects.None;
            layerDepth = 0.0f;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if(ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex = selectedIndex == menus.Length - 1 ? 0 : selectedIndex + 1;
            }
            if(ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex = selectedIndex == 0 ? menus.Length - 1 : selectedIndex - 1;
            }
            oldState = ks;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            for (int i = 0; i < menus.Length; i++)
            {
                if (i == selectedIndex)
                {
                    spriteBatch.DrawString(selected, menus[i], new Vector2(position.X, position.Y + selected.LineSpacing * i), selectedColor, rotation, menuOrigin, scale, spriteEffect, layerDepth);
                }
                else
                {
                    spriteBatch.DrawString(regular, menus[i], new Vector2(position.X, position.Y + selected.LineSpacing * i), regularColor, rotation, menuOrigin, scale, spriteEffect, layerDepth);
                }

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
