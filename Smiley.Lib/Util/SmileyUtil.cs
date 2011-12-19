using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework.Storage;

namespace Smiley.Lib.Util
{
    public static class SmileyUtil
    {
        /// <summary>
        /// Returns the angle between 2 points.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static float GetAngleBetween(float x1, float y1, float x2, float y2)
        {
            float angle;

            if (x1 == x2)
            {
                if (y1 > y2)
                {
                    angle = 3f * (float)Math.PI / 2f;
                }
                else
                {
                    angle = (float)Math.PI / 2f;
                }
            }
            else
            {
                angle = (float)Math.Atan((y2 - y1) / (x2 - x1));
                if (x1 - x2 > 0) angle += (float)Math.PI;
            }

            return angle;
        }

        public static StorageContainer GetStorageContainer()
        {
            IAsyncResult result = StorageDevice.BeginShowSelector(null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageDevice device = StorageDevice.EndShowSelector(result);
            result.AsyncWaitHandle.Close();

            result = device.BeginOpenContainer("Smiley", null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(result);
            result.AsyncWaitHandle.Close();

            return container;
        }

        public static float GetFlashingAlpha(float n)
        {
            float x = SMH.Now;
            while (x > n) x -= n;
            if (x < n / 2.0)
            {
                return 255f * (x / (n / 2f));
            }
            else
            {
                return 255f - 255f * ((x - n / 2f) / (n / 2f));
            }
        }

        /// <summary>
        /// Returns the distance between 2 points
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            if (x1 == x2) return Math.Abs(y1 - y2);
            if (y1 == y2) return Math.Abs(x1 - x2);

            return (int)Math.Sqrt(((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)));
        }

        public static Gem GetGem(ItemTile item)
        {
            switch (item)
            {
                case ItemTile.SMALL_GEM:
                    return Gem.Small;
                case ItemTile.MEDIUM_GEM:
                    return Gem.Medium;
                case ItemTile.LARGE_GEM:
                    return Gem.Large;
                default:
                    throw new Exception("ItemTile is not a gem!");
            }
        }

        public static bool IsCylinderSwitch(CollisionTile collision)
        {
            return IsCylinderSwitchLeft(collision) || IsCylinderSwitchRight(collision);
        }

        public static bool IsCylinderSwitchLeft(CollisionTile collision)
        {
            return collision == CollisionTile.WHITE_SWITCH_LEFT ||
                   collision == CollisionTile.YELLOW_SWITCH_LEFT ||
                   collision == CollisionTile.GREEN_SWITCH_LEFT ||
                   collision == CollisionTile.BLUE_SWITCH_LEFT ||
                   collision == CollisionTile.BROWN_SWITCH_LEFT ||
                   collision == CollisionTile.SILVER_SWITCH_LEFT;
        }

        public static bool IsCylinderSwitchRight(CollisionTile collision)
        {
            return collision == CollisionTile.WHITE_SWITCH_RIGHT ||
                   collision == CollisionTile.YELLOW_SWITCH_RIGHT ||
                   collision == CollisionTile.GREEN_SWITCH_RIGHT ||
                   collision == CollisionTile.BLUE_SWITCH_RIGHT ||
                   collision == CollisionTile.BROWN_SWITCH_RIGHT ||
                   collision == CollisionTile.SILVER_SWITCH_RIGHT;
        }

        public static bool IsCylinderDown(CollisionTile collision)
        {
            return collision == CollisionTile.WHITE_CYLINDER_DOWN ||
                   collision == CollisionTile.YELLOW_CYLINDER_DOWN ||
                   collision == CollisionTile.GREEN_CYLINDER_DOWN ||
                   collision == CollisionTile.BLUE_CYLINDER_DOWN ||
                   collision == CollisionTile.BROWN_CYLINDER_DOWN ||
                   collision == CollisionTile.SILVER_CYLINDER_DOWN;
        }

        public static bool IsCylinderUp(CollisionTile collision)
        {
            return collision == CollisionTile.WHITE_CYLINDER_UP ||
                   collision == CollisionTile.YELLOW_CYLINDER_UP ||
                   collision == CollisionTile.GREEN_CYLINDER_UP ||
                   collision == CollisionTile.BLUE_CYLINDER_UP ||
                   collision == CollisionTile.BROWN_CYLINDER_UP ||
                   collision == CollisionTile.SILVER_CYLINDER_UP;
        }

        public static bool IsArrowPad(CollisionTile collision)
        {
            return collision == CollisionTile.UP_ARROW ||
                   collision == CollisionTile.RIGHT_ARROW ||
                   collision == CollisionTile.DOWN_ARROW ||
                   collision == CollisionTile.LEFT_ARROW;
        }

        public static bool IsTileForGayFix(CollisionTile collision)
        {
            return IsArrowPad(collision) ||
                   collision == CollisionTile.SPRING_PAD ||
                   collision == CollisionTile.ICE ||
                   collision == CollisionTile.SUPER_SPRING ||
                   collision == CollisionTile.PIT;
        }

        /// <summary>
        /// Returns the parent area of the given area. Only the 5 parent areas have keys, so this method
        /// is used to determine which of these 5 areas to save the key to!! The number returned is the
        /// [area] index of SaveManager.numKeys[area][key color] for the parent area.
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static int GetKeyIndex(Level level)
        {
            switch (level)
            {
                case Level.OLDE_TOWNE:
                    return 0;
                case Level.FOREST_OF_FUNGORIA:
                    return 1;
                case Level.SESSARIA_SNOWPLAINS:
                    return 2;
                case Level.WORLD_OF_DESPAIR:
                    return 3;
                case Level.CASTLE_OF_EVIL:
                    return 4;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Returns the grid x coordinate that x appears in
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int GetGridX(float x)
        {
            return Convert.ToInt32((x - x % 64f) / 64f);
        }

        /// <summary>
        /// Returns the grid y coordinate that y appears in
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int GetGridY(float y)
        {
            return Convert.ToInt32((y - y % 64f) / 64f);
        }

        public static bool IsWarp(CollisionTile collision)
        {
            return collision == CollisionTile.RED_WARP ||
                   collision == CollisionTile.BLUE_WARP ||
                   collision == CollisionTile.GREEN_WARP ||
                   collision == CollisionTile.YELLOW_WARP;
        }

        /// <summary>
        /// Returns whether or not a collision tile should be visible to the player.
        /// </summary>
        /// <param name="collision"></param>
        /// <returns></returns>
        public static bool ShouldDrawCollision(CollisionTile collision)
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
                   collision != CollisionTile.EVIL_WALL_POSITION &&
                   collision != CollisionTile.EVIL_WALL_RESTART &&
                   !SmileyUtil.IsWarp(collision);
        }
    }
}
