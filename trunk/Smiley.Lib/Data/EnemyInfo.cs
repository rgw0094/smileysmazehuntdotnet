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
}
