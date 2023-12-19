using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BobbySpel {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private float scale;

        Texture2D pixel;

        Bobby bobby;
        Block block1;
        Block block2;
        Block longblock;

        bool drawboxes;

        public static GraphicsDevice gd;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {

            gd = GraphicsDevice;
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            int referenceWidth = 192 * 3;
            int referenceHeight = 108 * 3;
            _graphics.PreferredBackBufferWidth = 192 * 6;
            _graphics.PreferredBackBufferHeight = 108 * 6;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            UpdateScale(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, referenceWidth, referenceHeight);


            pixel = new Texture2D(GraphicsDevice, 1, 1);
            bobby = new Bobby(new Vector2(350, 200));
            block1 = new Block(new Vector2(200, 60), "Block.png");
            block2 = new Block(new Vector2(150, referenceHeight-100), "Block.png");
            longblock = new Block(new Vector2(-450, referenceHeight - 20), "LongBlock.png");
            
        }

        private void UpdateScale(int currentWidth, int currentHeight, int referenceWidth, int referenceHeight) {
            float scaleX = (float)currentWidth / referenceWidth;
            float scaleY = (float)currentHeight / referenceHeight;

            // Use the minimum scale to maintain aspect ratio
            scale = Math.Min(scaleX, scaleY);
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Helper.time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.B)) {
                drawboxes = true;
            }
            if (kstate.IsKeyDown(Keys.N)) {
                drawboxes = false;
            }
            if (kstate.IsKeyDown(Keys.Z)) {
                bobby.objectanchor.anchor = new Vector2(350, 200);
            }
            if (kstate.IsKeyDown(Keys.T)) {
                System.Diagnostics.Debug.WriteLine(bobby.objectanchor.anchor);
            }
            if (kstate.IsKeyDown (Keys.F)) {
                bobby.isFalling = true;
            }
            bobby.Check(kstate);

            
            
            Helper.CollisionCheck(bobby, block1);
            Helper.CollisionCheck(bobby, block2);
            Helper.CollisionCheck(bobby, longblock);           
            bobby.SyncPos2();


            base.Update(gameTime);
        }

        private void DrawRectangle(Rectangle Arect, Color Acolor) {

            pixel.SetData(new Color[] { Acolor });
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(scale));

            _spriteBatch.Draw(pixel, new Vector2(Arect.X, Arect.Y), null, Color.White, 0f, Vector2.Zero, new Vector2(Arect.Width, 1f), SpriteEffects.None, 0f);
            _spriteBatch.Draw(pixel, new Vector2(Arect.X, Arect.Y + Arect.Height), null, Color.White, 0f, Vector2.Zero, new Vector2(Arect.Width + 1, 1f), SpriteEffects.None, 0f);
            _spriteBatch.Draw(pixel, new Vector2(Arect.X, Arect.Y), null, Color.White, 0f, Vector2.Zero, new Vector2(1f, Arect.Height), SpriteEffects.None, 0f);
            _spriteBatch.Draw(pixel, new Vector2(Arect.X + Arect.Width, Arect.Y), null, Color.White, 0f, Vector2.Zero, new Vector2(1f, Arect.Height), SpriteEffects.None, 0f);

            _spriteBatch.End();
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(new Color(41, 44, 51));
            /*
            spriteBatch.Begin(
                SpriteSortMode.Deferred,  // SpriteSortMode sortMode
                null,                     // BlendState blendState
                null,                     // SamplerState samplerState
                null,                     // DepthStencilState depthStencilState
                null,                     // RasterizerState rasterizerState
                null,                     // Effect effect
                Matrix.CreateScale(scale)  // Matrix transformMatrix
            );
            
            spriteBatch.Draw(
                yourSprite,          // Texture2D texture
                spritePosition,      // Vector2 position
                null,                // Rectangle? sourceRectangle
                Color.White,         // Color color
                0f,                  // float rotation
                Vector2.Zero,        // Vector2 origin
                1f,                  // float scale
                SpriteEffects.None,  // SpriteEffects effects
                0f                   // float layerDepth
            );
            */


            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(scale));

            _spriteBatch.Draw(bobby.currentsprite, bobby.spriteposition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(block2.currentsprite, block2.spriteposition, Color.White);            
            _spriteBatch.Draw(block1.currentsprite, block1.spriteposition, Color.White);
            _spriteBatch.Draw(longblock.currentsprite, longblock.spriteposition, Color.White);

            _spriteBatch.DrawString(Content.Load<SpriteFont>("MYFONT"), $"{bobby.objectanchor.anchor.X-bobby.objectanchor.oldanchor.X}\n{bobby.objectanchor.anchor.Y - bobby.objectanchor.oldanchor.Y}", new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("MYFONT"), $"{bobby.isFalling} {bobby.isJumping}", new Vector2(0, 30), Color.White);
            _spriteBatch.End();

            if (drawboxes) {
                DrawRectangle(bobby.objecthitbox.hitbox, Color.White);
            }


            base.Draw(gameTime);
        }


    }
}
