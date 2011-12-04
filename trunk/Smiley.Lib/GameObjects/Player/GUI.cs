﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;

namespace Smiley.Lib.GameObjects.Player
{
    public class GUI
    {
        #region Constructors

        public GUI()
        {

            //abilityPoints[0].x = 33.0;
            //abilityPoints[0].y = 115.0;
            //abilityPoints[1].x = 79.0;
            //abilityPoints[1].y = 57.0;
            //abilityPoints[2].x = 122.0;
            //abilityPoints[2].y = 115.0;

            //resetAbilities();

            //availableAbilities[0].scale = SMALL_SCALE;
            //availableAbilities[1].scale = 1.0;
            //availableAbilities[2].scale = SMALL_SCALE;

            //smileyDamageDisplay = new SmileyDamageDisplay();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the selected ability. The selected ability is the one in the middle.
        /// </summary>
        /// <returns></returns>
        public Ability GetSelectedAbility()
        {
            return GetAbilityInSlot(1);
        }

        /// <summary>
        /// Gets the ability in the given slot.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public Ability GetAbilityInSlot(int slot)
        {
            //for (int i = 0; i < 3; i++) {
            //    if (availableAbilities[i].slot == slot) return availableAbilities[i].ability;
            //}
            return Ability.NO_ABILITY;
        }

        public void SetAbilityInSlot(Ability ability, int slot)
        {
            //for (int i = 0; i < 3; i++) {
            //    if (availableAbilities[i].slot == slot) {
            //        availableAbilities[i].ability = ability;
            //        return;
            //    }
            //}
        }

        /// <summary>
        /// Returns whether or not the specified ability is one of the ones available in the GUI.
        /// </summary>
        /// <param name="ability"></param>
        /// <returns></returns>
        public bool IsAbilityAvailable(Ability ability)
        {
            //for (int i = 0; i < 3; i++) {
            //    if (availableAbilities[i].ability == ability) return true;
            //}
            return false;
        }

        /// <summary>
        /// Returns the number of available abilities.
        /// </summary>
        /// <returns></returns>
        public int NumAvailableAbilities()
        {
            int n = 0;
            //for (int i = 0; i < 3; i++) {
            //    if (availableAbilities[i].ability != NO_ABILITY) n++;
            //}
            return n;
        }

        public void ResetAbilities()
        {
            //for (int i = 0; i < 3; i++) {
            //    availableAbilities[i].ability = NO_ABILITY;
            //    availableAbilities[i].slot = i;
            //    availableAbilities[i].x = abilityPoints[i].x;
            //    availableAbilities[i].y = abilityPoints[i].y;
            //}
        }

        /// <summary>
        /// Adds a new ability and pushes out the oldest one
        /// </summary>
        /// <param name="ability"></param>
        public void ToggleAvailableAbility(Ability ability)
        {
            ////If the ability is available, remove it
            //for (int i = 0; i < 3; i++) {
            //    if (availableAbilities[i].ability == ability) {
            //        availableAbilities[i].ability = NO_ABILITY;
            //        smh->soundManager->playSound("snd_AbilityDeSelect");
            //        return;
            //    }
            //}

            ////Otherwise, add it.
            //if (availableAbilities[1].ability == NO_ABILITY) {
            //    availableAbilities[1].ability = ability;
            //    smh->soundManager->playSound("snd_AbilitySelect");
            //    return;
            //}
            //if (availableAbilities[0].ability == NO_ABILITY) {
            //    availableAbilities[0].ability = ability;
            //    smh->soundManager->playSound("snd_AbilitySelect");
            //    return;
            //}
            //if (availableAbilities[2].ability == NO_ABILITY) {
            //    availableAbilities[2].ability = ability;
            //    smh->soundManager->playSound("snd_AbilitySelect");
            //    return;
            //}

            //If we got here then there is no room for the ability!
            SMH.Sound.PlaySound(Sound.Error);
        }

        public void Update(float dt)
        {
            //int collisionAtPlayer = smh->environment->collision[smh->player->gridX][smh->player->gridY];
            //int dir = -1;

            ////Input to change ability
            //if (!smh->windowManager->isOpenWindow()) {
            //    if (smh->input->keyPressed(INPUT_PREVIOUS_ABILITY)) {
            //        dir = LEFT;
            //    } else if  (smh->input->keyPressed(INPUT_NEXT_ABILITY)) {
            //        dir = RIGHT;
            //    }
            //}

            //if (dir != -1) {
            //    if (getSelectedAbility() == WATER_BOOTS && smh->player->isSmileyTouchingWater()) {
            //        if (collisionAtPlayer != DEEP_WATER && collisionAtPlayer != GREEN_WATER) {
            //            //player is on a land tile, but touching water; bump him over and change abilities
            //            changeAbility(dir);
            //            smh->player->graduallyMoveTo(smh->player->gridX * 64.0 + 32.0, smh->player->gridY * 64.0 + 32.0, 500.0);
            //        } else {
            //            //player actually on a water tile; cannot take off the sandals; play error message
            //            smh->soundManager->playSound("snd_Error");
            //        }
            //    } else {
            //        changeAbility(dir);
            //    }
            //}

            //float angle, x, y, targetX, targetY;
            //for (int i = 0; i < 3; i++) {
            //    //Move towards target slot
            //    x = availableAbilities[i].x;
            //    y = availableAbilities[i].y;
            //    targetX = abilityPoints[availableAbilities[i].slot].x;
            //    targetY = abilityPoints[availableAbilities[i].slot].y;
            //    angle = Util::getAngleBetween(x, y, targetX, targetY);
            //    if (Util::distance(x, y, targetX, targetY) < 600.0 * dt) {
            //        availableAbilities[i].x = targetX;
            //        availableAbilities[i].y = targetY;
            //    } else {
            //        availableAbilities[i].x += 600.0 * cos(angle) * dt;
            //        availableAbilities[i].y += 600.0 * sin(angle) * dt;
            //    }

            //    //Move towards correct size
            //    if (availableAbilities[i].slot == 1 && availableAbilities[i].scale < 1.0) {
            //        availableAbilities[i].scale += 3.0 * dt;
            //        if (availableAbilities[i].scale > 1.0) {
            //            availableAbilities[i].scale = 1.0;
            //        }
            //    } else if (availableAbilities[i].slot != 1 && availableAbilities[i].scale > SMALL_SCALE) {
            //        availableAbilities[i].scale -= 3.0 * dt;
            //        if (availableAbilities[i].scale < SMALL_SCALE) {
            //            availableAbilities[i].scale = SMALL_SCALE;
            //        }
            //    }
            //}
            //smileyDamageDisplay->update();
        }

        public void Daw()
        {
            //int drawX, drawY;

            ////Draw health
            //for (int i = 1; i <= smh->player->getMaxHealth(); i++) {
            //    drawX = (i <= 10) ? 120+i*35 : 120+(i-10)*35;
            //    drawY = (i <= 10) ? 25 : 70;
            //    if (smh->player->getHealth() >= i) {
            //        smh->resources->GetSprite("fullHealth")->Render(drawX, drawY);
            //    } else if (smh->player->getHealth() < i && smh->player->getHealth() >= i-.25) {
            //        smh->resources->GetSprite("threeQuartersHealth")->Render(drawX, drawY);
            //    } else if (smh->player->getHealth() < i-.25 && smh->player->getHealth() >= i -.5) {
            //        smh->resources->GetSprite("halfHealth")->Render(drawX, drawY);
            //    } else if (smh->player->getHealth() < i-.5 && smh->player->getHealth() >= i - .75) {
            //        smh->resources->GetSprite("quarterHealth")->Render(drawX, drawY);
            //    } else {
            //        smh->resources->GetSprite("emptyHealth")->Render(drawX, drawY);
            //    }
            //}

            ////Draw mana bar
            //drawX = 155;
            //drawY = smh->player->getMaxHealth() < 11 ? 65 : 110;
            //float manaBarSizeMultiplier = (1.0 + .15 * smh->saveManager->numUpgrades[1]) * 0.96; //adjust the size multiplier so max mana bar is the same width as max hearts

            //smh->resources->GetSprite("manabarBackgroundCenter")->SetTextureRect(675, 282, 115*manaBarSizeMultiplier-4, 22, true);
            //smh->resources->GetSprite("manabarBackgroundCenter")->Render(drawX+4, drawY);
            //smh->resources->GetSprite("manaBar")->SetTextureRect(661, 304, 115*(smh->player->getMana()/smh->player->getMaxMana())*manaBarSizeMultiplier, 15, true);
            //smh->resources->GetSprite("manaBar")->Render(drawX+4,drawY+3);

            //smh->resources->GetSprite("manabarBackgroundLeftTip")->Render(drawX, drawY);
            //smh->resources->GetSprite("manabarBackgroundRightTip")->Render(drawX + 115 * manaBarSizeMultiplier - 2, drawY);


            ////Draw abilities
            //smh->resources->GetSprite("abilityBackground")->Render(5.0, 12.0);
            //for (int i = 0; i < 3; i++) {
            //    if (availableAbilities[i].ability != NO_ABILITY) {
            //        smh->resources->GetAnimation("abilities")->SetFrame(availableAbilities[i].ability);
            //        smh->resources->GetAnimation("abilities")->RenderEx(availableAbilities[i].x, availableAbilities[i].y, 0.0, 
            //            availableAbilities[i].scale, availableAbilities[i].scale);
            //    }
            //}

            ////Draw damage display
            //smileyDamageDisplay->draw();

            ////Draw keys
            //if (Util::getKeyIndex(smh->saveManager->currentArea) != -1) 
            //{
            //    smh->drawSprite("keyBackground", 748.0, 714.0);

            //    int keyXOffset = 763.0;
            //    int keyYOffset = 724.0;
            //    for (int i = 0; i < 4; i++) {

            //        //Draw key icon
            //        smh->resources->GetAnimation("keyIcons")->SetFrame(i);
            //        smh->resources->GetAnimation("keyIcons")->Render(keyXOffset + 60.0*i, keyYOffset);

            //        //Draw num keys
            //        smh->resources->GetFont("numberFnt")->printf(keyXOffset + 60.0*i + 45.0, keyYOffset + 5.0, 
            //            HGETEXT_LEFT, "%d", smh->saveManager->numKeys[Util::getKeyIndex(smh->saveManager->currentArea)][i]);
            //    }	
            //}

            ////Show whether or not Smiley is invincible
            //if (smh->player->invincible) {
            //    smh->resources->GetFont("curlz")->printf(512.0, 3, HGETEXT_CENTER, "Invincibility On");
            //}
        }

        /// <summary>
        /// Cycles through the available abilities, skipping ones which are PASSIVE
        /// </summary>
        /// <param name="direction"></param>
        public void ChangeAbility(int direction)
        {
            //smh->soundManager->playSound("snd_SwitchItem");

            ////Stop old ability
            //smh->player->fireBreathParticle->Stop(false);
            //smh->player->iceBreathParticle->Stop(false);

            //if (smh->player->shrinkActive) {
            //    smh->player->shrinkActive = false;
            //    smh->soundManager->playSound("snd_DeShrink");
            //}

            //int a, b;

            //for (int i = 0; i < 3; i++) {
            //    if (direction == LEFT) {
            //        if (availableAbilities[i].slot == 0) a = i;
            //        if (availableAbilities[i].slot == 1) b = i;
            //    } else if (direction == RIGHT) {
            //        if (availableAbilities[i].slot == 1) a = i;
            //        if (availableAbilities[i].slot == 2) b = i;
            //    }
            //}

            //int temp = availableAbilities[a].slot;
            //availableAbilities[a].slot = availableAbilities[b].slot;
            //availableAbilities[b].slot = temp;
        }

        #endregion
    }
}