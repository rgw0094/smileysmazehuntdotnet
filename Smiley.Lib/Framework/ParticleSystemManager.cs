using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Framework
{
    public class ParticleSystemManager
    {
        /// <summary>
        /// Constructs a new particle system manager.
        /// </summary>
        public ParticleSystemManager()
        {
            nPS = 0;
            tX = 0;
            tY = 0;
        }

        public void Update(float dt)
        {
            foreach (ParticleSystem p in _psList)
            {
                p.Update(dt);
            }
        }

        public void Render()
        {
            foreach (ParticleSystem p in _psList)
            {
                p.Render();
            }
        }

        public void SpawnPS(ParticleSystemInfo psi, float x, float y)
        {
            ParticleSystem p = new ParticleSystem(psi);
            p.FireAt(x, y);
            p.Transpose(tX, tY);
            _psList.Add(p);           
        }

        public bool IsPSAlive(ParticleSystem ps)
        {
            return false;
        }

        public void Transpose(float x, float y)
        {
            foreach (ParticleSystem p in _psList)
            {
                p.Transpose(x, y);
            }
        }

        public Vector2 GetTransposition()
        {
            Vector2 dickens;
            dickens.X = 0;
            dickens.Y = 0;
            return dickens;
        }

        public void KillPS(ParticleSystem ps)
        {
            foreach (ParticleSystem p in _psList)
            {
                //IF p = ps then kill it. Need to add the EQUALS operation.
            }
        }

        public void KillAll()
        {
        
        }

        private int nPS;
        private float tX;
        private float tY;
        private List<ParticleSystem> _psList = new List<ParticleSystem>();
    }
}
