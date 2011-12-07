using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Data;
using Smiley.Lib.Framework;
using Smiley.Lib.Util;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.GameObjects.Environment
{
    public class Fountain : GameObject
    {
        public const float FountainHealRadius = 300f;
        private ParticleSystem _particle;

        public Fountain(int gridX, int gridY)
        {
            X = (float)gridX * 64f + 32f;
            Y = (float)gridY * 64f + 32f;
        }

        public bool IsAboveSmiley()
        {
            return Y + 32f > SMH.Player.Y;
        }

        public override void Draw()
        {
            if (SmileyUtil.Distance(X, Y, SMH.Player.X, SMH.Player.Y) > 1000f)
                return;

            //Bottom fountain part and pool
            SMH.Graphics.DrawSprite(Sprites.FountainBottom, ScreenX, ScreenY);
            SMH.Graphics.DrawAnimation(Animations.FountainRipple, ScreenX, ScreenY - 72f);

            //Top fountain part and pool
            SMH.Graphics.DrawSprite(Sprites.FountainTop, ScreenX, ScreenY - 115f);
            SMH.Graphics.DrawAnimation(Animations.FountainRipple, ScreenX, ScreenY - 215f, Color.White, 0f, 0.35f, 0.4f);
            
            //TODO:
            //Fountain particle
            //smh->resources->GetParticleSystem("fountain")->MoveTo(smh->getScreenX(x), smh->getScreenY(y - 220.0), true);
            //smh->resources->GetParticleSystem("fountain")->Render();
        }

        public override void Update(float dt)
        {
            Animations.FountainRipple.Update(dt);
            //_particle.Update(dt);TODO

            //Heal the player when they are close
            if (SmileyUtil.Distance(X, Y, SMH.Player.X, SMH.Player.Y) < Fountain.FountainHealRadius)
            {
                SMH.Player.Heal(0.5f);
            }
        }
    }
}
