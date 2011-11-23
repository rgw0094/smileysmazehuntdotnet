using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using System.IO;
using Smiley.Lib.Framework;

namespace Smiley.Lib.Data
{
    public class SaveFile
    {
        #region Private Variables

        private List<Change> _changes;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new SaveFile.
        /// </summary>
        /// <param name="fileName"></param>
        public SaveFile(string fileName)
        {
            Name = fileName;
        }

        #endregion

        #region Public Properties

        public string Name { get; private set; }
        public TimeSpan TimePlayed { get; set; }
        public int NumTongueLicks { get; set; }
        public int NumEnemiesKilled { get; set; }
        public int PixelsTraversed { get; set; }
        public TimeSpan TimeFileLoaded { get; set; }
        public bool AdviceManEncounterCompleted { get; set; }
        public int Money { get; set; }
        public Level Level { get; set; }
        public int GridX { get; set; }
        public int GridY { get; set; }
        public double PlayerHealth { get; set; }
        public double PlayerMana { get; set; }
        public Difficulty Difficulty { get; set; }
        public Dictionary<Ability, bool> HasAbility { get; private set; }
        public int[,] NumKeys { get; private set; }
        public Dictionary<Level, Dictionary<Gem, int>> NumGems { get; private set; }
        public Dictionary<Upgrade, int> NumUpgrades { get; private set; }
        public Dictionary<Level, bool> HasVisitedLevel { get; private set; }
        public Dictionary<Level, bool[,]> Explored { get; private set; }
        public Dictionary<Boss, bool> HasKilledBoss { get; private set; }

        /// <summary>
        /// Returns the total number of gems of all types found in all areas.
        /// </summary>
        /// <returns></returns>
        public int GetTotalGemCount
        {
            get
            {
                int total = 0;

                foreach (Level level in Enum.GetValues(typeof(Level)))
                {
                    foreach (Gem gem in Enum.GetValues(typeof(Gem)))
                    {
                        total += NumGems[level][gem];
                    }
                }

                return total;
            }
        }

        /// <summary>
        /// Returns the damage modifer for the save's upgrades and difficulties
        /// </summary>
        public double DamageModifer
        {
            get { return (1.0 + NumUpgrades[Upgrade.Damage] * 0.2) * SmileyData.GetDifficultyModifier(Difficulty); }
        }

        /// <summary>
        /// Returns the mana modifer for the save's upgrades and difficulties
        /// </summary>
        public double ManaModifier
        {
            get { return 1.0 * (double)NumUpgrades[Upgrade.Mana] * 0.25; }
        }

        #endregion

        #region Public Methods

        public void Reset()
        {
            Level = Enums.Level.FOUNTAIN_AREA;
            PlayerMana = 50.0;
            TimeFileLoaded = DateTime.Now.TimeOfDay;

            NumKeys = new int[5, 4];
            HasAbility = new Dictionary<Ability, bool>();
            NumGems = new Dictionary<Level, Dictionary<Gem, int>>();
            NumUpgrades = new Dictionary<Upgrade, int>();
            HasVisitedLevel = new Dictionary<Level, bool>();
            HasKilledBoss = new Dictionary<Boss, bool>();

            foreach (Level level in Enum.GetValues(typeof(Level)))
            {
                HasVisitedLevel[level] = false;
                Explored[level] = new bool[256, 256];
                NumGems[level] = new Dictionary<Gem, int>();
                foreach (Gem gem in Enum.GetValues(typeof(Gem)))
                {
                    NumGems[level][gem] = 0;
                }
            }

            foreach (Ability ability in Enum.GetValues(typeof(Ability)))
            {
                HasAbility[ability] = false;
            }

            foreach (Upgrade upgrade in Enum.GetValues(typeof(Upgrade)))
            {
                NumUpgrades[upgrade] = 0;
            }

            foreach (Boss boss in Enum.GetValues(typeof(Boss)))
            {
                HasKilledBoss[boss] = false;
            }
        }

        public void SaveChanges(BitStream stream)
        {
            stream.WriteBits(_changes.Count, 16);

            foreach (Change change in _changes)
            {
                stream.WriteByte((int)change.Level);
                stream.WriteByte(change.X);
                stream.WriteByte(change.Y);
            }
        }

        /// <summary>
        /// Toggles whether or not a tile has been changed for a certain level.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ChangeTile(Level level, int x, int y)
        {
            Change change = _changes.SingleOrDefault(c => c.Level == level && c.X == x && c.Y == y);
            if (change != null)
            {
                _changes.Remove(change);
            }
            else
            {
                _changes.Add(new Change
                {
                    Level = level,
                    X = x,
                    Y = y
                });
            }
        }

        /// <summary>
        /// Returns whether or not a tile is changed.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsTileChanged(Level level, int x, int y)
        {
            return _changes.Exists(c => c.Level == level && c.X == x && c.Y == y);
        }

        /// <summary>
        /// Explores in a 12 block radius around the given grid coordinates.
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        public void Explore(int gridX, int gridY)
        {
            for (int x = gridX - 6; x <= gridX + 6; x++)
            {
                for (int y = gridY - 6; y <= gridY + 6; y++)
                {
                    if (true) //smh->environment->isInBounds(curGridX,curGridY)) {//TODO
                    {
                        Explored[Level][gridX, gridY] = true;
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the save percentage for the currently open save file.
        /// Each dollar is worth .15%, each boss kill is 5.708%.
        /// </summary>
        /// <returns></returns>
        public int CalculateCompletionPercentage()
        {
            double p = 0;

            foreach (Level level in Enum.GetValues(typeof(Level)))
            {
                p += NumGems[level][Gem.Small] * Constants.SmallGemValue * 0.15;
                p += NumGems[level][Gem.Medium] * Constants.MediumGemValue * 0.15;
                p += NumGems[level][Gem.Large] * Constants.LargeGemValue * 0.15;
            }

            foreach (Boss boss in Enum.GetValues(typeof(Boss)))
            {
                if (HasKilledBoss[boss])
                {
                    p += 5.708;
                }
            }

            return (int)p;
        }

        #endregion

        #region Private Classes

        private class Change
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Level Level { get; set; }
        }

        #endregion
    }
}
