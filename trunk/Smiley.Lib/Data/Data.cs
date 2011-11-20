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

            

            enemies[0] = new EnemyData
            {
                Name = "Crazy Croc",
                SpriteColumn = 0,
                SpriteRow = 0,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 50,
                Damage = 25,
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true
            };

            enemies[1] = new EnemyData
            {
                Name = "GumDrop",
                SpriteColumn = 0,
                SpriteRow = 1,
                NumFrames = 2,
                enemyType = EnemyType.Burrower,
                wanderType = WanderType.WanderLeftRight,
                HP = 150,
                Damage = 50,
                Radius = 28,
                Speed = 80,
                CanTraverseLand = true,
                HasRangedAttack = true,
                Variable1 = 0, //uses gumdrop burrowing animation
                Variable2 = 200, //distance at which to burrow
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 2000,
                    ProjectileDamage = 50,
                    ProjectileSpeed = 400,
                    RangedType = ProjectileType.BlueBall
                }
            };

            enemies[2] = new EnemyData
            {
                Name = "Evil Eye",
                SpriteColumn = 1,
                SpriteRow = 2,
                enemyType = EnemyType.EvilEye,
                wanderType = WanderType.WanderRandomly,
                HP = 80,
                Damage = 25,
                Speed = 100,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                CanTraverseLava = true,
                ImmuneToFire = true,
                ImmuneToLightning = true,
                ImmuneToTongue = true,
                ImmuneToStun = true
            };

            enemies[3] = new EnemyData
            {
                Name = "Shifty Squirrel",
                SpriteColumn = 0,
                SpriteRow = 8,
                enemyType = EnemyType.Ranged,
                wanderType = WanderType.WanderRandomly,
                HP = 75,
                Damage = 25,
                Speed = 100,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                Chases = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 2000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 25,
                    RangedType = ProjectileType.Acorn
                }
            };

            enemies[4] = new EnemyData
            {
                Name = "Diabolical Diamond",
                HasOneGraphic = true,
                SpriteColumn = 9,
                SpriteRow = 0,
                enemyType = EnemyType.Floater,
                wanderType = WanderType.WanderRandomly,
                HP = 80,
                Damage = 75,
                Speed = 100,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                Chases = true
            };

            enemies[5] = new EnemyData
            {
                Name = "Evil Circle",
                HasOneGraphic = true,
                SpriteColumn = 10,
                SpriteRow = 0,
                NumFrames = 2,
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 100,
                Speed = 500,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true
            };

            enemies[6] = new EnemyData
            {
                Name = "Evil Circle",
                HasOneGraphic = true,
                SpriteColumn = 10,
                SpriteRow = 0,
                NumFrames = 2,
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderUpDown,
                HP = 99999,
                Damage = 100,
                Speed = 500,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true
            };

            enemies[7] = new EnemyData
            {
                Name = "Cactlet",
                SpriteColumn = 5,
                SpriteRow = 2,
                HasOneGraphic = true,                
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderUpDown,
                HP = 51,
                Damage = 25,
                Speed = 125,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                Chases = true
            };

            enemies[8] = new EnemyData
            {
                Name = "Sentinel",
                SpriteColumn = 2,
                SpriteRow = 2,
                HasOneGraphic = true,
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderLeftRight,
                HP = 100,
                Damage = 50,
                Speed = 0,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                Chases = false,
                ImmuneToFire = true,
                ImmuneToLightning = true,
                ImmuneToTongue = true,
                ImmuneToStun = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 450,
                    Delay = 2000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 150,
                    RangedType = ProjectileType.Acorn
                }
            };

            enemies[9] = new EnemyData
            {
                Name = "Mr. Bigglesworth",
                SpriteColumn = 8,
                SpriteRow = 4,                
                enemyType = EnemyType.BombGenerator,
                wanderType = WanderType.WanderLeftRight,
                HP = 9999,
                Damage = 50,
                Speed = 100,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true
            };

            enemies[10] = new EnemyData
            {
                Name = "Cunning Crab",
                SpriteColumn = 0,
                SpriteRow = 5,
                NumFrames = 2,
                enemyType = EnemyType.ClownBalloon,
                wanderType = WanderType.WanderRandomly,
                HP = 200,
                Damage = 50,
                Speed = 100,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true                
            };

            enemies[11] = new EnemyData
            {
                SpriteColumn = 0,
                SpriteRow = 6,
                enemyType = EnemyType.BatletDistributor,
                wanderType = WanderType.WanderRandomly,
                HP = 100,
                Damage = 50,
                Speed = 100,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true
            };

            enemies[12] = new EnemyData
            {
                Name = "Boastful Buzzard",
                SpriteColumn = 3,
                SpriteRow = 2,
                enemyType = EnemyType.Buzzard,
                wanderType = WanderType.WanderRandomly,
                HP = 100,
                Damage = 75,
                Speed = 400,
                Radius = 33,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true                
            };

            //Sad shooter
            enemies[13] = new EnemyData
            {
                Name = "Sad Shooter",
                SpriteColumn = 12,
                SpriteRow = 4,
                enemyType = EnemyType.SadShooter,
                wanderType = WanderType.WanderRandomly,
                HP = 50,
                Damage = 25,
                Speed = 400,
                Radius = 32,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true
            };

            //Mortimer (earth floater)
            enemies[14] = new EnemyData
            {
                Name = "Mortimer",
                SpriteColumn = 8,
                SpriteRow = 0,
                HasOneGraphic = true,
                enemyType = EnemyType.Floater,
                wanderType = WanderType.WanderRandomly,
                HP = 100,
                Damage = 100,
                Speed = 100,
                Radius = 32,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true
            };

            //Flailer 1
            enemies[15] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 1,
                enemyType = EnemyType.Flailer,
                wanderType = WanderType.WanderRandomly,
                HP = 200,
                Damage = 25,
                Speed = 100,
                Radius = 32,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                ImmuneToStun=true
            };

            //Small green tentacle
            enemies[16] = new EnemyData
            {                
                SpriteColumn = 6,
                SpriteRow = 2,
                enemyType = EnemyType.Tentacle,
                wanderType = WanderType.WanderRandomly,
                HP = 200,
                Damage = 25,
                Speed = 80,
                Radius = 9,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                Variable1 = 3
            };

            //Large orange tentacle
            enemies[17] = new EnemyData
            {
                SpriteColumn = 7,
                SpriteRow = 2,
                enemyType = EnemyType.Tentacle,
                wanderType = WanderType.WanderRandomly,
                HP = 200,
                Damage = 25,
                Speed = 50,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 18
            };

            //Brown turtle
            enemies[18] = new EnemyData
            {
                Name = "Tyrannical Turtle",
                SpriteColumn = 0,
                SpriteRow = 7,
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderRandomly,
                HP = 25,
                Damage = 25,
                Speed = 50,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true
            };

            //Fangy wanderer
            enemies[19] = new EnemyData
            {
                Name = "Fangy",
                SpriteColumn = 0,
                SpriteRow = 3,
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderRandomly,
                HP = 70,
                Damage = 25,
                Speed = 100,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = false
            };

            //Fire floater
            enemies[20] = new EnemyData
            {
                Name = "Furious Flame",
                SpriteColumn = 0,
                SpriteRow = 2,
                HasOneGraphic = true,
                enemyType = EnemyType.Floater,
                wanderType = WanderType.WanderRandomly,
                HP = 50,
                Damage = 25,
                Speed = 100,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseLava = true,
                Chases = true
            };

            //Fangy flailer
            enemies[21] = new EnemyData
            {                
                SpriteColumn = 0,
                SpriteRow = 3,
                enemyType = EnemyType.Flailer,
                wanderType = WanderType.WanderRandomly,
                HP = 110,
                Damage = 35,
                Speed = 100,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseLava = true,
                CanTraverseShallowWater = true,
                Chases = true
            };

            //Fangy charger
            enemies[22] = new EnemyData
            {
                SpriteColumn = 0,
                SpriteRow = 3,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 70,
                Damage = 35,
                Speed = 100,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseLava = true,
                CanTraverseShallowWater = true,
                Chases = true
            };

            //Ranged fangy
            enemies[23] = new EnemyData
            {
                SpriteColumn = 0,
                SpriteRow = 3,
                enemyType = EnemyType.Ranged,
                wanderType = WanderType.WanderRandomly,
                HP = 50,
                Damage = 25,
                Speed = 100,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseLava = true,
                CanTraverseShallowWater = true,
                Chases = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 300,
                    Delay = 2000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 25,
                    RangedType = ProjectileType.Fireball
                }
            };

            //Ranged snowman
            enemies[24] = new EnemyData
            {
                Name = "Sinful Snowman",
                SpriteColumn = 8,
                SpriteRow = 1,
                NumFrames = 2,
                enemyType = EnemyType.Ranged,
                wanderType = WanderType.WanderRandomly,
                HP = 85,
                Damage = 25, 
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseLava = false,
                CanTraverseShallowWater = false,
                Chases = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 300,
                    Delay = 2000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 25,
                    RangedType = ProjectileType.BlueBall
                }
            };

            //Snowfang wanderer
            enemies[25] = new EnemyData
            {
                Name = "Snowfang",
                SpriteColumn = 4,
                SpriteRow = 0,
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderRandomly,
                HP = 85,
                Damage = 50, 
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = false
            };

            //Snowfang charger
            enemies[26] = new EnemyData
            {
                SpriteColumn = 4,
                SpriteRow = 0,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 80,
                Damage = 25, 
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true
            };

            //Snowfang flailer
            enemies[27] = new EnemyData
            {
                SpriteColumn = 4,
                SpriteRow = 0,
                enemyType = EnemyType.Flailer,
                wanderType = WanderType.WanderRandomly,
                HP = 170,
                Damage = 75, 
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = false
            };

            //Skull charger
            enemies[28] = new EnemyData
            {
                Name = "Skullie",
                SpriteColumn = 0,
                SpriteRow = 4,
                NumFrames = 2,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 250,
                Damage = 25,
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseLava = true,
                CanTraverseShallowWater = true,
                Chases = true
            };

            // Skull ranged
            enemies[29] = new EnemyData
            {
                SpriteColumn = 0,
                SpriteRow = 4,
                enemyType = EnemyType.Ranged,
                wanderType = WanderType.WanderRandomly,
                HP = 200,
                Damage = 50 ,
                Radius = 28,
                Speed = 130,
                CanTraverseLand = true,
                CanTraverseLava = true,
                CanTraverseShallowWater = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 300,
                    Delay = 2000,
                    ProjectileSpeed = 300,
                    ProjectileDamage = 25,
                    RangedType = ProjectileType.Fireball
                }
            };

            // Skull flail
            enemies[30] = new EnemyData
            {
                SpriteColumn = 0,
                SpriteRow = 4,
                enemyType = EnemyType.Flailer,
                wanderType = WanderType.WanderRandomly,
                HP = 300,
                Damage = 75,
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseLava = true,
                CanTraverseShallowWater = true,
                Chases = false
            };

            // Clockwise turret, start UP
            enemies[31] = new EnemyData
            {
                Name = "Evil Turret",
                SpriteColumn = 8,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 0, //Clockwise
                Variable2 = 3, //Initial direction
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Clockwise turret, start DOWN
            enemies[32] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 0, //Clockwise
                Variable2 = 0, //Initial direction
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Clockwise turret, start LEFT
            enemies[33] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 0, //Clockwise
                Variable2 = 1, //Initial direction
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Clockwise turret, start RIGHT
            enemies[34] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 0, //Clockwise
                Variable2 = 2, //Initial direction
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-clockwise turret, start UP
            enemies[35] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 6,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 1, //Counter-clockwise
                Variable2 = 3, //Initial direction
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-clockwise turret, start DOWN
            enemies[36] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 6,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 1, //Counter-clockwise
                Variable2 = 0, //Initial direction
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-clockwise turret, start LEFT
            enemies[37] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 6,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 1, //Counter-clockwise
                Variable2 = 1, //Initial direction
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-clockwise turret, start UP
            enemies[38] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 6,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 1, //Counter-clockwise
                Variable2 = 2, //Initial direction
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary turret, start UP
            enemies[39] = new EnemyData
            {
                SpriteColumn = 12,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 2, //Stationary (no rotation)
                Variable2 = 3, //Initial direction
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary turret, start DOWN
            enemies[40] = new EnemyData
            {
                SpriteColumn = 12,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 2, //Stationary (no rotation)
                Variable2 = 0, //Initial direction
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary turret, start LEFT
            enemies[41] = new EnemyData
            {
                SpriteColumn = 12,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 2, //Stationary (no rotation)
                Variable2 = 1, //Initial direction
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary turret, start RIGHT
            enemies[42] = new EnemyData
            {
                SpriteColumn = 12,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 99999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true,
                Variable1 = 2, //Stationary (no rotation)
                Variable2 = 2, //Initial direction
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            //Mushroomlet
            enemies[43] = new EnemyData
            {
                Name = "Mushroomlet",
                SpriteColumn = 4,
                SpriteRow = 2,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 25,
                Damage = 25,
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true,
                CanTraverseMushrooms = true
            };

            //Ghost
            enemies[44] = new EnemyData
            {
                Name = "Choncey",
                SpriteColumn = 12,
                SpriteRow = 6,
                enemyType = EnemyType.Ghost,
                wanderType = WanderType.WanderRandomly,
                HP = 100,
                Damage = 50,
                Radius = 28,
                Speed = 125,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,                
            };

            //Fake turret
            enemies[45] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 6,
                enemyType = EnemyType.Fake,
                wanderType = WanderType.WanderRandomly,
                HP = 100,
                Damage = 50,
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                Chases = true
            };

            // Charging squirrel
            enemies[46] = new EnemyData
            {
                Name="Charging Squirrel",
                SpriteColumn = 0,
                SpriteRow = 8,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 100,
                Damage = 25,
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true
            };

            // Snake
            enemies[47] = new EnemyData
            {
                Name = "Sinister Snake",
                SpriteColumn = 8,
                SpriteRow = 7,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 350,
                Damage = 100,
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseLava = true,
                CanTraverseShallowWater = true,
                Chases = true
            };

            // Ranged mummy
            enemies[48] = new EnemyData
            {
                Name = "Mortifying Mummy",
                SpriteColumn = 4,
                SpriteRow = 7,
                enemyType = EnemyType.Ranged,
                wanderType = WanderType.WanderRandomly,
                HP = 200,
                Damage = 75,
                Radius = 28,
                Speed = 130,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 300,
                    Delay = 4000,
                    ProjectileSpeed = 300,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.Fireball
                }
            };

            // Mummy flailer
            enemies[49] = new EnemyData
            {
                SpriteColumn = 4,
                SpriteRow = 7,
                enemyType = EnemyType.Flailer,
                wanderType = WanderType.WanderRandomly,
                HP = 300,
                Damage = 125,
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = false,
                ImmuneToLightning = true
            };

            // Protector
            enemies[50] = new EnemyData
            {
                SpriteColumn = 14,
                SpriteRow = 4,
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderStandStill,
                HP = 999999,
                Damage = 999999,
                Radius = 28,
                Speed = 1200,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                Chases = true
            };

            // Hopping eye
            enemies[51] = new EnemyData
            {
                Name="Hopping Eye",
                SpriteColumn = 8,
                SpriteRow = 8,
                enemyType = EnemyType.Hopper,
                wanderType = WanderType.WanderRandomly,
                HP = 100,
                Damage = 25,
                Radius = 28,
                Speed = 200,
                CanTraverseLand = true,
                CanTraverseLava = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                Chases = true
            };

            // Clockwise Turret, start UP *FAST SHOOTING*
            enemies[52] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 999999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseSlime = true,
                CanTraverseShallowWater = true,
                IsInvincible = true,
                Variable1 = 0,//Clockwise
                Variable2 = 3,//Starts facing up
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Clockwise Turret, start DOWN *FAST SHOOTING*
            enemies[53] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 999999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseSlime = true,
                CanTraverseShallowWater = true,
                IsInvincible = true,
                Variable1 = 0,//Clockwise
                Variable2 = 0,//Starts facing down
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Clockwise Turret, start LEFT *FAST SHOOTING*
            enemies[54] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 999999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseSlime = true,
                CanTraverseShallowWater = true,
                IsInvincible = true,
                Variable1 = 0,//Clockwise
                Variable2 = 1,//Starts facing left
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Clockwise Turret, start RIGHT *FAST SHOOTING*
            enemies[55] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 999999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseSlime = true,
                CanTraverseShallowWater = true,
                IsInvincible = true,
                Variable1 = 0,//Clockwise
                Variable2 = 2,//Starts facing right
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-Clockwise Turret, start UP *FAST SHOOTING*
            enemies[56] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 999999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseSlime = true,
                CanTraverseShallowWater = true,
                IsInvincible = true,
                Variable1 = 1,//Counter-clockwise
                Variable2 = 3,//Starts facing up
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-Clockwise Turret, start DOWN *FAST SHOOTING*
            enemies[57] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 999999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseSlime = true,
                CanTraverseShallowWater = true,
                IsInvincible = true,
                Variable1 = 1,//Counter-clockwise
                Variable2 = 0,//Starts facing down
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-Clockwise Turret, start LEFT *FAST SHOOTING*
            enemies[58] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 999999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseSlime = true,
                CanTraverseShallowWater = true,
                IsInvincible = true,
                Variable1 = 1,//Counter-clockwise
                Variable2 = 1,//Starts facing left
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-Clockwise Turret, start RIGHT *FAST SHOOTING*
            enemies[59] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 999999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseSlime = true,
                CanTraverseShallowWater = true,
                IsInvincible = true,
                Variable1 = 1,//Counter-clockwise
                Variable2 = 2,//Starts facing right
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary Turret, start UP *FAST SHOOTING*
            enemies[60] = new EnemyData
            {
                SpriteColumn = 12,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 999999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseSlime = true,
                CanTraverseShallowWater = true,
                IsInvincible = true,
                Variable1 = 2,//Stationary (does not rotate)
                Variable2 = 3,//Starts facing up
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary Turret, start DOWN *FAST SHOOTING*
            enemies[61] = new EnemyData
            {
                SpriteColumn = 12,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 999999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseSlime = true,
                CanTraverseShallowWater = true,
                IsInvincible = true,
                Variable1 = 2,//Stationary (does not rotate)
                Variable2 = 0,//Starts facing down
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary Turret, start LEFT *FAST SHOOTING*
            enemies[62] = new EnemyData
            {
                SpriteColumn = 12,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 999999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseSlime = true,
                CanTraverseShallowWater = true,
                IsInvincible = true,
                Variable1 = 2,//Stationary (does not rotate)
                Variable2 = 1,//Starts facing left
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary Turret, start RIGHT *FAST SHOOTING*
            enemies[63] = new EnemyData
            {
                SpriteColumn = 12,
                SpriteRow = 5,
                enemyType = EnemyType.Turret,
                wanderType = WanderType.WanderLeftRight,
                HP = 999999,
                Damage = 0,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseSlime = true,
                CanTraverseShallowWater = true,
                IsInvincible = true,
                Variable1 = 2,//Stationary (does not rotate)
                Variable2 = 2,//Starts facing right
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Mummy, normal
            enemies[64] = new EnemyData
            {
                SpriteColumn = 4,
                SpriteRow = 7,
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderRandomly,
                HP = 175,
                Damage = 50,
                Radius = 28,
                Speed = 130,
                CanTraverseLand = true,
                CanTraverseLava = false,
                CanTraverseShallowWater = true,
                Chases = true,
                ImmuneToLightning = true
            };

            // Orange charger
            enemies[65] = new EnemyData
            {
                Name = "Orange Eye",
                SpriteColumn = 8,
                SpriteRow = 9,
                NumFrames = 1,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 350,
                Damage = 50,
                Radius = 28,
                Speed = 120,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true                
            };

            // Orange ranged
            enemies[66] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 9,
                enemyType = EnemyType.Ranged,
                wanderType = WanderType.WanderRandomly,
                HP = 300,
                Damage = 50,
                Radius = 28,
                Speed = 160,
                CanTraverseLand = true,
                CanTraverseLava = false,
                CanTraverseShallowWater = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 300,
                    Delay = 2000,
                    ProjectileSpeed = 300,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.Fireball
                }
            };

            // Orange flail
            enemies[67] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 9,
                NumFrames = 1,
                enemyType = EnemyType.Flailer,
                wanderType = WanderType.WanderRandomly,
                HP = 500,
                Damage = 75,
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = false                
            };

            // Mummy charger
            enemies[68] = new EnemyData
            {
                SpriteColumn = 4,
                SpriteRow = 7,
                NumFrames = 1,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 175,
                Damage = 50,
                Radius = 28,
                Speed = 130,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = false  
            };

            // Friigoth Moorer charger
            enemies[69] = new EnemyData
            {
                Name = "Friigoth Moorer",
                SpriteColumn = 8,
                SpriteRow = 11,
                NumFrames = 1,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 300,
                Damage =  75,
                Radius = 28,
                Speed = 120,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true                
            };

            // Friigoth Moorer ranged
            enemies[70] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 11,
                enemyType = EnemyType.Ranged,
                wanderType = WanderType.WanderRandomly,
                HP = 260,
                Damage = 25,
                Radius = 28,
                Speed = 300,
                CanTraverseLand = true,
                CanTraverseLava = false,
                CanTraverseShallowWater = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 500,
                    Delay = 1000,
                    ProjectileSpeed = 100,
                    ProjectileDamage = 25,
                    RangedType = ProjectileType.CactusSpike
                }
            };

            // Friigoth Moorer flail
            enemies[71] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 11,
                enemyType = EnemyType.Flailer,
                wanderType = WanderType.WanderRandomly,
                HP = 480,
                Damage = 100,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseLava = false,
                CanTraverseShallowWater = true,
                Chases = false 
            };

            // Friigoth Moorer really really fast
            enemies[72] = new EnemyData
            {
                SpriteColumn = 8,
                SpriteRow = 11,
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderRandomly,
                HP = 80,
                Damage = 25, 
                Radius = 28,
                Speed = 500,
                CanTraverseLand = true,
                CanTraverseLava = false,
                CanTraverseShallowWater = true,
                Chases = false 
            };

            // Floating red candy which spawns ice cream
            enemies[73] = new EnemyData
            {
                Name = "Hoorolo",
                HasOneGraphic = true,
                SpriteColumn = 12,
                SpriteRow = 0,
                enemyType = EnemyType.Spawner,
                wanderType = WanderType.WanderRandomly,
                HP = 260,
                Damage = 25,
                Radius = 28,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseLava = false,
                CanTraverseShallowWater = true,
                Chases = false,
                Variable1 = 75,//Enemy ID to spawn
                Variable2 = 77,//Enemy ID to spawn
                Variable3 = 76//Enemy ID to spawn
            };

            // Floating green candy which shoots at you
            enemies[74] = new EnemyData
            {
                Name = "Haliira",
                SpriteColumn = 13,
                SpriteRow = 0,
                HasOneGraphic = true,
                enemyType = EnemyType.Floater,
                wanderType = WanderType.WanderRandomly,
                HP = 230,
                Damage = 50,
                Radius = 28,
                Speed = 120,
                CanTraverseLand = true,
                CanTraverseLava = false,
                CanTraverseShallowWater = true,
                Chases = false,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 200,
                    Delay = 1000,
                    ProjectileSpeed = 300,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.BlueBall
                }
            };

            // Ice cream cone, normal
            enemies[75] = new EnemyData
            {
                Name = "Iceheart",
                SpriteColumn = 12,
                SpriteRow = 8,
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderRandomly,
                HP = 250,
                Damage = 75,
                Radius = 28,
                Speed = 110,
                CanTraverseLand = true,
                CanTraverseLava = false,
                CanTraverseShallowWater = true,
                Chases = true,
                ImmuneToLightning = true
            };

            // Ice cream cone, charger
            enemies[76] = new EnemyData
            {
                Name = "Coldheart",
                SpriteColumn = 12,
                SpriteRow = 8,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 270,
                Damage = 25,
                Speed = 125,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseLava = false,
                CanTraverseShallowWater = true,
                Chases = true,
                ImmuneToLightning = true
            };

            // Ice cream cone, ranged
            enemies[76] = new EnemyData
            {
                Name = "Evilheart",
                SpriteColumn = 12,
                SpriteRow = 11,
                enemyType = EnemyType.Ranged,
                wanderType = WanderType.WanderRandomly,
                HP = 240,
                Damage = 25,
                Speed = 100,
                Radius = 28,
                CanTraverseLand = true,
                CanTraverseLava = false,
                CanTraverseShallowWater = true,
                Chases = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackData
                {
                    Range = 300,
                    Delay = 2000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.FigureEight,
                    ProjectileHoming = true
                }                
            };

            return enemies;
        }

        #endregion

        #endregion
    }
}
