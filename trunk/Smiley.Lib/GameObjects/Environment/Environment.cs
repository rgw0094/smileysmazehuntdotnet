using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Data;
using Smiley.Lib.Enums;
using Smiley.Lib.Util;
using Smiley.Lib.Framework;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.GameObjects.Player;
using Smiley.Lib.GameObjects.Enemies;

namespace Smiley.Lib.GameObjects.Environment
{
    public class SmileyEnvironment
    {
        public const float SwitchDelay = 0.15f;
        public const float TongueSwitchDelay = 0.40f;

        #region Private Variables

        private List<Timer> _timers = new List<Timer>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current level's tiles.
        /// </summary>
        public Tiles Tiles
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads a new level.
        /// </summary>
        /// <param name="level">The new level to load.</param>
        /// <param name="previousLevel">The level we are coming from.</param>
        /// <param name="playMusic">Whether or not to start playing the new level's music</param>
        public void LoadLevel(Level level, Level previousLevel, bool playMusic)
        {
            Tiles = new Tiles(level);

            //TODO:
            //Setup collision stuff
            foreach (Tile tile in Tiles)
            {
                switch (tile.Collision)
                {
                    case CollisionTile.FOUNTAIN:
                        //fountain = new Fountain(col, row); TODO:
                        break;

                    case CollisionTile.RED_WARP:
                    case CollisionTile.BLUE_WARP:
                    case CollisionTile.YELLOW_WARP:
                    case CollisionTile.GREEN_WARP:
                        //Don't add hidden warps
                        //if (tile.Variable != 990)
                        //specialTileManager->addWarp(col, row, collision[col][row]);
                        break;

                    case CollisionTile.FLAME:
                        //specialTileManager->addFlame(col, row);
                        break;

                    case CollisionTile.FIRE_DESTROY:
                        //specialTileManager->addIceBlock(col, row);
                        break;

                    case CollisionTile.DIZZY_MUSHROOM_1:
                    case CollisionTile.DIZZY_MUSHROOM_2:
                        //specialTileManager->addMushroom(col,row,collision[col][row]);
                        break;

                    case CollisionTile.SMILELET:
                        //smileletManager->addSmilelet(col,row,ids[col][row]);
                        tile.Collision = CollisionTile.WALKABLE;
                        break;
                }

                //Evil wall stuff
                //if (collision[col][row] >= EVIL_WALL_POSITION && collision[col][row] <= EVIL_WALL_RESTART)
                //{
                //    evilWallManager->addEvilWall(ids[col][row]);
                //    evilWallManager->setState(ids[col][row], 0);
                //}
                //if (collision[col][row] == EVIL_WALL_POSITION)
                //{
                //    evilWallManager->setBeginWallPosition(ids[col][row], col, row);
                //    evilWallManager->setSpeed(ids[col][row], variable[col][row]);
                //}
                //else if (collision[col][row] == EVIL_WALL_TRIGGER)
                //{
                //    evilWallManager->setDir(ids[col][row], variable[col][row]);
                //}
                //else if (collision[col][row] == EVIL_WALL_RESTART)
                //{
                //    evilWallManager->setSmileyRestartPosition(ids[col][row], col, row);
                //}

                //Setup items
                if (SMH.SaveManager.CurrentSave.IsTileChanged(tile.X, tile.Y) &&
                    tile.Item != ItemTile.MANA_ITEM && tile.Item != ItemTile.HEALTH_ITEM)
                {
                    tile.Item = ItemTile.NONE;
                }
                else if ((int)tile.Item >= 16 && (int)tile.Item < 32)
                {
                    //tapestryManager->addTapestry(col, row, newItem);
                }

                //Setup enemies
                if (tile.Enemy == 255)
                {
                    //255 is fenwar encounter
                    if (!SMH.SaveManager.CurrentSave.IsTileChanged(tile.X, tile.Y))
                    {
                        //smh->fenwarManager->addFenwarEncounter(col, row, ids[col][row]);
                    }
                }
                else if (tile.Enemy >= 240)
                {
                    //240-254 are bosses
                    if (!SMH.SaveManager.CurrentSave.HasKilledBoss[(Boss)tile.Enemy])
                    {
                        //smh->bossManager->spawnBoss(enemy, variable[col][row], col, row);
                    }
                }
                else if (tile.Enemy > 0 && tile.Enemy < SMH.Data.Enemies.Count)
                {
                    //1-127 are enemies
                    //if (tile.ID == ENEMYGROUP_ENEMY)
                    //{
                    //    //If this enemy is part of a group, notify the manager
                    //    smh->enemyGroupManager->addEnemy(variable[col][row]);
                    //}
                    //if (tile.ID != ENEMYGROUP_ENEMY_POPUP)
                    //{
                    //    //Don't spawn popup enemies yet
                    //    smh->enemyManager->addEnemy(enemy - 1, col, row, .2, .2, variable[col][row], false);
                    //}
                }
                else if (tile.Enemy >= 128 && tile.Enemy < 240)
                {
                    //if (enemy != 128 + MONOCLE_MAN_NPC_ID || smh->saveManager->adviceManEncounterCompleted)
                    //{
                    //    smh->npcManager->addNPC(enemy - 128, ids[col][row], col, row);
                    //}
                }

                //Load changes
                if (SMH.SaveManager.CurrentSave.IsTileChanged(tile.X, tile.Y))
                {
                    if ((int)tile.Collision >= (int)CollisionTile.RED_KEYHOLE && (int)tile.Collision <= (int)CollisionTile.BLUE_KEYHOLE)
                        tile.Collision = CollisionTile.WALKABLE;

                    if (tile.Collision == CollisionTile.SMILELET_FLOWER_SAD)
                        tile.Collision = CollisionTile.SMILELET_FLOWER_HAPPY;

                    if (tile.Collision == CollisionTile.SMILELET)
                        tile.Collision = CollisionTile.WALKABLE;

                    if (tile.Collision == CollisionTile.BOMBABLE_WALL)
                        tile.Collision = CollisionTile.WALKABLE;

                    //Flip shrink tunnel switches
                    if (tile.Collision == CollisionTile.SHRINK_TUNNEL_SWITCH)
                    {
                        foreach (Tile otherTile in Tiles.Where(tile2 => tile2.ID == tile.ID))
                        {
                            if (otherTile.Collision == CollisionTile.SHRINK_TUNNEL_HORIZONTAL)
                                otherTile.Collision = CollisionTile.SHRINK_TUNNEL_VERTICAL;
                            else if (otherTile.Collision == CollisionTile.SHRINK_TUNNEL_VERTICAL)
                                otherTile.Collision = CollisionTile.SHRINK_TUNNEL_HORIZONTAL;
                        }
                    }

                    //Flip switches that have been marked as changed
                    if (SmileyUtil.IsCylinderSwitch(tile.Collision))
                    {
                        foreach (Tile otherTile in Tiles.Where(otherTile => otherTile.ID == tile.ID))
                        {
                            //Switch up cylinders down
                            if (SmileyUtil.IsCylinderUp(otherTile.Collision))
                            {
                                otherTile.Collision -= 16;
                            }
                            else if (SmileyUtil.IsCylinderDown(otherTile.Collision))
                            {
                                otherTile.Collision += 16;
                            }
                        }
                    }
                }
            }

            //Place the player. If after the first pass there was no zone entrance for where the player came from,
            //scan the area again and put the player in the first start square.
            int playerX = -1;
            int playerY = -1;
            for (int i = 0; i < 2; i++)
            {
                Tile startTile = Tiles.SingleOrDefault(tile => tile.Collision == CollisionTile.PLAYER_START && ((Level)tile.ID == previousLevel) || i == 1);
                if (startTile != null)
                {
                    playerX = startTile.X;
                    playerY = startTile.Y;
                }
            }

            //SMH.Player.moveTo(playerX, playerY);
            //if (smh->saveManager->currentArea == FOUNTAIN_AREA && !smh->saveManager->adviceManEncounterCompleted)
            //{
            //    adviceMan = new AdviceMan(SMH.Player.gridX, SMH.Player.gridY + 1);
            //}

            //Update to get shit set up
            Update(0);
            //smh->enemyManager->update(0.0);
            //smh->areaChanger->displayNewAreaName();
        }

        public void Reset()
        {
            //        smh->bossManager->reset();
            //smh->enemyManager->reset();
            //smh->projectileManager->reset();
            //smh->lootManager->reset();
            //smh->npcManager->reset();
            //smh->enemyGroupManager->resetGroups();
            //SMH.Player.resetTongue();
            //tapestryManager->reset();
            //specialTileManager->reset();
            //evilWallManager->reset();
            //smileletManager->reset();
            //smh->fenwarManager->reset();
            //removeAllParticles();

            //if (fountain) {
            //    delete fountain;
            //    fountain = NULL;
            //}

            //if (adviceMan) {
            //    delete adviceMan;
            //    adviceMan = NULL;
            //}

            ////Clear old level data
            //for (int i = 0; i < 256; i++) {
            //    for (int j = 0; j < 256; j++) {
            //        terrain[i][j] = 0;
            //        collision[i][j] = 0;
            //        ids[i][j] = -1;
            //        item[i][j] = 0;
            //        activated[i][j] = -100.0;
            //        variable[i][j] = 0;
            //        enemyLayer[i][j] = -1;
            //    }
            //}

            //for (std::list<Timer>::iterator i = timerList.begin(); i != timerList.end(); i++) {
            //    i = timerList.erase(i);
            //}

            //smh->explosionManager->reset();
        }

        public void Update(float dt)
        {
            //Update animations and shit
            //smh->resources->GetAnimation("water")->Update(dt);
            //smh->resources->GetAnimation("greenWater")->Update(dt);
            //smh->resources->GetAnimation("lava")->Update(dt);
            //smh->resources->GetAnimation("spring")->Update(dt);
            //smh->resources->GetAnimation("superSpring")->Update(dt);
            //smh->resources->GetAnimation("silverSwitch")->Update(dt);
            //smh->resources->GetAnimation("brownSwitch")->Update(dt);
            //smh->resources->GetAnimation("blueSwitch")->Update(dt);
            //smh->resources->GetAnimation("greenSwitch")->Update(dt);
            //smh->resources->GetAnimation("yellowSwitch")->Update(dt);
            //smh->resources->GetAnimation("whiteSwitch")->Update(dt);
            //smh->resources->GetAnimation("bunnySwitch")->Update(dt);
            //smh->resources->GetAnimation("shrinkTunnelSwitch")->Update(dt);
            //smh->resources->GetAnimation("mirrorSwitch")->Update(dt);
            //silverCylinder->Update(dt);
            //brownCylinder->Update(dt);
            //blueCylinder->Update(dt);
            //greenCylinder->Update(dt);
            //yellowCylinder->Update(dt);
            //whiteCylinder->Update(dt);
            //silverCylinderRev->Update(dt);
            //brownCylinderRev->Update(dt);
            //blueCylinderRev->Update(dt);
            //greenCylinderRev->Update(dt);
            //yellowCylinderRev->Update(dt);
            //whiteCylinderRev->Update(dt);

            ////Determine the grid offset to figure out which tiles to draw
            //xGridOffset = SMH.Player.gridX - screenWidth/2;
            //yGridOffset = SMH.Player.gridY - screenHeight/2;

            ////Determine the tile offset for smooth movement
            //xOffset = SMH.Player.x - float(SMH.Player.gridX) * float(64.0);
            //yOffset = SMH.Player.y - float(SMH.Player.gridY) * float(64.0);

            ////Update each grid square
            //for (int i = 0; i < areaWidth; i++) {
            //    for (int j = 0; j < areaHeight; j++) {

            //        //Update timed switches
            //        if (Util::isCylinderSwitchLeft(collision[i][j]) || Util::isCylinderSwitchRight(collision[i][j])) {
            //            if (variable[i][j] != -1 && activated[i][j] + (float)variable[i][j] < smh->getGameTime() && 
            //                    smh->saveManager->isTileChanged( i, j)) {

            //                //Make sure the player isn't on top of any of the cylinders that will pop up
            //                if (!playerOnCylinder(i,j)) {
            //                    flipCylinderSwitch(i, j);
            //                }	

            //            }
            //        }
            //    }
            //}

            //specialTileManager->update(dt);
            //evilWallManager->update(dt);
            //tapestryManager->update(dt);
            //smileletManager->update();
            //updateSwitchTimers(dt);
            //if (fountain) fountain->update(dt);
        }

        public void Draw()
        {
            //        drawPits(dt);

            ////Loop through each tile to draw shit
            //for (int j = -1; j <= screenHeight + 1; j++) {
            //    for (int i = -1; i <= screenWidth + 1; i++) {

            //        drawX = i * 64 - xOffset;
            //        drawY = j * 64 - yOffset;

            //        if (isInBounds(i+xGridOffset, j+yGridOffset)) {	

            //            int theTerrain = terrain[i+xGridOffset][j+yGridOffset];
            //            int theCollision = collision[i+xGridOffset][j+yGridOffset];
            //            int theItem = item[i+xGridOffset][j+yGridOffset];
            //            float timeSinceSquareActivated = smh->timePassedSince(activated[i+xGridOffset][j+yGridOffset]);

            //            //Terrain
            //            if (theCollision != PIT && theCollision != FAKE_PIT && theCollision != NO_WALK_PIT) {
            //                if (theTerrain > 0 && theTerrain < 256) {
            //                    smh->resources->GetAnimation("mainLayer")->SetFrame(theTerrain);
            //                    smh->resources->GetAnimation("mainLayer")->Render(drawX,drawY);
            //                } else {
            //                    smh->resources->GetSprite("blackSquare")->Render(drawX, drawY);
            //                }
            //            }

            //            //Collision
            //            if (shouldEnvironmentDrawCollision(theCollision))
            //            {					
            //                //Water animation
            //                if (theCollision == SHALLOW_WATER) {
            //                    smh->resources->GetAnimation("water")->SetColor(ARGB(125,255,255,255));
            //                    smh->resources->GetAnimation("water")->Render(drawX,drawY);
            //                } else if (theCollision == DEEP_WATER || theCollision == NO_WALK_WATER) {
            //                    smh->resources->GetAnimation("water")->SetColor(ARGB(255,255,255,255));
            //                    smh->resources->GetAnimation("water")->Render(drawX,drawY);
            //                } else if (theCollision == WALK_LAVA || theCollision == NO_WALK_LAVA) {
            //                    smh->resources->GetAnimation("lava")->Render(drawX,drawY);
            //                } else if (theCollision == GREEN_WATER) {
            //                    smh->resources->GetAnimation("greenWater")->SetColor(ARGB(255,255,255,255));
            //                    smh->resources->GetAnimation("greenWater")->Render(drawX,drawY);
            //                } else if (theCollision == SHALLOW_GREEN_WATER) {
            //                    smh->resources->GetAnimation("greenWater")->SetColor(ARGB(125,255,255,255));
            //                    smh->resources->GetAnimation("greenWater")->Render(drawX,drawY);
            //                } else if (theCollision == SPRING_PAD && smh->getGameTime() - 0.5f < activated[i+xGridOffset][j+yGridOffset]) {
            //                    smh->resources->GetAnimation("spring")->Render(drawX,drawY);
            //                } else if (theCollision == SUPER_SPRING && smh->getGameTime() - 0.5f < activated[i+xGridOffset][j+yGridOffset]) {
            //                    smh->resources->GetAnimation("superSpring")->Render(drawX, drawY);

            //                //Switch animations
            //                } else if ((theCollision == SILVER_SWITCH_LEFT || theCollision == SILVER_SWITCH_RIGHT) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    smh->resources->GetAnimation("silverSwitch")->Render(drawX,drawY);
            //                } else if ((theCollision == BROWN_SWITCH_LEFT || theCollision == BROWN_SWITCH_RIGHT) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    smh->resources->GetAnimation("brownSwitch")->Render(drawX,drawY);
            //                } else if ((theCollision == BLUE_SWITCH_LEFT || theCollision == BLUE_SWITCH_RIGHT) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    smh->resources->GetAnimation("blueSwitch")->Render(drawX,drawY);
            //                } else if ((theCollision == GREEN_SWITCH_LEFT || theCollision == GREEN_SWITCH_RIGHT) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    smh->resources->GetAnimation("greenSwitch")->Render(drawX,drawY);
            //                } else if ((theCollision == YELLOW_SWITCH_LEFT || theCollision == YELLOW_SWITCH_RIGHT) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    smh->resources->GetAnimation("yellowSwitch")->Render(drawX,drawY);
            //                } else if ((theCollision == WHITE_SWITCH_LEFT || theCollision == WHITE_SWITCH_RIGHT) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    smh->resources->GetAnimation("whiteSwitch")->Render(drawX,drawY);

            //                //Special switches
            //                } else if (theCollision == SPIN_ARROW_SWITCH && timeSinceSquareActivated < 0.45) {
            //                    smh->resources->GetAnimation("bunnySwitch")->Render(drawX, drawY);
            //                } else if (theCollision == MIRROR_SWITCH && timeSinceSquareActivated < 0.45) {
            //                    smh->resources->GetAnimation("mirrorSwitch")->Render(drawX, drawY);
            //                } else if (theCollision == SHRINK_TUNNEL_SWITCH && timeSinceSquareActivated < 0.45) {
            //                    smh->resources->GetAnimation("shrinkTunnelSwitch")->Render(drawX, drawY);

            //                //Cylinder animations
            //                } else if ((theCollision == SILVER_CYLINDER_DOWN) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    silverCylinder->Render(drawX,drawY);
            //                } else if ((theCollision == SILVER_CYLINDER_UP) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    silverCylinderRev->Render(drawX,drawY);
            //                } else if ((theCollision == BROWN_CYLINDER_DOWN) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    brownCylinder->Render(drawX,drawY);
            //                } else if ((theCollision == BROWN_CYLINDER_UP) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    brownCylinderRev->Render(drawX,drawY);
            //                } else if ((theCollision == BLUE_CYLINDER_DOWN) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    blueCylinder->Render(drawX,drawY);
            //                } else if ((theCollision == BLUE_CYLINDER_UP) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    blueCylinderRev->Render(drawX,drawY);
            //                } else if ((theCollision == GREEN_CYLINDER_DOWN) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    greenCylinder->Render(drawX,drawY);
            //                } else if ((theCollision == GREEN_CYLINDER_UP) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    greenCylinderRev->Render(drawX,drawY);
            //                } else if ((theCollision == YELLOW_CYLINDER_DOWN) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    yellowCylinder->Render(drawX,drawY);
            //                } else if ((theCollision == YELLOW_CYLINDER_UP) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    yellowCylinderRev->Render(drawX,drawY);
            //                } else if ((theCollision == WHITE_CYLINDER_DOWN) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    whiteCylinder->Render(drawX,drawY);
            //                } else if ((theCollision == WHITE_CYLINDER_UP) && timeSinceSquareActivated < SWITCH_DELAY) {
            //                    whiteCylinderRev->Render(drawX,drawY);

            //                //Save thing
            //                } else if (theCollision == SAVE_SHRINE) {
            //                    smh->resources->GetAnimation("savePoint")->Update(dt);
            //                    smh->resources->GetAnimation("savePoint")->Render(drawX, drawY);

            //                //Don't draw the EVIL WALL position and restart tiles
            //                } else if (theCollision == EVIL_WALL_POSITION || theCollision == EVIL_WALL_RESTART) {

            //                    //Don't draw anything

            //                //Non-animated collision tiles
            //                } else {

            //                    //Set to current tile
            //                    smh->resources->GetAnimation("walkLayer")->SetFrame(theCollision);

            //                    //Set color values
            //                    if (theCollision >= UP_ARROW && theCollision <= LEFT_ARROW) {
            //                        if (ids[i+xGridOffset][j+yGridOffset] == -1 || ids[i+xGridOffset][j+yGridOffset] == 990) {
            //                            //render normal, red arrow
            //                            smh->resources->GetAnimation("walkLayer")->SetColor(ARGB(255,255,0,255));							
            //                        } else { 
            //                            //it's a rotating arrow, make it green
            //                            smh->resources->GetAnimation("walkLayer")->SetColor(ARGB(255,0,255,255));					
            //                        }
            //                    } else if (theCollision == SHALLOW_WATER) {
            //                        smh->resources->GetAnimation("walkLayer")->SetColor(ARGB(125,255,255,255));
            //                    } else if (theCollision == SLIME) {
            //                        smh->resources->GetAnimation("walkLayer")->SetColor(ARGB(200,255,255,255));
            //                    }

            //                    //Draw it and set the color back to normal
            //                    smh->resources->GetAnimation("walkLayer")->Render(drawX,drawY);
            //                    smh->resources->GetAnimation("walkLayer")->SetColor(ARGB(255,255,255,255));

            //                }
            //            }

            //            //Items
            //            if (theItem != NONE && ids[i+xGridOffset][j+yGridOffset] != DRAW_AFTER_SMILEY) {
            //                if (theItem == ENEMYGROUP_BLOCKGRAPHIC) {
            //                    //If this is an enemy block, draw it with the enemy group's
            //                    //block alpha
            //                    itemLayer[theItem]->SetColor(ARGB(
            //                        smh->enemyGroupManager->groups[variable[i+xGridOffset][j+yGridOffset]].blockAlpha, 255, 255, 255));
            //                    itemLayer[theItem]->Render(drawX,drawY);
            //                    itemLayer[theItem]->SetColor(ARGB(255,255,255,255));
            //                } else {
            //                    itemLayer[theItem]->Render(drawX,drawY);
            //                }
            //            }

            //        } else {
            //            //Out of bounds
            //            smh->resources->GetSprite("blackSquare")->Render(drawX, drawY);
            //        }
            //    }
            //}

            ////Draw fountain before smiley if he is below it
            //if (fountain && !fountain->isAboveSmiley()) {
            //    fountain->draw(dt);
            //}

            ////Draw particles
            //for (std::list<ParticleStruct>::iterator i = particleList.begin(); i != particleList.end(); i++) {
            //    i->particle->MoveTo(smh->getScreenX(i->x), smh->getScreenY(i->y), true);
            //    i->particle->Update(dt);
            //    i->particle->Render();
            //}

            ////Debug mode stuff
            //if (smh->isDebugOn()) {

            //    //Column lines
            //    for (int i = 0; i <= screenWidth; i++) {
            //        smh->hge->Gfx_RenderLine(i*64.0 - xOffset,0,i*64.0 - xOffset,768.0);
            //    }
            //    //Row lines
            //    for (int i = 0; i <= screenHeight; i++) {
            //        smh->hge->Gfx_RenderLine(0,i*64.0 - yOffset,1024.0,i*64.0 - yOffset);
            //    }
            //    //Draw Terrain collision boxes
            //    for (int i = SMH.Player.gridX - 2; i <= SMH.Player.gridX + 2; i++) {
            //        for (int j = SMH.Player.gridY - 2; j <= SMH.Player.gridY + 2; j++) {
            //            if (isInBounds(i,j)) {
            //                //Set collision box depending on collision type
            //                if (hasSillyPad(i,j)) {
            //                    collisionBox->SetRadius(i*64+32,j*64+32,24);
            //                } else {
            //                    setTerrainCollisionBox(collisionBox, collision[i][j], i, j);
            //                }
            //                if (!SMH.Player.canPass(collision[i][j]) || hasSillyPad(i,j)) smh->drawCollisionBox(collisionBox, Colors::GREEN);
            //            }
            //        }
            //    }
            //}

            ////Draw other shit
            //tapestryManager->draw(dt);
            //smileletManager->drawBeforeSmiley();
            //smileletManager->drawAfterSmiley();
            //specialTileManager->draw(dt);
            //if (adviceMan) adviceMan->draw(dt);
            //evilWallManager->draw(dt);
        }

        ///<summary>
        ///Draws stuff on the item layer that was marked to be drawn after
        ///Smiley (indicated by ID 990) as well as shrink tunnels, explosions and other SHIT.
        ///</summary>
        ///<param name="dt"></param>
        public void DrawAfterSmiley(float dt)
        {
            ////Loop through each tile to draw shit
            //for (int gridY = yGridOffset-1; gridY <= yGridOffset + screenHeight + 1; gridY++) {
            //    for (int gridX = xGridOffset-1; gridX <= xGridOffset + screenWidth + 1; gridX++) {
            //        if (isInBounds(gridX, gridY)) {

            //            drawX = smh->getScreenX(gridX * 64);
            //            drawY = smh->getScreenY(gridY * 64);

            //            //Marked as draw after smiley or the thing above Smiley is marked as draw
            //            //above Smiley and Smiley is behind it. That way you can't walk behind a tree
            //            //and lick through it.
            //            if (ids[gridX][gridY] == DRAW_AFTER_SMILEY || 
            //                    (ids[gridX][gridY-1] == DRAW_AFTER_SMILEY && 
            //                    SMH.Player.gridX == gridX && 
            //                    SMH.Player.gridY < gridY)) {
            //                itemLayer[item[gridX][gridY]]->Render(drawX,drawY);
            //            }

            //            //Shrink tunnels unless Smiley is directly underneath it and not shrunk
            //            if ((collision[gridX][gridY] == SHRINK_TUNNEL_HORIZONTAL || 
            //                    collision[gridX][gridY] == SHRINK_TUNNEL_VERTICAL) &&
            //                    !(SMH.Player.gridY == gridY+1 && !SMH.Player.isShrunk())) {
            //                smh->resources->GetAnimation("walkLayer")->SetFrame(collision[gridX][gridY]);
            //                smh->resources->GetAnimation("walkLayer")->Render(drawX, drawY);
            //            }

            //        }
            //    }
            //}

            ////Draw fountain after smiley if he is above it
            //if (fountain && fountain->isAboveSmiley()) {
            //    fountain->draw(dt);
            //}
        }

        /// <summary>
        /// Kills a switch timer at (gridX, gridY) if there is one.
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        public void KillSwitchTimer(int gridX, int gridY)
        {
            //for (std::list<Timer>::iterator i = timerList.begin(); i != timerList.end(); i++) {
            //    if (Util::getGridX(i->x) == gridX && Util::getGridY(i->y) == gridY) {
            //        i = timerList.erase(i);
            //    }
            //}
        }

        /// <summary>
        /// Returns what type of collision there is at point (x,y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public CollisionTile CollisionAt(float x, float y)
        {
            //Determine grid coords of the object
            int gridX = Convert.ToInt32(x / 64f);
            int gridY = Convert.ToInt32(y / 64f);

            //Handle out of bounds input
            if (!IsInBounds(gridX, gridY))
            {
                return CollisionTile.UNWALKABLE;
            }

            //Return the collision type
            return Tiles[gridX, gridY].Collision;
        }

        /// <summary>
        /// Unlocks a door at the given grid position.
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        public void EnvironmentUnlockDoor(int gridX, int gridY)
        {
            bool doorOpened = false;

            int keyIndex = SmileyUtil.GetKeyIndex(SMH.SaveManager.CurrentSave.Level);
            CollisionTile collision = Tiles[gridX, gridY].Collision;

            //If we're not on a level with keys (and hence getKeyIndex = -1), don't open any doors
            if (keyIndex == -1)
                return;

            Dictionary<CollisionTile, ItemTile> tiles = new Dictionary<CollisionTile, ItemTile>();
            tiles[CollisionTile.RED_KEYHOLE] = ItemTile.RED_KEY;
            tiles[CollisionTile.BLUE_KEYHOLE] = ItemTile.BLUE_KEY;
            tiles[CollisionTile.YELLOW_KEYHOLE] = ItemTile.YELLOW_KEY;
            tiles[CollisionTile.GREEN_KEYHOLE] = ItemTile.GREEN_KEY;

            //If this square is a door and the player has the key, unlock it
            //The -1 after each key index is because in the item layer, item 0 is blank, and item 1 is red key -- but we want the key indices to start with 0.
            foreach (KeyValuePair<CollisionTile, ItemTile> kvp in tiles)
            {
                if (collision == kvp.Key && SMH.SaveManager.CurrentSave.NumKeys[keyIndex, (int)kvp.Value - 1] > 0)
                {
                    Tiles[gridX, gridY].Collision = CollisionTile.WALKABLE;
                    SMH.SaveManager.CurrentSave.NumKeys[keyIndex, (int)kvp.Value - 1]--;
                    doorOpened = true;
                }
            }


            //Remember that this door was opened! Also, play a sound
            if (doorOpened)
            {
                SMH.Sound.PlaySound(Sound.DoorUnlock);
                SMH.SaveManager.CurrentSave.ChangeTile(gridX, gridY);
            }

        }

        /// <summary>
        /// Toggles switches hit by a collision box. Returns whether or not a switch
        /// was toggled.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="playSoundFarAway">If this is true the switch sound will always play even if Smiley is really far away</param>
        /// <param name="playTimerSound"></param>
        /// <returns></returns>
        public bool ToggleSwitches(Rect box, bool playSoundFarAway, bool playTimerSound)
        {
            //Determine what grid square the collision box is in
            int boxGridX = Convert.ToInt32((box.X + box.Width * 0.5f) / 64f);
            int boxGridY = Convert.ToInt32((box.Y + box.Height * 0.5f) / 64f);

            //Loop through all the adjacent grid squares
            for (int gridX = boxGridX - 2; gridX <= boxGridX + 2; gridX++)
            {
                for (int gridY = boxGridY - 2; gridY <= boxGridY + 2; gridY++)
                {
                    //Make sure the square is in bounds
                    if (IsInBounds(gridX, gridY))
                    {
                        Rect tileBox = GetTerrainCollisionBox(Tiles[gridX, gridY].Collision, gridX, gridY);

                        //Check collision with any switches
                        if (SMH.GameTimePassed(Tiles[gridX, gridY].ActivatedTime, SmileyEnvironment.SwitchDelay) && tileBox.Intersects(box))
                        {
                            if (ToggleSwitchAt(gridX, gridY, playSoundFarAway, playTimerSound))
                                return true;
                        }
                    }
                }
            }

            //No switches were toggled so return false;
            return false;
        }

        /// <summary>
        /// Toggles any switches hit by smiley's tongue. Returns whether or not a
        /// switch was toggled.
        /// </summary>
        /// <param name="tongue"></param>
        /// <returns></returns>
        bool ToggleSwitches(Tongue tongue)
        {
            //Loop through all the squares adjacent to Smiley
            for (int gridX = SMH.Player.Tile.X - 2; gridX <= SMH.Player.Tile.X + 2; gridX++)
            {
                for (int gridY = SMH.Player.Tile.Y - 2; gridY <= SMH.Player.Tile.Y + 2; gridY++)
                {
                    //Make sure the square is in bounds
                    if (IsInBounds(gridX, gridY))
                    {
                        Rect box = GetTerrainCollisionBox(Tiles[gridX, gridY].Collision, gridX, gridY);

                        //Check collision with any switches
                        if (SMH.GameTimePassed(Tiles[gridX, gridY].ActivatedTime, SmileyEnvironment.TongueSwitchDelay) && tongue.Intersects(box))
                        {
                            if (ToggleSwitchAt(gridX, gridY, true, true))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            //No switches were toggled so return false;
            return false;
        }

        /// <summary>
        /// Toggles a switch.
        /// </summary>
        /// <param name="id">The id of the switch to toggle.</param>
        public void ToggleSwitch(int id)
        {
            foreach (Tile tile in Tiles.Where(tile => tile.ID == id && SmileyUtil.IsCylinderSwitch(tile.Collision)))
            {
                ToggleSwitchAt(tile.X, tile.Y, true, false);
            }
        }

        /// <summary>
        /// Returns whether or not a grid coordinate is in bounds.
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <returns></returns>
        public bool IsInBounds(int gridX, int gridY)
        {
            return gridX >= 0 && gridY >= 0 && gridX < Tiles.Width && gridY < Tiles.Height;
        }

        /// <summary>
        /// Returns the item in square (x,y) and removes it.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ItemTile RemoveItem(int x, int y)
        {
            ItemTile retVal = Tiles[x, y].Item;
            Tiles[x, y].Item = ItemTile.NONE;
            SMH.SaveManager.CurrentSave.ChangeTile(x, y);
            return retVal;
        }

        /// <summary>
        /// Removes a particle at x,y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void RemoveParticle(int x, int y)
        {
            //TODO:
            //for (std::list<ParticleStruct>::iterator i = particleList.begin(); i != particleList.end(); i++) {
            //    if (i->gridX == x && i->gridY == y) {
            //        i->particle->Stop();
            //        delete i->particle;
            //        i = particleList.erase(i);
            //    }
            //}
        }

        public void RemoveAllParticles()
        {
            //for (std::list<ParticleStruct>::iterator i = particleList.begin(); i != particleList.end(); i++) {
            //    i->particle->Stop();
            //    delete i->particle;
            //    i = particleList.erase(i);
            //}
            //particleList.clear();
        }

        public void AddParticle(string particleName, float x, float y)
        {
            //ParticleStruct particleStruct;
            //particleStruct.x = x;
            //particleStruct.y = y;
            //particleStruct.gridX = Util::getGridX(x);
            //particleStruct.gridY = Util::getGridY(y);
            //particleStruct.particle = new hgeParticleSystem(&smh->resources->GetParticleSystem(particleName)->info);
            //particleStruct.particle->FireAt(smh->getScreenX(x), smh->getScreenY(y));

            //particleList.push_back(particleStruct);
        }

        /// <summary>
        /// Returns whether or not the player is on top of any cylinders that will pop up
        /// when the switch at grid position (x,y) is toggled.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsPlayerOnCylinder(int x, int y)
        {
            //Make sure the player isn't on top of any of the cylinders that will pop up
            for (int k = SMH.Player.Tile.X - 1; k <= SMH.Player.Tile.X + 1; k++)
            {
                for (int l = SMH.Player.Tile.Y - 1; l <= SMH.Player.Tile.Y + 1; l++)
                {
                    if (Tiles[k, l].ID == Tiles[x, y].ID && SmileyUtil.IsCylinderDown(Tiles[k, l].Collision))
                    {
                        Rect box = new Rect(k * 64, l * 64, 64, 64);
                        //Player collides with a cylinder
                        if (SMH.Player.CollisionCircle.Intersects(box))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Returns whether or not there is deep water of any kind at grid (x,y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsDeepWaterAt(int x, int y)
        {
            return (Tiles[x, y].Collision == CollisionTile.DEEP_WATER || Tiles[x, y].Collision == CollisionTile.GREEN_WATER);
        }

        /// <summary>
        /// Returns whether this is a good spot to "return" to after drowning or falling.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsReturnSpotAt(int x, int y)
        {
            CollisionTile c = Tiles[x, y].Collision;

            return
                c == CollisionTile.WALKABLE ||
                c == CollisionTile.SHALLOW_WATER ||
                c == CollisionTile.WALK_LAVA ||
                c == CollisionTile.RED_WARP ||
                c == CollisionTile.BLUE_WARP ||
                c == CollisionTile.YELLOW_WARP ||
                c == CollisionTile.GREEN_WARP ||
                c == CollisionTile.SHALLOW_GREEN_WATER ||
                c == CollisionTile.BOMB_PAD_UP ||
                c == CollisionTile.BOMB_PAD_DOWN ||
                c == CollisionTile.HOVER_PAD ||
                c == CollisionTile.SUPER_SPRING ||
                c == CollisionTile.SMILELET ||
                c == CollisionTile.SMILELET_FLOWER_HAPPY ||
                c == CollisionTile.FAKE_COLLISION ||
                c == CollisionTile.PLAYER_START ||
                (c >= CollisionTile.WHITE_CYLINDER_DOWN && c <= CollisionTile.SILVER_CYLINDER_DOWN) ||
                (c >= CollisionTile.EVIL_WALL_POSITION && c <= CollisionTile.EVIL_WALL_RESTART);
        }

        /// <summary>
        /// Returns whether or not there is shallow water of any kind at grid (x,y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsShallowWaterAt(int x, int y)
        {
            return (Tiles[x, y].Collision == CollisionTile.SHALLOW_WATER || Tiles[x, y].Collision == CollisionTile.SHALLOW_GREEN_WATER);
        }

        public bool IsArrowAt(int x, int y)
        {
            return Tiles[x, y].Collision >= CollisionTile.UP_ARROW && Tiles[x, y].Collision <= CollisionTile.LEFT_ARROW;
        }

        /// <summary>
        /// Returns whether or not the given grid square has a silly pad.
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <returns></returns>
        public bool HasSillyPad(int gridX, int gridY)
        {
            return true;//TODO:
            //return specialTileManager->isSillyPadAt(gridX, gridY);
        }

        /// <summary>
        /// Returns whether or not there is an unobstructed straight line from pixel position 
        /// (x1,y1) to (x2,y2).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="radius">Radius of the object taking the path.</param>
        /// <param name="canPassCollision"></param>
        /// <returns></returns>
        public bool ValidPath(int x1, int y1, int x2, int y2, int radius, Dictionary<CollisionTile, bool> canPassCollision)
        {
            //First get the velocities of the path
            float angle = SmileyUtil.GetAngleBetween(x1, y1, x2, y2);

            //Call validPath using this angle
            return ValidPath(angle, x1, y1, x2, y2, radius, canPassCollision, false);
        }

        /// <summary>
        /// Returns whether or not there is an unobstructed straight line from pixel position 
        /// (x1,y1) to (x2,y2).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="radius">Radius of the object taking the path.</param>
        /// <param name="canPassCollision"></param>
        /// <returns></returns>
        public bool ValidPath(float angle, int x1, int y1, int x2, int y2, int radius, Dictionary<CollisionTile, bool> canPassCollision, bool needsToHitPlayer)
        {
            float dx = 10f * (float)Math.Cos(angle);
            float dy = 10f * (float)Math.Sin(angle);

            //Now trace the path using dx and dy and see if you run into any SHIT
            float xTravelled = 0;
            float yTravelled = 0;
            float curX = x1;
            float curY = y1;

            CollisionCircle circle = new CollisionCircle();

            //This can throw an exception if the enemy is perfectly on top of the player
            try
            {
                while (Math.Abs(xTravelled) < Math.Abs(x2 - x1) && Math.Abs(yTravelled) < Math.Abs(y2 - y1))
                {
                    //Top left of the object
                    if (!canPassCollision[CollisionAt(curX - radius, curY - radius)] || HasSillyPad((int)(curX - radius) / 64, (int)(curY - radius) / 64)) return false;
                    //Top right of the object
                    if (!canPassCollision[CollisionAt(curX + radius, curY - radius)] || HasSillyPad((int)(curX + radius) / 64, (int)(curY - radius) / 64)) return false;
                    //Bottom left of the object
                    if (!canPassCollision[CollisionAt(curX - radius, curY + radius)] || HasSillyPad((int)(curX - radius) / 64, (int)(curY + radius) / 64)) return false;
                    //Bottom right of the object
                    if (!canPassCollision[CollisionAt(curX + radius, curY + radius)] || HasSillyPad((int)(curX + radius) / 64, (int)(curY + radius) / 64)) return false;
                    curX += dx;
                    curY += dy;
                    xTravelled += dx;
                    yTravelled += dy;

                    if (needsToHitPlayer)
                    {
                        circle.X = curX;
                        circle.Y = curY;
                        circle.Radius = radius;

                        if (SMH.Player.CollisionCircle.Intersects(circle)) return true;
                    }
                }
            }
            catch (Exception)
            {
                return true;
            }

            if (!needsToHitPlayer)
            {
                //You didnt hit any SHIT so return true
                return true;
            }

            //Since needsToHitPlayer is true, we only return true if we hit the player
            circle.X = curX;
            circle.Y = curY;
            circle.Radius = radius;

            return SMH.Player.CollisionCircle.Intersects(circle);
        }

        /// <summary>
        /// Returns whether or not player, when centered at (x,y), collides with any terrain. 
        /// Also autoadjusts the player's position to navigate corners.
        /// </summary>
        /// <param name="x">x-coord of the player</param>
        /// <param name="y">y-coord of the player</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool PlayerCollision(int x, int y, float dt)
        {
            //Determine the location of the collision box
            int gridX = x / 64;
            int gridY = y / 64;

            bool onIce = Tiles[SMH.Player.Tile.X, SMH.Player.Tile.Y].Collision == CollisionTile.ICE;

            //Check all neighbor squares
            for (int i = gridX - 2; i <= gridX + 2; i++)
            {
                for (int j = gridY - 2; j <= gridY + 2; j++)
                {
                    //Special logic for shrink tunnels
                    bool canPass;
                    if (Tiles[i, y].Collision == CollisionTile.SHRINK_TUNNEL_HORIZONTAL)
                    {
                        canPass = SMH.Player.IsShrunk && j == SMH.Player.Tile.Y;
                    }
                    else if (Tiles[i, y].Collision == CollisionTile.SHRINK_TUNNEL_VERTICAL)
                    {
                        canPass = SMH.Player.IsShrunk && i == SMH.Player.Tile.X;
                    }
                    else
                    {
                        canPass = SMH.Player.CanPass(Tiles[i, y].Collision);
                    }

                    //Ignore squares off the map
                    if (IsInBounds(i, j) && !canPass)
                    {
                        //Note that this is different than normal circle/box collision!!!
                        Rect rect = GetTerrainCollisionBox(Tiles[i, y].Collision, i, j);

                        //Test top and bottom of box
                        if (x > rect.X && x < rect.Right)
                        {
                            if (Math.Abs(rect.Bottom - y) < SMH.Player.Radius) return true;
                            if (Math.Abs(rect.Y - y) < SMH.Player.Radius) return true;
                        }

                        //Test left and right side of box
                        if (y > rect.Y && y < rect.Bottom)
                        {
                            if (Math.Abs(rect.Right - x) < SMH.Player.Radius) return true;
                            if (Math.Abs(rect.X - x) < SMH.Player.Radius) return true;
                        }

                        //Test the center of the box
                        if (SmileyUtil.Distance(rect.X + (rect.Right - rect.X) / 2f, rect.Y + (rect.Bottom - rect.Y) / 2f, x, y) < SMH.Player.Radius) return true;

                        //Now do a ton of shit to make smiley round corners
                        bool onlyDownPressed = SMH.Input.IsDown(Input.Down) && !SMH.Input.IsDown(Input.Up) && !SMH.Input.IsDown(Input.Left) && !SMH.Input.IsDown(Input.Right);
                        bool onlyUpPressed = !SMH.Input.IsDown(Input.Down) && SMH.Input.IsDown(Input.Up) && !SMH.Input.IsDown(Input.Left) && !SMH.Input.IsDown(Input.Right);
                        bool onlyLeftPressed = !SMH.Input.IsDown(Input.Down) && !SMH.Input.IsDown(Input.Up) && SMH.Input.IsDown(Input.Left) && !SMH.Input.IsDown(Input.Right);
                        bool onlyRightPressed = !SMH.Input.IsDown(Input.Down) && !SMH.Input.IsDown(Input.Up) && !SMH.Input.IsDown(Input.Left) && SMH.Input.IsDown(Input.Right);
                        float angle;

                        //Top left corner
                        if (SmileyUtil.Distance(rect.X, rect.Y, x, y) < SMH.Player.Radius)
                        {
                            if (SMH.Player.IsOnIce) return true;
                            angle = SmileyUtil.GetAngleBetween(rect.X, rect.Y, SMH.Player.X, SMH.Player.Y);
                            if (onlyDownPressed && SMH.Player.Facing == Direction.Down && x < rect.X && SMH.Player.CanPass(Tiles[i - 1, j].Collision) && !HasSillyPad(i - 1, j) && !onIce)
                            {
                                angle -= 4f * (float)Math.PI * dt;
                            }
                            else if (onlyRightPressed && SMH.Player.Facing == Direction.Right && y < rect.Y && SMH.Player.CanPass(Tiles[i, j - 1].Collision) && !HasSillyPad(i, j - 1) && !onIce)
                            {
                                angle += 4f * (float)Math.PI * dt;
                            }
                            else return true;
                            SMH.Player.X = rect.X + (SMH.Player.Radius + 1f) * (float)Math.Cos(angle);
                            SMH.Player.Y = rect.Y + (SMH.Player.Radius + 1f) * (float)Math.Sin(angle);
                            return true;
                        }

                        //Top right corner
                        if (SmileyUtil.Distance(rect.Right, rect.Y, x, y) < SMH.Player.Radius)
                        {
                            if (SMH.Player.IsOnIce) return true;
                            angle = SmileyUtil.GetAngleBetween(rect.Right, rect.Y, SMH.Player.X, SMH.Player.Y);
                            if (onlyDownPressed && SMH.Player.Facing == Direction.Down && x > rect.Right && SMH.Player.CanPass(Tiles[i + 1, j].Collision) && !HasSillyPad(i + 1, j) && !onIce)
                            {
                                angle += 4f * (float)Math.PI * dt;
                            }
                            else if (onlyLeftPressed && SMH.Player.Facing == Direction.Left && y < rect.Y && SMH.Player.CanPass(Tiles[i, j - 1].Collision) && !HasSillyPad(i, j - 1) && !onIce)
                            {
                                angle -= 4f * (float)Math.PI * dt;
                            }
                            else return true;
                            SMH.Player.X = rect.Right + (SMH.Player.Radius + 1f) * (float)Math.Cos(angle);
                            SMH.Player.Y = rect.Y + (SMH.Player.Radius + 1f) * (float)Math.Sin(angle);
                            return true;
                        }

                        //Bottom right corner
                        if (SmileyUtil.Distance(rect.Right, rect.Bottom, x, y) < SMH.Player.Radius)
                        {
                            if (SMH.Player.IsOnIce) return true;
                            angle = SmileyUtil.GetAngleBetween(rect.Right, rect.Bottom, SMH.Player.X, SMH.Player.Y);
                            if (onlyUpPressed && SMH.Player.Facing == Direction.Up && x > rect.Right && SMH.Player.CanPass(Tiles[i + 1, j].Collision) && !HasSillyPad(i + 1, j) && !onIce)
                            {
                                angle -= 4f * (float)Math.PI * dt;
                            }
                            else if (onlyLeftPressed && SMH.Player.Facing == Direction.Left && y > rect.Bottom && SMH.Player.CanPass(Tiles[i, j + 1].Collision) && !HasSillyPad(i, j + 1) && !onIce)
                            {
                                angle += 4f * (float)Math.PI * dt;
                            }
                            else return true;
                            SMH.Player.X = rect.Right + (SMH.Player.Radius + 1f) * (float)Math.Cos(angle);
                            SMH.Player.Y = rect.Bottom + (SMH.Player.Radius + 1f) * (float)Math.Sin(angle);
                            return true;
                        }

                        //Bottom left corner
                        if (SmileyUtil.Distance(rect.X, rect.Bottom, x, y) < SMH.Player.Radius)
                        {
                            if (SMH.Player.IsOnIce) return true;
                            angle = SmileyUtil.GetAngleBetween(rect.X, rect.Bottom, SMH.Player.X, SMH.Player.Y);
                            if (onlyUpPressed && SMH.Player.Facing == Direction.Up && x < rect.X && SMH.Player.CanPass(Tiles[i - 1, j].Collision) && !HasSillyPad(i - 1, j) && !onIce)
                            {
                                angle += 4f * (float)Math.PI * dt;
                            }
                            else if (onlyRightPressed && SMH.Player.Facing == Direction.Right && y > rect.Bottom && SMH.Player.CanPass(Tiles[i, j + 1].Collision) && !HasSillyPad(i, j + 1) && !onIce)
                            {
                                angle -= 4f * (float)Math.PI * dt;
                            }
                            else return true;
                            SMH.Player.X = rect.X + (SMH.Player.Radius + 1f) * (float)Math.Cos(angle);
                            SMH.Player.Y = rect.Bottom + (SMH.Player.Radius + 1f) * (float)Math.Sin(angle);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        ///  Returns whether or not an enemy bounded by *box box collides with any terrain. Also
        /// adjusts the enemy's position to help it round corners
        /// </summary>
        /// <param name="box"></param>
        /// <param name="enemy"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool EnemyCollision(Rect enemyBox, BaseEnemy enemy, float dt)
        {
            //Check all neighbor squares
            for (int i = enemy.Tile.X - 2; i <= enemy.Tile.X + 2; i++)
            {
                for (int j = enemy.Tile.Y - 2; j <= enemy.Tile.Y + 2; j++)
                {
                    //Ignore squares off the map
                    if (!IsInBounds(i, j))
                        continue;

                    if (HasSillyPad(i, j) || !enemy.CanPass(Tiles[i, j].Collision))
                    {
                        //Test collision
                        Rect box = GetTerrainCollisionBox(HasSillyPad(i, j) ? CollisionTile.UNWALKABLE : Tiles[i, j].Collision, i, j);
                        if (enemyBox.Intersects(box))
                        {
                            //Help the enemy round corners
                            if ((int)enemy.DX == 0 && enemy.DY > 0)
                            {
                                //Moving down
                                if (enemy.X < box.X)
                                {
                                    enemy.X -= enemy.Speed * dt;
                                }
                                else if (enemy.X > box.Right)
                                {
                                    enemy.X += enemy.Speed * dt;
                                }
                            }
                            else if ((int)enemy.DX == 0 && enemy.DY < 0)
                            {
                                //Moving up
                                if (enemy.X < box.X)
                                {
                                    enemy.X -= enemy.Speed * dt;
                                }
                                else if (enemy.X > box.Right)
                                {
                                    enemy.X += enemy.Speed * dt;
                                }
                            }
                            else if (enemy.DX < 0 && (int)enemy.DY == 0)
                            {
                                //Moving left
                                if (enemy.Y < box.Y)
                                {
                                    enemy.Y -= enemy.Speed * dt;
                                }
                                else if (enemy.Y > box.Bottom)
                                {
                                    enemy.Y += enemy.Speed * dt;
                                }
                            }
                            else if (enemy.DX > 0 && (int)enemy.DY == 0)
                            {
                                //Moving right
                                if (enemy.Y < box.Y)
                                {
                                    enemy.Y -= enemy.Speed * dt;
                                }
                                else if (enemy.Y > box.Bottom)
                                {
                                    enemy.Y += enemy.Speed * dt;
                                }
                            }

                            return true;
                        }
                    }
                    else if (!IsInBounds(i, j))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Reads any sign hit by the player's tongue.
        /// </summary>
        /// <param name="tongue"></param>
        /// <returns></returns>
        public bool HitSigns(Tongue tongue)
        {
            for (int i = SMH.Player.Tile.X - 2; i <= SMH.Player.Tile.X + 2; i++)
            {
                for (int j = SMH.Player.Tile.Y - 2; j <= SMH.Player.Tile.Y + 2; j++)
                {
                    if (IsInBounds(i, j) && Tiles[i, j].Collision == CollisionTile.SIGN)
                    {
                        Rect box = new Rect(i * 64, j * 64, 64, 64);
                        if (tongue.Intersects(box))
                        {
                            //smh->windowManager->openSignTextBox(ids[i][j]);//TODO:
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Opens the save menu if tongue collides with a save shrine
        /// </summary>
        /// <param name="tongue"></param>
        /// <returns></returns>
        public bool HitSaveShrine(Tongue tongue)
        {
            for (int i = SMH.Player.Tile.X - 2; i <= SMH.Player.Tile.X + 2; i++)
            {
                for (int j = SMH.Player.Tile.Y - 2; j <= SMH.Player.Tile.Y + 2; j++)
                {
                    if (IsInBounds(i, j) && Tiles[i, j].Collision == CollisionTile.SAVE_SHRINE)
                    {
                        Rect box = new Rect(i * 64, j * 64, 64, 64);
                        if (tongue.Intersects(box))
                        {
                            //smh->windowManager->openMiniMenu(MiniMenuMode::MINIMENU_SAVEGAME);//TODO:
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Bombs any bombable walls at the given coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void BombWall(int x, int y)
        {
            if (IsInBounds(x, y) && Tiles[x, y].Collision == CollisionTile.BOMBABLE_WALL)
            {
                Tiles[x, y].Collision = CollisionTile.WALKABLE;
                SMH.SaveManager.CurrentSave.ChangeTile(x, y);
            }
        }

        /// <summary>
        /// Returns whether or not box collides with any silly pads or terrain 
        /// as specified by canPass.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="canPass"></param>
        /// <returns></returns>
        public bool TestCollision(Rect box, Dictionary<CollisionTile, bool> canPass)
        {
            return TestCollision(box, canPass, false);
        }

        /// <summary>
        /// Returns whether or not box collides with terrain as specified by canPass.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="canPass"></param>
        /// <returns></returns>
        public bool TestCollision(Rect box, Dictionary<CollisionTile, bool> canPass, bool ignoreSillyPads)
        {
            //Determine the location of the collision box
            int gridX = Convert.ToInt32((box.X + box.Width / 2f) / 64f);
            int gridY = Convert.ToInt32((box.Y + box.Height / 2f) / 64f);

            //Check all neighbor squares
            for (int i = gridX - 2; i <= gridX + 2; i++)
            {
                for (int j = gridY - 2; j <= gridY + 2; j++)
                {
                    //Ignore squares off the map
                    if (IsInBounds(i, j) && (!canPass[Tiles[i, j].Collision] || (!ignoreSillyPads && HasSillyPad(i, j))))
                    {
                        //Test collision
                        Rect terrainBox = GetTerrainCollisionBox((!ignoreSillyPads && HasSillyPad(i, j))
                                ? CollisionTile.UNWALKABLE : Tiles[i, j].Collision, i, j);

                        if (box.Intersects(terrainBox))
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Places a silly pad at the specified grid location.
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        public void PlaceSillyPad(int gridX, int gridY)
        {
            //specialTileManager->addSillyPad(gridX, gridY);TODO:

            //Play sound effect
            SMH.Sound.PlaySound(Sound.SillyPad);
        }

        public bool DestroySillyPad(int gridX, int gridY)
        {
            return true; //TODO:
            //return specialTileManager->destroySillyPad(gridX, gridY);
        }

        public void UpdateAdviceMan(float dt)
        {
            //TODO:
            //if (adviceMan) adviceMan->update(dt);
        }

        public bool IsAdviceManActive()
        {
            return true;//TODO:
            //return (adviceMan && adviceMan->isActive());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Attempts to toggle a switch at (gridX, gridY). Returns whether or not there is
        /// a switch there to toggle. 
        ///
        /// All other switch toggling functions will eventually call this, which either contains the switching
        /// logic or delegates it to another method.
        ///
        /// All switch sound effects also originate from this method. A switch sound will be triggered if either the 
        /// switched switch is close to smiley, or one of the things it switched is close to smiley. If the
        /// playSoundFarAway parameter is true then the sound will be guaranteed to play if a switch is switched.
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <param name="playSoundFarAway"></param>
        /// <param name="playTimerSound"></param>
        /// <returns></returns>
        private bool ToggleSwitchAt(int gridX, int gridY, bool playSoundFarAway, bool playTimerSound)
        {
            Tile tile = Tiles[gridX, gridY];
            int switchID = tile.ID;
            bool hasSwitch = false;
            bool hitToggledTimedSwitch = (SMH.Now - tile.ActivatedTime < tile.Variable) && SMH.SaveManager.CurrentSave.IsTileChanged(gridX, gridY);

            if (SmileyUtil.IsCylinderSwitch(tile.Collision))
            {
                //Flip cylinder switch
                FlipCylinderSwitch(gridX, gridY);
                hasSwitch = true;

            }
            else if (tile.Collision == CollisionTile.SHRINK_TUNNEL_SWITCH)
            {
                //Rotate Shrink tunnels
                hasSwitch = true;
                tile.ActivatedTime = SMH.GameTime;
                //smh->resources->GetAnimation("shrinkTunnelSwitch")->Play();//TODO:
                SMH.SaveManager.CurrentSave.ChangeTile(gridX, gridY);

                //Loop through the grid and look for shrink tunnels with the same id as the switch
                foreach (Tile otherTile in Tiles.Where(otherTile => otherTile.ID == switchID))
                {
                    SMH.Sound.PlaySwitchSound(otherTile.X, otherTile.Y, false);
                    //When found, rotate clockwise.
                    if (otherTile.Collision == CollisionTile.SHRINK_TUNNEL_HORIZONTAL)
                        otherTile.Collision = CollisionTile.SHRINK_TUNNEL_VERTICAL;
                    else if (otherTile.Collision == CollisionTile.SHRINK_TUNNEL_VERTICAL)
                        otherTile.Collision = CollisionTile.SHRINK_TUNNEL_HORIZONTAL;
                }
            }
            else if (tile.Collision == CollisionTile.SPIN_ARROW_SWITCH)
            {
                //Rotate arrows switch
                hasSwitch = true;
                tile.ActivatedTime = SMH.GameTime;
                //smh->resources->GetAnimation("bunnySwitch")->Play();//TODO:

                //Loop through the grid and look for arrows with the same id as the switch
                foreach (Tile otherTile in Tiles.Where(otherTile => otherTile.ID == switchID))
                {
                    SMH.Sound.PlaySwitchSound(tile.X, tile.Y, false);
                    //When found, rotate clockwise.
                    if (otherTile.Collision == CollisionTile.UP_ARROW)
                        otherTile.Collision = CollisionTile.RIGHT_ARROW;
                    else if (otherTile.Collision == CollisionTile.RIGHT_ARROW)
                        otherTile.Collision = CollisionTile.DOWN_ARROW;
                    else if (tile.Collision == CollisionTile.DOWN_ARROW)
                        otherTile.Collision = CollisionTile.LEFT_ARROW;
                    else if (tile.Collision == CollisionTile.LEFT_ARROW)
                        otherTile.Collision = CollisionTile.UP_ARROW;
                }
            }
            else if (tile.Collision == CollisionTile.MIRROR_SWITCH)
            {
                //Rotate mirrors switch
                hasSwitch = true;
                tile.ActivatedTime = SMH.GameTime;
                //smh->resources->GetAnimation("mirrorSwitch")->Play();//TODO:

                foreach (Tile otherTile in Tiles.Where(otherTile => otherTile.ID == switchID))
                {
                    SMH.Sound.PlaySwitchSound(tile.X, tile.Y, false);
                    if (otherTile.Collision == CollisionTile.MIRROR_UP_LEFT) otherTile.Collision = CollisionTile.MIRROR_UP_RIGHT;
                    else if (otherTile.Collision == CollisionTile.MIRROR_UP_RIGHT) otherTile.Collision = CollisionTile.MIRROR_DOWN_RIGHT;
                    else if (otherTile.Collision == CollisionTile.MIRROR_DOWN_RIGHT) otherTile.Collision = CollisionTile.MIRROR_DOWN_LEFT;
                    else if (otherTile.Collision == CollisionTile.MIRROR_DOWN_LEFT) otherTile.Collision = CollisionTile.MIRROR_UP_LEFT;
                }
            }

            if (hasSwitch)
            {
                SMH.Sound.PlaySwitchSound(gridX, gridY, playSoundFarAway);

                if (hitToggledTimedSwitch)
                {
                    //If a timed switch was already toggled and then we just untoggled it, we need to
                    //remove the switch's timer.
                    KillSwitchTimer(gridX, gridY);
                }
                else if (tile.Variable != -1)
                {
                    //If this is a timed switch, create a timer to display the time left over the switch
                    _timers.Add(new Timer
                    {
                        X = gridX * 64 + 32,
                        Y = gridY * 64,
                        Duration = tile.Variable,
                        StartTime = SMH.GameTime,
                        LastClockTickTime = SMH.GameTime,
                        PlayTickSound = playTimerSound
                    });
                }
            }

            return hasSwitch;
        }

        /// <summary>
        /// Flips the cylinder switch at gridX, gridY
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        private void FlipCylinderSwitch(int gridX, int gridY)
        {
            //activated[gridX][gridY] = smh->getGameTime();

            ////Flip switch in collision layer
            //if (Util::isCylinderSwitchLeft(collision[gridX][gridY])) {
            //    collision[gridX][gridY] += 16;
            //    smh->resources->GetAnimation("silverSwitch")->SetMode(HGEANIM_FWD);
            //    smh->resources->GetAnimation("brownSwitch")->SetMode(HGEANIM_FWD);
            //    smh->resources->GetAnimation("blueSwitch")->SetMode(HGEANIM_FWD);
            //    smh->resources->GetAnimation("greenSwitch")->SetMode(HGEANIM_FWD);
            //    smh->resources->GetAnimation("yellowSwitch")->SetMode(HGEANIM_FWD);
            //    smh->resources->GetAnimation("whiteSwitch")->SetMode(HGEANIM_FWD);
            //    smh->saveManager->change(gridX, gridY);
            //} else if (Util::isCylinderSwitchRight(collision[gridX][gridY])) {
            //    collision[gridX][gridY] -= 16;
            //    smh->resources->GetAnimation("silverSwitch")->SetMode(HGEANIM_REV);
            //    smh->resources->GetAnimation("brownSwitch")->SetMode(HGEANIM_REV);
            //    smh->resources->GetAnimation("blueSwitch")->SetMode(HGEANIM_REV);
            //    smh->resources->GetAnimation("greenSwitch")->SetMode(HGEANIM_REV);
            //    smh->resources->GetAnimation("yellowSwitch")->SetMode(HGEANIM_REV);
            //    smh->resources->GetAnimation("whiteSwitch")->SetMode(HGEANIM_REV);
            //    smh->saveManager->change(gridX, gridY);
            //}

            ////Play animation
            //smh->resources->GetAnimation("silverSwitch")->Play();
            //smh->resources->GetAnimation("brownSwitch")->Play();
            //smh->resources->GetAnimation("blueSwitch")->Play();
            //smh->resources->GetAnimation("greenSwitch")->Play();
            //smh->resources->GetAnimation("yellowSwitch")->Play();
            //smh->resources->GetAnimation("whiteSwitch")->Play();
            //activated[gridX][gridY] = smh->getGameTime();

            ////Switch up and down cylinders if the player isn't on top of any down cylindersw
            //if (!playerOnCylinder(gridX,gridY)) {
            //    switchCylinders(ids[gridX][gridY]);
            //}
        }

        /// <summary>
        /// Switches all cylinders with the specified ID.
        /// </summary>
        /// <param name="switchID"></param>
        private void SwitchCylinders(int switchID)
        {
            if (switchID < 0) return;

            //Switch up and down cylinders if the player isn't on top of any down cylinders
            foreach (Tile tile in Tiles.Where(tile => tile.ID == switchID))
            {
                SMH.Sound.PlaySwitchSound(tile.X, tile.Y, false);
                if (SmileyUtil.IsCylinderUp(tile.Collision))
                {
                    tile.Collision -= 16;
                    tile.ActivatedTime = SMH.GameTime;
                }
                else if (SmileyUtil.IsCylinderDown(tile.Collision))
                {
                    tile.Collision += 16;
                    tile.ActivatedTime = SMH.GameTime;
                }
                //TODO:
                //silverCylinder->Play();
                //brownCylinder->Play();
                //blueCylinder->Play();
                //greenCylinder->Play();
                //yellowCylinder->Play();
                //whiteCylinder->Play();
                //silverCylinderRev->Play();
                //brownCylinderRev->Play();
                //blueCylinderRev->Play();
                //greenCylinderRev->Play();
                //yellowCylinderRev->Play();
                //whiteCylinderRev->Play();
            }
        }

        /// <summary>
        /// Get a collision box for the speicifed collision type decalared in smiley.
        /// This allows different things to have different shaped collision boxes.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="whatFor"></param>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        private Rect GetTerrainCollisionBox(CollisionTile collision, int gridX, int gridY)
        {
            if (collision == CollisionTile.FOUNTAIN)
            {
                float x = (gridX - 1) * 64;
                float y = gridY * 64 + 35;
                float right = (gridX + 2) * 64;
                float bottom = (gridY + 1) * 64 + 10;
                return new Rect(x, y, right - x, bottom - y);
            }
            else
            {
                return new Rect(gridX * 64f, gridY * 64f, 64f, 64f);
            }
        }

        private void DrawPits(float dt)
        {
            //bool draw;
            //int drawX, drawY, gridX, gridY;

            ////Loop through each tile to draw the parallax layer
            //for (int j = -1; j <= screenHeight + 1; j++) {
            //    for (int i = -1; i <= screenWidth + 1; i++) {
            //        draw = false;

            //        drawX = int(i*64.0 - xOffset*.5); if (xGridOffset%2) drawX += 32;
            //        drawY = int(j*64.0 - yOffset*.5); if (yGridOffset%2) drawY += 32;
            //        gridX = Util::getGridX(i*64.0 + xGridOffset*64);
            //        gridY = Util::getGridY(j*64.0 + yGridOffset*64);

            //        //Only draw the pit graphic here if it is in bounds and near a pit to improve performance.

            //        for (int x = gridX-1; x <= gridX+1; x++) {
            //            for (int y = gridY-1; y <= gridY+1; y++) {
            //                if (!isInBounds(x,y)) break;
            //                if (collision[x][y] == PIT || collision[x][y] == FAKE_PIT || collision[x][y] == NO_WALK_PIT) draw = true;
            //            }
            //        }

            //        if (draw && isInBounds(gridX, gridY)) {
            //            smh->resources->GetSprite("parallaxPit")->Render(drawX, drawY);
            //        }
            //    }
            //}

            //int smileyScreenX = smh->getScreenX(SMH.Player.x);
            //int smileyScreenY = smh->getScreenY(SMH.Player.y);

            //int smileyGridX = Util::getGridX(smileyScreenX - xGridOffset*64 - xOffset);
            //int smileyGridY = Util::getGridY(smileyScreenY - yGridOffset*64 - yOffset);
        }

        /// <summary>
        /// Draws the number of seconds left over every activated timed switch.
        /// </summary>
        /// <param name="dt"></param>
        private void DrawSwitchTimers(float dt)
        {
            //for (std::list<Timer>::iterator i = timerList.begin(); i != timerList.end(); i++) {
            //    smh->resources->GetFont("controls")->SetColor(ARGB(255,255,255,255));
            //    smh->resources->GetFont("controls")->SetScale(1.0);
            //    smh->resources->GetFont("controls")->printf(smh->getScreenX(i->x), smh->getScreenY(i->y),
            //        HGETEXT_CENTER, "%d", int(i->duration - smh->timePassedSince(i->startTime)) + 1);
            //        smh->resources->GetFont("controls")->SetColor(ARGB(255,0,0,0));
            //}
        }

        private void UpdateSwitchTimers(float dt)
        {
            //for (std::list<Timer>::iterator i = timerList.begin(); i != timerList.end(); i++) {
            //    if (i->playTickSound && smh->timePassedSince(i->lastClockTickTime) > 1.0) {
            //        i->lastClockTickTime = smh->getGameTime();
            //        smh->soundManager->playSound("snd_ClockTick", 1.0);
            //    }
            //    if (smh->timePassedSince(i->startTime) > i->duration) {
            //        i = timerList.erase(i);
            //    }
            //}
        }

        /// <summary>
        /// Returns whether or not the environment should draw the collision sprite for the
        /// given collision type.
        /// </summary>
        /// <param name="collision"></param>
        /// <returns></returns>
        public bool ShouldEnvironmentDrawCollision(CollisionTile collision)
        {
            return collision != CollisionTile.WALKABLE &&
                   collision != CollisionTile.UNWALKABLE &&
                   collision != CollisionTile.ENEMY_NO_WALK &&
                   collision != CollisionTile.PLAYER_START &&
                   collision != CollisionTile.DIZZY_MUSHROOM_1 &&
                   collision != CollisionTile.DIZZY_MUSHROOM_2 &&
                   collision != CollisionTile.PLAYER_END &&
                   collision != CollisionTile.PIT &&
                   collision != CollisionTile.UNWALKABLE_PROJECTILE &&
                   collision != CollisionTile.FAKE_PIT &&
                   collision != CollisionTile.FLAME &&
                   collision != CollisionTile.NO_WALK_PIT &&
                   collision != CollisionTile.FIRE_DESTROY &&
                   collision != CollisionTile.FAKE_COLLISION &&
                   collision != CollisionTile.FOUNTAIN &&
                   !SmileyUtil.IsWarp(collision);
        }


        #endregion

        private class Timer
        {
            public float Duration { get; set; }
            public float StartTime { get; set; }
            public float LastClockTickTime { get; set; }
            public float X { get; set; }
            public float Y { get; set; }
            public bool PlayTickSound { get; set; }
        };
    }
}
