using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Smiley.Lib.Data
{
    public partial class SmileyData
    {
        #region Private Variables

        private Dictionary<SmileyTexture, Texture2D> _textures = new Dictionary<SmileyTexture, Texture2D>();
        private Dictionary<SmileyFont, SpriteFont> _fonts = new Dictionary<SmileyFont, SpriteFont>();
        private ContentManager _contentMaager;

        #endregion

        #region Constructors

        public SmileyData(ContentManager contentManager)
        {
            _contentMaager = contentManager;
            Abilities = CreateAbilities();
            GemsPerArea = GetGemsPerArea();
            CreateEnemies();

            //TODO: precache textures
        }

        #endregion

        #region Properties

        public Dictionary<Ability, AbilityInfo> Abilities
        {
            get;
            private set;
        }

        public Dictionary<Level, Dictionary<Gem, int>> GemsPerArea
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public SpriteFont GetFont(SmileyFont font)
        {
            if (!_fonts.ContainsKey(font))
            {
                _fonts[font] = _contentMaager.Load<SpriteFont>(font.GetDescription());
            }
            return _fonts[font];
        }

        public Texture2D GetTexture(SmileyTexture texture)
        {
            if (!_textures.ContainsKey(texture))
            {
                PreCacheTextures(texture);
            }
            return _textures[texture];
        }

        public void PreCacheTextures(params SmileyTexture[] textures)
        {
            foreach (SmileyTexture texture in textures)
            {
                if (!_textures.ContainsKey(texture))
                {
                    _textures[texture] = _contentMaager.Load<Texture2D>(texture.GetDescription());
                }
            }
        }

        public void UnloadTextures(params SmileyTexture[] textures)
        {
            foreach (SmileyTexture texture in textures)
            {
                if (_textures.ContainsKey(texture))
                {
                    _textures[texture].Dispose();
                    _textures.Remove(texture);
                }
            }
        }

        public float GetDifficultyModifier(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.VeryEasy:
                    return 1.5f;
                case Difficulty.Easy:
                    return 1.25f;
                case Difficulty.Medium:
                    return 1.0f;
                case Difficulty.Hard:
                    return 0.75f;
                case Difficulty.VeryHard:
                    return 0.5f;
                default:
                    return 1.0f;
            }
        }

        public string GetAbilityDescription(Ability ability)
        {
            switch (ability)
            {
                case Ability.CANE:
                    return "Use for 3 seconds to communicate\ntelepathically with Bill \nClinton. \n\nMana Cost: 10";
                case Ability.WATER_BOOTS:
                    return "While equipped you gain the \npower of Jesus Christ. (That means\nyou can walk on water)";
                case Ability.SPRINT_BOOTS:
                    return "When activated you run \n75% faster.";
                case Ability.FIRE_BREATH:
                    return string.Format("Allows you to breath deadly \nfire breath.\n\nMana Cost: 15/second\nDamage: {0:N2} per second", SMH.Player.FireBreathDamage);
                case Ability.ICE_BREATH: 
                    return "Unleases an icy blast that can\nfreeze enemies.\n\nManaCost: 10";
                case Ability.REFLECTION_SHIELD:
                    return "Activate to deflect certain \nprojectiles.\n\n\nMana Cost: 35/second";
                case Ability.HOVER: 
                    return "Grants you the power to use \nhover pads.";
                case Ability.LIGHTNING_ORB:
                    return string.Format("Shoots orbs of lightning. \n\n\nMana Cost: 5\nDamage:{0:N0}", SMH.Player.LightningOrbDamage);
                case Ability.SHRINK:
                    return "When activated Smiley will shrink \nin size and be able to fit into\nsmaller spaces.";
                case Ability.SILLY_PAD:
                    return "Places a Silly Pad. They are so silly\nthat enemies can't even cross \nthem!\n\nMana Cost: 5";
                case Ability.TUTS_MASK:
                    return "Grants the wearer the power of\ninvisibility.\n\n\nMana Cost: 5/second";
                case Ability.FRISBEE: 
                    return "Throws a frisbee that can stun\nenemies.";
                default:
                    return string.Empty;

            };
        }

        #endregion

        #region GemsPerArea

        private Dictionary<Level, Dictionary<Gem, int>> GetGemsPerArea()
        {
            Dictionary<Level, Dictionary<Gem, int>> gemCount = new Dictionary<Level, Dictionary<Gem, int>>();
            foreach (Level level in Enum.GetValues(typeof(Level)))
            {
                gemCount[level] = new Dictionary<Gem, int>();
            }

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

        #endregion

        #region Abilities

        private Dictionary<Ability, AbilityInfo> CreateAbilities()
        {
            Dictionary<Ability, AbilityInfo> abilities = new Dictionary<Ability, AbilityInfo>();

            abilities[Ability.CANE] = new AbilityInfo
            {
                Name = "Cane Of Clinton",
                Type = AbilityType.Activated,
                ManaCost = 10
            };

            abilities[Ability.WATER_BOOTS] = new AbilityInfo
            {
                Name = "Jesus' Sandals",
                Type = AbilityType.Hold,
                ManaCost = 0
            };


            abilities[Ability.SPRINT_BOOTS] = new AbilityInfo
            {
                Name = "Speed Boots",
                Type = AbilityType.Activated,
                ManaCost = 0
            };

            abilities[Ability.FIRE_BREATH] = new AbilityInfo
            {
                Name = "Fire Breath",
                Type = AbilityType.Hold,
                ManaCost = 15
            };

            abilities[Ability.ICE_BREATH] = new AbilityInfo
            {
                Name = "Ice Breath",
                Type = AbilityType.Activated,
                ManaCost = 20
            };

            abilities[Ability.REFLECTION_SHIELD] = new AbilityInfo
            {
                Name = "Reflection Shield",
                Type = AbilityType.Hold,
                ManaCost = 35
            };

            abilities[Ability.HOVER] = new AbilityInfo
            {
                Name = "Hover",
                Type = AbilityType.Hold,
                ManaCost = 0
            };

            abilities[Ability.LIGHTNING_ORB] = new AbilityInfo
            {
                Name = "Lightning Orbs",
                Type = AbilityType.Activated,
                ManaCost = 5
            };

            abilities[Ability.SHRINK] = new AbilityInfo
            {
                Name = "Shrink",
                Type = AbilityType.Activated,
                ManaCost = 0
            };

            abilities[Ability.SILLY_PAD] = new AbilityInfo
            {
                Name = "Silly Pad",
                Type = AbilityType.Activated,
                ManaCost = 5
            };

            abilities[Ability.TUTS_MASK] = new AbilityInfo
            {
                Name = "Tut's Mask",
                Type = AbilityType.Hold,
                ManaCost = 5
            };

            abilities[Ability.FRISBEE] = new AbilityInfo
            {
                Name = "Frisbee",
                Type = AbilityType.Activated,
                ManaCost = 0
            };

            return abilities;
        }

        #endregion
    }

    public class AbilityInfo
    {
        public string Name { get; set; }
        public int ManaCost { get; set; }
        public AbilityType Type { get; set; }
    }
}
