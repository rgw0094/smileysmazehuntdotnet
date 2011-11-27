using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;

namespace Smiley.Lib.GameObjects.Enemies
{
    public abstract class BaseEnemy : GameObject
    {
        public bool CanPass(CollisionTile collision)
        {
            return true;
        }
    }
}
