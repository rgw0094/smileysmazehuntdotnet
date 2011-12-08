using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using Smiley.Lib.Framework.Drawing;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.GameObjects.NPCs
{
    public enum NPCStage
    {
        Walk,
        Rest
    }

    public class NPC : GameObject
    {
        #region Private Variables

        private Direction _facing;
        private NPCStage _stage;
        private Direction _walkDirection;
        private float _timeEnteredStage;
        private float _stageLength;
        private Rect _futureCollisionBox;
        private Rect _futureCollisionBox2;
        private Dictionary<Direction, Sprite> _sprites = new Dictionary<Direction, Sprite>();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new NPC.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="textID"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public NPC(int id, int textID, int x, int y)
        {
            ID = id;
            TextID = id;
            X = x * 64 + 32;
            Y = y * 64 + 32;
            _facing = _walkDirection = Direction.Down;
            _stage = NPCStage.Rest;
            _timeEnteredStage = SMH.GameTime;
            Speed = 70f;
            _stageLength = (float)SMH.Random.NextDouble() + 1f;
            CollisionBox = new Rect(X - 32f, Y - 32f, 64, 64);

            _sprites[Direction.Down] = new Sprite(SmileyTexture.NPCs, new Rectangle(0, id * 64, 64, 64), new Vector2(32, 32));
            _sprites[Direction.Left] = new Sprite(SmileyTexture.NPCs, new Rectangle(64, id * 64, 64, 64), new Vector2(32, 32));
            _sprites[Direction.Right] = new Sprite(SmileyTexture.NPCs, new Rectangle(128, id * 64, 64, 64), new Vector2(32, 32));
            _sprites[Direction.Up] = new Sprite(SmileyTexture.NPCs, new Rectangle(192, id * 64, 64, 64), new Vector2(32, 32));
        }

        #endregion

        #region Properties

        public Rect CollisionBox
        {
            get;
            private set;
        }

        public int ID
        {
            get;
            private set;
        }

        public int TextID
        {
            get;
            private set;
        }

        public bool InConversation
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draws the NPC.
        /// </summary>
        public override void Draw()
        {
            SMH.Graphics.DrawSprite(_sprites[_facing], ScreenX, ScreenY);

            //Debug mode stuff
            if (SMH.Console.IsActive)
            {
                SMH.Graphics.DrawRect(CollisionBox, Color.Red, false);
            }
        }

        /// <summary>
        /// Updates the NPC.
        /// </summary>
        /// <param name="dt"></param>
        public override void Update(float dt)
        {
            //Update collision box
            CollisionBox = new Rect(X - 32f, Y - 32f, 64, 64);

            //Exit conversation
            if (!SMH.WindowManager.IsTextBoxOpen) InConversation = false;

            //If in conversation, stand still and face the player
            if (InConversation)
            {
                if (SMH.Player.X > X + 32) _facing = Direction.Right;
                else if (SMH.Player.X < X - 32) _facing = Direction.Left;
                else if (SMH.Player.Y > Y + 32) _facing = Direction.Down;
                else if (SMH.Player.Y < Y - 32) _facing = Direction.Up;
                return;
            }
            else
            {
                _facing = _walkDirection;
            }

            if (_stage == NPCStage.Rest)
            {
                DX = DY = 0;
            }
            else if (_stage == NPCStage.Walk)
            {
                if (_walkDirection == Direction.Left) { DX = -Speed; DY = 0; }
                else if (_walkDirection == Direction.Right) { DX = Speed; DY = 0; }
                else if (_walkDirection == Direction.Up) { DX = 0; DY = -Speed; }
                else if (_walkDirection == Direction.Down) { DX = 0; DY = Speed; }
            }

            //Switch stage when the current one is finished
            if (SMH.GameTimePassed(_timeEnteredStage, _stageLength))
            {
                ChangeStage();
            }

            //Move
            _futureCollisionBox = new Rect(X + DX * dt * 3f - 32f, Y + DY * dt * 3f - 32f, 64, 64);
            _futureCollisionBox2 = new Rect(X + DX * dt * 3f - 32f, Y + DY * dt * 3f - 32f, 64, 64);
            if (SMH.Player.CollisionCircle.Intersects(_futureCollisionBox2))
            {
                //If colliding with the player, enter rest mode
                _stage = NPCStage.Rest;
            }
            else if (!SMH.Environment.TestCollision(_futureCollisionBox, CanPass) &&
                  !SMH.NPCManager.TestCollision(this))
            {
                X += DX * dt;
                Y += DY * dt;
            }
            else
            {
                ChangeDirection();
            }
        }

        /// <summary>
        /// Returns whether or not the NPC can pass a collision tile.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public bool CanPass(CollisionTile tile)
        {
            return tile == CollisionTile.WALKABLE ||
                   tile == CollisionTile.SLIME ||
                   tile == CollisionTile.PLAYER_START ||
                   tile == CollisionTile.PLAYER_END;
        }

        #endregion

        #region Private Methods

        private void ChangeStage()
        {
            if (_stage == NPCStage.Rest)
            {
                _stage = NPCStage.Walk;
                ChangeDirection();
            }
            else
            {
                _stage = NPCStage.Rest;
            }
            _timeEnteredStage = SMH.GameTime;
            _stageLength = (float)SMH.Random.NextDouble() * 2f + 1f;
        }

        private void ChangeDirection()
        {
            _walkDirection = _facing = GetRandomDirection();
        }

        private Direction GetRandomDirection()
        {
            switch (SMH.Random.Next(1, 4))
            {
                case 1:
                    return Direction.Down;
                case 2:
                    return Direction.Left;
                case 3:
                    return Direction.Right;
                default:
                    return Direction.Up;
            }
        }

        #endregion
    }
}
