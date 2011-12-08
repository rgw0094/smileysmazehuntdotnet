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
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawSprite(Sprite sprite, float x, float y)
        {
            DrawSprite(sprite, x, y, Color.White);
        }

        /// <summary>
        /// Draws a sprite.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void DrawSprite(Sprite sprite, float x, float y, Color color)
        {
            Vector2 drawPosition = drawPosition = new Vector2(x - sprite.HotSpot.X, y - sprite.HotSpot.Y);
            _spriteBatch.Draw(SMH.Data.GetTexture(sprite.Texture), drawPosition, sprite.Rect, color);
        }

        /// <summary>
        /// Draws a sprite.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="targetRect"></param>
        /// <param name="color"></param>
        public void DrawSprite(Sprite sprite, Rectangle targetRect, Color color)
        {
            Texture2D texture = SMH.Data.GetTexture(sprite.Texture);
            _spriteBatch.Draw(texture, targetRect, sprite.Rect, color);
        }

        /// <summary>
        /// Draws a sprite.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        public void DrawSprite(Sprite sprite, float x, float y, Color color, float rotation, float scale)
        {
            _spriteBatch.Draw(SMH.Data.GetTexture(sprite.Texture), new Vector2(x, y), sprite.Rect, color, rotation, sprite.HotSpot, scale, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Draws a sprite.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="xScale"></param>
        /// <param name="yScale"></param>
        public void DrawSprite(Sprite sprite, float x, float y, Color color, float rotation, float xScale, float yScale)
        {
            _spriteBatch.Draw(SMH.Data.GetTexture(sprite.Texture), new Vector2(x, y), sprite.Rect, color, rotation, sprite.HotSpot, new Vector2(xScale, yScale), SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Draws a cropped portion of a sprite.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cropRect"></param>
        public void DrawCroppedSprite(Sprite sprite, float x, float y, Rectangle cropRect)
        {
            Vector2 drawPosition = new Vector2(x - sprite.HotSpot.X, y - sprite.HotSpot.Y);
            _spriteBatch.Draw(SMH.Data.GetTexture(sprite.Texture), drawPosition, cropRect, Color.White);
        }

        /// <summary>
        /// Draws an animation.
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawAnimation(Animation animation, float x, float y)
        {
            DrawSprite(animation.ActiveSprite, x, y);
        }

        /// <summary>
        /// Draws an animation.
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void DrawAnimation(Animation animation, float x, float y, Color color)
        {
            DrawSprite(animation.ActiveSprite, x, y, color);
        }

        /// <summary>
        /// Draws an animation.
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="targetRect"></param>
        /// <param name="color"></param>
        public void DrawAnimation(Animation animation, Rectangle targetRect, Color color)
        {
            DrawSprite(animation.ActiveSprite, targetRect, color);
        }

        /// <summary>
        /// Draws an animation.
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        public void DrawAnimation(Animation animation, float x, float y, Color color, float rotation, float scale)
        {
            DrawSprite(animation.ActiveSprite, x, y, color, rotation, scale);
        }

        /// <summary>
        /// Draws an animation.
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="xScale"></param>
        /// <param name="yScale"></param>
        public void DrawAnimation(Animation animation, float x, float y, Color color, float rotation, float xScale, float yScale)
        {
            DrawSprite(animation.ActiveSprite, x, y, color, rotation, xScale, yScale);
        }

        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        public void DrawRect(Rect rect, Color color, bool fill)
        {
            DrawRect(rect.X, rect.Y, rect.Width, rect.Height, color, fill);
        }

        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void DrawRect(float x, float y, float width, float height, Color color, bool fill)
        {
            //Just stretch a texture to draw shapes
            Texture2D texture = SMH.Data.GetTexture(SmileyTexture.UserInterface);

            if (fill)
            {
                DrawSprite(Sprites.StretchableBlackSquare, new Rectangle((int)x, (int)y, (int)width, (int)height), color);
            }
            else
            {
                _spriteBatch.Draw(texture, new Rectangle((int)x, (int)y, (int)width, 1), color);//top
                _spriteBatch.Draw(texture, new Rectangle((int)x, (int)y, 1, (int)height), color);//left
                _spriteBatch.Draw(texture, new Rectangle((int)x + (int)width, (int)y, 1, (int)height), color);//right
                _spriteBatch.Draw(texture, new Rectangle((int)x, (int)y + (int)height, (int)width, 1), color);//bottom
            }
        }

        #endregion
    }
}
