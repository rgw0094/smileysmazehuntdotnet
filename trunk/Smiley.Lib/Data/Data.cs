using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;

namespace Smiley.Lib.Data
{
    public static class Data
    {
        #region Constructor

        static Data()
        {
            Abilities = CreateAbilities();
            Enemies = CreateEnemies();
            GemsPerArea = GetGemsPerArea();
        }

        #endregion

        #region Properties

        public static Dictionary<Ability, AbilityData> Abilities
        {
            get;
            private set;
        }

        public static Dictionary<int, EnemyData> Enemies
        {
            get;
            private set;
        }

        public static Dictionary<Level, Dictionary<Gem, int>> GemsPerArea
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public static double GetDifficultyModifier(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.VeryEasy:
                    return 1.5;
                case Difficulty.Easy:
                    return 1.25;
                case Difficulty.Medium:
                    return 1.0;
                case Difficulty.Hard:
                    return 0.75;
                case Difficulty.VeryHard:
                    return 0.5;
                default:
                    return 1.0;
            }
        }

        #endregion

        #region Private Methods

        private static Dictionary<Level, Dictionary<Gem, int>> GetGemsPerArea()
        {
            Dictionary<Level, Dictionary<Gem, int>> gemCount = new Dictionary<Level, Dictionary<Gem, int>>();

            gemCount[Level.FOUNTAIN_AREA][Gem.Small] = 8;
            gemCount[Level.FOUNTAIN_AREA][Gem.Medium] = 1;
            gemCount[Level.FOUNTAIN_AREA][Gem.Large] = 1;

            gemCount[Level.OLDE_TOWNE][Gem.Small] = 7;
            gemCount[Level.OLDE_TOWNE][Gem.Medium] = 3;
            gemCount[Level.OLDE_TOWNE][Gem.Large] = 1;

            gemCount[Level.FOREST_OF_FUNGORIA][Gem.Small] = 14;
            gemCount[Level.FOREST_OF_FUNGORIA][Gem.Medium] = 6;
            gemCount[Level.FOREST_OF_FUNGORIA][Gem.Large] = 3;

            gemCount[Level.SESSARIA_SNOWPLAINS][Gem.Small] = 7;
            gemCount[Level.SESSARIA_SNOWPLAINS][Gem.Medium] = 2;
            gemCount[Level.SESSARIA_SNOWPLAINS][Gem.Large] = 1;

            gemCount[Level.WORLD_OF_DESPAIR][Gem.Small] = 8;
            gemCount[Level.WORLD_OF_DESPAIR][Gem.Medium] = 3;
            gemCount[Level.WORLD_OF_DESPAIR][Gem.Large] = 1;

            gemCount[Level.SERPENTINE_PATH][Gem.Small] = 1;
            gemCount[Level.SERPENTINE_PATH][Gem.Medium] = 1;
            gemCount[Level.SERPENTINE_PATH][Gem.Large] = 1;

            gemCount[Level.TUTS_TOMB][Gem.Small] = 8;
            gemCount[Level.TUTS_TOMB][Gem.Medium] = 2;
            gemCount[Level.TUTS_TOMB][Gem.Large] = 1;

            gemCount[Level.CASTLE_OF_EVIL][Gem.Small] = 3;
            gemCount[Level.CASTLE_OF_EVIL][Gem.Medium] = 3;
            gemCount[Level.CASTLE_OF_EVIL][Gem.Large] = 4;

            gemCount[Level.CONSERVATORY][Gem.Small] = 4;
            gemCount[Level.CONSERVATORY][Gem.Medium] = 1;
            gemCount[Level.CONSERVATORY][Gem.Large] = 1;

            gemCount[Level.SMOLDER_HOLLOW][Gem.Small] = 1;
            gemCount[Level.SMOLDER_HOLLOW][Gem.Medium] = 1;
            gemCount[Level.SMOLDER_HOLLOW][Gem.Large] = 2;

            return gemCount;
        }

        #region CreateAbilities

        private static Dictionary<Ability, AbilityData> CreateAbilities()
        {
            Dictionary<Ability, AbilityData> abilities = new Dictionary<Ability, AbilityData>();

            abilities[Ability.CANE] = new AbilityData
            {
                Name = "Cane Of Clinton",
                Description = "Use for 3 seconds to communicate telepathically with Bill Clinton. \n\nMana Cost: 10",
                Type = AbilityType.Activated,
                ManaCost = 10
            };

            abilities[Ability.WATER_BOOTS] = new AbilityData
            {
                Name = "Jesus' Sandals",
                Description = "While equipped you gain the power of Jesus Christ. (That means you can walk on water)",
                Type = AbilityType.Hold,
                ManaCost = 0
            };


            abilities[Ability.SPRINT_BOOTS] = new AbilityData
            {
                Name = "Speed Boots",
                Description = "When activated you run 75% faster.",
                Type = AbilityType.Activated,
                ManaCost = 0
            };

            abilities[Ability.FIRE_BREATH] = new AbilityData
            {
                Name = "Fire Breath",
                Description = string.Format("Allows you to breath deadly fire breath.\n\nMana Cost: 15/second\nDamage: {0} per second",
                 0), //Util::intToString(smh->player->getFireBreathDamage()).c_str())
                Type = AbilityType.Hold,
                ManaCost = 15
            };

            abilities[Ability.ICE_BREATH] = new AbilityData
            {
                Name = "Ice Breath",
                Description = "Unleases an icy blast that can freeze enemies.\n\nManaCost: 10",
                Type = AbilityType.Activated,
                ManaCost = 20
            };

            abilities[Ability.REFLECTION_SHIELD] = new AbilityData
            {
                Name = "Reflection Shield",
                Description = "Activate to deflect certain projectiles.\n\n\nMana Cost: 35/second",
                Type = AbilityType.Hold,
                ManaCost = 35
            };

            abilities[Ability.HOVER] = new AbilityData
            {
                Name = "Hover",
                Description = "Grants you the power to use hover pads.",
                Type = AbilityType.Hold,
                ManaCost = 0
            };

            abilities[Ability.LIGHTNING_ORB] = new AbilityData
            {
                Name = "Lightning Orbs",
                Description = string.Format("Shoots orbs of lightning. \n\n\nMana Cost: 5\nDamage:{0}",
                0), //Util::intToString(smh->player->getLightningOrbDamage()).c_str())
                Type = AbilityType.Activated,
                ManaCost = 5
            };

            abilities[Ability.SHRINK] = new AbilityData
            {
                Name = "Shrink",
                Description = "When activated Smiley will shrink in size and be able to fit into smaller spaces.",
                Type = AbilityType.Activated,
                ManaCost = 0
            };

            abilities[Ability.SILLY_PAD] = new AbilityData
            {
                Name = "Silly Pad",
                Description = "Places a Silly Pad. They are so silly that enemies can't even cross them!\n\nMana Cost: 5",
                Type = AbilityType.Activated,
                ManaCost = 5
            };

            abilities[Ability.TUTS_MASK] = new AbilityData
            {
                Name = "Tut's Mask",
                Description = "Grants the wearer the power of invisibility.\n\n\nMana Cost: 5/second",
                Type = AbilityType.Hold,
                ManaCost = 5
            };

            abilities[Ability.FRISBEE] = new AbilityData
            {
                Name = "Frisbee",
                Description = "Throws a frisbee that can stun enemies.",
                Type = AbilityType.Activated,
                ManaCost = 0
            };

            return abilities;
        }

        #endregion

        #region CreateEnemies

        private static Dictionary<int, EnemyData> CreateEnemies()
        {
            Dictionary<int, EnemyData> enemies = new Dictionary<int, EnemyData>();

            //TODO:
            
            return enemies;
        }

        #endregion

        #endregion
    }
}
