using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.GameObjects.Player;
using Smiley.Lib.Framework.Drawing;

namespace Smiley.Lib.GameObjects.NPCs
{
    public class NPCManager
    {
        private List<NPC> _npcs = new List<NPC>();

        public void Draw()
        {
            _npcs.ForEach(npc => npc.Draw());
        }

        public void Update(float dt)
        {
            _npcs.ForEach(npc => npc.Update(dt));
        }

        public bool TalkToNPCs(Tongue tongue)
        {
            foreach (NPC npc in _npcs)
            {
                if (tongue.Intersects(npc.CollisionBox))
                {
                    npc.InConversation = true;
                    SMH.WindowManager.OpenDialogTextBox(npc.ID, npc.TextID);
                    return true;
                }
            }
            return false;
        }

        public void AdddNPC(int id, int textID, int x, int y)
        {
            _npcs.Add(new NPC(id, textID, x, y));
        }

        public bool TestCollision(NPC npc)
        {
            return _npcs.Exists(otherNPC => otherNPC.ID != npc.ID && otherNPC.CollisionBox.Intersects(npc.CollisionBox));
        }
    }
}
