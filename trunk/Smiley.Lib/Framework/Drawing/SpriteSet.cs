using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Framework.Drawing
{
    public class SpriteSet
    {
        #region Private Variables

        private SmileyTexture _texture;
        private int _numTiles;
        private Rectangle _rect;
        private List<Sprite> _tiles;
        private Vector2? _hotSpot;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new TileSet.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="numTiles"></param>
        /// <param name="rect"></param>
        /// <param name="hotSpot"></param>
        public SpriteSet(SmileyTexture texture, int numTiles, Rectangle rect, Vector2? hotSpot = null)
        {
            //Defer creating the tile sprites until they are accessed, because we need the texture height/width
            //to compute the sprites and they might not be loaded yet!
            _texture = texture;
            _numTiles = numTiles;
            _rect = rect;
            _hotSpot = hotSpot;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of tiles in the TileSet.
        /// </summary>
        public int Count
        {
            get { return _numTiles; }
        }

        /// <summary>
        /// Gets the tile at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Sprite this[int index]
        {
            get
            {
                if (_tiles == null)
                {
                    CreateTiles();
                }
                return _tiles[index];
            }
        }

        #endregion

        #region Private Methods

        private void CreateTiles()
        {
            Texture2D texture = SMH.Data.GetTexture(_texture);
            _tiles = new List<Sprite>();
            int x = _rect.X;
            int y = _rect.Y;

            for (int i = 0; i < _numTiles; i++)
            {
                _tiles.Add(new Sprite(_texture, new Rectangle(x, y, _rect.Width, _rect.Height), _hotSpot.GetValueOrDefault()));
                x += _rect.Width;
                if (x >= texture.Width)
                {
                    x = 0;
                    y += _rect.Height;
                }
            }
        }

        #endregion
    }
}
