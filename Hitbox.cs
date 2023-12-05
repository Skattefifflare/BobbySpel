using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobbySpel {
    internal class Hitbox {
        // innehåller bara en rektangel & updateringsmetoder

        public Rectangle hitbox;

        public Hitbox(Vector2 Aposition, int Awidth, int Aheight) {

            hitbox.X = (int)Aposition.X;
            hitbox.Y = (int)Aposition.Y;

            hitbox.Width = Awidth;
            hitbox.Height = Aheight;
        }

        public void UpdateSize(int NEWwidth, int NEWheight) {
            hitbox.Width = NEWwidth;
            hitbox.Height = NEWheight;
        }

        public void UpdatePos(Vector2 NEWposition) {
            hitbox.X = (int)NEWposition.X;
            hitbox.Y = (int)NEWposition.Y;
        }



    }
}
