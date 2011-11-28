using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Data;
using Smiley.Lib.Enums;

namespace Smiley.Lib.Framework.Drawing
{
    /// <summary>
    /// Wraps the XNA 2D stuff to provide a cleaner API for our needs.
    /// </summary>
    public class Graphics2DWrapper
    {
        #region Private Variables

        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphicsDeviceManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new Graphics2DWrapper.
        /// </summary>
        /// <param name="game"></param>
        public Graphics2DWrapper(GraphicsDeviceManager graphicsDeviceManager)
        {
            _spriteBatch = new SpriteBatch(graphicsDeviceManager.GraphicsDevice);
            _graphicsDeviceManager = graphicsDeviceManager;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prepares a frame for rendering.
        /// </summary>
        public void BeginFrame()
        {
            _spriteBatch.Begin();
        }

        /// <summary>
        /// Finishes rendering a frame.
        /// </summary>
        public void EndFrame()
        {
            _spriteBatch.End();
        }

        /// <summary>
        /// Returns whether or not the cursor is currently over the window.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsPointInWindow(Vector2 point)
        {
            return _graphicsDeviceManager.GraphicsDevice.Viewport.Bounds.Contains(new Point((int)point.X, (int)point.Y));
        }

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="alignment"></param>
        public void DrawString(SmileyFont font, string text, float x, float y, TextAlignment alignment)
        {
            DrawString(font, text, x, y, alignment, Color.Black, 1f);
        }

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="alignment"></param>
        /// <param name="color"></param>
        public void DrawString(SmileyFont font, string text, float x, float y, TextAlignment alignment, Color color)
        {
            DrawString(font, text, x, y, alignment, color, 1f);
        }

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="alignment"></param>
        /// <param name="color"></param>
        /// <param name="scale"></param>
        public void DrawString(SmileyFont font, string text, float x, float y, TextAlignment alignment, Color color, float scale)
        {
            Vector2 drawVector = new Vector2(x, y);
            if (alignment == TextAlignment.Center)
            {
                Vector2 v = SMH.Data.GetFont(font).MeasureString(text);
                drawVector = new Vector2(x - v.X / 2f, y);
            }
            else if (alignment == TextAlignment.Right)
            {
                Vector2 v = SMH.Data.GetFont(font).MeasureString(text);
                drawVector = new Vector2(x - v.X, y);
            }

            _spriteBatch.DrawString(SMH.Data.GetFont(font), text, drawVector, color, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draws a sprite.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="position"></param>
        public void DrawSprite(Sprite sprite, Vector2 position)
        {
            DrawSprite(sprite, position.X, position.Y);
        }

        /// <summary>
        /// Draws a sprite.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawSprite(Sprite sprite, float x, float y)
        {
            Vector2 drawPosition = drawPosition = new Vector2(x - sprite.HotSpot.X, y - sprite.HotSpot.Y);
            _spriteBatch.Draw(SMH.Data.GetTexture(sprite.Texture), drawPosition, sprite.Rect, Color.White);
        }

        /// <summary>
        /// Draws a sprite scaled vertically and horizontally.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        public void DrawSpriteScaled(Sprite sprite, float x, float y, float scale)
        {
            DrawSpriteScaled(sprite, x, y, scale, scale);
        }

        /// <summary>
        /// Draws a sprite scaled horizontally and/or vertically.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xScale"></param>
        /// <param name="yScale"></param>
        public void DrawSpriteScaled(Sprite sprite, float x, float y, float xScale, float yScale)
        {
            if (xScale == 1f && yScale == 1f)
            {
                DrawSprite(sprite, x, y);
            }
            else
            {
                Texture2D texture = SMH.Data.GetTexture(sprite.Texture);
                Rectangle drawRect = new Rectangle(
                    Convert.ToInt32(x - sprite.HotSpot.X * xScale),
                    Convert.ToInt32(y - sprite.HotSpot.Y * yScale),
                    Convert.ToInt32(sprite.Rect == null ? texture.Width * xScale : sprite.Rect.Value.Width * xScale),
                    Convert.ToInt32(sprite.Rect == null ? texture.Height * yScale : sprite.Rect.Value.Height * yScale));

                _spriteBatch.Draw(texture, drawRect, sprite.Rect, Color.White);
            }
        }

        public void DrawRect(Rect rect, Color color)
        {
            Texture2D texture = SMH.Data.GetTexture(SmileyTexture.UserInterface);

            _spriteBatch.Draw(texture, new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, 1), color);//top
            _spriteBatch.Draw(texture, new Rectangle((int)rect.X, (int)rect.Y, 1, (int)rect.Height), color);//left
            _spriteBatch.Draw(texture, new Rectangle((int)rect.X + (int)rect.Width, (int)rect.Y, 1, (int)rect.Height), color);//right
            _spriteBatch.Draw(texture, new Rectangle((int)rect.X, (int)rect.Y + (int)rect.Height, (int)rect.Width, 1), color);//bottom
        }

        #endregion
    }
}
