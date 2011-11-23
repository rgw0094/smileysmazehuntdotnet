using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Smiley.Lib.Framework.Drawing;

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
        /// Draws a string starting at a point.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawString(SpriteFont font, string text, float x, float y)
        {
            _spriteBatch.DrawString(font, text, new Vector2(x, y), Color.Black);
        }

        /// <summary>
        /// Draws a string centered at a point.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawStringCentered(SpriteFont font, string text, float x, float y)
        {
            Vector2 v = font.MeasureString(text);
            DrawString(font, text, x - v.X / 2f, y - v.Y / 2f);
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

        #endregion
    }
}
