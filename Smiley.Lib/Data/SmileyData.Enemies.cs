using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;

namespace Smiley.Lib.Data
{
    /// <summary>
    /// Encapsulates data about an enemy type that is stored in a resource file.
    /// </summary>
    public class EnemyInfo
    {
        public string Name { get; set; }
        public int SpriteRow { get; set; }
        public int SpriteColumn { get; set; }
        public EnemyType enemyType { get; set; }
        public WanderType wanderType { get; set; }
        public int HP { get; set; }
        public int Speed { get; set; }
        public int Radius { get; set; }
        public int Damage { get; set; }
        public bool CanTraverseLand { get; set; }
        public bool CanTraverseShallowWater { get; set; }
        public bool CanTraverseDeepWater { get; set; }
        public bool CanTraverseSlime { get; set; }
        public bool CanTraverseLava { get; set; }
        public bool CanTraverseMushrooms { get; set; }
        public bool ImmuneToFire { get; set; }
        public bool ImmuneToTongue { get; set; }
        public bool ImmuneToLightning { get; set; }
        public bool ImmuneToStun { get; set; }
        public bool ImmuneToFreeze { get; set; }
        public bool IsInvincible { get; set; }
        public int Variable1 { get; set; }
        public int Variable2 { get; set; }
        public int Variable3 { get; set; }
        public int NumFrames { get; set; }
        public bool HasOneGraphic { get; set; }
        public bool Chases { get; set; }
        public bool HasRangedAttack { get; set; }
        public RangedAttackInfo RangedAttack { get; set; }
    }

    public class RangedAttackInfo
    {
        public int Range { get; set; }
        public int Delay { get; set; }
        public int ProjectileSpeed { get; set; }
        public double ProjectileDamage { get; set; }
        public bool ProjectileHoming { get; set; }
        public ProjectileType RangedType { get; set; }
    }
    
    public partial class SmileyData
    {
        public Dictionary<int, EnemyInfo> Enemies
        {
            get;
            private set;
        }

        private void CreateEnemies()
        {
            Enemies = new Dictionary<int, EnemyInfo>();

            Enemies[0] = new EnemyInfo
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

            Enemies[1] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 2000,
                    ProjectileDamage = 50,
                    ProjectileSpeed = 400,
                    RangedType = ProjectileType.BlueBall
                }
            };

            Enemies[2] = new EnemyInfo
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

            Enemies[3] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 2000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 25,
                    RangedType = ProjectileType.Acorn
                }
            };

            Enemies[4] = new EnemyInfo
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

            Enemies[5] = new EnemyInfo
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

            Enemies[6] = new EnemyInfo
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

            Enemies[7] = new EnemyInfo
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

            Enemies[8] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 450,
                    Delay = 2000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 150,
                    RangedType = ProjectileType.Acorn
                }
            };

            Enemies[9] = new EnemyInfo
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

            Enemies[10] = new EnemyInfo
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

            Enemies[11] = new EnemyInfo
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

            Enemies[12] = new EnemyInfo
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
            Enemies[13] = new EnemyInfo
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
            Enemies[14] = new EnemyInfo
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
            Enemies[15] = new EnemyInfo
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
                ImmuneToStun = true
            };

            //Small green tentacle
            Enemies[16] = new EnemyInfo
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
            Enemies[17] = new EnemyInfo
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
            Enemies[18] = new EnemyInfo
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
            Enemies[19] = new EnemyInfo
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
            Enemies[20] = new EnemyInfo
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
            Enemies[21] = new EnemyInfo
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
            Enemies[22] = new EnemyInfo
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
            Enemies[23] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 300,
                    Delay = 2000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 25,
                    RangedType = ProjectileType.Fireball
                }
            };

            //Ranged snowman
            Enemies[24] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 300,
                    Delay = 2000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 25,
                    RangedType = ProjectileType.BlueBall
                }
            };

            //Snowfang wanderer
            Enemies[25] = new EnemyInfo
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
            Enemies[26] = new EnemyInfo
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
            Enemies[27] = new EnemyInfo
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
            Enemies[28] = new EnemyInfo
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
            Enemies[29] = new EnemyInfo
            {
                SpriteColumn = 0,
                SpriteRow = 4,
                enemyType = EnemyType.Ranged,
                wanderType = WanderType.WanderRandomly,
                HP = 200,
                Damage = 50,
                Radius = 28,
                Speed = 130,
                CanTraverseLand = true,
                CanTraverseLava = true,
                CanTraverseShallowWater = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackInfo
                {
                    Range = 300,
                    Delay = 2000,
                    ProjectileSpeed = 300,
                    ProjectileDamage = 25,
                    RangedType = ProjectileType.Fireball
                }
            };

            // Skull flail
            Enemies[30] = new EnemyInfo
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
            Enemies[31] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Clockwise turret, start DOWN
            Enemies[32] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Clockwise turret, start LEFT
            Enemies[33] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Clockwise turret, start RIGHT
            Enemies[34] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-clockwise turret, start UP
            Enemies[35] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-clockwise turret, start DOWN
            Enemies[36] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-clockwise turret, start LEFT
            Enemies[37] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-clockwise turret, start UP
            Enemies[38] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary turret, start UP
            Enemies[39] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary turret, start DOWN
            Enemies[40] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary turret, start LEFT
            Enemies[41] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary turret, start RIGHT
            Enemies[42] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            //Mushroomlet
            Enemies[43] = new EnemyInfo
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
            Enemies[44] = new EnemyInfo
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
            Enemies[45] = new EnemyInfo
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
            Enemies[46] = new EnemyInfo
            {
                Name = "Charging Squirrel",
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
            Enemies[47] = new EnemyInfo
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
            Enemies[48] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 300,
                    Delay = 4000,
                    ProjectileSpeed = 300,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.Fireball
                }
            };

            // Mummy flailer
            Enemies[49] = new EnemyInfo
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
            Enemies[50] = new EnemyInfo
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
            Enemies[51] = new EnemyInfo
            {
                Name = "Hopping Eye",
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
            Enemies[52] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Clockwise Turret, start DOWN *FAST SHOOTING*
            Enemies[53] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Clockwise Turret, start LEFT *FAST SHOOTING*
            Enemies[54] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Clockwise Turret, start RIGHT *FAST SHOOTING*
            Enemies[55] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-Clockwise Turret, start UP *FAST SHOOTING*
            Enemies[56] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-Clockwise Turret, start DOWN *FAST SHOOTING*
            Enemies[57] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-Clockwise Turret, start LEFT *FAST SHOOTING*
            Enemies[58] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Counter-Clockwise Turret, start RIGHT *FAST SHOOTING*
            Enemies[59] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary Turret, start UP *FAST SHOOTING*
            Enemies[60] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary Turret, start DOWN *FAST SHOOTING*
            Enemies[61] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary Turret, start LEFT *FAST SHOOTING*
            Enemies[62] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Stationary Turret, start RIGHT *FAST SHOOTING*
            Enemies[63] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 400,
                    Delay = 1000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.TurretCannonball
                }
            };

            // Mummy, normal
            Enemies[64] = new EnemyInfo
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
            Enemies[65] = new EnemyInfo
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
            Enemies[66] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 300,
                    Delay = 2000,
                    ProjectileSpeed = 300,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.Fireball
                }
            };

            // Orange flail
            Enemies[67] = new EnemyInfo
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
            Enemies[68] = new EnemyInfo
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
            Enemies[69] = new EnemyInfo
            {
                Name = "Friigoth Moorer",
                SpriteColumn = 8,
                SpriteRow = 11,
                NumFrames = 1,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 300,
                Damage = 75,
                Radius = 28,
                Speed = 120,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true
            };

            // Friigoth Moorer ranged
            Enemies[70] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 500,
                    Delay = 1000,
                    ProjectileSpeed = 100,
                    ProjectileDamage = 25,
                    RangedType = ProjectileType.CactusSpike
                }
            };

            // Friigoth Moorer flail
            Enemies[71] = new EnemyInfo
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
            Enemies[72] = new EnemyInfo
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
            Enemies[73] = new EnemyInfo
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
            Enemies[74] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 200,
                    Delay = 1000,
                    ProjectileSpeed = 300,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.BlueBall
                }
            };

            // Ice cream cone, normal
            Enemies[75] = new EnemyInfo
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
            Enemies[76] = new EnemyInfo
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
            Enemies[76] = new EnemyInfo
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
                RangedAttack = new RangedAttackInfo
                {
                    Range = 300,
                    Delay = 2000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.FigureEight,
                    ProjectileHoming = true
                }
            };

            // Pumpkin, charger
            Enemies[78] = new EnemyInfo
            {
                Name = "Mr. Pumpking",
                SpriteColumn = 0,
                SpriteRow = 12,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 230,
                Damage = 125,
                Radius = 28,
                Speed = 125,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true
            };

            // Pumpkin, ranged, moves horizontal
            Enemies[79] = new EnemyInfo
            {
                SpriteColumn = 0,
                SpriteRow = 12,
                enemyType = EnemyType.Ranged,
                wanderType = WanderType.WanderLeftRight,
                HP = 200,
                Damage = 100,
                Radius = 28,
                Speed = 130,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackInfo
                {
                    Range = 320,
                    Delay = 1000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.Fireball,
                    ProjectileHoming = false
                }
            };

            // Pumpkin, ranged, moves vertical
            Enemies[80] = new EnemyInfo
            {
                SpriteColumn = 0,
                SpriteRow = 12,
                enemyType = EnemyType.Ranged,
                wanderType = WanderType.WanderUpDown,
                HP = 200,
                Damage = 100,
                Radius = 28,
                Speed = 130,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackInfo
                {
                    Range = 320,
                    Delay = 1000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.Fireball,
                    ProjectileHoming = false
                }
            };

            // Pumpkin, hopping
            Enemies[81] = new EnemyInfo
            {
                SpriteColumn = 0,
                SpriteRow = 12,
                enemyType = EnemyType.Hopper,
                wanderType = WanderType.WanderRandomly,
                HP = 180,
                Damage = 50,
                Radius = 28,
                Speed = 200,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                Chases = true
            };

            // Pumpkin, flail
            Enemies[82] = new EnemyInfo
            {
                SpriteColumn = 0,
                SpriteRow = 12,
                enemyType = EnemyType.Flailer,
                wanderType = WanderType.WanderRandomly,
                HP = 450,
                Damage = 125,
                Radius = 28,
                Speed = 125,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                CanTraverseDeepWater = true,
                Chases = true
            };

            // Brown desert burrower
            Enemies[83] = new EnemyInfo
            {
                Name = "Rockhorn",
                SpriteColumn = 4,
                SpriteRow = 12,
                enemyType = EnemyType.Burrower,
                wanderType = WanderType.WanderStandStill,
                HP = 75,
                Damage = 50,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Variable1 = 1,//Burrowing animation number
                Variable2 = 250,//Distance at which it burrows
                HasRangedAttack = true,
                RangedAttack = new RangedAttackInfo
                {
                    Range = 525,//Range in old Smiley was 600
                    Delay = 3000,
                    ProjectileSpeed = 350,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.Orange,
                    ProjectileHoming = false
                }
            };

            // Alphanoid
            Enemies[84] = new EnemyInfo
            {
                Name = "Alphanoid",
                SpriteColumn = 0,
                SpriteRow = 14,
                enemyType = EnemyType.Botonoid,
                wanderType = WanderType.WanderRandomly,
                HP = 120,
                Damage = 25,
                Radius = 26,
                Speed = 150,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                Chases = false,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackInfo
                {
                    Range = 700,
                    Delay = 6000,
                    ProjectileSpeed = 500,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.Boomerang,
                    ProjectileHoming = false
                }
            };

            // Barvinoid
            Enemies[85] = new EnemyInfo
            {
                Name = "Barvinoid",
                SpriteColumn = 4,
                SpriteRow = 14,
                enemyType = EnemyType.Botonoid,
                wanderType = WanderType.WanderRandomly,
                HP = 160,
                Damage = 25,
                Radius = 23,
                Speed = 100,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = false,
                ImmuneToLightning = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackInfo
                {
                    Range = 700,
                    Delay = 6000,
                    ProjectileSpeed = 500,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.Boomerang,
                    ProjectileHoming = false
                }
            };

            // Herbanoid
            Enemies[86] = new EnemyInfo
            {
                Name = "Herbanoid",
                SpriteColumn = 8,
                SpriteRow = 14,
                enemyType = EnemyType.Botonoid,
                wanderType = WanderType.WanderRandomly,
                HP = 100,
                Damage = 25,
                Radius = 21,
                Speed = 180,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = false,
                ImmuneToLightning = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackInfo
                {
                    Range = 700,
                    Delay = 6000,
                    ProjectileSpeed = 500,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.Boomerang,
                    ProjectileHoming = false
                }
            };

            // Blue alien guy
            Enemies[87] = new EnemyInfo
            {
                Name = "Blue Alien Guy",
                SpriteColumn = 8,
                SpriteRow = 13,
                NumFrames = 2,
                enemyType = EnemyType.Hopper,
                wanderType = WanderType.WanderRandomly,
                HP = 50,
                Damage = 25,
                Radius = 28,
                Speed = 150,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true
            };

            // Green snake
            Enemies[88] = new EnemyInfo
            {
                Name = "Origsin",
                SpriteColumn = 8,
                SpriteRow = 2,
                NumFrames = 2,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 80,
                Damage = 50,
                Radius = 28,
                Speed = 125,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = true
            };

            // Beaker
            Enemies[89] = new EnemyInfo
            {
                Name = "Beaker",
                SpriteColumn = 0,
                SpriteRow = 13,
                NumFrames = 2,
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderRandomly,
                HP = 210,
                Damage = 50,
                Radius = 28,
                Speed = 80,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = false
            };

            // Cone
            Enemies[90] = new EnemyInfo
            {
                Name = "Cone",
                SpriteColumn = 8,
                SpriteRow = 12,
                NumFrames = 2,
                enemyType = EnemyType.DiagoShooter,
                wanderType = WanderType.WanderRandomly,
                HP = 130,
                Damage = 50,
                Radius = 28,
                Speed = 120,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = false,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackInfo
                {
                    Range = 500,
                    Delay = 1500,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 25,
                    RangedType = ProjectileType.Orange,
                    ProjectileHoming = false
                }
            };

            // Fenwar eye spider
            Enemies[91] = new EnemyInfo
            {
                Name = "Fen-Eye",
                SpriteColumn = 8,
                SpriteRow = 3,
                NumFrames = 2,
                enemyType = EnemyType.FenwarEyeSpider,
                wanderType = WanderType.WanderRandomly,
                HP = 250,
                Damage = 100,
                Radius = 28,
                Speed = 220,
                CanTraverseLand = true,
                CanTraverseShallowWater = false,
                Chases = false,
                Variable1 = 28,
                Variable2 = 45,
                ImmuneToTongue = false
            };

            // Botonoid spawner 1
            Enemies[92] = new EnemyInfo
            {
                Name = "Botonoid Spawner",
                SpriteColumn = 12,
                SpriteRow = 14,
                HasOneGraphic = true,
                enemyType = EnemyType.Spawner,
                wanderType = WanderType.WanderRandomly,
                HP = 300,
                Damage = 50,
                Radius = 28,
                Speed = 50,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseLava = true,
                Chases = false,
                Variable1 = 84,//These variables define which enemy
                Variable2 = 85,//types spawn, in order of decreasing
                Variable3 = 86,//frequency
                ImmuneToLightning = true
            };

            // Botonoid spawner 2
            Enemies[93] = new EnemyInfo
            {
                SpriteColumn = 12,
                SpriteRow = 14,
                HasOneGraphic = true,
                enemyType = EnemyType.Spawner,
                wanderType = WanderType.WanderRandomly,
                HP = 300,
                Damage = 50,
                Radius = 28,
                Speed = 50,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseLava = true,
                Chases = false,
                Variable1 = 85,//These variables define which enemy
                Variable2 = 86,//types spawn, in order of decreasing
                Variable3 = 84,//frequency
                ImmuneToLightning = true
            };

            // Botonoid spawner 3
            Enemies[94] = new EnemyInfo
            {
                SpriteColumn = 12,
                SpriteRow = 14,
                HasOneGraphic = true,
                enemyType = EnemyType.Spawner,
                wanderType = WanderType.WanderRandomly,
                HP = 300,
                Damage = 50,
                Radius = 28,
                Speed = 50,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseLava = true,
                Chases = false,
                Variable1 = 86,//These variables define which enemy
                Variable2 = 84,//types spawn, in order of decreasing
                Variable3 = 85,//frequency
                ImmuneToLightning = true
            };

            // Spider
            Enemies[95] = new EnemyInfo
            {
                Name = "Lyster",
                SpriteColumn = 0,
                SpriteRow = 9,
                NumFrames = 2,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 150,
                Damage = 100,
                Radius = 25,
                Speed = 225,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseLava = true,
                Chases = true,
            };

            // Blimp (spawns spiders)
            Enemies[96] = new EnemyInfo
            {
                Name = "Blimp",
                SpriteColumn = 14,
                SpriteRow = 0,
                NumFrames = 2,
                HasOneGraphic = true,
                enemyType = EnemyType.Spawner,
                wanderType = WanderType.WanderRandomly,
                HP = 300,
                Damage = 50,
                Radius = 28,
                Speed = 50,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseLava = true,
                Chases = false,
                Variable1 = 95,//Spawns mostly spiders, with the
                Variable2 = 95,//occasional green croc
                Variable3 = 97,
                ImmuneToFreeze = true,
                ImmuneToLightning = true
            };

            // Green croc
            Enemies[97] = new EnemyInfo
            {
                Name = "K. Greene",
                SpriteColumn = 8,
                SpriteRow = 10,
                enemyType = EnemyType.Charger,
                wanderType = WanderType.WanderRandomly,
                HP = 250,
                Damage = 75,
                Radius = 27,
                Speed = 130,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseLava = true,
                Chases = true,
                ImmuneToFreeze = true
            };

            // Ent
            Enemies[98] = new EnemyInfo
            {
                Name = "Ent",
                SpriteColumn = 15,
                SpriteRow = 0,
                HasOneGraphic = true,
                enemyType = EnemyType.AdjacentShooter,
                wanderType = WanderType.WanderStandStill,
                HP = 900,
                Damage = 100,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseLava = true,
                Chases = false,
                IsInvincible = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackInfo
                {
                    Range = 700,
                    Delay = 500,
                    ProjectileSpeed = 450,
                    ProjectileDamage = 75,
                    RangedType = ProjectileType.Orange,
                    ProjectileHoming = false
                }
            };

            //Super beaker            
            Enemies[99] = new EnemyInfo
            {
                Name = "Super Beaker",
                SpriteColumn = 0,
                SpriteRow = 15,
                NumFrames = 2,
                enemyType = EnemyType.BasicEnemy,
                wanderType = WanderType.WanderRandomly,
                HP = 550,
                Damage = 25,
                Radius = 28,
                Speed = 80,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                Chases = false,
                ImmuneToFreeze = true
            };

            // Zugzug the goblin
            Enemies[100] = new EnemyInfo
            {
                Name = "Zugzug",
                SpriteColumn = 8,
                SpriteRow = 15,
                NumFrames = 2,
                enemyType = EnemyType.ClownBalloon,
                wanderType = WanderType.WanderRandomly,
                HP = 220,
                Damage = 50,
                Radius = 28,
                Speed = 150,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true
            };

            // Proennok
            Enemies[101] = new EnemyInfo
            {
                Name = "Proennok",
                SpriteColumn = 12,
                SpriteRow = 10,
                enemyType = EnemyType.Ranged,
                wanderType = WanderType.WanderRandomly,
                HP = 290,
                Damage = 50,
                Radius = 28,
                Speed = 80,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseLava = true,
                Chases = false,
                ImmuneToFreeze = true,
                HasRangedAttack = true,
                RangedAttack = new RangedAttackInfo
                {
                    Range = 300,
                    Delay = 2000,
                    ProjectileSpeed = 400,
                    ProjectileDamage = 50,
                    RangedType = ProjectileType.Skull,
                    ProjectileHoming = true
                }
            };

            // Batlet Distributor 2
            Enemies[102] = new EnemyInfo
            {
                SpriteColumn = 0,
                SpriteRow = 10,
                NumFrames = 2,
                enemyType = EnemyType.BatletDistributor,
                wanderType = WanderType.WanderStandStill,
                HP = 100,
                Damage = 50,
                Radius = 28,
                Speed = 0,
                CanTraverseLand = true,
                CanTraverseShallowWater = true,
                CanTraverseSlime = true,
                IsInvincible = true
            };
        }
    }
}
