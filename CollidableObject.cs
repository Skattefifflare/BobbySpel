using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BobbySpel {
    internal class CollidableObject : SpriteObject {
        // innehåller hitbox och synkar hitbox med spriten

        public Hitbox ohb;

        public void SyncHitbox() {
            OffsetSprite();
            ohb.UpdateSize(currentsprite.Width, currentsprite.Height);
            ohb.UpdatePos(spriteposition);
        }
    }
}
