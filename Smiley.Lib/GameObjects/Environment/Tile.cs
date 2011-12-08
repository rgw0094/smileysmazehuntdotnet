using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using System.Collections;
using System.IO;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Data;
using Smiley.Lib.Util;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.GameObjects.Environment
{
    /// <summary>
    /// A single tile in a smiley level.
    /// </summary>
    public class Tile
    {
        #region Private Variables

        private CollisionTile _collision;
        private Animation _animation;
        private float _animationFiredTime;

        #endregion

        #region Properties

        public int X { get; set; }
        public int Y { get; set; }
        public int ID { get; set; }
        public int Variable { get; set; }
        public int Terrain { get; set; }
        public int Item { get; set; }
        public int Enemy { get; set; }
        public float ActivatedTime { get; set; }

        public CollisionTile Collision
        {
            get { return _collision; }
            set
            {
                if (_collision != value)
                {
                    _collision = value;
                    _animation = GetAnimation(value);
                }
            }
        }

        /// <summary>
        /// Returns whether or not there is deep water of any kind on the tile.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool HasDeepWater
        {
            get
            {
                return Collision == CollisionTile.DEEP_WATER ||
                       Collision == CollisionTile.GREEN_WATER;
            }
        }

        /// <summary>
        /// Returns whether this is a good spot to "return" to after drowning or falling.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsReturnSpot
        {
            get
            {
                return
                    Collision == CollisionTile.WALKABLE ||
                    Collision == CollisionTile.SHALLOW_WATER ||
                    Collision == CollisionTile.WALK_LAVA ||
                    Collision == CollisionTile.RED_WARP ||
                    Collision == CollisionTile.BLUE_WARP ||
                    Collision == CollisionTile.YELLOW_WARP ||
                    Collision == CollisionTile.GREEN_WARP ||
                    Collision == CollisionTile.SHALLOW_GREEN_WATER ||
                    Collision == CollisionTile.BOMB_PAD_UP ||
                    Collision == CollisionTile.BOMB_PAD_DOWN ||
                    Collision == CollisionTile.HOVER_PAD ||
                    Collision == CollisionTile.SUPER_SPRING ||
                    Collision == CollisionTile.SMILELET ||
                    Collision == CollisionTile.SMILELET_FLOWER_HAPPY ||
                    Collision == CollisionTile.FAKE_COLLISION ||
                    Collision == CollisionTile.PLAYER_START ||
                    (Collision >= CollisionTile.WHITE_CYLINDER_DOWN && Collision <= CollisionTile.SILVER_CYLINDER_DOWN) ||
                    (Collision >= CollisionTile.EVIL_WALL_POSITION && Collision <= CollisionTile.EVIL_WALL_RESTART);
            }
        }

        /// <summary>
        /// Returns whether or not there is shallow water of any kind at grid on the tile.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool HasShallowWater
        {
            get
            {
                return Collision == CollisionTile.SHALLOW_WATER ||
                       Collision == CollisionTile.SHALLOW_GREEN_WATER;
            }
        }

        /// <summary>
        /// Returns whether or not there is an arrow pad on the tile.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool HasArrowPad
        {
            get
            {
                return Collision >= CollisionTile.UP_ARROW && Collision <= CollisionTile.LEFT_ARROW;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Toggles this tile's animation if it has one.
        /// </summary>
        public void ToggleAnimation()
        {
            if (_animation == null)
                return;

            _animation.Play();
        }

        /// <summary>
        /// Draws tile stuff that needs to be drawn before smiley.
        /// </summary>
        public void DrawBeforeSmiley(float x, float y)
        {
            //Draw Terrain
            if (Collision != CollisionTile.PIT && Collision != CollisionTile.FAKE_PIT && Collision != CollisionTile.NO_WALK_PIT)
            {
                SMH.Graphics.DrawSprite(SpriteSets.MainLayer[Terrain], x, y);
            }

            //Draw Collision
            if (_animation != null)
            {
                if (Collision == CollisionTile.SHALLOW_WATER || Collision == CollisionTile.SHALLOW_GREEN_WATER)
                    SMH.Graphics.DrawAnimation(_animation, x, y, Color.FromNonPremultiplied(255, 255, 255, 125));
                else
                    SMH.Graphics.DrawAnimation(_animation, x, y);
            }
            else if (SmileyUtil.ShouldDrawCollision(Collision))
            {
                Color color = Color.White;
                if (Collision >= CollisionTile.UP_ARROW && Collision <= CollisionTile.LEFT_ARROW)
                {
                    if (ID == -1 || ID == 990)
                    {
                        //Render normal, red arrow
                        color = Color.FromNonPremultiplied(255, 0, 255, 255);
                    }
                    else
                    {
                        //It's a rotating arrow, make it green
                        color = Color.FromNonPremultiplied(0, 255, 255, 255);
                    }
                }
                SMH.Graphics.DrawSprite(SpriteSets.WalkLayer[(int)Collision], x, y, color);
            }

            //Items
            if (Item == (int)ItemTile.EnemyGroupBlockGraphic)
            {
                //If this is an enemy block, draw it with the enemy group's block alpha
                //TODO: SMH.Graphics.DrawSprite(SpriteSets.ItemLayer[Item], x, y, Color.FromNonPremultiplied(255, 255, 255, smh->enemyGroupManager->groups[variable[i + xGridOffset][j + yGridOffset]].blockAlpha));
            }
            else if (Item != (int)ItemTile.NONE && ID != Constants.ID_DrawAfterSmiley)
            {
                SMH.Graphics.DrawSprite(SpriteSets.ItemLayer[Item], x, y);
            }
        }

        /// <summary>
        /// Draws tile stuff that needs to be drawn after smiley.
        /// </summary>
        public void DrawAfterSmiley(float x, float y)
        {
            //Marked as draw after smiley or the thing above smiley is marked as draw
            //above smiley and smiley is behind it. that way you can't walk behind a tree
            //and lick through it.
            if (ID == Constants.ID_DrawAfterSmiley ||
                    (SMH.Environment.Tiles[X, Y - 1].ID == Constants.ID_DrawAfterSmiley && SMH.Player.Tile.X == X && SMH.Player.Tile.Y < Y))
            {
                SMH.Graphics.DrawSprite(SpriteSets.ItemLayer[Item], x, y);
            }

            //Shrink tunnels unless smiley is directly underneath it and not shrunk
            if ((Collision == CollisionTile.SHRINK_TUNNEL_HORIZONTAL ||
                    Collision == CollisionTile.SHRINK_TUNNEL_VERTICAL) &&
                    !(SMH.Player.Tile.Y == Y + 1 && !SMH.Player.IsShrunk))
            {
                SMH.Graphics.DrawSprite(SpriteSets.WalkLayer[(int)Collision], x, y);
            }
        }

        #endregion

        #region Private Methods

        private Animation GetAnimation(CollisionTile tile)
        {
            switch (tile)
            {
                case CollisionTile.SPRING_PAD:
                    return Animations.Spring;
                case CollisionTile.SUPER_SPRING:
                    return Animations.SuperSpring;
                case CollisionTile.SILVER_SWITCH_LEFT:
                case CollisionTile.SILVER_SWITCH_RIGHT:
                    return Animations.SilverSwitch;
                case CollisionTile.BROWN_SWITCH_LEFT:
                case CollisionTile.BROWN_SWITCH_RIGHT:
                    return Animations.BrownSwitch;
                case CollisionTile.BLUE_SWITCH_LEFT:
                case CollisionTile.MIRROR_DOWN_RIGHT:
                    return Animations.BlueSwitch;
                case CollisionTile.GREEN_SWITCH_LEFT:
                case CollisionTile.GREEN_SWITCH_RIGHT:
                    return Animations.GreenSwitch;
                case CollisionTile.YELLOW_SWITCH_LEFT:
                case CollisionTile.YELLOW_SWITCH_RIGHT:
                    return Animations.YellowSwitch;
                case CollisionTile.WHITE_SWITCH_LEFT:
                case CollisionTile.WHITE_SWITCH_RIGHT:
                    return Animations.WhiteSwitch;
                case CollisionTile.SPIN_ARROW_SWITCH:
                    return Animations.BunnySwitch;
                case CollisionTile.MIRROR_SWITCH:
                    return Animations.MirrorSwitch;
                case CollisionTile.SHRINK_TUNNEL_SWITCH:
                    return Animations.ShrinkTunnelSwitch;
                case CollisionTile.SILVER_CYLINDER_DOWN:
                case CollisionTile.SILVER_CYLINDER_UP:
                    return Animations.SilverCylinder;
                case CollisionTile.BROWN_CYLINDER_DOWN:
                case CollisionTile.BROWN_CYLINDER_UP:
                    return Animations.BrownCylinder;
                case CollisionTile.BLUE_CYLINDER_DOWN:
                case CollisionTile.BLUE_CYLINDER_UP:
                    return Animations.BlueCylinder;
                case CollisionTile.GREEN_CYLINDER_DOWN:
                case CollisionTile.GREEN_CYLINDER_UP:
                    return Animations.GreenCylinder;
                case CollisionTile.YELLOW_CYLINDER_DOWN:
                case CollisionTile.YELLOW_CYLINDER_UP:
                    return Animations.YellowCylinder;
                case CollisionTile.WHITE_CYLINDER_DOWN:
                case CollisionTile.WHITE_CYLINDER_UP:
                    return Animations.WhiteCylinder;
                case CollisionTile.SAVE_SHRINE:
                    return Animations.SaveShrine;
                case CollisionTile.NO_WALK_LAVA:
                case CollisionTile.WALK_LAVA:
                    return Animations.Lava;
                case CollisionTile.SHALLOW_WATER:
                case CollisionTile.DEEP_WATER:
                case CollisionTile.NO_WALK_WATER:
                    return Animations.Water;
                case CollisionTile.GREEN_WATER:
                case CollisionTile.SHALLOW_GREEN_WATER:
                    return Animations.GreenWater;
                default:
                    return null;
            }
        }

        #endregion
    }
}
