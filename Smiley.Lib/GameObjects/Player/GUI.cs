using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;
using Smiley.Lib.Data;
using Smiley.Lib.Util;

namespace Smiley.Lib.GameObjects.Player
{
    public class AbilitySlot
    {
        public Ability Ability { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Scale { get; set; }
        public int Slot { get; set; }
    }

    public enum SpinDirection
    {
        None,
        Left,
        Right
    }

    public class GUI
    {
        private const float SmallScale = 0.6f;

        #region Private Variables

        private AbilitySlot[] _abilitySlots = new AbilitySlot[3];
        private Vector2[] _points = new Vector2[3];

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new GUI.
        /// </summary>
        public GUI()
        {
            _points[0] = new Vector2(33, 115);
            _points[1] = new Vector2(79, 57);
            _points[2] = new Vector2(122, 115);

            for (int i = 0; i < 3; i++)
            {
                _abilitySlots[i] = new AbilitySlot();
                _abilitySlots[i].Ability = Ability.NO_ABILITY;
                _abilitySlots[i].Slot = i;
                _abilitySlots[i].X = _points[i].X;
                _abilitySlots[i].Y = _points[i].Y;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the selected ability. The selected ability is the one in the middle.
        /// </summary>
        /// <returns></returns>
        public Ability SelectedAbility
        {
            get { return GetAbilityInSlot(1); }
        }

        /// <summary>
        /// Gets the ability in the given slot.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public Ability GetAbilityInSlot(int slot)
        {
            for (int i = 0; i < 3; i++)
            {
                if (_abilitySlots[i].Slot == slot)
                    return _abilitySlots[i].Ability;
            }
            return Ability.NO_ABILITY;
        }

        /// <summary>
        /// Sets the ability in the specified slot.
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="slot"></param>
        public void SetAbilityInSlot(Ability ability, int slot)
        {
            _abilitySlots.Single(s => s.Slot == slot).Ability = ability;
        }

        /// <summary>
        /// Returns whether or not the specified ability is one of the ones available in the GUI.
        /// </summary>
        /// <param name="ability"></param>
        /// <returns></returns>
        public bool IsAbilityAvailable(Ability ability)
        {
            return _abilitySlots.Any(slot => slot.Ability == ability);
        }

        /// <summary>
        /// Returns the number of available abilities.
        /// </summary>
        /// <returns></returns>
        public int NumAvailableAbilities()
        {
            return _abilitySlots.Count(slot => slot.Ability != Ability.NO_ABILITY);
        }

        /// <summary>
        /// Adds a new ability and pushes out the oldest one
        /// </summary>
        /// <param name="ability"></param>
        public void ToggleAvailableAbility(Ability ability)
        {
            //If the ability is available, remove it
            for (int i = 0; i < 3; i++)
            {
                if (_abilitySlots[i].Ability == ability)
                {
                    _abilitySlots[i].Ability = Ability.NO_ABILITY;
                    SMH.Sound.PlaySound(Sound.AbilityDeSelect);
                    return;
                }
            }

            //Otherwise, add it.
            if (_abilitySlots[1].Ability == Ability.NO_ABILITY)
            {
                _abilitySlots[1].Ability = ability;
                SMH.Sound.PlaySound(Sound.AbilitySelect);
                return;
            }
            if (_abilitySlots[0].Ability == Ability.NO_ABILITY)
            {
                _abilitySlots[0].Ability = ability;
                SMH.Sound.PlaySound(Sound.AbilitySelect);
                return;
            }
            if (_abilitySlots[2].Ability == Ability.NO_ABILITY)
            {
                _abilitySlots[2].Ability = ability;
                SMH.Sound.PlaySound(Sound.AbilitySelect);
                return;
            }

            //If we got here then there is no room for the ability!
            SMH.Sound.PlaySound(Sound.Error);
        }

        public void Update(float dt)
        {
            CollisionTile collisionAtPlayer = SMH.Player.Tile.Collision;
            SpinDirection dir = SpinDirection.None;

            //Input to change ability
            if (!SMH.WindowManager.IsAnyWindowOpen)
            {
                if (SMH.Input.IsPressed(Input.PreviousAbility))
                    dir = SpinDirection.Left;
                else if (SMH.Input.IsPressed(Input.NextAbility))
                    dir = SpinDirection.Right;
            }

            if (dir != SpinDirection.None)
            {
                if (SelectedAbility == Ability.WATER_BOOTS && SMH.Player.IsSmileyTouchingWater())
                {
                    if (collisionAtPlayer != CollisionTile.DEEP_WATER && collisionAtPlayer != CollisionTile.GREEN_WATER)
                    {
                        //player is on a land tile, but touching water; bump him over and change abilities
                        ChangeAbility(dir);
                        SMH.Player.GraduallyMoveTo(SMH.Player.Tile.X * 64f + 32f, SMH.Player.Tile.Y * 64f + 32f, 500);
                    }
                    else
                    {
                        //player actually on a water tile; cannot take off the sandals; play error message
                        SMH.Sound.PlaySound(Sound.Error);
                    }
                }
                else
                {
                    ChangeAbility(dir);
                }
            }

            float angle, x, y, targetX, targetY;
            for (int i = 0; i < 3; i++)
            {
                //Move towards target slot
                x = _abilitySlots[i].X;
                y = _abilitySlots[i].Y;
                targetX = _points[_abilitySlots[i].Slot].Y;
                targetY = _points[_abilitySlots[i].Slot].Y;
                angle = SmileyUtil.GetAngleBetween(x, y, targetX, targetY);
                if (SmileyUtil.Distance(x, y, targetX, targetY) < 600.0 * dt)
                {
                    _abilitySlots[i].X = targetX;
                    _abilitySlots[i].Y = targetY;
                }
                else
                {
                    _abilitySlots[i].X += 600f * (float)Math.Cos(angle) * dt;
                    _abilitySlots[i].Y += 600f * (float)Math.Sin(angle) * dt;
                }

                //Move towards correct size
                if (_abilitySlots[i].Slot == 1 && _abilitySlots[i].Scale < 1f)
                {
                    _abilitySlots[i].Scale += 3f * dt;
                    if (_abilitySlots[i].Scale > 1f)
                    {
                        _abilitySlots[i].Scale = 1f;
                    }
                }
                else if (_abilitySlots[i].Slot != 1 && _abilitySlots[i].Scale > SmallScale)
                {
                    _abilitySlots[i].Scale -= 3f * dt;
                    if (_abilitySlots[i].Scale < SmallScale)
                    {
                        _abilitySlots[i].Scale = SmallScale;
                    }
                }
            }
        }

        public void Draw()
        {
            int drawX, drawY;

            //Draw health
            for (int i = 1; i <= SMH.Player.MaxHealth; i++)
            {
                drawX = (i <= 10) ? 120 + i * 35 : 120 + (i - 10) * 35;
                drawY = (i <= 10) ? 25 : 70;
                if (SMH.Player.Health >= i)
                    SMH.Graphics.DrawSprite(Sprites.FullHealth, drawX, drawY);
                else if (SMH.Player.Health < i && SMH.Player.Health >= i - .25)
                    SMH.Graphics.DrawSprite(Sprites.ThreeQuartersHealth, drawX, drawY);
                else if (SMH.Player.Health < i - .25 && SMH.Player.Health >= i - .5)
                    SMH.Graphics.DrawSprite(Sprites.HalfHealth, drawX, drawY);
                else if (SMH.Player.Health < i - .5 && SMH.Player.Health >= i - .75)
                    SMH.Graphics.DrawSprite(Sprites.QuarterHealth, drawX, drawY);
                else
                    SMH.Graphics.DrawSprite(Sprites.EmptyHealth, drawX, drawY);
            }

            //Draw mana bar
            drawX = 155;
            drawY = SMH.Player.MaxMana < 11 ? 65 : 110;
            float manaBarSizeMultiplier = (1f + .15f * SMH.SaveManager.CurrentSave.NumUpgrades[Upgrade.Mana]) * 0.96f; //adjust the size multiplier so max mana bar is the same width as max hearts

            SMH.Graphics.DrawCroppedSprite(Sprites.ManaBarBackgroundCenter, drawX + 4, drawY, new Rectangle(675, 282, Convert.ToInt32(115f * manaBarSizeMultiplier - 4f), 22));
            SMH.Graphics.DrawCroppedSprite(Sprites.ManaBar, drawX + 4, drawY + 3, new Rectangle(661, 304, Convert.ToInt32(115f * (SMH.Player.Mana / SMH.Player.MaxMana) * manaBarSizeMultiplier), 15));
            SMH.Graphics.DrawSprite(Sprites.ManaBarBackgroundLeftTip, drawX, drawY);
            SMH.Graphics.DrawSprite(Sprites.ManaBarBackgroundRightTip, drawX + 115f * manaBarSizeMultiplier - 2f, drawY);

            //Draw abilities
            SMH.Graphics.DrawSprite(Sprites.AbilityBackground, 5f, 12f);
            foreach (AbilitySlot slot in _abilitySlots)
            {
                if (slot.Ability != Ability.NO_ABILITY)
                {
                    SMH.Graphics.DrawSprite(SpriteSets.Abilities[(int)slot.Ability], slot.X, slot.Y, Color.White, 0f, slot.Scale);
                }
            }

            //Draw keys
            int keyIndex = SmileyUtil.GetKeyIndex(SMH.SaveManager.CurrentSave.Level);
            if (keyIndex != -1)
            {
                SMH.Graphics.DrawSprite(Sprites.KeyBackground, 748, 714);

                int keyXOffset = 763;
                int keyYOffset = 724;
                for (int i = 0; i < 4; i++)
                {
                    //Draw key icon
                    SMH.Graphics.DrawSprite(SpriteSets.KeyIcons[i], keyXOffset + 60 * i, keyYOffset);

                    //Draw num keys
                    SMH.Graphics.DrawString(SmileyFont.Number, SMH.SaveManager.CurrentSave.NumKeys[keyIndex, i].ToString(),
                        keyXOffset + 60 * i + 45, keyYOffset + 5, TextAlignment.Left);
                }
            }

            //Show whether or not Smiley is invincible
            if (SMH.Player.IsInvincible)
                SMH.Graphics.DrawString(SmileyFont.Curlz, "Invincibility On", 512, 3, TextAlignment.Center);

            //Smiley progress bar
            Rectangle? r = SMH.Player.GetProgressBar();
            if (r != null)
            {
                SMH.Graphics.DrawSprite(Sprites.BossHealthBar, r.Value, Color.White);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Cycles through the available abilities, skipping ones which are PASSIVE
        /// </summary>
        /// <param name="direction"></param>
        private void ChangeAbility(SpinDirection direction)
        {
            SMH.Sound.PlaySound(Sound.SwitchItem);

            //Stop old ability//TODO:
            //smh->player->fireBreathParticle->Stop(false);
            //smh->player->iceBreathParticle->Stop(false);
            SMH.Player.DeShrink();

            int a = 0;
            int b = 0;

            for (int i = 0; i < 3; i++)
            {
                if (direction == SpinDirection.Left)
                {
                    if (_abilitySlots[i].Slot == 0) a = i;
                    if (_abilitySlots[i].Slot == 1) b = i;
                }
                else if (direction == SpinDirection.Right)
                {
                    if (_abilitySlots[i].Slot == 1) a = i;
                    if (_abilitySlots[i].Slot == 2) b = i;
                }
            }

            int temp = _abilitySlots[a].Slot;
            _abilitySlots[a].Slot = _abilitySlots[b].Slot;
            _abilitySlots[b].Slot = temp;
        }

        #endregion
    }
}
