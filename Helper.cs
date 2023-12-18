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

                // LÖS DENNA
                if (ydif == 0 && xdif == 0) {
                    
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

                
                else if (obj1.objectanchor.oldanchor.X + obj1.offsetlist[obj1.prevspriteindex].Item1 < obj2.objectanchor.anchor.X + obj2.objecthitbox.hitbox.Width && 
                    obj2.objectanchor.anchor.X < obj1.objectanchor.oldanchor.X + obj1.objecthitbox.hitbox.Width + (obj1.offsetlist[obj1.spritelist.IndexOf(obj1.currentsprite)].Item1 - obj1.offsetlist[obj1.prevspriteindex].Item1)) {
                    float yb4 = obj1.objectanchor.oldanchor.Y;
                    ResolveY();
                    float ydif2 = yb4 - obj1.objectanchor.anchor.Y;
                    float ratioY = ydif / xdif;
                    obj1.objectanchor.anchor.X += ydif2 / ratioY;
                    
                    if (Sign(ydif) == 1) {
                        obj1.isFalling = false;
                    }
                }

                else if (obj1.objectanchor.oldanchor.Y + obj1.offsetlist[obj1.prevspriteindex].Item2 < obj2.objectanchor.anchor.Y + obj2.objecthitbox.hitbox.Height && 
                    obj2.objectanchor.anchor.Y < obj1.objectanchor.oldanchor.Y + obj1.objecthitbox.hitbox.Height) {
                    float xb4 = obj1.objectanchor.oldanchor.X;
                    ResolveX();
                    float xdif2 = xb4 - obj1.objectanchor.anchor.X;
                    float ratioX = xdif / ydif;
                    obj1.objectanchor.anchor.Y += xdif2 / ratioX;             

                    obj1.isFalling = true;
                }
                else {
                    System.Diagnostics.Debug.WriteLine("?????????????");
                }              
            }
            

            void ResolveX() {
                obj1.objectanchor.anchor.X = obj2.objectanchor.anchor.X + ((obj1.objectanchor.oldanchor.X > obj2.objectanchor.anchor.X) ? obj2.currentsprite.Width : -obj1.currentsprite.Width) - obj1.offsetlist[obj1.spritelist.IndexOf(obj1.currentsprite)].Item1;
            }
            void ResolveY() {
                obj1.objectanchor.anchor.Y = obj2.objectanchor.anchor.Y + ((obj1.objectanchor.oldanchor.Y > obj2.objectanchor.anchor.Y) ? obj2.currentsprite.Height : -obj1.currentsprite.Height) - obj1.offsetlist[obj1.spritelist.IndexOf(obj1.currentsprite)].Item2;
            }
     
            static int Sign(float f) {
                return (f > 0) ? 1 : -1;
            }
        }

    }
}
