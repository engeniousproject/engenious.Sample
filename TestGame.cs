using System;
using System.Drawing;
using System.Linq;
using engenious;
using engenious.Audio;
using engenious.Content.CodeGenerator;
using engenious.Graphics;
using engenious.Input;
// using engenious.Graphics.UserDefined.Materials;
using Color = engenious.Color;
using Rectangle = engenious.Rectangle;
using engenious.UserDefined;
namespace Sample
{
    public class TestGame : Game
    {
        private readonly Texture2D _texture;
        // private readonly Texture2DArray _textureArray;
        private readonly SpriteBatch _spriteBatch;
        private readonly SpriteFont _font, _fontBitmap;
        private readonly BasicEffect _basicEffect;

        private RenderTarget2D _target;

        private readonly simple _effect;
        private readonly Model _sphere;
        private SoundEffect _testSoundEffect;
        private SoundEffectInstance _testSound,_testSound2;

        private engenious.Graphics.UserDefined.Materials.Simple simpleMat;

        private float scale;

        // private Simple material;
        public TestGame()
        {
            _texture = Content.Load<Texture2D>("brick");
            _font = Content.Load<SpriteFont>("test");
            _fontBitmap = Content.Load<SpriteFont>("testBitmap");
            _effect = Content.Load<simple>("simple");
            _sphere = Content.Load<Model>("wauzi");

            BoundingBox bb = BoundingBox.CreateMerged(_sphere.Meshes.Select(x => x.BoundingBox));

            scale = 1f/(bb.Max - bb.Min).Length;

            _basicEffect = new BasicEffect(GraphicsDevice);
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // material = new Simple("SphereMat");
            // material.Albedo = Vector3.One;
            //
            // _effect!.Ambient.Pass1.Simple = material;

            simpleMat = new engenious.Graphics.UserDefined.Materials.Simple("simpleMat");
            
            simpleMat.Albedo = _texture;
            simpleMat.Metallic = 0.0f;
            simpleMat.Roughness = 0.2f;
            simpleMat.AmbientOcclusion = new Vector3(1f);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _testSoundEffect = Content.Load<SoundEffect>("blub_x");
            _testSound = _testSoundEffect.CreateInstance();
            _testSound2 = _testSoundEffect.CreateInstance();

            //_testSound2.Play();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (GraphicsDevice.Viewport.Width != 0 && GraphicsDevice.Viewport.Height != 0)
            {
                // if (_target != null && !_target.IsDisposed)
                //     _target.Dispose();
                // _target = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width,
                //     GraphicsDevice.Viewport.Height, PixelInternalFormat.Rgba8);
            }
        }

        private float rotX, rotY;
        private bool wireframe;

        private KeyboardState prevState;
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            

            
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            
            _spriteBatch.DrawString(_fontBitmap,
                $"Metallic: {simpleMat.Metallic}\nRoughness: {simpleMat.Roughness}\n", new Vector2(), Color.White);
            
            _spriteBatch.End();
            
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            
            _effect.CurrentTechnique = _effect.Ambient;

            
            _effect.Texture = _texture;
            _sphere.Transform = Matrix.CreateScaling(new Vector3(scale))*Matrix.CreateScaling(3,3,3);//Matrix.CreateRotationX((float)gameTime.TotalGameTime.TotalSeconds);

            var rotated = new Vector4(1, 0, 0, 1);
            var getState = Keyboard.GetState();
            if (getState.IsKeyDown(Keys.Left))
                rotX -= 0.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (getState.IsKeyDown(Keys.Right))
                rotX += 0.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (getState.IsKeyDown(Keys.Up))
                rotY += 0.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (getState.IsKeyDown(Keys.Down))
                rotY -= 0.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!getState.IsKeyDown(Keys.W) && prevState.IsKeyDown(Keys.W))
            {
                wireframe = !wireframe;
            }
            
            if (!getState.IsKeyDown(Keys.Y) && prevState.IsKeyDown(Keys.Y))
            {
                simpleMat.Metallic = MathF.Max(0, simpleMat.Metallic - 0.1f);
            }
            else if (!getState.IsKeyDown(Keys.X) && prevState.IsKeyDown(Keys.X))
            {
                simpleMat.Metallic = MathF.Min(1, simpleMat.Metallic + 0.1f);
            }
            
            if (!getState.IsKeyDown(Keys.A) && prevState.IsKeyDown(Keys.A))
            {
                simpleMat.Roughness = MathF.Max(0, simpleMat.Roughness - 0.1f);
            }
            else if (!getState.IsKeyDown(Keys.S) && prevState.IsKeyDown(Keys.S))
            {
                simpleMat.Roughness = MathF.Min(1, simpleMat.Roughness + 0.1f);
            }
            
            prevState = getState;

            var rotMat = Matrix.CreateRotationZ(rotX) * Matrix.CreateRotationY(rotY);
            
            rotated = Vector4.Transform(rotMat, rotated)*10;
            
            var view =  Matrix.CreateLookAt(new Vector3(rotated.X, rotated.Y, rotated.Z), Vector3.Zero, Vector3.UnitY);
            var projection = Matrix.CreatePerspectiveFieldOfView(MathF.PI / 2,
                (float)GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height, 0.1f, 100f);
            _effect.Ambient.ViewProj = projection*view;

            _effect.Ambient.Pass1.camPos = new Vector3(rotated.X, rotated.Y, rotated.Z);
            //simpleMat.Roughness = MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds) / 2 + 0.5f;
            _effect.Ambient.Pass1.Simple = simpleMat;
            _effect.Ambient.Pass1.lights[0].position = Vector3.UnitX*11;
            _effect.Ambient.Pass1.lights[0].color = new Vector3(300f,300f,300f);
            // _effect.Ambient.Pass1.lights[1].position = Vector3.UnitY*10;
            // _effect.Ambient.Pass1.lights[1].color = new Vector3(300f,300f,300f);
            // _effect.Ambient.Pass1.lights[2].position = Vector3.UnitZ*15;
            // _effect.Ambient.Pass1.lights[2].color = new Vector3(300f,300f,300f);
            // _effect.Ambient.Pass1.lights[3].position = Vector3.UnitX*15;
            // _effect.Ambient.Pass1.lights[3].color = new Vector3(300f,300f,300f);

            _sphere.CurrentAnimation = _sphere.Animations.First();
            _sphere.UpdateAnimation((float)gameTime.ElapsedGameTime.TotalSeconds);
            
            simpleMat.Update();
            
            GraphicsDevice.RasterizerState = wireframe ? new RasterizerState() { FillMode = PolygonMode.Line } : RasterizerState.CullNone;
            _sphere.Draw(_effect);
            
        }
    }
}