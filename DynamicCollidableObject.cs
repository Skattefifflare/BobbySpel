using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobbySpel {
    internal class DynamicCollidableObject : CollidableObject {
        // innehåller vars för beräkning av förflyttning

        public int mass;
        public float speed;
        public bool isFalling;
        public float fallingspeed;

        // OTROLIGT DUM VARIABEL
        public int prevspriteindex;

        public void UpdatePos() {
            oa.oldX = oa.X;
            oa.oldY = oa.Y;
        }
    }
}
