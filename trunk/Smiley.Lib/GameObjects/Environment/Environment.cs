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
using Microsoft.Xna.Framework;

namespace Smiley.Lib.GameObjects.Environment
{
    public class SmileyEnvironment
    {
        #region Private Variables

        private List<Timer> _timers = new List<Timer>();
        private Fountain _fountain;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new SmileyEnvironment.
        /// </summary>
        public SmileyEnvironment()
        {
            Animations.SaveShrine.Play();
            Animations.Water.Play();
            Animations.GreenWater.Play();
            Animations.Lava.Play();

            //smh->log("Creating Environment.SpecialTileManager");
            //specialTileManager = new SpecialTileManager();
            //smh->log("Creating Environment.EvilWallManager");
            //evilWallManager = new EvilWallManager();
            //smh->log("Creating Environment.TapestryManager");
            //tapestryManager = new TapestryManager();
            //smh->log("Creating Environment.SmileletManager");
            //smileletManager = new SmileletManager();

            //collisionBox = new hgeRect();
            //collisionCircle = new CollisionCircle();
        }

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

        /// <summary>
        /// Number of pixels the player is off alignment with the grid.
        /// </summary>
        public float XOffset { get; private set; }

        /// <summary>
        /// Number of pixels the player is off alignment with the grid.
        /// </summary>
        public float YOffset { get; private set; }

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
            SMH.SaveManager.CurrentSave.Level = level;

            //TODO:
            //Setup collision stuff
            foreach (Tile tile in Tiles)
            {
                switch (tile.Collision)
                {
                    case CollisionTile.FOUNTAIN:
                        _fountain = new Fountain(tile.X, tile.Y);
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
                    (ItemTile)tile.Item != ItemTile.MANA_ITEM && (ItemTile)tile.Item != ItemTile.HEALTH_ITEM)
                {
                    tile.Item = (int)ItemTile.NONE;
                }
                else if ((int)tile.Item >= 16 && (int)tile.Item < 32)
                {
                    //tapestryManager->addTapestry(col, row, newItem);
                }

                //Setup enemies
                int enemy = tile.Enemy + 1;//enemy codes are fucked in the editor
                if (enemy == 255)
                {
                    //255 is fenwar encounter
                    if (!SMH.SaveManager.CurrentSave.IsTileChanged(tile.X, tile.Y))
                    {
                        //smh->fenwarManager->addFenwarEncounter(col, row, ids[col][row]);
                    }
                }
                else if (enemy >= 240)
                {
                    //240-254 are bosses
                    if (!SMH.SaveManager.CurrentSave.HasKilledBoss[(Boss)enemy])
                    {
                        //smh->bossManager->spawnBoss(enemy, variable[col][row], col, row);
                    }
                }
                else if (enemy > 0 && tile.Enemy < SMH.Data.Enemies.Count)
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
                else if (enemy >= 128 && enemy < 240)
                {
                    if (enemy != 128 +  Constants.MonocleManNPCId || SMH.SaveManager.CurrentSave.AdviceManEncounterCompleted)
                    {
                        SMH.NPCManager.AdddNPC(enemy - 128, tile.ID, tile.X, tile.Y);
                    }
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
            for (int i = 0; i < 2; i++)
            {
                Tile startTile = Tiles.FirstOrDefault(tile => tile.Collision == CollisionTile.PLAYER_START && ((Level)tile.ID == previousLevel) || i == 1);
                if (startTile != null)
                {
                    SMH.Player.MoveTo(startTile.X, startTile.Y);
                    break;
                }
            }

            //if (smh->saveManager->currentArea == FOUNTAIN_AREA && !smh->saveManager->adviceManEncounterCompleted)
            //{
            //    adviceMan = new AdviceMan(SMH.Player.gridX, SMH.Player.gridY + 1);
            //}

            //Update to get shit set up
            Update(0);
            SMH.EnemyManager.Update(0);
            SMH.AreaChanger.DisplayNewAreaName();

            if (playMusic)
            {
                SMH.Sound.PlayLevelMusic(level);
            }
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

            _fountain = null;

            //if (adviceMan) {
            //    delete adviceMan;
            //    adviceMan = NULL;
            //}

            //for (std::list<Timer>::iterator i = timerList.begin(); i != timerList.end(); i++) {
            //    i = timerList.erase(i);
            //}

            //smh->explosionManager->reset();
        }

        public void Update(float dt)
        {
            //Update animations
            Animations.SaveShrine.Update(dt);
            Animations.Water.Update(dt);
            Animations.GreenWater.Update(dt);
            Animations.Lava.Update(dt);
            Animations.SilverSwitch.Update(dt);
            Animations.BrownSwitch.Update(dt);
            Animations.BlueSwitch.Update(dt);
            Animations.GreenSwitch.Update(dt);
            Animations.WhiteSwitch.Update(dt);
            Animations.SilverCylinder.Update(dt);
            Animations.BrownCylinder.Update(dt);
            Animations.BlueCylinder.Update(dt);
            Animations.GreenCylinder.Update(dt);
            Animations.WhiteCylinder.Update(dt);
            Animations.BunnySwitch.Update(dt);
            Animations.ShrinkTunnelSwitch.Update(dt);
            Animations.MirrorSwitch.Update(dt);

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
            UpdateSwitchTimers(dt);

            if (_fountain != null)
                _fountain.Update(dt);
        }

        public void Draw()
        {
            DrawPits();

            //Loop through each tile to draw shit
            for (int j = SMH.Player.Tile.Y - 6; j < SMH.Player.Tile.Y + 7; j++)
            {
                int drawY = Convert.ToInt32(SMH.GetScreenY(j * 64 - XOffset));
                for (int i = SMH.Player.Tile.X - 9; i < SMH.Player.Tile.X + 9; i++)
                {
                    float drawX = Convert.ToInt32(SMH.GetScreenX(i * 64 - XOffset));

                    if (IsInBounds(i, j))
                        Tiles[i, j].DrawBeforeSmiley(drawX, drawY);
                    else
                        SMH.Graphics.DrawSprite(Sprites.BlackSquare, drawX, drawY);
                }
            }

            //Draw fountain before smiley if he is below it
            if (_fountain != null && !_fountain.IsAboveSmiley())
            {
                _fountain.Draw();
            }

            ////Draw particles
            //for (std::list<ParticleStruct>::iterator i = particleList.begin(); i != particleList.end(); i++) {
            //    i->particle->MoveTo(smh->getScreenX(i->x), smh->getScreenY(i->y), true);
            //    i->particle->Update(dt);
            //    i->particle->Render();
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
        public void DrawAfterSmiley()
        {
            for (int j = SMH.Player.Tile.Y - 6; j < SMH.Player.Tile.Y + 7; j++)
            {
                int drawY = Convert.ToInt32(SMH.GetScreenY(j * 64 - XOffset));
                for (int i = SMH.Player.Tile.X - 9; i < SMH.Player.Tile.X + 9; i++)
                {
                    float drawX = Convert.ToInt32(SMH.GetScreenX(i * 64 - XOffset));
                    if (IsInBounds(i, j))
                    {
                        Tiles[i, j].DrawAfterSmiley(drawX, drawY);
                    }
                }
            }

            //draw fountain after smiley if he is above it
            if (_fountain != null && _fountain.IsAboveSmiley())
            {
                _fountain.Draw();
            }
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
        public void UnlockDoor(int gridX, int gridY)
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
                        if (SMH.GameTimePassed(Tiles[gridX, gridY].ActivatedTime, Constants.SwitchDelay) && tileBox.Intersects(box))
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
        public bool ToggleSwitches(Tongue tongue)
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
                        if (SMH.GameTimePassed(Tiles[gridX, gridY].ActivatedTime, Constants.TongueSwitchDelay) && tongue.Intersects(box))
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
            ItemTile retVal = (ItemTile)Tiles[x, y].Item;
            Tiles[x, y].Item = (int)ItemTile.NONE;
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
        /// Returns whether or not the given grid square has a silly pad.
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <returns></returns>
        public bool HasSillyPad(int gridX, int gridY)
        {
            return false;//TODO:
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
        public bool PlayerCollision(float x, float y, float dt)
        {
            //Determine the location of the collision box
            int gridX = Convert.ToInt32(x / 64f);
            int gridY = Convert.ToInt32(y / 64f);

            bool onIce = Tiles[SMH.Player.Tile.X, SMH.Player.Tile.Y].Collision == CollisionTile.ICE;

            //Check all neighbor squares
            for (int i = gridX - 2; i <= gridX + 2; i++)
            {
                for (int j = gridY - 2; j <= gridY + 2; j++)
                {
                    //Special logic for shrink tunnels
                    bool canPass;
                    if (Tiles[i, j].Collision == CollisionTile.SHRINK_TUNNEL_HORIZONTAL)
                    {
                        canPass = SMH.Player.IsShrunk && j == SMH.Player.Tile.Y;
                    }
                    else if (Tiles[i, j].Collision == CollisionTile.SHRINK_TUNNEL_VERTICAL)
                    {
                        canPass = SMH.Player.IsShrunk && i == SMH.Player.Tile.X;
                    }
                    else
                    {
                        canPass = SMH.Player.CanPass(Tiles[i, j].Collision);
                    }

                    //Ignore squares off the map
                    if (IsInBounds(i, j) && !canPass)
                    {
                        //Note that this is different than normal circle/box collision!!!
                        Rect rect = GetTerrainCollisionBox(Tiles[i, j].Collision, i, j);

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
                            if (SMH.Player.IsIceSliding) return true;
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
                            if (SMH.Player.IsIceSliding) return true;
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
                            if (SMH.Player.IsIceSliding) return true;
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
                            if (SMH.Player.IsIceSliding) return true;
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
        public bool TestCollision(Rect box, Func<CollisionTile, bool> canPass)
        {
            return TestCollision(box, canPass, false);
        }

        /// <summary>
        /// Returns whether or not box collides with terrain as specified by canPass.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="canPass"></param>
        /// <returns></returns>
        public bool TestCollision(Rect box, Func<CollisionTile, bool> canPass, bool ignoreSillyPads)
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
                    if (IsInBounds(i, j) && (!canPass(Tiles[i, j].Collision) || (!ignoreSillyPads && HasSillyPad(i, j))))
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

        private void DrawPits()
        {
            //TODO:
            //bool draw;
            //int drawX, drawY, gridX, gridY;

            //Loop through each tile to draw the parallax layer
            for (int j = -1; j <= Tiles.Height + 1; j++)
            {
                for (int i = -1; i <= Tiles.Width + 1; i++)
                {
                    //draw = false;

                    //drawX = int(i*64.0 - xOffset*.5); if (xGridOffset%2) drawX += 32;
                    //drawY = int(j*64.0 - yOffset*.5); if (yGridOffset%2) drawY += 32;
                    //gridX = SmileyUtil.GetGridX(i*64.0 + xGridOffset*64);
                    //gridY = SmileyUtil.GetGridY(j*64.0 + yGridOffset*64);

                    ////Only draw the pit graphic here if it is in bounds and near a pit to improve performance.

                    //for (int x = gridX-1; x <= gridX+1; x++) {
                    //    for (int y = gridY-1; y <= gridY+1; y++) {
                    //        if (!isInBounds(x,y)) break;
                    //        if (collision[x][y] == PIT || collision[x][y] == FAKE_PIT || collision[x][y] == NO_WALK_PIT) draw = true;
                    //    }
                    //}

                    //if (draw && isInBounds(gridX, gridY)) {
                    //    smh->resources->GetSprite("parallaxPit")->Render(drawX, drawY);
                    //}
                }
            }
        }

        /// <summary>
        /// Draws the number of seconds left over every activated timed switch.
        /// </summary>
        /// <param name="dt"></param>
        private void DrawSwitchTimers(float dt)
        {
            foreach (Timer timer in _timers)
            {
                string text = (Convert.ToInt32(timer.Duration - (SMH.GameTime - timer.StartTime)) + 1).ToString();
                SMH.Graphics.DrawString(SmileyFont.Controls, text, SMH.GetScreenX(timer.X), SMH.GetScreenY(timer.Y), TextAlignment.Center, Color.White);
            }
        }

        private void UpdateSwitchTimers(float dt)
        {
            _timers.RemoveAll(timer =>
                {
                    if (timer.PlayTickSound && SMH.GameTimePassed(timer.LastClockTickTime, 1f))
                    {
                        timer.LastClockTickTime = SMH.GameTime;
                        SMH.Sound.PlaySound(Sound.ClickTick);
                    }
                    return SMH.GameTimePassed(timer.StartTime, timer.Duration);
                });
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
