using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Smiley.Lib.Enums;
using Smiley.Lib.Framework.Drawing;

namespace Smiley.Lib.Framework
{
    //public class ParticleSystem
    //{
    //    /// <summary>
    //    /// Creates a particle system
    //    /// </summary>
    //    /// <param name="fileName"></param>
    //    /// <param name="ParticleSprite"></param>
    //    public ParticleSystem(String fileName, Sprite ParticleSprite)
    //    {
    //        //Will load the contents of the file in fileName into the particle system here
    //    }

    //    /// <summary>
    //    /// Creates a particle system
    //    /// </summary>
    //    /// <param name="ParticleSystemInfo"></param>
    //    public ParticleSystem(ParticleSystemInfo psi)
    //    { 
    //        //Will load the contents of the psi into a new particle system
    //    }

    //    /// <summary>
    //    /// Renders the particle system
    //    /// </summary>
    //    public void Render()
    //    { 
    //        //Render code goes here
    //    }

    //    /// <summary>
    //    /// Fires the particle system at x, y
    //    /// </summary>
    //    /// <param name="x"></param>
    //    /// <param name="y"></param>
    //    public void FireAt(float x, float y)
    //    {
    //        //FireAt code goes here
    //    }

    //    /// <summary>
    //    /// Fires the particle system
    //    /// </summary>
    //    public void Fire()
    //    {
    //        //Fire code goes here
    //    }

    //    /// <summary>
    //    /// Stops the particle system. Can also immediately terminate all of the child particles at that time.
    //    /// </summary>
    //    /// <param name="bKillParticles"></param>
    //    public void Stop(bool bKillParticles = false)
    //    {
    //        //Stop code goes here
    //    }

    //    /// <summary>
    //    /// Given dt, updates the particle system
    //    /// </summary>
    //    /// <param name="deltaTime"></param>
    //    public void Update(float deltaTime)
    //    {
    //        //Update code goes here
    //    }

    //    /// <summary>
    //    /// Moves the particle system, with the option of moving the child particles with the system
    //    /// </summary>
    //    /// <param name="x"></param>
    //    /// <param name="y"></param>
    //    /// <param name="bMoveParticles"></param>
    //    public void MoveTo(float x, float y, bool bMoveParticles = false)
    //    {
    //        //MoveTo code goes here
    //    }

    //    /// <summary>
    //    /// Transposes the particle system
    //    /// </summary>
    //    /// <param name="x"></param>
    //    /// <param name="y"></param>
    //    public void Transpose(float x, float y)
    //    {
    //        fTx = x;
    //        fTy = y;
    //    }

    //    /// <summary>
    //    /// Sets the scale for the particle system.
    //    /// </summary>
    //    /// <param name="scale"></param>
    //    public void SetScale(float scale)
    //    {
    //        fScale = scale;
    //    }

    //    /// <summary>
    //    /// Sets whether to track the bounding box or not ???
    //    /// </summary>
    //    /// <param name="bTrack"></param>
    //    public void TrackBoundingBox(bool bTrack)
    //    {
    //        bUpdateBoundingBox = bTrack;
    //    }

    //    /// <summary>
    //    /// Creates a particle system.
    //    /// </summary>
    //    private ParticleSystem()
    //    {
        
    //    }

    //    /// <summary>
    //    /// The age of the particle system.
    //    /// </summary>
    //    private float fAge
    //    {
    //        get;
    //        private set;
    //    }

    //    /// <summary>
    //    /// I'm not sure what this one does yet.
    //    /// </summary>
    //    private float fEmmisionResidue { private get; private set; }

    //    /// <summary>
    //    /// Location of the particle system in the previous frame.
    //    /// </summary>
    //    private Vector2 vecPrevLocation
    //    {
    //        private get;
    //        private set;
    //    }

    //    /// <summary>
    //    /// Current location of the particle system.
    //    /// </summary>
    //    private Vector2 vecLocation
    //    {
    //        get;
    //        private set;
    //    }

    //    /// <summary>
    //    /// X transposition of the particle system
    //    /// </summary>
    //    private float fTx
    //    {
    //         get;
    //        private set;
    //    }

    //    /// <summary>
    //    /// Y transposition of the particle system
    //    /// </summary>
    //    private float fTy
    //    {
    //        get;
    //        private set;
    //    }

    //    private float fScale
    //    {
    //        get;
    //        private set;
    //    }

    //    private int nParticlesAlive { get; private set; }
    //    private Rectangle rectBoundingBox { get; private set; }
    //    private bool bUpdateBoundingBox { private get; private set; }

    //    //private fixed SingleParticle particles[(int)ParticleEnums.MaxParticles];
    //}

    //public class SingleParticle
    //{
    //    public Vector2 vecLocation;
    //    public Vector2 vecVelocity;

    //    public float fGravity;
    //    public float fRadialAccel;
    //    public float fTangentialAccel;

    //    public float fSpin;
    //    public float fSpinDelta;

    //    public float fSize;
    //    public float fSizeDelta;

    //    public Color colColor; // + alpha
    //    public Color colColorDelta;

    //    public float fAge;
    //    public float fTerminalAge;

    //}

    //public class ParticleSystemInfo
    //{
    //    public Sprite particleSprite; //texture + blend mode, hopefully the XNA sprites have blend modes
    //    public int nEmission; //particles per sec
    //    public float fLifetime;

    //    public float fDirection;
    //    public float fSpread;
    //    public bool bRelative;

    //    public float fSpeedMin;
    //    public float fSpeedMax;

    //    public float fGravityMin;
    //    public float fGravityMax;

    //    public float fRadialAccelMin;
    //    public float fRadialAccelMax;

    //    public float fTangentialAccelMin;
    //    public float fTangentialAccelMax;

    //    public float fSizeStart;
    //    public float fSizeEnd;
    //    public float fSizeVar;

    //    public Color colColorStart; // + alpha
    //    public Color colColorEnd;
    //    public float fColorVar;
    //    public float fAlphaVar;
    //}
}
