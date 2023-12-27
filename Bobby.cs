using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;
using Microsoft.Xna.Framework.Content;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;


namespace BobbySpel {
    internal class Bobby : DynamicCollidableObject {

        public bool isJumping;
        public float jumpingspeed;
        public Bobby(Vector2 Aanchorposition) {


            oa = new Anchor(Aanchorposition);

            spritelist = new List<Texture2D> {
                Texture2D.FromFile(graphicsDevice, "BobbyIdle.png"),
                Texture2D.FromFile(graphicsDevice, "BobbyJump.png"),
                Texture2D.FromFile(graphicsDevice, "BobbyRunLeft1.png"),
                Texture2D.FromFile(graphicsDevice, "BobbyRunLeft2.png"),
                Texture2D.FromFile(graphicsDevice, "BobbyRunRight1.png"),
                Texture2D.FromFile(graphicsDevice, "BobbyRunRight2.png")
            };
            currentsprite = spritelist[0];

            ohb = new Hitbox(Aanchorposition, 0, 0);

            offsetlist = new List<(int, int)> {
                (7, 4),
                (0, 0),
                (0, 0),
                (3, 0),
                (0, 0),
                (3, 0),
            };

            mass = 70;
            speed = 220f;
            isFalling = true;
            fallingspeed = 0;
            jumpingspeed = 450;

            SyncHitbox();          
        }

        public void Check(KeyboardState Akstate) {

            UpdatePos();
            prevspriteindex = spritelist.IndexOf(currentsprite);

            if (Akstate.GetPressedKeys().Length == 0 || (Akstate.IsKeyDown(Keys.A) && Akstate.IsKeyDown(Keys.D))) {
                if (spritelist.IndexOf(currentsprite) != 0) {
                    currentsprite = spritelist[0];
                    ResetSpriteTimer();
                }
            }          
            else {
                if (Akstate.IsKeyDown(Keys.A)) {
                    CycleSprites(0.2f, 2, 3);

                    Run(-speed);
                }
                if (Akstate.IsKeyDown(Keys.D)) {
                    CycleSprites(0.2f, 4, 5);
                    Run(speed);
                }
                if (Akstate.IsKeyDown(Keys.Space)) {
                    isJumping = true;
                }
                
                if (Akstate.IsKeyDown(Keys.W)) {
                    oa.Y -= speed * Helper.time;
                }
                if (Akstate.IsKeyDown(Keys.S)) {
                    oa.Y += speed * Helper.time;
                }
                
            }

            if (isFalling) {
                // Fall();
            }
            else {
                fallingspeed = 0;
                isJumping = false;
            }

            SyncHitbox();
        }
        public void Run(float Aspeed) {
            oa.X += Aspeed * Helper.time;
        }
        public void Jump() {
            oa.Y -= jumpingspeed * Helper.time;
        }
        public void Fall() {

            fallingspeed += ((fallingspeed < 1000) ? 500 : 0 ) * Helper.time;
            oa.Y += fallingspeed * Helper.time;
        }
        

    }
}