using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;

namespace Smiley.Lib.GameObjects
{
    public class ProjectileManager
    {
        public bool IsFrisbeeActive { get; private set; }

        public void AddFrisbee(float x, float y, float speed, float angle, float stunPower)
        {
        }

        public void AddProjectile(float x, float y, float speed, float angle, float damage, bool hostile, bool homing,
            ProjectileType type, bool makesSmileyFlash)
        {
        }

        public void AddProjectile(float x, float y, float speed, float angle, float damage, bool hostile, bool homing,
            ProjectileType type, bool makesSmileyFlash, bool hasParabola, float parabolaLength, float parabolaDuration, float parabolaHeight)
        {
        }
    }
}
