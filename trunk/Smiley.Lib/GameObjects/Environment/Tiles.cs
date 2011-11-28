using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using System.IO;
using System.Collections;

namespace Smiley.Lib.GameObjects.Environment
{
    /// <summary>
    /// A set of tiles making up a smiley level.
    /// </summary>
    public class Tiles : IEnumerable<Tile>
    {
        #region Private Variables

        private Tile[,] _tiles;

        #endregion

        #region Constructors

        /// <summary>
        /// Loads a level file.
        /// </summary>
        /// <param name="fileName"></param>
        public Tiles(Level level)
        {
            switch (level)
            {
                case Level.FOUNTAIN_AREA:
                    Load("Smiley.Content\\Levels\\fountain.smh");
                    break;
                case Level.OLDE_TOWNE:
                    Load("Smiley.Content\\Levels\\oldetowne.smh");
                    break;
                case Level.SMOLDER_HOLLOW:
                    Load("Smiley.Content\\Levels\\smhollow.smh");
                    break;
                case Level.FOREST_OF_FUNGORIA:
                    Load("Smiley.Content\\Levels\\forest.smh");
                    break;
                case Level.SESSARIA_SNOWPLAINS:
                    Load("Smiley.Content\\Levels\\snow.smh");
                    break;
                case Level.TUTS_TOMB:
                    Load("Smiley.Content\\Levels\\tutstomb.smh");
                    break;
                case Level.WORLD_OF_DESPAIR:
                    Load("Smiley.Content\\Levels\\despair.smh");
                    break;
                case Level.SERPENTINE_PATH:
                    Load("Smiley.Content\\Levels\\path.smh");
                    break;
                case Level.CASTLE_OF_EVIL:
                    Load("Smiley.Content\\Levels\\castle.smh");
                    break;
                case Level.CONSERVATORY:
                    Load("Smiley.Content\\Levels\\conserve.smh");
                    break;
                case Level.DEBUG_AREA:
                    Load("Smiley.Content\\Levels\\debug.smh");
                    break;
                default:
                    throw new Exception("Unknown level: " + level);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the level.
        /// </summary>
        public int Width
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the height of the level.
        /// </summary>
        public int Height
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the tile at the given (x,y) coordinate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile this[int x, int y]
        {
            get
            {
                if (x >= Width || y >= Height || x < 0 || y < 0)
                    throw new ArgumentException(string.Format("Attemping to access tile ({0},{1}). Level size is only ({2},{3})", x, y, Width, Height));
                return _tiles[x, y];
            }
        }

        #endregion

        #region Private Methods

        private void Load(string fileName)
        {
            using (FileStream file = File.Open(fileName, FileMode.Open))
            {
                file.Position += 3; //skip id range

                //Read area width
                Width = ReadCharacters(file, 3);
                file.Position++; //skip space

                //Read area height
                Height = ReadCharacters(file, 3);
                file.Position += 2; //skip newline

                _tiles = new Tile[Width, Height];
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        _tiles[x, y] = new Tile
                        {
                            X = x,
                            Y = y
                        };
                    }
                }

                //Load Layers
                ReadLayer(file, (x, y, i) => _tiles[x, y].ID = i);
                ReadLayer(file, (x, y, i) => _tiles[x, y].Variable = i);
                ReadLayer(file, (x, y, i) => _tiles[x, y].Terrain = i);
                ReadLayer(file, (x, y, i) => _tiles[x, y].Collision = (CollisionTile)i);
                ReadLayer(file, (x, y, i) => _tiles[x, y].Item = i);
                ReadLayer(file, (x, y, i) => _tiles[x, y].Enemy = i - 1);
            }
        }

        private int ReadCharacters(FileStream file, int numChars)
        {
            string s = string.Empty;
            for (int i = 0; i < numChars; i++)
            {
                s += Convert.ToChar(file.ReadByte());
            }
            return Convert.ToInt32(s);
        }

        private void ReadLayer(FileStream file, Action<int, int, int> loadAction)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    loadAction(x, y, ReadCharacters(file, 3));
                }
                file.Position += 2; //skip newline
            }
            file.Position += 9; //Skip garbage characters.
        }

        #endregion

        #region IEnumerable<Tile> Implementation

        public IEnumerator<Tile> GetEnumerator()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    yield return this[x, y];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
