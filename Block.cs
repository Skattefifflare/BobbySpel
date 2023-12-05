using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace BobbySpel {
    internal class Block : CollidableObject {

        public Block(Vector2 Aanchorposition) {
            objectanchor = new Anchor(Aanchorposition);

            spritelist = new List<Texture2D> { Texture2D.FromFile(graphicsDevice, "Block.png") };
            currentsprite = spritelist[0];

            offsetlist = new List<(int, int)> { (0, 0) };


            objecthitbox = new Hitbox(Aanchorposition, currentsprite.Width, currentsprite.Height);
            SyncPos2();

        }
    }
}
