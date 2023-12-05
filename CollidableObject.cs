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
        // innehåller hitbox och synkar hitbox med resten

        public Hitbox objecthitbox;

        public void SyncPos2() {
            SyncPos1();
            objecthitbox.UpdateSize(currentsprite.Width, currentsprite.Height);
            objecthitbox.UpdatePos(spriteposition);
        }

    }
}
