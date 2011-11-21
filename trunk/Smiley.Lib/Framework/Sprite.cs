using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;
using Smiley.Lib.Data;

namespace Smiley.Lib.Framework
{
    public class Sprite
    {
        /// <summary>
        /// Constructs a new Sprite.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="rect"></param>
        public Sprite(SmileyTexture texture, Rectangle? rect)
            : this(texture, rect, new Vector2(0, 0))
        {
        }

        /// <summary>
        /// Constructs a new Sprite.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="rect"></param>
        /// <param name="hotSpot"></param>
        public Sprite(SmileyTexture texture, Rectangle? rect, Vector2 hotSpot)
        {
            Texture = texture;
            Rect = rect;
            HotSpot = hotSpot;
        }

        /// <summary>
        /// The texture containing the sprite.
        /// </summary>
        public SmileyTexture Texture
        {
            get;
            private set;
        }

        /// <summary>
        /// The Sprite's rectangle within the texture, or null if the sprite
        /// is the entire texture.
        /// </summary>
        public Rectangle? Rect
        {
            get;
            private set;
        }

        /// <summary>
        /// The center point of the sprite.
        /// </summary>
        public Vector2 HotSpot
        {
            get;
            private set;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Vector2 drawPosition = drawPosition = new Vector2(position.X - HotSpot.X, position.Y - HotSpot.Y);
            spriteBatch.Draw(SmileyData.GetTexture(Texture), drawPosition, Rect, Color.White);
        }
    }
}
