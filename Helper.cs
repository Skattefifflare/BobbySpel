using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace BobbySpel {
    internal static class Helper {

        public static float time;
        public static float gravity = 5f;




        public static void CollisionCheck(DynamicCollidableObject obj1, CollidableObject obj2) {
            if (obj1.objecthitbox.hitbox.Intersects(obj2.objecthitbox.hitbox)) {
                Rectangle intersection = Rectangle.Intersect(obj1.objecthitbox.hitbox, obj2.objecthitbox.hitbox);

                float xdif = obj1.objectanchor.anchor.X - obj1.objectanchor.oldanchor.X;
                // om färdriktning är åt höger är xdif +
                float ydif = obj1.objectanchor.anchor.Y - obj1.objectanchor.oldanchor.Y;
                // om färdriktning är nedåt är ydif +
                if (ydif == 0 && xdif == 0) {
                    obj1.objectanchor.anchor.X += intersection.X;
                    obj1.objectanchor.anchor.Y += intersection.Y;
                }
                else if (xdif == 0) {
                    obj1.objectanchor.anchor.Y += intersection.Height * -Sign(ydif);
                    if (Sign(ydif) == 1) {
                        obj1.isFalling = false;
                    }
                }
                else if (ydif == 0) {
                    obj1.objectanchor.anchor.X += intersection.Width * -Sign(xdif);
                }
                else if (obj1.objectanchor.anchor.X <= obj2.objectanchor.anchor.X + obj2.objecthitbox.hitbox.Width && obj2.objectanchor.anchor.X <= obj1.objectanchor.anchor.X + obj1.objecthitbox.hitbox.Width) {
                    if (obj1.objectanchor.anchor.Y <= obj2.objectanchor.anchor.Y) {
                        obj1.objectanchor.anchor.Y;
                    }
                }
            }


            static int Sign(float f) {
                return (f > 0) ? 1 : -1;
            }
        }

    }
}
