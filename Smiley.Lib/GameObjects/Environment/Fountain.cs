using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Data;
using Smiley.Lib.Framework;
using Smiley.Lib.Util;

namespace Smiley.Lib.GameObjects.Environment
{
    public class Fountain
    {
        public const float FountainHealRadius = 300f;
        private ParticleSystem _particle;
        private float _x;
        private float _y;

        public Fountain(int gridX, int gridY)
        {
            _x = (float)gridX * 64f + 32f;
            _y = (float)gridY * 64f + 32f;
        }

        public bool IsAboveSmiley()
        {
            return _y + 32f > SMH.Player.Y;
        }

        public void Draw()
        {
            if (SmileyUtil.Distance(_x, _y, SMH.Player.X, SMH.Player.Y) > 1000f)
                return;

            //Bottom fountain part and pool
            SMH.Graphics.DrawSprite(Sprites.FountainBottom, SMH.GetScreenX(_x), SMH.GetScreenY(_y));
            Animations.FountainRipple.Draw(_x, _y - 72f);

            //Top fountain part and pool
            SMH.Graphics.DrawSprite(Sprites.FountainTop, SMH.GetScreenX(_x), SMH.GetScreenY(_y - 115f));
            //TODO:
        }

        public void Update(float dt)
        {
            Animations.FountainRipple.Update(dt);
            _particle.Update(dt);

            //Heal the player when they are close
            if (SmileyUtil.Distance(_x, _y, SMH.Player.X, SMH.Player.Y) < Fountain.FountainHealRadius)
            {
                SMH.Player.Heal(0.5f);
            }
        }
    }
}
