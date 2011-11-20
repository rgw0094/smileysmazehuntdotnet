using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smiley.Lib.Data
{
    /// <summary>
    /// Encapsulates data about an enemy type that is stored in a resource file.
    /// </summary>
    public class EnemyData
    {
        public string Name { get; set; }
        public int SpriteRow { get; set; }
        public int SpriteColumn { get; set; }
        public int EnemyType { get; set; }
        public int WanderType { get; set; }
        public int HP { get; set; }
        public int Speed { get; set; }
        public int Radius { get; set; }
        public int Damage { get; set; }
        public int RangedType { get; set; }
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
        public RangedAttackData RangedAttack { get; set; }        
    }

    public class RangedAttackData
    {
        public int Range { get; set; }
        public int Delay { get; set; }
        public int ProjectileSpeed { get; set; }
        public double ProjectileDamage { get; set; }
        public double ProjectileHoming { get; set; }
    }
}
