using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BobbySpel {
    internal class Anchor {
               
        public float X;
        public float Y;

        public float oldX;
        public float oldY;

        public Anchor(Vector2 Aposition) {
            X = Aposition.X;
            Y = Aposition.Y;
        }
        public void UpdateAnchor(Vector2 Aposition) {
            X = Aposition.X;
            Y = Aposition.Y;
        }
        public void UpdateOldAnchor(Vector2 Aposition) {
            oldX = Aposition.X;
            oldY = Aposition.Y;
        }
    }
}
