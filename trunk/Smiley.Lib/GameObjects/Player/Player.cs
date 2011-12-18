using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;
using Smiley.Lib.GameObjects.Environment;
using Smiley.Lib.Data;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Util;

namespace Smiley.Lib.GameObjects.Player
{
    public class Player : GameObject
    {
        #region Private Variables

        private bool _isJesusSoundPlaying;
        private int _enteredWaterX;
        private int _enteredWaterY;
        private int _enteredSpringX;
        private int _enteredSpringY;
        private Tile _lastTile;
        private Tile _lastNonIceTile;
        private Tile _startedFallingTile;
        private float _baseX;
        private float _baseY;
        private bool _usingManaItem;
        private float _fallingDx;
        private float _fallingDy;
        private float _frisbeePower;
        private float _graduallyMoveTargetX;
        private float _graduallyMoveTargetY;
        private bool _needToIceHop;
        private float _springVelocity;
        private float _springOffset;
        private bool _isShrinkActive;
        private float _timeStartedIceHop;
        private float _iceHopOffset;
        private float _startedFlashing;
        private float _startedKnockBack;
        private float _startedDrowning;
        private float _timeStartedCane;
        private float _startedSpringing;
        private float _startedIceBreath;
        private float _startedSliding;
        private float _startedFalling;
        private float _lastLavaDamage;			//Last time the player took damage from lava
        private float _startedWaterWalk;
        private float _lastOrb;
        private float _timeToSlide;
        private float _springTime;				//How long to be in the air after touching a spring pad
        private float _timeEnteredShrinkTunnel; //Time smiley entered the shrink tunnel
        private float _timeInShrinkTunnel;		//Time to take to go through the shrink tunnel
        private float _timeStartedHovering;
        private float _timeFrozen;
        private float _freezeDuration;
        private float _stunDuration;
        private float _timeStartedStun;
        private float _timeStartedHeal;
        private float _timeStartedImmobilize;
        private float _immobilizeDuration;
        private float _timeSlimed;
        private float _slimeDuration;
        private float _timeStartedGraduallyMoving;
        private float _timeToGraduallyMove;
        private float _timeStoppedBreathingFire;
        private float _timeLastUsedMana;
        private float _health;
        private float _mana;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new Player.
        /// </summary>
        public Player()
        {
            Facing = Direction.Down;
            Tongue = new Tongue();
            Radius = Constants.DefaultSmileyRadius;

            Scale = 1f;
            HoverScale = 1f;
            ShrinkScale = 1f;

            //worm = new Worm(0, 0);

            ////Load Particles
            //fireBreathParticle = new WeaponParticleSystem("firebreath.psi", smh->resources->GetSprite("particleGraphic1"), PARTICLE_FIRE_BREATH);
            //iceBreathParticle = new WeaponParticleSystem("icebreath.psi", smh->resources->GetSprite("particleGraphic4"), PARTICLE_ICE_BREATH);

            //smh->resources->GetSprite("iceBlock")->SetColor(ARGB(200, 255, 255, 255));
            //smh->resources->GetSprite("reflectionShield")->SetColor(ARGB(100, 255, 255, 255));
            //smh->resources->GetSprite("playerShadow")->SetColor(ARGB(75, 255, 255, 255));
        }

        #endregion

        #region Properties

        public Tongue Tongue { get; private set; }
        public bool IsBreathingFire { get; private set; }
        public bool IsBreathingIce { get; private set; }
        public bool IsOnWarp { get; private set; }
        public bool IsFlashing { get; private set; }
        public bool IsKnockback { get; private set; }
        public bool IsSliding { get; private set; }
        public bool IsIceSliding { get; private set; }
        public bool IsSpringing { get; private set; }
        public bool IsReflectionShieldActive { get; private set; }
        public bool IsFalling { get; private set; }
        public bool IsInLava { get; private set; }
        public bool IsInShallowWater { get; private set; }
        public bool IsWaterWalking { get; private set; }
        public bool IsOnWater { get; private set; }
        public bool IsDrowning { get; private set; }
        public bool IsSprinting { get; private set; }
        public bool IsHovering { get; private set; }
        public bool IsCloaked { get; private set; }
        public bool IsUsingCane { get; private set; }
        public bool IsInShrinkTunnel { get; private set; }
        public bool IsFrozen { get; private set; }
        public bool IsStunned { get; private set; }
        public bool IsHealing { get; private set; }
        public bool IsImmobile { get; private set; }
        public bool IsSlimed { get; private set; }
        public bool IsChargingFrisbee { get; private set; }
        public bool IsInvincible { get; set; }
        public bool IsGraduallyMoving { get; private set; }
        public bool IsUber { get; set; }
        public float HoveringYOffset { get; private set; }
        public bool IsTongueLocked { get; set; }
        public bool DontUpdate { get; private set; }
        public float SpringOffset { get; private set; }
        public Direction Facing { get; private set; }
        public CollisionCircle CollisionCircle { get; private set; }
        public float ShrinkScale { get; private set; }
        public float HoverScale { get; private set; }
        public float Scale { get; private set; }
        public float Rotation { get; private set; }
        public float Radius { get; private set; }

        /// <summary>
        /// Gets the tile that smiley's feet are on, as opposed to <see cref="Tile"/> which is the tile that
        /// Smiley's center is on.
        /// </summary>
        public Tile FeetTile { get; private set; }

        public float Health
        {
            get { return _health; }
            set { _health = Math.Min(value, MaxHealth); }
        }

        public float MaxHealth
        {
            get { return 5f + SMH.SaveManager.CurrentSave.NumUpgrades[Upgrade.Health]; }
        }

        public float Mana
        {
            get { return _mana; }
            set { _mana = Math.Min(value, MaxMana); }
        }

        public float MaxMana
        {
            get { return Constants.InitialMana + SMH.SaveManager.CurrentSave.ManaModifier; }
        }

        public bool IsShrunk
        {
            get { return _isShrinkActive && ShrinkScale == 0.5f; }
        }

        public float Damage
        {
            get { return (IsUber ? 1f : 0.25f) * SMH.SaveManager.CurrentSave.DamageModifer; }
        }

        public float FireBreathDamage
        {
            get { return (IsUber ? 4f : 1f) * SMH.SaveManager.CurrentSave.DamageModifer; }
        }

        public float LightningOrbDamage
        {
            get { return (IsUber ? 0.6f : 0.15f) * SMH.SaveManager.CurrentSave.DamageModifer; }
        }

        #endregion

        #region Public Methods

        public override void Update(float dt)
        {
            if (DontUpdate)
            {
                DX = DY = 0f;
                return;
            }

            //Movement stuff
            SetFacingDirection();
            DoFalling(dt);
            DoIce(dt);
            DoSprings(dt);
            DoArrowPads(dt);
            DoShrinkTunnels(dt);
            UpdateVelocities(dt);
            DoMove(dt);

            //Update location stuff now that the player's movement for this frame has been completed
            UpdateLocation();

            //Check to see if an ICE GLITCH has occurred
            //checkForIceGlitch();

            ////Do level exits
            if (Tile.Collision == CollisionTile.PLAYER_END)
            {
                SMH.AreaChanger.ChangeArea(0, 0, (Level)Tile.ID);
                return;
            }

            //Explore!
            SMH.SaveManager.CurrentSave.Explore(Tile.X, Tile.Y);

            //Update Smiley's collisionCircle
            CollisionCircle.X = X;
            CollisionCircle.Y = Y;
            CollisionCircle.Radius = (Constants.PlayerWidth / 2f - 3f) * ShrinkScale;

            //Update timed statuses
            if (IsFlashing && SMH.GameTimePassed(_startedFlashing, 1.5f)) IsFlashing = false;
            if (IsFrozen && SMH.GameTimePassed(_timeFrozen, _freezeDuration)) IsFrozen = false;
            if (IsStunned && SMH.GameTimePassed(_timeStartedStun, _stunDuration)) IsStunned = false;
            if (IsHealing && SMH.GameTimePassed(_timeStartedHeal, Constants.HealFlashDuration)) IsHealing = false;
            if (IsImmobile && SMH.GameTimePassed(_timeStartedImmobilize, _immobilizeDuration)) IsImmobile = false;
            if (IsSlimed && SMH.GameTimePassed(_timeSlimed, _slimeDuration)) IsSlimed = false;
            if (IsGraduallyMoving && SMH.GameTimePassed(_timeStartedGraduallyMoving, _timeToGraduallyMove))
            {
                IsGraduallyMoving = false;
                X = _graduallyMoveTargetX;
                Y = _graduallyMoveTargetY;
                DX = DY = 0;
            }

            //Update shit if in Knockback state
            if (!IsFalling && !IsGraduallyMoving && !IsSliding && IsKnockback && SMH.GameTimePassed(_startedKnockBack, Constants.KnockbackDuration))
            {
                IsKnockback = false;
                DX = DY = 0f;
                //Help slow the player down if they are on ice by using PLAYER_ACCEL for 1 frame
                if (Tile.Collision == CollisionTile.ICE)
                {
                    if (DX > 0f) 
                        DX -= Constants.PlayerAcceleration; 
                    else if (DX < 0f) 
                        DX += Constants.PlayerAcceleration;

                    if (DY > 0f) 
                        DY -= Constants.PlayerAcceleration; 
                    else if (DY < 0f) 
                        DX += Constants.PlayerAcceleration;
                }
            }

            //Do Attack
            if (SMH.Input.IsPressed(Input.Attack) && !IsTongueLocked && !IsBreathingFire && !IsFrozen && !IsHovering &&
                    !IsFalling && !IsSpringing && !IsCloaked && !_isShrinkActive && !IsDrowning &
                    !IsReflectionShieldActive && !IsStunned &&
                    SMH.CurrentFrame != SMH.WindowManager.FrameLastWindowClosed)
            {
                Tongue.StartAttack();
            }

            Tongue.Update(dt);
            //TODO:worm->update();

            //Update health and mana
            if (!_usingManaItem && SMH.GameTimePassed(_timeLastUsedMana, Constants.ManaRegenDelay)) 
                Mana += (MaxMana * 1.2f *Constants.ManaRegenerationRate/ 100f) * dt;
            if (Mana < 0f)
                Mana = 0f;
            if (IsInvincible)
                Mana = MaxMana;
            _usingManaItem = false;

            DoWarps(dt);
            DoAbilities(dt);
            DoItems();
            DoWater();
            UpdateJesusSound();

            //Die
            if (Health <= 0 && SMH.State == GameState.Game)
            {
                IsFlashing = false;
                //TODO:smh->deathEffectManager->beginEffect();
            }
        }

        public override void Draw()
        {
            //Breath attacks - draw below player if facing up
            if (Facing == Direction.Up || Facing == Direction.UpLeft || Facing == Direction.UpRight)
            {
                //fireBreathParticle->Render();//TODO
                //iceBreathParticle->Render();
            }

            //Draw Smiley's shadow
            if ((Tile.Collision != CollisionTile.FAKE_PIT && Tile.Collision != CollisionTile.PIT && Tile.Collision != CollisionTile.NO_WALK_PIT) ||
                HoveringYOffset > 0f || IsDrowning || IsSpringing || (IsOnWater && IsWaterWalking) || (!IsFalling && SMH.Environment.CollisionAt(X, Y + 15f) != CollisionTile.WALK_LAVA))
            {
                Color color = IsDrowning ? Color.White : Color.FromNonPremultiplied(255, 255, 255, 50);
                SMH.Graphics.DrawSprite(Sprites.PlayerShadow, ScreenX, ScreenY + 22f * ShrinkScale, color, 0f, Scale * ShrinkScale);
            }

            //Draw Smiley
            if (!IsDrowning && (IsFlashing && ((int)SMH.GameTime * 100) % 20 > 15 || !IsFlashing))
            {
                //Draw UP, UP_LEFT, UP_RIGHT tongues before smiley
                if (Facing == Direction.Up || Facing == Direction.UpLeft || Facing == Direction.UpRight)
                {
                    Tongue.Draw();
                }

                //Draw Smiley sprite
                SMH.Graphics.DrawSprite(SpriteSets.Player[(int)Facing], 512, 384f - HoveringYOffset - SpringOffset - _iceHopOffset, GetSmileyColor(), Rotation, Scale * HoverScale * ShrinkScale);

                //Draw every other tongue after smiley
                if (Facing != Direction.Up && Facing != Direction.UpLeft && Facing != Direction.UpRight)
                {
                    Tongue.Draw();
                }
            }

            //Cane effects//TODO:
            //smh->resources->GetParticleSystem("smileysCane")->Render();

            //Draw an ice block over smiley if he is frozen;
            if (IsFrozen)
            {
                SMH.Graphics.DrawSprite(Sprites.IceBlock, ScreenX, ScreenY);
            }

            if (IsStunned)
            {
                float angle;
                for (int n = 0; n < 5; n++)
                {
                    angle = (((float)n + 1f) / 5f) * 2f * (float)Math.PI + SMH.GameTime;
                    SMH.Graphics.DrawSprite(Sprites.StunStar, ScreenX + (float)Math.Cos(angle) * 25f, ScreenY + (float)Math.Sin(angle) * 7f - 30f);
                }
            }

            //Breath Attacks - draw on top of player if facing up, left or down
            if (Facing != Direction.Up && Facing != Direction.UpLeft && Facing != Direction.UpRight)
            {
                //TODO:
                //fireBreathParticle->Render();
                //iceBreathParticle->Render();
            }

            //Draw reflection shield
            if (IsReflectionShieldActive)
            {
                SMH.Graphics.DrawSprite(Sprites.ReflectionShield, ScreenX, ScreenY);
            }
        }

        /// <summary>
        /// Moves the player to the specified position.
        /// </summary>
        /// <param name="_gridX"></param>
        /// <param name="_gridY"></param>
        public void MoveTo(int gridX, int gridY)
        {
            X = gridX * 64 + 32;
            Y = gridY * 64 + 32;
            DX = DY = 0f;
            UpdateLocation();
        }

        /// <summary>
        /// Moves the player to (x,y) at the given speed. The player cannot move while this is taking place.
        /// </summary>
        /// <param name="targetX"></param>
        /// <param name="targetY"></param>
        /// <param name="speed"></param>
        public void GraduallyMoveTo(float targetX, float targetY, float speed)
        {
            _graduallyMoveTargetX = targetX;
            _graduallyMoveTargetY = targetY;
            IsGraduallyMoving = true;
            _timeStartedGraduallyMoving = SMH.GameTime;
            _timeToGraduallyMove = SmileyUtil.Distance(targetX, targetY, X, Y) / speed;

            float angle = SmileyUtil.GetAngleBetween(X, Y, targetX, targetY);

            DX = speed * (float)Math.Cos(angle);
            DY = speed * (float)Math.Sin(angle);
        }

        /// <summary>
        /// Stops smiley's x and y velocities.
        /// </summary>
        public void StopMovement()
        {
            DX = DY = 0f;
        }

        /// <summary>
        /// Freezes Smiley for the specified duration.
        /// </summary>
        /// <param name="duration"></param>
        public void Freeze(float duration)
        {
            if (!IsFalling)
            {
                IsFrozen = true;
                _timeFrozen = SMH.GameTime;
                _freezeDuration = duration;
            }
        }

        /// <summary>
        /// Stuns Smiley for the specified duration.
        /// </summary>
        /// <param name="duration"></param>
        public void Stun(float duration)
        {
            if (!IsFalling)
            {
                IsStunned = true;
                _timeStartedStun = SMH.GameTime;
                _stunDuration = duration;
            }
        }

        /// <summary>
        /// Immobilizes Smiley for the specified duration.
        /// </summary>
        /// <param name="duration"></param>
        public void Immobilize(float duration)
        {
            if (!IsImmobile)
            {
                IsImmobile = true;
                _timeStartedImmobilize = SMH.GameTime;
                _immobilizeDuration = duration;
            }
        }

        /// <summary>
        /// Deals damage to the player (with no knockback!)
        /// </summary>
        /// <param name="damage">Damage to deal</param>
        /// <param name="makesFlash">Whether or not the attack makes smiley flash</param>
        public void DealDamage(float damage, bool makesFlash)
        {
            DealDamageAndKnockback(damage, makesFlash, 0, 0, 0);
        }

        /// <summary>
        /// Deals the specified damage to the smiley and knocks him back.
        /// </summary>
        /// <param name="damage">Damage to deal</param>
        /// <param name="makesFlash">Whether or not the attack makes smiley flash</param>
        /// <param name="knockbackDist">Distance to knock smiley back from the center of the knockbacker.</param>
        /// <param name="knockbackerX">x location of the object that knocked smiley back</param>
        /// <param name="knockbackerY">y location of the object that knocked smiley back</param>
        public void DealDamageAndKnockback(float damage, bool makesFlash, float knockbackDist, float knockbackerX, float knockbackerY)
        {
            DealDamageAndKnockback(damage, makesFlash, false, knockbackDist, knockbackerX, knockbackerY);
        }

        /// <summary>
        /// Deals the specified damage to the smiley and knocks him back.
        /// </summary>
        /// <param name="damage">Damage to deal</param>
        /// <param name="makesFlash">Whether or not the attack makes smiley flash</param>
        /// <param name="alwaysKnockback">True if Smiley should be knocked back even if flashing</param>
        /// <param name="knockbackDist">Distance to knock smiley back from the center of the knockbacker</param>
        /// <param name="knockbackerX">x location of the object that knocked smiley back</param>
        /// <param name="knockbackerY">y location of the object that knocked smiley back</param>
        public void DealDamageAndKnockback(float damage, bool makesFlash, bool alwaysKnockback, float knockbackDist,
                float knockbackerX, float knockbackerY)
        {
            if (!makesFlash || (makesFlash && !IsFlashing))
            {
                if (!IsInvincible)
                {
                    Health -= (damage * (2f - SMH.Data.GetDifficultyModifier(SMH.SaveManager.CurrentSave.Difficulty)));
                }
            }

            float knockbackAngle = SmileyUtil.GetAngleBetween(knockbackerX, knockbackerY, X, Y);
            float knockbackX = (knockbackDist - SmileyUtil.Distance(knockbackerX, knockbackerY, X, Y)) * (float)Math.Cos(knockbackAngle);
            float knockbackY = (knockbackDist - SmileyUtil.Distance(knockbackerX, knockbackerY, X, Y)) * (float)Math.Sin(knockbackAngle);

            //Do knockback if not sliding etc.
            if (knockbackDist > 0 && (!IsFlashing || alwaysKnockback) && !IsIceSliding && !IsSliding && !IsSpringing && !IsFalling)
            {
                DX = knockbackX / Constants.KnockbackDuration;
                DY = knockbackY / Constants.KnockbackDuration;
                IsKnockback = true;
                _startedKnockBack = SMH.GameTime;
            }

            if (makesFlash && !IsFlashing)
            {
                IsFlashing = true;
                _startedFlashing = SMH.GameTime;
            }
        }

        /// <summary>
        /// Stops fire breath if it is active.
        /// </summary>
        public void StopFireBreath()
        {
            SMH.Sound.StopLoopingSound(Sound.FireBreath);
            IsBreathingFire = false;
            //fireBreathParticle->Stop(false);//TODO:
        }

        /// <summary>
        /// Draws the Jesus beam when the sandals are active.
        /// This is called by smh's draw function, after everything else, so that the beam from heaven is drawn on top of everything.
        /// (Note that the GUI and other similar things are drawn after the light from heaven)
        /// </summary>
        public void DrawJesusBeam()
        {
            if (IsWaterWalking)
            {
                //the alpha is based on how much longer Smiley can walk on water
                //Note that it goes to 128 rather than 255. That's cause I think the 255 is too noticeable and thus looks like shit.
                float jAlpha = 128f * (SMH.GameTime - _startedWaterWalk) / Constants.JesusSandleTime;
                jAlpha = 128f - jAlpha; //so it goes from 128 to 0 rather than from 0 to 128. This way it "fades out" as Smiley walks on water.
                SMH.Graphics.DrawSprite(Sprites.JesusBeam, ScreenX, ScreenY, Color.FromNonPremultiplied(0, 0, 0, (int)jAlpha));
            }
        }

        /// <summary>
        /// Returns the Rectangle bounding any progress bar that should be drawn above smiley's 
        /// head, or null for no progress bar.
        /// </summary>
        /// <returns></returns>
        public Rectangle? GetProgressBar()
        {
            //Jesus bar
            if (IsWaterWalking)
            {
                return new Rectangle(
                    512 - 30,
                    Convert.ToInt32(384f - 55f - HoveringYOffset),
                    Convert.ToInt32(512f - 30f + 60f * ((Constants.JesusSandleTime - (SMH.GameTime - _startedWaterWalk)) / Constants.JesusSandleTime)),
                    Convert.ToInt32(384 - 50 - HoveringYOffset));
            }

            //Cane bar
            if (IsUsingCane)
            {
                return new Rectangle(
                    512 - 30,
                    384 - 55 - Convert.ToInt32(HoveringYOffset),
                    Convert.ToInt32(512f - 30f + 60f * ((SMH.GameTime - _timeStartedCane) / Constants.CaneTime)),
                    384 - 50 - Convert.ToInt32(HoveringYOffset));
            }

            //Hover bar
            if (IsHovering)
            {
                return new Rectangle(
                    512 - 30,
                    384 - 55 - Convert.ToInt32(HoveringYOffset),
                    Convert.ToInt32(512f - 30f + 60 * ((Constants.HoverDuration - (SMH.GameTime - _timeStartedHovering)) / Constants.HoverDuration)),
                    384 - 50 - Convert.ToInt32(HoveringYOffset));
            }

            //Frisbee bar
            if (IsChargingFrisbee && _frisbeePower > (Constants.MaxFrisbeePower / 10f))
            {
                return new Rectangle(
                    512 - 30,
                    384 - 55 - Convert.ToInt32(HoveringYOffset),
                    Convert.ToInt32(512f - 30f + 60f * (_frisbeePower / Constants.MaxFrisbeePower)),
                    384 - 50 - Convert.ToInt32(HoveringYOffset));
            }

            return null;
        }

        /// <summary>
        /// DeShrinks the player if they are shrunk.
        /// </summary>
        public void DeShrink()
        {
            if (_isShrinkActive)
            {
                _isShrinkActive = false;
                ShrinkScale = 1f;
                SMH.Sound.PlaySound(Sound.DeShrink);
            }
        }

        public bool CanPass(CollisionTile collision)
        {
            return CanPass(collision, true);
        }

        public bool CanPass(CollisionTile collision, bool applyCurrentAbilities)
        {
            if (applyCurrentAbilities && IsSpringing) return true;

            bool canPassWater = (((SMH.GUI.SelectedAbility == Ability.WATER_BOOTS) && !IsDrowning) || IsSpringing || IsHovering || IsGraduallyMoving) && SMH.GameTimePassed(_timeStoppedBreathingFire, 0.5f);

            switch (collision)
            {
                case CollisionTile.SLIME: return true;
                case CollisionTile.ICE: return !IsKnockback;
                case CollisionTile.WALK_LAVA: return true;
                case CollisionTile.SPRING_PAD: return true;
                case CollisionTile.SUPER_SPRING: return true;
                case CollisionTile.RED_WARP: return true;
                case CollisionTile.BLUE_WARP: return true;
                case CollisionTile.YELLOW_WARP: return true;
                case CollisionTile.GREEN_WARP: return true;
                case CollisionTile.LEFT_ARROW: return true;
                case CollisionTile.RIGHT_ARROW: return true;
                case CollisionTile.UP_ARROW: return true;
                case CollisionTile.DOWN_ARROW: return true;
                case CollisionTile.PLAYER_START: return true;
                case CollisionTile.PLAYER_END: return true;
                case CollisionTile.WALKABLE: return true;
                case CollisionTile.PIT: return true;
                case CollisionTile.FAKE_PIT: return true;
                case CollisionTile.ENEMY_NO_WALK: return true;
                case CollisionTile.WHITE_CYLINDER_DOWN: return true;
                case CollisionTile.YELLOW_CYLINDER_DOWN: return true;
                case CollisionTile.GREEN_CYLINDER_DOWN: return true;
                case CollisionTile.BLUE_CYLINDER_DOWN: return true;
                case CollisionTile.BROWN_CYLINDER_DOWN: return true;
                case CollisionTile.SILVER_CYLINDER_DOWN: return true;
                case CollisionTile.UNWALKABLE: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.HOVER_PAD: return true;
                case CollisionTile.SHALLOW_WATER: return true;
                case CollisionTile.SHALLOW_GREEN_WATER: return true;
                case CollisionTile.EVIL_WALL_POSITION: return true;
                case CollisionTile.EVIL_WALL_TRIGGER: return true;
                case CollisionTile.EVIL_WALL_DEACTIVATOR: return true;
                case CollisionTile.EVIL_WALL_RESTART: return true;
                case CollisionTile.DEEP_WATER: return canPassWater;
                case CollisionTile.NO_WALK_WATER: return false;
                case CollisionTile.GREEN_WATER: return canPassWater;
                case CollisionTile.SMILELET_FLOWER_HAPPY: return true;
                case CollisionTile.WHITE_SWITCH_LEFT: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.YELLOW_SWITCH_LEFT: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.GREEN_SWITCH_LEFT: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.BLUE_SWITCH_LEFT: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.BROWN_SWITCH_LEFT: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.SILVER_SWITCH_LEFT: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.WHITE_SWITCH_RIGHT: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.YELLOW_SWITCH_RIGHT: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.GREEN_SWITCH_RIGHT: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.BLUE_SWITCH_RIGHT: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.BROWN_SWITCH_RIGHT: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.SILVER_SWITCH_RIGHT: return false || (applyCurrentAbilities && IsSpringing);
                case CollisionTile.DIZZY_MUSHROOM_1: return true;
                case CollisionTile.DIZZY_MUSHROOM_2: return true;
                case CollisionTile.BOMB_PAD_UP: return true;
                case CollisionTile.BOMB_PAD_DOWN: return true;
                case CollisionTile.FLAME: return true;
                case CollisionTile.FAKE_COLLISION: return true;

                default: return false;
            }
        }

        /// <summary>
        /// Heals Smiley the specified amount.
        /// </summary>
        /// <param name="amount"></param>
        public void Heal(float amount)
        {
            if (!IsHealing)
            {
                Health += amount;
                IsHealing = true;
                _timeStartedHeal = SMH.GameTime;
            }
        }

        /// <summary>
        /// Slimes Smiley for the specified duration.
        /// </summary>
        /// <param name="duration"></param>
        public void Slime(float duration)
        {
            IsSlimed = true;
            _slimeDuration = duration;
            _timeSlimed = SMH.GameTime;
        }

        /// <summary>
        /// Makes smiley face straight.
        /// </summary>
        public void SetFacingStraight()
        {
            if (_enteredSpringX < Tile.X) Facing = Direction.Right;
            else if (_enteredSpringX > Tile.X) Facing = Direction.Left;
            else if (_enteredSpringX < Tile.Y) Facing = Direction.Down;
            else if (_enteredSpringX > Tile.Y) Facing = Direction.Up;
            else if (Facing == Direction.UpLeft || Facing == Direction.UpRight) Facing = Direction.Up;
            else if (Facing == Direction.DownLeft || Facing == Direction.DownRight) Facing = Direction.Down;
        }

        /// <summary>
        /// Returns true if smiley is overlapping a water tile at all.
        /// </summary>
        /// <returns></returns>
        public bool IsSmileyTouchingWater()
        {
            for (int i = Tile.X - 1; i <= Tile.X + 1; i++)
            {
                for (int j = Tile.Y - 1; j <= Tile.Y + 1; j++)
                {
                    if (SMH.Environment.Tiles[i, j].Collision == CollisionTile.DEEP_WATER || SMH.Environment.Tiles[i, j].Collision == CollisionTile.GREEN_WATER)
                    {
                        Rect box = new Rect(i * 64, j * 64, 64, 64);
                        if (CollisionCircle.Intersects(box))
                            return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the color with which to draw smiley.
        /// </summary>
        private Color GetSmileyColor()
        {
            float alpha = (IsCloaked) ? 75f : 255f;
            float r = 255f;
            float g = 255f;
            float b = 255f;

            if (IsHealing)
            {
                g = 255f - (float)Math.Sin(((SMH.GameTime - _timeStartedHeal) / Constants.HealFlashDuration) * (float)Math.PI) * 65f;
            }
            else if (IsUber)
            {
                r = ((float)Math.Sin(SMH.Now * 1.3f) + 1f) / 2f * 50f + 200f;
                g = ((float)Math.Sin(SMH.Now * 1.6f) + 1f) / 2f * 50f + 200f;
                b = ((float)Math.Sin(SMH.Now * 0.7f) + 1f) / 2f * 50f + 200f;
            }
            else if (IsSlimed)
            {
                r = 100f;
                g = 200f;
                b = 100f;
            }

            return Color.FromNonPremultiplied((int)r, (int)g, (int)b, (int)alpha);
        }

        /// <summary>
        /// Updates dx/dy by listening for movement input from the player and other shit.
        /// </summary>
        /// <param name="dt"></param>
        private void UpdateVelocities(float dt)
        {
            //For the following states, velocities are handled in their respective update methods
            if (IsFalling || IsInShrinkTunnel || IsIceSliding || IsSliding || IsSpringing || IsGraduallyMoving)
                return;

            if (IsFrozen || IsDrowning || IsStunned || IsImmobile)
            {
                DX = DY = 0f;
                return;
            }

            //Determine acceleration - normal ground or slime
            float accel = (Tile.Collision == CollisionTile.SLIME && HoveringYOffset == 0f) ? Constants.SlimeAcceleration : Constants.PlayerAcceleration;

            //Stop drifting when abs(dx) < accel
            if (!IsIceSliding && !IsSliding && !IsSpringing)
            {
                if (DX > -1f * accel * dt && DX < accel * dt) DX = 0f;
                if (DY > -1f * accel * dt && DY < accel * dt) DY = 0f;
            }

            //Decelerate
            if (!IsIceSliding && !IsSliding && !IsSpringing)
            {
                if ((SMH.Input.IsDown(Input.Aim) && !IsIceSliding && !IsKnockback) || (!SMH.Input.IsDown(Input.Left) && !SMH.Input.IsDown(Input.Right) && !IsKnockback))
                    if (DX > 0) DX -= accel * dt;
                    else if (DX < 0) DX += accel * dt;

                if ((SMH.Input.IsDown(Input.Aim) && !IsIceSliding && !IsKnockback) || (!SMH.Input.IsDown(Input.Up) && !SMH.Input.IsDown(Input.Down) && !IsKnockback))
                    if (DY > 0) DY -= accel * dt;
                    else if (DY < 0) DY += accel * dt;
            }

            //Movement input
            if (!SMH.Input.IsDown(Input.Aim) && !IsIceSliding && !IsSliding && !IsKnockback && !IsSpringing)
            {
                //Move Left
                if (SMH.Input.IsDown(Input.Left))
                {
                    if (DX > -1 * Constants.MoveSpeed && !IsSliding) DX -= accel * dt;
                }
                //Move Right
                if (SMH.Input.IsDown(Input.Right))
                {
                    if (DX < Constants.MoveSpeed && !IsSliding) DX += accel * dt;
                }
                //Move Up
                if (SMH.Input.IsDown(Input.Up))
                {
                    if (DY > -1 * Constants.MoveSpeed && !IsSliding) DY -= accel * dt;
                }
                //Move Down
                if (SMH.Input.IsDown(Input.Down))
                {
                    if (DY < Constants.MoveSpeed && !IsSliding) DY += accel * dt;
                }
            }
        }

        /// <summary>
        /// Location-related update logic.
        /// </summary>
        private void UpdateLocation()
        {
            _lastTile = Tile;
            if (Tile.Collision != CollisionTile.ICE)
            {
                _lastNonIceTile = Tile;
            }

            _baseX = X + 0;
            _baseY = Y + 15f * ShrinkScale;
            FeetTile = SMH.Environment.Tiles[(int)Math.Floor(_baseX / 64f), (int)Math.Floor(_baseY / 64f)];
        }

        /// <summary>
        /// Updates the sound of the holy choir. Starts if you just walked onto water, stops if you are off
        /// water or drowning!
        /// </summary>
        private void UpdateJesusSound()
        {
            if (!_isJesusSoundPlaying && IsWaterWalking)
            {
                SMH.Sound.StartLoopingSound(Sound.Jesus);
                _isJesusSoundPlaying = true;
            }

            if (_isJesusSoundPlaying && (!IsWaterWalking || IsDrowning))
            {
                SMH.Sound.StopLoopingSound(Sound.Jesus);
                _isJesusSoundPlaying = false;

                if (Tile.HasShallowWater) SMH.Sound.StartLoopingSound(Sound.ShallowWater);
                if (Tile.Collision == CollisionTile.WALK_LAVA) SMH.Sound.StartLoopingSound(Sound.Lava);
            }
        }

        /// <summary>
        /// Sets the player's facing direction based on what directional keys are pressed.
        /// </summary>
        private void SetFacingDirection()
        {
            if (!IsFrozen && !IsDrowning && !IsFalling && !IsIceSliding && !IsKnockback && !IsSpringing && Tile.Collision != CollisionTile.SPRING_PAD && Tile.Collision != CollisionTile.SUPER_SPRING)
            {
                if (SMH.Input.IsDown(Input.Left)) Facing = Direction.Left;
                else if (SMH.Input.IsDown(Input.Right)) Facing = Direction.Right;
                else if (SMH.Input.IsDown(Input.Up)) Facing = Direction.Up;
                else if (SMH.Input.IsDown(Input.Down)) Facing = Direction.Down;

                //Diagonals
                if (SMH.Input.IsDown(Input.Down) && SMH.Input.IsDown(Input.Up))
                {
                    Facing = Direction.UpLeft;
                }
                else if (SMH.Input.IsDown(Input.Right) && SMH.Input.IsDown(Input.Up))
                {
                    Facing = Direction.UpRight;
                }
                else if (SMH.Input.IsDown(Input.Left) && SMH.Input.IsDown(Input.Down))
                {
                    Facing = Direction.DownLeft;
                }
                else if (SMH.Input.IsDown(Input.Right) && SMH.Input.IsDown(Input.Down))
                {
                    Facing = Direction.DownRight;
                }
            }
        }

        /// <summary>
        /// Performs movement for the current frame based on current DX/DY.
        /// </summary>
        /// <param name="dt"></param>
        private void DoMove(float dt)
        {
            float xDist = DX * dt;
            float yDist = DY * dt;

            if ((IsInShallowWater || IsInLava) && !IsSpringing && SMH.GUI.SelectedAbility != Ability.WATER_BOOTS)
            {
                //Slow movement in shallow water or lava.
                xDist *= 0.5f;
                yDist *= 0.5f;
            }
            if (IsSprinting && !IsSpringing && !IsSliding && !IsIceSliding && !IsOnWater)
            {
                xDist *= Constants.SpeedBootsModifier;
                yDist *= Constants.SpeedBootsModifier;
            }
            if (IsIceSliding)
            {
                xDist *= 1.2f;
                yDist *= 1.2f;
            }
            if (!IsIceSliding && !IsSpringing && !IsSliding && !IsGraduallyMoving)
            {
                if (IsUber)
                {
                    xDist *= 3f;
                    yDist *= 3f;
                }
                if (IsSlimed)
                {
                    xDist *= 0.5f;
                    yDist *= 0.5f;
                }
                if (Tongue.IsAttacking)
                {
                    xDist *= 0.1f;
                    yDist *= 0.1f;
                }
            }

            //Check for collision with frozen enemies
            if (SMH.EnemyManager.CollidesWithFrozenEnemy(new CollisionCircle
            {
                X = X + xDist,
                Y = Y + yDist,
                Radius = (Constants.PlayerWidth / 2f - 3f) * ShrinkScale
            })) return;

            CollisionCircle = new CollisionCircle(X, Y, (Constants.PlayerWidth / 2f - 3f) * ShrinkScale);

            //We have to do a thing here
            if (DoHugeHackToFixAMovementBug(xDist, yDist)) return;

            //Move left or right
            if (xDist != 0f && Tile.Collision != CollisionTile.SHRINK_TUNNEL_VERTICAL)
            {
                if (!SMH.Environment.PlayerCollision(X + xDist, Y, dt))
                {
                    X += xDist;
                    SMH.SaveManager.CurrentSave.PixelsTraversed += (int)Math.Abs(xDist);
                }
                else
                {
                    //Since Smiley just ran into something, maybe its a locked door. Dickens!
                    if (xDist > 0)
                    {
                        SMH.Environment.UnlockDoor(Tile.X + 1, Tile.Y);
                    }
                    else if (xDist < 0)
                    {
                        SMH.Environment.UnlockDoor(Tile.X - 1, Tile.Y);
                    }
                    IsKnockback = false;
                    //If sliding on puzzle ice, bounce back the other direction
                    if (IsIceSliding)
                    {
                        DX = -DX;
                        Facing = (Facing == Direction.Right) ? Direction.Left : Direction.Right;
                    }
                    else
                    {
                        DX = 0;
                    }
                }
            }

            //Move up or down
            if (yDist != 0f && Tile.Collision != CollisionTile.SHRINK_TUNNEL_HORIZONTAL)
            {
                if (!SMH.Environment.PlayerCollision(X, Y + yDist, dt))
                {
                    Y += yDist;
                    SMH.SaveManager.CurrentSave.PixelsTraversed += (int)Math.Abs(yDist);
                }
                else
                {
                    //Since Smiley just ran into something, maybe its a locked door. Dickens!
                    if (yDist > 0)
                    {
                        SMH.Environment.UnlockDoor(Tile.X, Tile.Y + 1);
                    }
                    else if (yDist < 0)
                    {
                        SMH.Environment.UnlockDoor(Tile.X, Tile.Y - 1);
                    }
                    IsKnockback = false;
                    //If sliding on puzzle ice, bounce back the other direction
                    if (IsIceSliding)
                    {
                        DY = -DY;
                        Facing = (Facing == Direction.Up) ? Direction.Down : Direction.Up;
                    }
                    else
                    {
                        DY = 0;
                    }
                }
            }
        }

        private bool DoHugeHackToFixAMovementBug(float xDist, float yDist)
        {
            return false;
        }

        /// <summary>
        /// Updates warp related stuff.
        /// </summary>
        /// <param name="dt"></param>
        private void DoWarps(float dt)
        {
            //If the player is on a warp, move the player to the other warp of the same color
            if (!IsSpringing && HoveringYOffset == 0.0f && !IsOnWarp && (Tile.Collision == CollisionTile.RED_WARP || Tile.Collision == CollisionTile.GREEN_WARP ||
                Tile.Collision == CollisionTile.YELLOW_WARP || Tile.Collision == CollisionTile.BLUE_WARP))
            {
                IsOnWarp = true;

                //Play the warp sound effect for non-invisible warps
                if (Tile.Variable != 990)
                {
                    SMH.Sound.PlaySound(Sound.Warp);
                }

                //Make it so Smiley's not sliding or iceSliding or springing
                IsSliding = false;
                IsIceSliding = false;
                IsSpringing = false;

                //Find the other warp square
                Tile otherTile = SMH.Environment.Tiles.SingleOrDefault(tile => tile.ID == Tile.ID && (tile.X != Tile.X || tile.Y != Tile.Y) && SmileyUtil.IsWarp(tile.Collision));
                if (otherTile == null)
                    throw new Exception("Walked onto a warp tile which has no matching receiver warp!!");

                //If this is an invisible warp, use the load effect to move 
                //Smiley to its destination
                if (Tile.Variable == 990)
                {
                    int destX = otherTile.X;
                    int destY = otherTile.Y;
                    if (Facing == Direction.Down || Facing == Direction.DownLeft || Facing == Direction.DownRight)
                        destY++;
                    else if (Facing == Direction.Up || Facing == Direction.UpLeft || Facing == Direction.UpRight)
                        destY--;
                    SMH.AreaChanger.ChangeArea(destX, destY, SMH.SaveManager.CurrentSave.Level);
                }
                else
                {
                    MoveTo(otherTile.X, otherTile.Y);
                }
                return;
            }

            IsOnWarp = SmileyUtil.IsWarp(Tile.Collision);
        }

        /// <summary>
        /// Updates spring related stuff.
        /// </summary>
        /// <param name="dt"></param>
        private void DoSprings(float dt)
        {
            //Start springing
            if (HoveringYOffset == 0.0f && !IsSpringing && (Tile.Collision == CollisionTile.SPRING_PAD || Tile.Collision == CollisionTile.SUPER_SPRING))
            {
                bool superSpring = (Tile.Collision == CollisionTile.SUPER_SPRING);
                SMH.Sound.PlaySound(Sound.Spring);
                IsSpringing = true;
                _startedSpringing = SMH.GameTime;
                DX = DY = 0;

                //Start the spring animation
                //TODO:
                //Tile.ActivatedTime = SMH.GameTime;
                //if (superSpring) {
                //    smh->resources->GetAnimation("superSpring")->Play();
                //} else {
                //    smh->resources->GetAnimation("spring")->Play();
                //}

                //Set Smiley facing a straight direction(not diagonally)
                SetFacingStraight();

                //Determine how long smiley will have to spring to skip a square
                int jumpGridDist = superSpring ? 4 : 2;
                _springVelocity = superSpring ? Constants.SpringVelocity * 1.5f : Constants.SpringVelocity;
                int dist = 0;
                if (Facing == Direction.Left) dist = Convert.ToInt32(X - ((Tile.X - jumpGridDist) * 64 + 32));
                else if (Facing == Direction.Right) dist = Convert.ToInt32(((Tile.X + jumpGridDist) * 64 + 32) - X);
                else if (Facing == Direction.Down) dist = Convert.ToInt32((Tile.Y + jumpGridDist) * 64 + 32 - Y);
                else if (Facing == Direction.Up) dist = Convert.ToInt32(Y - ((Tile.Y - jumpGridDist) * 64 + 32));
                _springTime = (float)dist / _springVelocity;
            }

            //Continue springing - don't use dx/dy just adjust positions directly!
            if (!IsFalling && !IsSliding && IsSpringing && HoveringYOffset == 0.0f)
            {
                Scale = 1f + (float)Math.Sin((float)Math.PI * ((SMH.GameTime - _startedSpringing) / _springTime)) * .2f;
                _springOffset = (float)Math.Sin((float)Math.PI * ((SMH.GameTime - _startedSpringing) / _springTime)) * (_springVelocity / 4f);
                DX = DY = 0;

                if (Facing == Direction.Left)
                {
                    //Spring left
                    X -= _springVelocity * dt;
                    //Adjust the player to land in the middle of the square vertically
                    if (Y < _enteredSpringY * 64 + 31)
                    {
                        Y += 40.0f * dt;
                    }
                    else if (Y > _enteredSpringY * 64 + 33)
                    {
                        Y -= 40.0f * dt;
                    }
                }
                else if (Facing == Direction.Right)
                {
                    //Spring right
                    X += _springVelocity * dt;
                    //Adjust the player to land in the middle of the square vertically
                    if (Y < _enteredSpringY * 64 + 31)
                    {
                        Y += 40.0f * dt;
                    }
                    else if (Y > _enteredSpringY * 64 + 33)
                    {
                        Y -= 40.0f * dt;
                    }
                }
                else if (Facing == Direction.Down)
                {
                    //Spring down
                    Y += _springVelocity * dt;
                    //Adjust the player to land in the center of the square horizontally
                    if (X < Tile.X * 64 + 31)
                    {
                        X += 40.0f * dt;
                    }
                    else if (X > Tile.X * 64 + 33)
                    {
                        X -= 40.0f * dt;
                    }

                    //Spring up
                }
                else if (Facing == Direction.Up)
                {
                    Y -= _springVelocity * dt;
                    //Adjust the player to land in the center of the square horizontally
                    if (X < Tile.X * 64 + 31)
                    {
                        X += 40.0f * dt;
                    }
                    else if (X > Tile.X * 64 + 33)
                    {
                        X -= 40.0f * dt;
                    }
                }

            }

            //Stop springing
            if (IsSpringing && SMH.GameTimePassed(_startedSpringing, _springTime))
            {
                IsSpringing = false;
                Scale = 1;
                X = Tile.X * 64f + 32f;
                _springOffset = 0;
                Y = Tile.Y * 64f + 32f;

                StartPuzzleIce(); //start puzzle ice -- needed here so if Smiley jumps from spring to ice it actually works
            }

            //Remember where the player is before touching a spring
            if (!IsSpringing)
            {
                _enteredSpringX = Tile.X;
                _enteredSpringY = Tile.Y;
            }
        }

        /// <summary>
        /// Updates falling related stuff.
        /// </summary>
        /// <param name="dt"></param>
        private void DoFalling(float dt)
        {
            //Start falling
            if (!IsSpringing && HoveringYOffset == 0.0f && !IsFalling && SMH.Environment.CollisionAt(_baseX, _baseY) == CollisionTile.PIT)
            {
                DX = DY = 0;
                IsFalling = true;
                _startedFalling = SMH.GameTime;
                //Set dx and dy to fall towards the center of the pit
                float angle = SmileyUtil.GetAngleBetween(X, Y, (_baseX / 64) * 64 + 32, (_baseY / 64) * 64 + 32);
                float dist = SmileyUtil.Distance(FeetTile.X * 64 + 32,FeetTile.Y * 64 + 32, X, Y);
                _fallingDx = (dist / 2f) * (float)Math.Cos(angle);
                _fallingDy = (dist / 2f) * (float)Math.Sin(angle);
                SMH.Sound.PlaySound(Sound.Falling);
            }

            //Continue falling
            if (IsFalling)
            {
                X += _fallingDx * dt;
                Y += _fallingDy * dt;
                Scale -= (_isShrinkActive ? .25f : .5f) * dt;
                if (Scale < 0.0f) Scale = 0.0f;
                Rotation += (float)Math.PI * dt;
            }

            //Stop falling
            if (IsFalling && SMH.GameTimePassed(_startedFalling, 2f))
            {
                IsFalling = false;
                MoveTo(_startedFallingTile.X, _startedFallingTile.Y);
                Scale = 1;
                Rotation = 0;
                DealDamage(0.5f, true);
            }

            //Keep track of where the player was before he fell
            if (Tile.IsReturnSpot)
            {
                _startedFallingTile = Tile;
            }
        }

        /// <summary>
        /// Updates arrow pad related stuff.
        /// </summary>
        /// <param name="dt"></param>
        private void DoArrowPads(float dt)
        {
            //Start sliding
            //int arrowPad = smh->environment->collision[gridX][gridY];
            if (!IsSpringing && HoveringYOffset == 0.0f && !IsSliding && SmileyUtil.IsArrowPad(Tile.Collision))
            {
                _startedSliding = SMH.GameTime;
                IsSliding = true;
                DX = DY = 0;
                if (Tile.Collision == CollisionTile.LEFT_ARROW)
                {
                    DX = -250;
                    _timeToSlide = (64.0f + X - ((float)Tile.X * 64.0f + 32.0f)) / 250.0f;
                }
                if (Tile.Collision == CollisionTile.RIGHT_ARROW)
                {
                    DX = 250;
                    _timeToSlide = (64.0f - X + ((float)Tile.X * 64.0f + 32.0f)) / 250.0f;
                }
                if (Tile.Collision == CollisionTile.UP_ARROW)
                {
                    DY = -250;
                    _timeToSlide = (64.0f + Y - ((float)Tile.Y * 64.0f + 32.0f)) / 250.0f;
                }
                if (Tile.Collision == CollisionTile.DOWN_ARROW)
                {
                    DY = 250;
                    _timeToSlide = (64.0f - Y + ((float)Tile.Y * 64.0f + 32.0f)) / 250.0f;
                }
            }

            //Continue sliding - move towards the center of the square
            if (IsSliding)
            {
                if (Tile.Collision == CollisionTile.UP_ARROW || Tile.Collision == CollisionTile.DOWN_ARROW)
                {
                    if (X < Tile.X * 64 + 31)
                    {
                        X += 80.0f * dt;
                    }
                    else if (X > Tile.X * 64 + 33)
                    {
                        X -= 80.0f * dt;
                    }
                }
                else if (Tile.Collision == CollisionTile.LEFT_ARROW || Tile.Collision == CollisionTile.RIGHT_ARROW)
                {
                    if (Y < Tile.Y * 64 + 31)
                    {
                        Y += 80.0f * dt;
                    }
                    else if (Y > Tile.Y * 64 + 33)
                    {
                        Y -= 80.0f * dt;
                    }
                }
            }

            //Stop sliding
            if (IsSpringing || (IsSliding && SMH.GameTimePassed(_startedSliding, _timeToSlide))) IsSliding = false;
        }

        /// <summary>
        /// Updates item related stuff.
        /// </summary>
        private void DoItems()
        {
            bool gatheredItem = false;

            if (Tile.Item == (int)ItemTile.RED_KEY || Tile.Item == (int)ItemTile.GREEN_KEY || Tile.Item == (int)ItemTile.BLUE_KEY || Tile.Item == (int)ItemTile.YELLOW_KEY)
            {
                SMH.Sound.PlaySound(Sound.Key);
                SMH.SaveManager.CurrentSave.NumKeys[SmileyUtil.GetKeyIndex(SMH.SaveManager.CurrentSave.Level), Tile.Item - 1]++;
                gatheredItem = true;
            }
            else if (Tile.Item == (int)ItemTile.SMALL_GEM || Tile.Item == (int)ItemTile.MEDIUM_GEM || Tile.Item == (int)ItemTile.LARGE_GEM)
            {
                SMH.Sound.PlaySound(Sound.Gem);

                //If this is the first gem the player has collected, open up the
                //shop advice.
                if (SMH.SaveManager.CurrentSave.GetTotalGemCount == 0)
                {
                    SMH.PopupMessageManager.ShowNewAdvice(Advice.Shop);
                }

                SMH.SaveManager.CurrentSave.NumGems[SMH.SaveManager.CurrentSave.Level][SmileyUtil.GetGem((ItemTile)Tile.Item)]++;
                if (Tile.Item == (int)ItemTile.SMALL_GEM) SMH.SaveManager.CurrentSave.Money += Constants.SmallGemValue;
                else if (Tile.Item == (int)ItemTile.MEDIUM_GEM) SMH.SaveManager.CurrentSave.Money += Constants.MediumGemValue;
                else if (Tile.Item == (int)ItemTile.LARGE_GEM) SMH.SaveManager.CurrentSave.Money += Constants.LargeGemValue;
                gatheredItem = true;
            }
            else if (Tile.Item == (int)ItemTile.HEALTH_ITEM)
            {
                if (Health != MaxHealth)
                {
                    Health++;
                    gatheredItem = true;
                    SMH.Sound.PlaySound(Sound.Health);
                }
                else
                {
                    SMH.PopupMessageManager.ShowFullHealth();
                }
            }
            else if (Tile.Item == (int)ItemTile.MANA_ITEM)
            {
                if (Mana != MaxMana)
                {
                    Mana += Constants.ManaPerItem;
                    gatheredItem = true;
                    SMH.Sound.PlaySound(Sound.Mana);
                    _timeLastUsedMana = SMH.GameTime - Constants.ManaRegenDelay;
                }
                else
                {
                    SMH.PopupMessageManager.ShowFullMana();
                }
            }

            if (gatheredItem)
            {
                SMH.Environment.RemoveItem(Tile.X, Tile.Y);
            }
        }

        /// <summary>
        /// Updates water/lava related stuff.
        /// </summary>
        private void DoWater()
        {
            //Start water walk
            if (!IsSpringing && SMH.GUI.SelectedAbility == Ability.WATER_BOOTS && HoveringYOffset == 0.0f && !IsWaterWalking && !IsOnWater && FeetTile.HasDeepWater)
            {
                IsWaterWalking = true;
                _startedWaterWalk = SMH.GameTime;
            }

            //Stop water walk
            if (SMH.GUI.SelectedAbility != Ability.WATER_BOOTS || !FeetTile.HasDeepWater || HoveringYOffset > 0.0f || SMH.GameTimePassed(_startedWaterWalk, Constants.JesusSandleTime))
            {
                IsWaterWalking = false;
            }

            //Do lava
            if (!IsSpringing)
            {
                //Enter Lava
                if (!IsInLava && HoveringYOffset == 0f && SMH.Environment.CollisionAt(_baseX, _baseY) == CollisionTile.WALK_LAVA)
                {
                    IsInLava = true;
                    SMH.Sound.StartLoopingSound(Sound.Lava);
                }

                //In Lava
                if (IsInLava)
                {
                    //Take damage every half second
                    if (SMH.GameTimePassed(_lastLavaDamage, .5f))
                    {
                        _lastLavaDamage = SMH.GameTime;
                        if (!SMH.Player.IsInvincible) SMH.Player.Health -= .25f;
                    }
                }
                //Exit Lava
                if (HoveringYOffset > 0f || IsInLava && SMH.Environment.CollisionAt(_baseX, _baseY) != CollisionTile.WALK_LAVA)
                {
                    IsInLava = false;
                    SMH.Sound.StopLoopingSound(Sound.Lava);
                }
            }

            //Do shallow water
            if (!IsSpringing)
            {
                //Enter Shallow Water
                if (HoveringYOffset == 0.0f && !IsInShallowWater && FeetTile.HasShallowWater)
                {
                    IsInShallowWater = true;
                    if (!IsWaterWalking) SMH.Sound.StartLoopingSound(Sound.ShallowWater);
                }
                //Exit Shallow Water
                if (HoveringYOffset > 0f || IsInShallowWater && !FeetTile.HasShallowWater)
                {
                    IsInShallowWater = false;
                    SMH.Sound.StopLoopingSound(Sound.ShallowWater);
                }
            }

            //Do drowning
            if (!IsSpringing && HoveringYOffset == 0f)
            {
                //Start drowning
                if (!IsDrowning && FeetTile.HasDeepWater && !IsWaterWalking)
                {
                    IsDrowning = true;
                    SMH.Sound.PlaySound(Sound.Drowning);
                    _startedDrowning = SMH.GameTime;
                }
                //Stop drowning
                if (IsDrowning && SMH.GameTimePassed(_startedDrowning, 4f))
                {
                    IsDrowning = false;
                    MoveTo(_enteredWaterX, _enteredWaterY);
                    DealDamage(0.5f, true);

                    //If smiley was placed onto an up cylinder, toggle its switch
                    if (SmileyUtil.IsCylinderUp(Tile.Collision))
                    {
                        SMH.Environment.ToggleSwitch(Tile.ID);
                    }

                }
            }

            //Determine if the player is on water
            IsOnWater = (HoveringYOffset == 0f) && FeetTile.HasDeepWater;

            //Keep track of where the player was before entering deep water
            if (Tile.IsReturnSpot)
            {
                _enteredWaterX = Tile.X;
                _enteredWaterY = Tile.Y;
            }
        }

        /// <summary>
        /// Updates ice related stuff.
        /// </summary>
        /// <param name="dt"></param>
        private void DoIce(float dt)
        {
            StartPuzzleIce();

            //Continue Puzzle Ice - slide towards the center of the square
            if (IsIceSliding)
            {
                if (Facing == Direction.Left || Facing == Direction.Right)
                {
                    if ((int)Y % 64 < 32) Y += 30.0f * dt;
                    if ((int)Y % 64 > 32) Y -= 30.0f * dt;
                }
                else if (Facing == Direction.Up || Facing == Direction.Down)
                {
                    if ((int)X % 64 < 32) X += 30.0f * dt;
                    if ((int)X % 64 > 32) X -= 30.0f * dt;
                }
            }

            //Ice hop
            if (_needToIceHop)
            {
                float sinAngle = (SMH.GameTime - _timeStartedIceHop) * 20f;
                _iceHopOffset = 10f * (float)Math.Sin(sinAngle);
                if (sinAngle >= 3.14159) { _iceHopOffset = 0f; _needToIceHop = false; }
            }

            //Stop puzzle ice
            CollisionTile c = Tile.Collision;
            if (IsIceSliding && c != CollisionTile.ICE)
            {

                //If the player is on a new special tile, stop sliding now. Otherwise only 
                //stop once the player is in the middle of the square.
                if (c == CollisionTile.SPRING_PAD || c == CollisionTile.SHRINK_TUNNEL_HORIZONTAL || c == CollisionTile.SHRINK_TUNNEL_VERTICAL ||
                        c == CollisionTile.UP_ARROW || c == CollisionTile.DOWN_ARROW || c == CollisionTile.LEFT_ARROW || c == CollisionTile.RIGHT_ARROW ||
                        (Facing == Direction.Right && (int)X % 64 > 32) ||
                        (Facing == Direction.Left && (int)X % 64 < 32) ||
                        (Facing == Direction.Up && (int)Y % 64 < 32) ||
                        (Facing == Direction.Down && (int)Y % 64 > 32))
                {
                    DX = DY = 0;
                    IsIceSliding = false;
                    SMH.Sound.StopLoopingSound(Sound.SnowFootstep);
                }
            }
        }

        /// <summary>
        /// Start puzzle ice
        /// </summary>
        private void StartPuzzleIce()
        {
            //Start Puzzle Ice
            if (!IsSpringing && HoveringYOffset == 0.0f && !IsIceSliding && Tile.Collision == CollisionTile.ICE)
            {
                if (_lastTile.X < Tile.X)
                {
                    Facing = Direction.Right;
                    DX = Constants.MoveSpeed;
                    DY = 0;
                    _needToIceHop = true;
                    _timeStartedIceHop = SMH.GameTime;
                    SMH.Sound.PlaySound(Sound.HopOntoIce);
                }
                else if (_lastTile.X > Tile.X)
                {
                    Facing = Direction.Left;
                    DX = -Constants.MoveSpeed;
                    DY = 0;
                    _needToIceHop = true;
                    _timeStartedIceHop = SMH.GameTime;
                    SMH.Sound.PlaySound(Sound.HopOntoIce);
                }
                else if (_lastTile.Y < Tile.Y)
                {
                    Facing = Direction.Down;
                    DX = 0;
                    DY = Constants.MoveSpeed;
                    _needToIceHop = true;
                    _timeStartedIceHop = SMH.GameTime;
                    SMH.Sound.PlaySound(Sound.HopOntoIce);
                }
                else if (_lastTile.Y > Tile.Y)
                {
                    Facing = Direction.Up;
                    DX = 0;
                    DY = -Constants.MoveSpeed;
                    _needToIceHop = true;
                    _timeStartedIceHop = SMH.GameTime;
                    SMH.Sound.PlaySound(Sound.HopOntoIce);
                }
                else
                { //there was no _lastTile.X or _lastTile.Y, so let's go by "Facing" (this happens when you jump onto ice)
                    switch (Facing)
                    {
                        case Direction.Right:
                            DX = Constants.MoveSpeed;
                            DY = 0;
                            break;
                        case Direction.Left:
                            DX = -Constants.MoveSpeed;
                            DY = 0;
                            break;
                        case Direction.Down:
                            DX = 0;
                            DY = Constants.MoveSpeed;
                            break;
                        case Direction.Up:
                            DX = 0;
                            DY = -Constants.MoveSpeed;
                            break;
                        default:
                            //Smiley is trying to move diagonally onto puzzle ice -- bump him back!
                            X = _lastNonIceTile.X * 64 + 20;
                            Y = _lastNonIceTile.Y * 64 + 40;
                            IsIceSliding = false;
                            SMH.Sound.StopLoopingSound(Sound.SnowFootstep);
                            break;
                    };
                }
                IsIceSliding = true;
                SMH.Sound.StartLoopingSound(Sound.SnowFootstep);
            }
        }

        /// <summary>
        /// Updates shrink tunnel related stuff.
        /// </summary>
        /// <param name="dt"></param>
        private void DoShrinkTunnels(float dt)
        {
            //Enter shrink tunnel
            if (!IsInShrinkTunnel && !IsSpringing && !IsSliding && (Tile.Collision == CollisionTile.SHRINK_TUNNEL_HORIZONTAL || Tile.Collision == CollisionTile.SHRINK_TUNNEL_VERTICAL))
            {
                _timeEnteredShrinkTunnel = SMH.GameTime;
                IsInShrinkTunnel = true;
                DX = DY = 0;

                //Entering from left (going right)
                if (Tile.Collision == CollisionTile.SHRINK_TUNNEL_HORIZONTAL && Facing == Direction.Right || Facing == Direction.UpRight || Facing == Direction.DownRight)
                {
                    DX = Constants.ShrinkTunnelSpeed;
                    _timeInShrinkTunnel = (64f - X + ((float)Tile.X * 64f + 32f)) / Constants.ShrinkTunnelSpeed;

                    //Entering from right (going left)
                }
                else if (Tile.Collision == CollisionTile.SHRINK_TUNNEL_HORIZONTAL && Facing == Direction.Left || Facing == Direction.UpLeft || Facing == Direction.DownLeft)
                {
                    DX = -Constants.ShrinkTunnelSpeed;
                    _timeInShrinkTunnel = (64f + X - ((float)Tile.X * 64f + 32f)) / Constants.ShrinkTunnelSpeed;

                    //Entering from top (going down)
                }
                else if (Tile.Collision == CollisionTile.SHRINK_TUNNEL_VERTICAL && Facing == Direction.Down || Facing == Direction.DownLeft || Facing == Direction.DownRight)
                {
                    DY = Constants.ShrinkTunnelSpeed;
                    _timeInShrinkTunnel = (64f - Y + ((float)Tile.Y * 64f + 32f)) / Constants.ShrinkTunnelSpeed;

                    //Entering from bottom (going up)
                }
                else if (Tile.Collision == CollisionTile.SHRINK_TUNNEL_VERTICAL && Facing == Direction.Up || Facing == Direction.UpLeft || Facing == Direction.UpRight)
                {
                    DY = -Constants.ShrinkTunnelSpeed;
                    _timeInShrinkTunnel = (64f + Y - ((float)Tile.Y * 64f + 32f)) / Constants.ShrinkTunnelSpeed;
                }
            }

            //Continue moving through shrink tunnel - move towards the center of the square
            if (IsInShrinkTunnel)
            {
                if (Tile.Collision == CollisionTile.SHRINK_TUNNEL_VERTICAL)
                {
                    if (X < Tile.X * 64 + 31)
                    {
                        X += 80.0f * dt;
                    }
                    else if (X > Tile.X * 64 + 33)
                    {
                        X -= 80.0f * dt;
                    }
                }
                else if (Tile.Collision == CollisionTile.SHRINK_TUNNEL_HORIZONTAL)
                {
                    if (Y < Tile.Y * 64 + 31)
                    {
                        Y += 80f * dt;
                    }
                    else if (Y > Tile.Y * 64 + 33)
                    {
                        Y -= 80f * dt;
                    }
                }
            }

            //Exit shrink tunnel
            if (SMH.GameTimePassed(_timeEnteredShrinkTunnel, _timeInShrinkTunnel))
            {
                IsInShrinkTunnel = false;
            }
        }

        #endregion

        #region Ability Stuff

        /// <summary>
        /// Some day there might be a leet framework, but for now there is just this
        ///  shitty method.
        /// </summary>
        /// <param name="dt"></param>
        private void DoAbilities(float dt)
        {
            //Base requirements for being allowed to use an ability
            bool canUseAbility = !IsWaterWalking && !IsFalling && !IsSpringing && !IsFrozen
                && !IsDrowning && !IsSpringing && HoveringYOffset == 0f;

            IsSprinting = (canUseAbility && SMH.Input.IsDown(Input.Ability) &&
                 SMH.GUI.SelectedAbility == Ability.SPRINT_BOOTS &&
                 Tile.Collision != CollisionTile.LEFT_ARROW &&
                 Tile.Collision != CollisionTile.RIGHT_ARROW &&
                 Tile.Collision != CollisionTile.UP_ARROW &&
                 Tile.Collision != CollisionTile.DOWN_ARROW &&
                 Tile.Collision != CollisionTile.ICE);

            DoHover(dt);
            DoReflectionShield(dt, canUseAbility);
            DoTutsMask(dt, canUseAbility);
            DoFrisbee(dt, canUseAbility);
            DoFireBreath(dt, canUseAbility);
            DoIceBreath(dt, canUseAbility);
            DoTriggeredAbilities(dt, canUseAbility);
            DoShrink(dt, canUseAbility);
            DoCane(dt, canUseAbility);
        }

        /// <summary>
        /// Updates hover related stuff.
        /// </summary>
        private void DoHover(float dt)
        {
            bool wasHovering = IsHovering;
            IsHovering = ((IsHovering || Tile.Collision == CollisionTile.HOVER_PAD) &&
                    SMH.GUI.SelectedAbility == Ability.HOVER &&
                    SMH.Input.IsDown(Input.Ability));

            //Start hovering
            if (!wasHovering && IsHovering)
            {
                _timeStartedHovering = SMH.GameTime;
            }

            //Continue hovering
            if (IsHovering)
            {
                if (SMH.GameTimePassed(_timeStartedHovering, Constants.HoverDuration))
                    IsHovering = false;

                if (HoverScale < 1.2f) HoverScale += 0.4f * dt;
                if (HoverScale > 1.2f) HoverScale = 1.2f;
                if (HoveringYOffset < 20.0f) HoveringYOffset += 40f * dt;
                if (HoveringYOffset > 20.0f) HoveringYOffset = 20.0f;
            }
            else
            {
                if (HoverScale > 1.0f) HoverScale -= 0.4f * dt;
                if (HoverScale < 1.0f) HoverScale = 1.0f;
                if (HoveringYOffset > 0.0f) HoveringYOffset -= 40.0f * dt;
                if (HoveringYOffset < 0.0f) HoveringYOffset = 0.0f;
            }
        }

        /// <summary>
        /// Updates reflection shield stuff
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="canUseAbility"></param>
        private void DoReflectionShield(float dt, bool canUseAbility)
        {
            if (canUseAbility && SMH.Input.IsDown(Input.Ability) && SMH.GUI.SelectedAbility == Ability.REFLECTION_SHIELD &&
                Mana >= SMH.Data.Abilities[Ability.REFLECTION_SHIELD].ManaCost * dt)
            {
                if (!IsReflectionShieldActive)
                {
                    SMH.Sound.StartLoopingSound(Sound.ReflectionShield);
                    IsReflectionShieldActive = true;
                }
                Mana -= SMH.Data.Abilities[Ability.REFLECTION_SHIELD].ManaCost * dt;
                _usingManaItem = true;
                _timeLastUsedMana = SMH.GameTime;
            }
            else if (IsReflectionShieldActive)
            {
                IsReflectionShieldActive = false;
                SMH.Sound.StopLoopingSound(Sound.ReflectionShield);
            }
        }

        /// <summary>
        /// Updates Tut's mask related stuff.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="canUseAbility"></param>
        private void DoTutsMask(float dt, bool canUseAbility)
        {
            if (canUseAbility && SMH.Input.IsDown(Input.Ability) &&
               SMH.GUI.SelectedAbility == Ability.TUTS_MASK &&
               Mana >= SMH.Data.Abilities[Ability.TUTS_MASK].ManaCost * dt)
            {
                if (!IsCloaked)
                {
                    IsCloaked = true;
                    SMH.Sound.PlaySound(Sound.StartTut);
                }
                Mana -= SMH.Data.Abilities[Ability.TUTS_MASK].ManaCost * dt;
                _usingManaItem = true;
                _timeLastUsedMana = SMH.GameTime;
            }
            else if (IsCloaked)
            {
                IsCloaked = false;
                SMH.Sound.PlaySound(Sound.EndTut);
            }
        }

        /// <summary>
        /// Updates Frisbee related stuff.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="canUseAbility"></param>
        private void DoFrisbee(float dt, bool canUseAbility)
        {
            if (canUseAbility && SMH.GUI.SelectedAbility == Ability.FRISBEE)
            {
                if (SMH.Input.IsDown(Input.Ability) && !IsChargingFrisbee)
                {
                    _frisbeePower = 0;
                    IsChargingFrisbee = true;
                }
                if (IsChargingFrisbee) _frisbeePower = Math.Min(Constants.MaxFrisbeePower, _frisbeePower + (Constants.MaxFrisbeePower / 2f) * dt);

                //release frisbee
                if (IsChargingFrisbee && !SMH.Input.IsDown(Input.Ability))
                {
                    if (!SMH.ProjectileManager.IsFrisbeeActive)
                    { //no frisbee out there, so throw a frisbee
                        SMH.ProjectileManager.AddFrisbee(X, Y, 400f, Constants.SmileyAngles[Facing] - .5f * (float)Math.PI, _frisbeePower > (Constants.MaxFrisbeePower / 10f) ? _frisbeePower : 0f);
                        IsChargingFrisbee = false;
                    }
                    else
                    { //already a frisbee, so just stop charging
                        IsChargingFrisbee = false;
                        _frisbeePower = 0;
                    }
                }
            }
            if (SMH.GUI.SelectedAbility != Ability.FRISBEE)
            {
                _frisbeePower = 0;
                IsChargingFrisbee = false;
            }
        }

        /// <summary>
        /// Update fire breath related stuff.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="canUseAbility"></param>
        private void DoFireBreath(float dt, bool canUseAbility)
        {
            if (canUseAbility && SMH.GUI.SelectedAbility == Ability.FIRE_BREATH && SMH.Input.IsDown(Input.Ability) &&
                Mana >= SMH.Data.Abilities[Ability.FIRE_BREATH].ManaCost * (IsBreathingFire ? dt : .25f))
            {
                Mana -= SMH.Data.Abilities[Ability.FIRE_BREATH].ManaCost * dt;
                _usingManaItem = true;
                _timeLastUsedMana = SMH.GameTime;

                //Start breathing fire
                if (!IsBreathingFire)
                {
                    IsBreathingFire = true;
                    //TODO: fireBreathParticle->FireAt(smh->getScreenX(x) + mouthXOffset[facing], smh->getScreenY(y) + mouthYOffset[facing]);
                    SMH.Sound.StartLoopingSound(Sound.FireBreath);
                }

                //Update breath direction and location TODO:
                //fireBreathParticle->info.fDirection = angles[facing];
                //fireBreathParticle->MoveTo(smh->getScreenX(x) + mouthXOffset[facing], smh->getScreenY(y) + mouthYOffset[facing], false);
            }
            else if (IsBreathingFire)
            {
                //Stop breathing fire
                StopFireBreath();
                _timeStoppedBreathingFire = SMH.GameTime;
            }

            //TODO:fireBreathParticle->Update(dt);
        }

        /// <summary>
        /// Updates triggered abilities.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="canUseAbility"></param>
        private void DoTriggeredAbilities(float dt, bool canUseAbility)
        {
            if (!SMH.Input.IsDown(Input.Ability) || !canUseAbility)
                return;

            //Shoot lightning orbs
            if (SMH.GUI.SelectedAbility == Ability.LIGHTNING_ORB && Mana >= SMH.Data.Abilities[Ability.LIGHTNING_ORB].ManaCost &&
                SMH.GameTimePassed(_lastOrb, Constants.SwitchDelay))
            {
                Mana -= SMH.Data.Abilities[Ability.LIGHTNING_ORB].ManaCost;
                _timeLastUsedMana = SMH.GameTime;
                _lastOrb = SMH.GameTime;
                SMH.Sound.PlaySound(Sound.LightningOrb);
                SMH.ProjectileManager.AddProjectile(X, Y, 700f, Constants.SmileyAngles[Facing] - .5f * (float)Math.PI, LightningOrbDamage, false, false, ProjectileType.LightningOrb, true);
            }

            //Start using cane
            if (SMH.GUI.SelectedAbility == Ability.CANE && !IsUsingCane && Mana >= SMH.Data.Abilities[Ability.CANE].ManaCost)
            {
                IsUsingCane = true;
                //TODO: smh->resources->GetParticleSystem("smileysCane")->FireAt(smh->getScreenX(x), smh->getScreenY(y));
                _timeStartedCane = SMH.GameTime;
            }

            //Place Silly Pad
            if (SMH.GUI.SelectedAbility == Ability.SILLY_PAD && Mana >= SMH.Data.Abilities[Ability.SILLY_PAD].ManaCost)
            {
                SMH.Environment.PlaceSillyPad(Tile.X, Tile.Y);
                Mana -= SMH.Data.Abilities[Ability.SILLY_PAD].ManaCost;
                _timeLastUsedMana = SMH.GameTime;
            }

            //Toggle shrink mode
            if (SMH.GUI.SelectedAbility == Ability.SHRINK)
            {
                _isShrinkActive = !_isShrinkActive;
                if (_isShrinkActive)
                {
                    SMH.Sound.PlaySound(Sound.Shrink);
                }
                else
                {
                    SMH.Sound.PlaySound(Sound.DeShrink);
                }
            }
        }

        /// <summary>
        /// Updates ice breath related stuff.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="canUseAbility"></param>
        private void DoIceBreath(float dt, bool canUseAbility)
        {
            if (IsBreathingIce)
            {
                //TODO:
                //iceBreathParticle->info.fDirection = angles[facing];
                //iceBreathParticle->MoveTo(smh->getScreenX(x) + mouthXOffset[facing], smh->getScreenY(y) + mouthYOffset[facing], false);
                //iceBreathParticle->Update(dt);

                if (SMH.GameTimePassed(_startedIceBreath, 0.6f))
                {
                    //TODO:iceBreathParticle->Stop(false);
                }
                if (SMH.GameTimePassed(_startedIceBreath, 1.2f))
                {
                    IsBreathingIce = false;
                }
            }
            else if (SMH.GUI.SelectedAbility == Ability.ICE_BREATH && SMH.GameTimePassed(_startedIceBreath, 1.5f) && Mana >= SMH.Data.Abilities[Ability.ICE_BREATH].ManaCost)
            {
                //Start ice breath
                Mana -= SMH.Data.Abilities[Ability.ICE_BREATH].ManaCost;
                _timeLastUsedMana = SMH.GameTime;
                SMH.Sound.PlaySound(Sound.IceBreath);
                _startedIceBreath = SMH.GameTime;
                //TODO:iceBreathParticle->FireAt(smh->getScreenX(x) + mouthXOffset[facing], smh->getScreenY(y) + mouthYOffset[facing]);
                IsBreathingIce = true;
            }
        }

        /// <summary>
        /// Updates shrink related stuff.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="canUseAbility"></param>
        private void DoShrink(float dt, bool canUseAbility)
        {
            if (!canUseAbility)
                return;

            //If you change abilities while shrunk you lose shrink
            if (_isShrinkActive) _isShrinkActive = (SMH.GUI.SelectedAbility == Ability.SHRINK);

            //Shrinking
            if (_isShrinkActive && ShrinkScale > .5f)
            {
                ShrinkScale -= 1.0f * dt;
                if (ShrinkScale < .5f) ShrinkScale = .5f;
                //Unshrinking
            }
            else if (!_isShrinkActive && ShrinkScale < 1.0f)
            {
                ShrinkScale += 1.0f * dt;
                if (ShrinkScale > 1.0f) ShrinkScale = 1.0f;

                //While unshrinking push Smiley away from any adjacent walls
                if (!CanPass(SMH.Environment.Tiles[Tile.X - 1, Tile.Y].Collision) && (int)X % 64 < Radius)
                {
                    X += Radius - ((int)X % 64) + 1;
                }
                if (!CanPass(SMH.Environment.Tiles[Tile.X + 1, Tile.Y].Collision) && (int)X % 64 > 64 - Radius)
                {
                    X -= Radius - (64 - (int)X % 64) + 1;
                }
                if (!CanPass(SMH.Environment.Tiles[Tile.X, Tile.Y - 1].Collision) && (int)Y % 64 < Radius)
                {
                    Y += Radius - ((int)Y % 64) + 1;
                }
                if (!CanPass(SMH.Environment.Tiles[Tile.X, Tile.Y + 1].Collision) && (int)Y % 64 > 64 - Radius)
                {
                    Y -= Radius - (64 - (int)Y % 64) + 1;
                }

                //Adjacent corners
                //Up-Left
                if (!CanPass(SMH.Environment.Tiles[Tile.X - 1, Tile.Y - 1].Collision))
                {
                    if ((int)X % 64 < Radius && (int)Y % 64 < Radius)
                    {
                        X += Radius - ((int)X % 64) + 1;
                        Y += Radius - ((int)Y % 64) + 1;
                    }
                }
                //Up-Right
                if (!CanPass(SMH.Environment.Tiles[Tile.X + 1, Tile.Y - 1].Collision))
                {
                    if ((int)X % 64 > 64 - Radius && (int)Y % 64 < Radius)
                    {
                        X -= Radius - (64 - (int)X % 64) + 1;
                        Y += Radius - ((int)Y % 64) + 1;
                    }
                }
                //Down-Left
                if (!CanPass(SMH.Environment.Tiles[Tile.X - 1, Tile.Y + 1].Collision))
                {
                    if ((int)X % 64 < Radius && (int)Y % 64 > 64 - Radius)
                    {
                        X += Radius - ((int)X % 64) + 1;
                        Y -= Radius - (64 - (int)Y % 64) + 1;
                    }
                }
                //Down-Right
                if (!CanPass(SMH.Environment.Tiles[Tile.X + 1, Tile.Y + 1].Collision))
                {
                    if ((int)X % 64 > 64 - Radius && (int)Y % 64 > 64 - Radius)
                    {
                        X -= Radius - (64 - (int)X % 64) + 1;
                        Y -= Radius - (64 - (int)Y % 64) + 1;
                    }
                }

            }
            Radius = Constants.DefaultSmileyRadius * ShrinkScale;
        }

        /// <summary>
        /// Updates can related stuff.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="canUseAbility"></param>
        private void DoCane(float dt, bool canUseAbility)
        {
            //TODO:smh->resources->GetParticleSystem("smileysCane")->Update(dt);
            if (IsUsingCane)
            {
                //The cane usage gets interrupted if Smiley moves
                if (!SMH.Input.IsDown(Input.Ability) || SMH.Input.IsDown(Input.Left) ||
                     SMH.Input.IsDown(Input.Right) || SMH.Input.IsDown(Input.Up) ||
                     SMH.Input.IsDown(Input.Down) || DX > 0.0 || DY > 0.0)
                {
                    IsUsingCane = false;
                    //TODO:smh->resources->GetParticleSystem("smileysCane")->Stop(false);
                }

                //Summon Bill Clinton after using the cane for the required amount of time
                if (SMH.GameTimePassed(_timeStartedCane, Constants.CaneTime))
                {
                    //TODO:smh->resources->GetParticleSystem("smileysCane")->Stop(false);
                    IsUsingCane = false;
                    Facing = Direction.Down;
                    Mana -= SMH.Data.Abilities[Ability.CANE].ManaCost;
                    _timeLastUsedMana = SMH.GameTime;
                    if (Mana < 0) Mana = 0;
                    SMH.WindowManager.OpenHintTextBox();
                }
            }
        }

        #endregion
    }
}
