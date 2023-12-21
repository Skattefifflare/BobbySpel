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

        // will be assigned during each colcheck.
        public static bool notfalling;

        public static void CollisionCheck(DynamicCollidableObject obj1, CollidableObject obj2) {
            if (obj1.ohb.hitbox.Intersects(obj2.ohb.hitbox)) {
                Rectangle intersection = Rectangle.Intersect(obj1.ohb.hitbox, obj2.ohb.hitbox);

                float xdif = obj1.oa.X - obj1.oa.oldX;
                // om färdriktning är åt höger är xdif +
                float ydif = obj1.oa.Y - obj1.oa.oldY;
                // om färdriktning är nedåt är ydif +

                // LÖS DENNA
                if (ydif == 0 && xdif == 0) {
                    ResolveY();
                }
                //Fall / Hopp
                else if (xdif == 0) {
                    ResolveY();
                    if (Sign(ydif) == 1) {
                        obj1.isFalling = false;
                    }
                }
                // Spring
                else if (ydif == 0) {
                    ResolveX();
                    obj1.isFalling = true;
                }               
                else if (obj1.oa.oldX + obj1.offsetlist[obj1.prevspriteindex].Item1 < obj2.oa.X + obj2.ohb.hitbox.Width && 
                    obj2.oa.X < obj1.oa.oldX + obj1.ohb.hitbox.Width + (obj1.offsetlist[obj1.spritelist.IndexOf(obj1.currentsprite)].Item1 - obj1.offsetlist[obj1.prevspriteindex].Item1)) {
                    float yb4 = obj1.oa.oldY;
                    ResolveY();
                    float ydif2 = yb4 - obj1.oa.Y;
                    float ratioY = ydif / xdif;
                    obj1.oa.X += ydif2 / ratioY;
                    
                    if (Sign(ydif) == 1) {
                        obj1.isFalling = false;
                    }
                }
                else if (obj1.oa.oldY + obj1.offsetlist[obj1.prevspriteindex].Item2 < obj2.oa.Y + obj2.ohb.hitbox.Height && 
                    obj2.oa.Y < obj1.oa.oldY + obj1.ohb.hitbox.Height) {
                    float xb4 = obj1.oa.oldX;
                    ResolveX();
                    float xdif2 = xb4 - obj1.oa.X;
                    float ratioX = xdif / ydif;
                    obj1.oa.Y += xdif2 / ratioX;             

                    obj1.isFalling = true;
                }
                else {
                    System.Diagnostics.Debug.WriteLine("?????????????");
                }
            }
            

            void ResolveX() {
                obj1.oa.X = obj2.oa.X + ((obj1.oa.oldX > obj2.oa.X) ? obj2.currentsprite.Width : -obj1.currentsprite.Width) - obj1.offsetlist[obj1.spritelist.IndexOf(obj1.currentsprite)].Item1;
            }
            void ResolveY() {
                obj1.oa.Y = obj2.oa.Y + ((obj1.oa.oldY > obj2.oa.Y) ? obj2.currentsprite.Height : -obj1.currentsprite.Height) - obj1.offsetlist[obj1.spritelist.IndexOf(obj1.currentsprite)].Item2;
            }    
            static int Sign(float f) {
                return (f > 0) ? 1 : -1;
            }
        }

    }
}
