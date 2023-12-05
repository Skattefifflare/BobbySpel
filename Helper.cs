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
                else {

                    Point center1 = obj1.objecthitbox.hitbox.Center;
                    Point center2 = obj2.objecthitbox.hitbox.Center;

                    float Xoffset = obj1.objectanchor.anchor.X - center1.X;
                    float Yoffset = obj1.objectanchor.anchor.Y - center1.Y;


                    float paddingX = obj1.objecthitbox.hitbox.Width / 2;
                    float paddingY = obj1.objecthitbox.hitbox.Height / 2;

                    float halfwidth = obj2.objecthitbox.hitbox.Width / 2;
                    float halfheight = obj2.objecthitbox.hitbox.Height / 2;


                    float inversX = 1 / xdif;
                    float inversY = 1 / ydif;

                    // NEARX
                    float nx_side = center2.X - (Sign(xdif) * (halfwidth + paddingX));
                    float nx_diff = nx_side - center1.X;
                    float nx_diff2 = nx_diff + xdif;
                    float nx_near = nx_diff2 * inversX * Sign(xdif);
                    float nearX = nx_near;

                    // NEARY
                    float ny_side = center2.Y - (Sign(ydif) * (halfheight + paddingY));
                    float ny_diff = ny_side - center1.Y;
                    float ny_diff2 = ny_diff + ydif;
                    float ny_near = ny_diff2 * inversY * Sign(ydif);
                    float nearY = ny_near;

                    // FARX
                    float fx_side = center2.X + (Sign(xdif) * (halfwidth + paddingX));
                    float fx_diff = fx_side - center1.X;
                    float fx_diff2 = fx_diff + xdif;
                    float fx_near = fx_diff2 * inversX * Sign(xdif);
                    float farX = fx_near;

                    // FARY 
                    float fy_side = center2.Y + (Sign(ydif) * (halfheight + paddingY));
                    float fy_diff = fy_side - center1.Y;
                    float fy_diff2 = fy_diff + ydif;
                    float fy_near = fy_diff2 * inversY * Sign(ydif);
                    float farY = fy_near;
                    // Jag har skrivit om alla near-far uträkningar för att vara lättläsliga.
                    // near-far värdena jag fått ser rimliga ut 

                    var firsttime = (new List<float> { nearX, nearY, farX, farY }.Where(v => v > 0)).Min();

                    obj1.objectanchor.anchor.X = obj1.objectanchor.oldanchor.X + (xdif * firsttime) + Xoffset;
                    obj2.objectanchor.anchor.Y = obj1.objectanchor.oldanchor.Y + (ydif * firsttime) + Yoffset;
                    obj1.SyncPos2();
                    float newx = obj1.objectanchor.anchor.X;
                    float newy = obj1.objectanchor.anchor.Y;


                }
            }


            static int Sign(float f) {
                return (f > 0) ? 1 : -1;
            }
        }

    }
}
