using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.GameObjects.Player;
using Smiley.Lib.Framework;

namespace Smiley.Lib.GameObjects.Enemies
{
    public class EnemyManager
    {
        public void TongueCollision(Tongue tongue, float damage)
        {
        }

        public bool CollidesWithFrozenEnemy(CollisionCircle circle)
        {
            return false;
        }

        public void Update(float dt)
        {
        }
    }
}
