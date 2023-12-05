using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobbySpel {
    internal class Anchor {

        public Vector2 anchor;
        public Vector2 oldanchor;

        public Anchor(Vector2 Aposition) {
            anchor = Aposition;
        }
        public void UpdateAnchor(int Ax, int Ay) {
            anchor.X = Ax;
            anchor.Y = Ay;
        }
        public void UpdateOldAnchor(int Ax, int Ay) {
            oldanchor.X = Ax;
            oldanchor.Y = Ay;
        }
    }
}
