using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Data;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;
using Smiley.Lib.Util;
using Microsoft.Xna.Framework.Graphics;
using Smiley.Lib.Framework.Drawing;

namespace Smiley.Lib.UI.Windows
{
    public class Inventory : BaseWindow
    {
        private const float SquareSize = 70f;
        private const int Height = 3;
        private const int Width = 4;
        private const float InventoryXOffset = (1024 - 660) / 2;
        private const float InventoryYOffset = (768 - 492) / 2;

        private int _cursorX;
        private int _cursorY;

        public override bool Update(float dt)
        {
            //Process Input to move cursor
            if (SMH.Input.IsPressed(Input.Left))
            {
                if (_cursorX > 0)
                {
                    _cursorX--;
                    SMH.Sound.PlaySound(Sound.MouseOver);
                }
            }
            if (SMH.Input.IsPressed(Input.Right))
            {
                if (_cursorX < Width - 1)
                {
                    _cursorX++;
                    SMH.Sound.PlaySound(Sound.MouseOver);
                }
            }
            if (SMH.Input.IsPressed(Input.Up))
            {
                if (_cursorY > 0)
                {
                    _cursorY--;
                    SMH.Sound.PlaySound(Sound.MouseOver);
                }
            }
            if (SMH.Input.IsPressed(Input.Down))
            {
                if (_cursorY < Height- 1)
                {
                    _cursorY++;
                    SMH.Sound.PlaySound(Sound.MouseOver);
                }
            }

            if (SMH.Input.IsPressed(Input.Attack) && SMH.SaveManager.CurrentSave.HasAbility[(Ability)(_cursorY * 4 + _cursorX)])
            {
                SMH.GUI.ToggleAvailableAbility((Ability)(_cursorY * 4 + _cursorX));
            }

            return true;
        }

        public override void Draw()
        {
            //Shade the screen behind the inventory
            SMH.Graphics.DrawRect(new Rect(0, 0, 1024, 768), Color.FromNonPremultiplied(0, 0, 0, 80), true);

            //Draw the inventory background
            SMH.Graphics.DrawSprite(Sprites.Inventory, InventoryXOffset, InventoryYOffset);

            //Ability grid
            int drawX, drawY;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Ability ability = (Ability)(j * 4 + i);
                    if (SMH.SaveManager.CurrentSave.HasAbility[ability])
                    {
                        drawX = Convert.ToInt32(InventoryXOffset + 40f + i * SquareSize + 32f);
                        drawY = Convert.ToInt32(InventoryYOffset + 40f + j * SquareSize + 32f);

                        SMH.Graphics.DrawSprite(SpriteSets.Abilities[j * 4 + i], drawX, drawY);

                        //Draw the ability name and info if it is highlighted
                        if (_cursorX == i && _cursorY == j)
                        {
                            SMH.Graphics.DrawString(SmileyFont.AbilityTitle, SMH.Data.Abilities[ability].Name, InventoryXOffset + 170, InventoryYOffset + 275, TextAlignment.Center, Color.White);
                            SMH.Graphics.DrawString(SmileyFont.Segoe14, SMH.Data.GetAbilityDescription(ability), InventoryXOffset + 40, InventoryYOffset + 340, TextAlignment.Left, Color.White);
                        }
                        //Draw a check if the ability is one of the ones selected to be available in the GUI
                        if (SMH.GUI.IsAbilityAvailable(ability))
                        {
                            SMH.Graphics.DrawSprite(Sprites.SelectedAbilityCheck, drawX - 27, drawY + 23, Color.FromNonPremultiplied(255, 255, 255, (int)SmileyUtil.GetFlashingAlpha(0.6f)));
                        }
                    }
                }
            }

            //Key selection graphic
            float selectedKeyAlpha = ((float)Math.Sin(SMH.Now) + 1f) / 2f * 60f + 30f;
            float selectedKeyRed = ((float)Math.Sin(SMH.Now * 1.3) + 1f) / 2f * 100f + 100f;
            float selectedKeyGreen = ((float)Math.Sin(SMH.Now * 1.6) + 1f) / 2f * 100f + 100f;
            float selectedKeyBlue = ((float)Math.Sin(SMH.Now * 0.7) + 1f) / 2f * 100f + 100f;
            Color color = Color.FromNonPremultiplied((int)selectedKeyRed, (int)selectedKeyGreen, (int)selectedKeyBlue, (int)selectedKeyAlpha);

            switch (SMH.SaveManager.CurrentSave.Level)
            {
                case Level.OLDE_TOWNE:
                    SMH.Graphics.DrawSprite(Sprites.SelectedKeys, InventoryXOffset + 375 + 50 * 0, InventoryYOffset + 28, color);
                    break;
                case Level.FOREST_OF_FUNGORIA:
                    SMH.Graphics.DrawSprite(Sprites.SelectedKeys, InventoryXOffset + 375 + 50 * 1, InventoryYOffset + 28, color);
                    break;
                case Level.SESSARIA_SNOWPLAINS:
                    SMH.Graphics.DrawSprite(Sprites.SelectedKeys, InventoryXOffset + 375 + 50 * 2, InventoryYOffset + 28, color);
                    break;
                case Level.WORLD_OF_DESPAIR:
                    SMH.Graphics.DrawSprite(Sprites.SelectedKeys, InventoryXOffset + 375 + 50 * 3, InventoryYOffset + 28, color);
                    break;
                case Level.CASTLE_OF_EVIL:
                    SMH.Graphics.DrawSprite(Sprites.SelectedKeys, InventoryXOffset + 375 + 50 * 4, InventoryYOffset + 28, color);
                    break;
            };

            //Key Matrix
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //Level icons
                    if (j == 0)
                    {
                        SMH.Graphics.DrawSprite(SpriteSets.KeyIcons[i], InventoryXOffset + 385 + i * 50, InventoryYOffset + 35);
                    }
                    //Key icons
                    if (i == 0)
                    {
                        SMH.Graphics.DrawSprite(SpriteSets.KeyIcons[j],InventoryXOffset + 345, InventoryYOffset + 75 + j * 50);
                    }
                    //Key numbers
                    SMH.Graphics.DrawString(SmileyFont.Number, SMH.SaveManager.CurrentSave.NumKeys[i, j].ToString(), InventoryXOffset + 405 + i * 50, InventoryYOffset + 80 + j * 50, TextAlignment.Center, Color.White);
                }
            }

            //Upgrades
            SMH.Graphics.DrawSprite(Sprites.MoneyIcon, InventoryXOffset + 348, InventoryYOffset + 295);
            SMH.Graphics.DrawString(SmileyFont.Segoe14, SMH.SaveManager.CurrentSave.Money.ToString(), InventoryXOffset + 398, InventoryYOffset + 302, TextAlignment.Left, Color.White);

            for (int i = 0; i < 3; i++)
            {
                SMH.Graphics.DrawSprite(SpriteSets.UpgradeIcons[i], InventoryXOffset + 423 + i * 68, InventoryYOffset + 297);
                SMH.Graphics.DrawString(SmileyFont.Segoe14, SMH.SaveManager.CurrentSave.NumUpgrades[(Upgrade)i].ToString(), InventoryXOffset + 466 + i * 68, InventoryYOffset + 302, TextAlignment.Left, Color.White);
            }

            //Maximum mana
            SMH.Graphics.DrawString(SmileyFont.Segoe14, "Mana Multiplier: ", InventoryXOffset + 355, InventoryYOffset + 343, TextAlignment.Left, Color.White);
            SMH.Graphics.DrawString(SmileyFont.Segoe14, String.Format("{0:N2}", SMH.SaveManager.CurrentSave.ManaModifier), InventoryXOffset + 615, InventoryYOffset + 341, TextAlignment.Right, Color.White);
            //Damage multiplier
            SMH.Graphics.DrawString(SmileyFont.Segoe14, "Damage Multiplier:", InventoryXOffset + 355, InventoryYOffset + 367, TextAlignment.Left, Color.White);
            SMH.Graphics.DrawString(SmileyFont.Segoe14, String.Format("{0:N2}", SMH.SaveManager.CurrentSave.DamageModifer), InventoryXOffset + 615, InventoryYOffset + 365, TextAlignment.Right, Color.White);
            //Number of licks
            SMH.Graphics.DrawString(SmileyFont.Segoe14, "Number Of Licks:", InventoryXOffset + 355, InventoryYOffset + 391, TextAlignment.Left, Color.White);
            SMH.Graphics.DrawString(SmileyFont.Segoe14, String.Format("{0:N2}", SMH.SaveManager.CurrentSave.NumTongueLicks), InventoryXOffset + 615, InventoryYOffset + 391, TextAlignment.Right, Color.White);
            //Enemies killed
            SMH.Graphics.DrawString(SmileyFont.Segoe14, "Enemies Killed:", InventoryXOffset + 355, InventoryYOffset + 415, TextAlignment.Left, Color.White);
            SMH.Graphics.DrawString(SmileyFont.Segoe14, String.Format("{0}", SMH.SaveManager.CurrentSave.NumEnemiesKilled), InventoryXOffset + 615, InventoryYOffset + 415, TextAlignment.Right, Color.White);
            //Pixels travelled
            SMH.Graphics.DrawString(SmileyFont.Segoe14, "Pixels Travelled:", InventoryXOffset + 355, InventoryYOffset + 439, TextAlignment.Left, Color.White);
            SMH.Graphics.DrawString(SmileyFont.Segoe14, String.Format("{0}", SMH.SaveManager.CurrentSave.PixelsTraversed), InventoryXOffset + 615, InventoryYOffset + 439, TextAlignment.Right, Color.White);

            //Draw the cursor
            SMH.Graphics.DrawSprite(Sprites.InventoryCursor, InventoryXOffset + _cursorX * SquareSize + 31, InventoryYOffset + _cursorY * SquareSize + 31);
        }
    }
}
