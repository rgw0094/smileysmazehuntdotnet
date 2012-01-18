using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Data;
using Smiley.Lib.Enums;
using Smiley.Lib.Framework;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.GameObjects.Player
{
    public class Tongue
    {
        #region Private Variables

        private const float AttackRadius = (float)Math.PI / 3f;
        private const int NumCollisionPoints = 9;
        private const float TongueLength = 65f;
        private const int NumFrames = 12;

        private Rect _collisionBox;
        private float _timeStartedAttack = -10f;
        private bool _hasActivatedSomething;
        private TongueState _state;
        private float _tongueOffsetAngle;

        #endregion

        #region Properties

        /// <summary>
        /// Gets whether or not the Tongue is currently attacking.
        /// </summary>
        public bool IsAttacking
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts swinging the tongue at shit.
        /// </summary>
        public void StartAttack()
        {
            if (IsAttacking || SMH.WindowManager.IsTextBoxOpen)
                return;

            SMH.Sound.PlaySound(GetRandomTongueSound());

            SMH.SaveManager.CurrentSave.NumTongueLicks++;
            _hasActivatedSomething = false;
            _timeStartedAttack = SMH.GameTime;
            _tongueOffsetAngle = -AttackRadius / 2f;
            IsAttacking = true;
            Animations.SmileyTongue.Reverse = false;
            Animations.SmileyTongue.Play();
            _state = TongueState.Extending;
        }

        /// <summary>
        /// Stops the tongue mid swing.
        /// </summary>
        public void StopAttack()
        {
            IsAttacking = false;
            _timeStartedAttack = SMH.GameTime;
        }

        /// <summary>
        /// Updates the Tongue.
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {
            if (!IsAttacking) return;

            //Hit Enemies
            SMH.EnemyManager.TongueCollision(this, SMH.Player.Damage);

            //Activate stuff - only one thing can be activated per attack
            if (!_hasActivatedSomething)
            {
                if (SMH.Environment.ToggleSwitches(this) ||
                    SMH.NPCManager.TalkToNPCs(this) ||
                    (!SMH.WindowManager.IsWindowOpen && SMH.Environment.HitSaveShrine(this)) ||
                    (!SMH.WindowManager.IsTextBoxOpen && SMH.Environment.HitSigns(this)))
                {
                    _hasActivatedSomething = true;
                }
            }

            switch (_state)
            {
                case TongueState.Extending:
                    Animations.SmileyTongue.Update(dt);

                    //Once the tongue is fully extended enter swinging state
                    if (Animations.SmileyTongue.ActiveFrame == NumFrames - 1)
                    {
                        _state = TongueState.Swinging;
                    }
                    break;

                case TongueState.Swinging:
                    _tongueOffsetAngle += 8f * (float)Math.PI * dt;

                    //When tongue finishes swinging, start retracting it
                    if (_tongueOffsetAngle > AttackRadius / 2f)
                    {
                        _tongueOffsetAngle = AttackRadius / 2f;
                        Animations.SmileyTongue.ActiveFrame = NumFrames - 1;
                        Animations.SmileyTongue.Reverse = true;
                        Animations.SmileyTongue.Play();
                        _state = TongueState.Retracting;
                    }
                    break;

                case TongueState.Retracting:
                    Animations.SmileyTongue.Update(dt);

                    //Once the tongue is fully retracted the attack is done
                    if (Animations.SmileyTongue.ActiveFrame < 1)
                    {
                        IsAttacking = false;
                    }
                    break;
            }
        }

        /// <summary>
        /// Draws the tongue.
        /// </summary>
        public void Draw()
        {
            if (!IsAttacking) return;

            SMH.Graphics.DrawAnimation(Animations.SmileyTongue,
                SMH.Player.ScreenX + Constants.MouthPositions[SMH.Player.Facing].X,
                SMH.Player.ScreenY - SMH.Player.SpringOffset + Constants.MouthPositions[SMH.Player.Facing].Y - SMH.Player.HoveringYOffset,
                Color.White,
                Constants.SmileyAngles[SMH.Player.Facing] + (SMH.Player.Facing == Direction.Left ? -1f : 1f) * _tongueOffsetAngle,
                SMH.Player.Scale);

            //Draw tongue collision for debug mode
            //if (smh->isDebugOn()) {
            //    numPoints = int((float)smh->resources->GetAnimation("smileyTongue")->GetFrame() / (float)smh->resources->GetAnimation("smileyTongue")->GetFrames() * (float)NUM_COLLISION_POINTS) + 1;
            //    for (int i = 0; i < numPoints; i++) {
            //        testAngle = -(PI / 2.0) + smh->player->angles[smh->player->facing] + (smh->player->facing == LEFT ? -1 : 1) * tongueOffsetAngle;
            //        pointX = smh->player->x + smh->player->mouthXOffset[smh->player->facing] + (i+1)*(TONGUE_LENGTH / (NUM_COLLISION_POINTS-1)) * cos(testAngle);
            //        pointY = smh->player->y + smh->player->mouthYOffset[smh->player->facing] + (i+1)*(TONGUE_LENGTH / (NUM_COLLISION_POINTS-1)) * sin(testAngle);
            //        collisionBox->SetRadius(pointX, pointY, 5.0);
            //        smh->drawCollisionBox(collisionBox, Colors::GREEN);
            //    }
            //}
        }

        /// <summary>
        /// Returns whether or not the tongue intersects with a rectangle.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool Intersects(Rect rect)
        {
            if (!IsAttacking)
                return false;

            foreach (Vector2 v in GetCollisionPoints())
            {
                if (rect.Contains(v))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns whether or not the tongue intersects with a circle.
        /// </summary>
        /// <param name="collisionCircle"></param>
        /// <returns></returns>
        public bool Intersects(CollisionCircle collisionCircle)
        {
            if (!IsAttacking)
                return false;

            foreach (Vector2 v in GetCollisionPoints())
            {
                if (collisionCircle.Contains(v.X, v.Y))
                    return true;
            }

            return false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the list of points to test for collision.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Vector2> GetCollisionPoints()
        {
            int numPoints = Convert.ToInt32(((float)Animations.SmileyTongue.ActiveFrame / (float)Animations.SmileyTongue.NumFrames) * (float)NumCollisionPoints) + 1;
            for (int i = 0; i < numPoints; i++)
            {
                float testAngle = -((float)Math.PI / 2f) + Constants.SmileyAngles[SMH.Player.Facing] + (SMH.Player.Facing == Direction.Left ? -1f : 1f) * _tongueOffsetAngle;

                yield return new Vector2(
                    SMH.Player.X + Constants.MouthPositions[SMH.Player.Facing].X + ((float)i + 1f) * (TongueLength / ((float)NumCollisionPoints - 1f)) * (float)Math.Cos(testAngle),
                    SMH.Player.Y + Constants.MouthPositions[SMH.Player.Facing].Y + ((float)i + 1f) * (TongueLength / ((float)NumCollisionPoints - 1f)) * (float)Math.Sin(testAngle));
            }
        }

        private Sound GetRandomTongueSound()
        {
            switch (SMH.Random.Next(1, 5))
            {
                case 1:
                    return Sound.Lick1;
                case 2:
                    return Sound.Lick2;
                case 3:
                    return Sound.Lick3;
                case 4:
                    return Sound.Lick4;
                case 5:
                    return Sound.Lick5;
                default: throw new Exception("impossible state");
            }
        }

        private enum TongueState
        {
            Extending,
            Swinging,
            Retracting
        }

        #endregion
    }
}
