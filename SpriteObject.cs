using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BobbySpel {
    internal class SpriteObject {

        // för att ladda sprites
        public static GraphicsDevice graphicsDevice = Game1.gd;

        public List<Texture2D> spritelist;
        public Texture2D currentsprite;

        public Anchor oa;
        // för att offsetta sprites från ankare
        public Vector2 spriteposition;
        public List<(int, int)> offsetlist;
        public void OffsetSprite() {
            int currentindex = spritelist.IndexOf(currentsprite);
            spriteposition.X = oa.X + offsetlist[currentindex].Item1;
            spriteposition.Y = oa.Y + offsetlist[currentindex].Item2;
        }


        private float spritetimer = 0;
        private Tuple<float, int, int> previousparameters = new Tuple<float, int, int>(0f, 0, 0);


        
        public void ResetSpriteTimer() {
            spritetimer = 0;
        }
        // range av sprites loopas
        public void CycleSprites(float interval, int spriteliststart, int spriteliststop) {

            Tuple<float, int, int> currentparameters = new Tuple<float, int, int>(interval, spriteliststart, spriteliststop);

            if (currentparameters.Item1 != previousparameters.Item1 || currentparameters.Item2 != previousparameters.Item2 || currentparameters.Item3 != previousparameters.Item3) {
                spritetimer = 0;
            }

            previousparameters = currentparameters;

            float totaltime = (spriteliststop - spriteliststart + 1) * interval;

            spritetimer += Helper.time;
            if (spritetimer > totaltime) {
                spritetimer = 0;
            }
            int index = (int)Math.Floor((spritetimer / interval)) + spriteliststart;
            currentsprite = spritelist[index];
        }
        // alla sprites loopas
        public void CycleAllSprites(float interval) {
            float totaltime = spritelist.Count * interval;

            spritetimer += Helper.time;
            if (spritetimer > totaltime) {
                spritetimer = 0;
            }
            int index = (int)Math.Floor((spritetimer / interval));
            currentsprite = spritelist[index];

        }

    }
}
