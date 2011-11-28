using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework;
using Smiley.Lib.Enums;

namespace Smiley.Lib.GameObjects.Player
{
    public class Player : GameObject
    {
        #region Properties

        public bool IsShrunk
        {
            get;
            private set;
        }

        public bool IsOnIce
        {
            get;
            private set;
        }

        public Direction Facing
        {
            get;
            private set;
        }

        public float Radius
        {
            get;
            private set;
        }

        public CollisionCircle CollisionCircle
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        public override void Update(float dt)
        {
        }

        public override void Draw()
        {
        }

        public bool CanPass(CollisionTile collision)
        {
            return CanPass(collision, true);
        }

        public bool CanPass(CollisionTile collision, bool applyCurrentAbilities)
        {
            return true;
        }

        public void Heal(float amount)
        {
        }

        #endregion
    }
}
