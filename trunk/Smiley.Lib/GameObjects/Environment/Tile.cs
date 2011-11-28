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
        /// Draws the tile.
        /// </summary>
        public void Draw(float x, float y)
        {
            //Draw Terrain
            if (Collision != CollisionTile.PIT && Collision != CollisionTile.FAKE_PIT && Collision != CollisionTile.NO_WALK_PIT)
            {
                SMH.Graphics.DrawSprite(SpriteSets.MainLayer[Terrain], x, y);
            }

            //Draw Collision
            if (_animation != null)
            {
                _animation.Draw(x, y);
            }
            else if (Collision == CollisionTile.NO_WALK_LAVA || Collision == CollisionTile.WALK_LAVA)
            {
                Animations.Lava.Draw(x, y);
            }
            else if (Collision == CollisionTile.SHALLOW_WATER)
            {
                Animations.Water.Draw(x, y, 125f);
            }
            else if (Collision == CollisionTile.DEEP_WATER || Collision == CollisionTile.NO_WALK_WATER)
            {
                Animations.Water.Draw(x, y);
            }
            else if (Collision == CollisionTile.GREEN_WATER)
            {
                Animations.GreenWater.Draw(x, y);
            }
            else if (Collision == CollisionTile.SHALLOW_GREEN_WATER)
            {
                Animations.GreenWater.Draw(x, y, 125f);
            }
            else if (SmileyUtil.ShouldDrawCollision(Collision))
            {
                //Set color values
                if (Collision >= CollisionTile.UP_ARROW && Collision <= CollisionTile.LEFT_ARROW)
                {
                    if (ID == -1 || ID == 990)
                    {
                        //TODO: render normal, red arrow
                        //smh->resources->GetAnimation("walkLayer")->SetColor(ARGB(255, 255, 0, 255));
                    }
                    else
                    {
                        //TODO:it's a rotating arrow, make it green
                        //smh->resources->GetAnimation("walkLayer")->SetColor(ARGB(255, 0, 255, 255));
                    }
                }
                else
                {
                    SMH.Graphics.DrawSprite(SpriteSets.WalkLayer[(int)Collision], x, y);
                }
            }

            //Draw Tile
            //Items
            if (Item != (int)ItemTile.NONE && ID != Constants.ID_DrawAfterSmiley)
            {
                if (Item == (int)ItemTile.EnemyGroupBlockGraphic)
                {
                    //If this is an enemy block, draw it with the enemy group's block alpha
                    //TODO:
                    //itemLayer[theItem]->SetColor(ARGB(
                    //    smh->enemyGroupManager->groups[variable[i + xGridOffset][j + yGridOffset]].blockAlpha, 255, 255, 255));
                    //itemLayer[theItem]->Render(drawX, drawY);
                    //itemLayer[theItem]->SetColor(ARGB(255, 255, 255, 255));
                }
                else
                {
                    SMH.Graphics.DrawSprite(SpriteSets.ItemLayer[Item], x, y);
                }
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
                default:
                    return null;
            }
        }

        #endregion
    }


}
