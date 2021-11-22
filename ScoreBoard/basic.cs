https://pastebin.com/Wx6Y1yVs
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CubeGenerator
{
    class Cube
    {
        Vector3 position;
        Vector3 rotationSelf;
        Vector3 rotationCenter;
        Vector3 scale;

        Matrix worldMatrix;

        VertexPositionColor[] planetVertices;

        float rotSpeed;

        private Cube origin;

        public Vector3 Position { get => position; set => position = value; }
        public Vector3 RotationSelf { get => rotationSelf; set => rotationSelf = value; }
        public Vector3 RotationCenter { get => rotationCenter; set => rotationCenter = value; }
        public Vector3 Scale { get => scale; set => scale = value; }
        public Matrix WorldMatrix { get => worldMatrix; set => worldMatrix = value; }
        public VertexPositionColor[] PlanetVertices { get => planetVertices; set => planetVertices = value; }
        public float RotSpeed { get => rotSpeed; set => rotSpeed = value; }
        internal Cube Origin { get => origin; set => origin = value; }

        public Cube(Vector3 position, Vector3 scale, float rotSpeed, Color color1, Color color2)
        {
            Position = position;
            Scale = scale;
            RotSpeed = rotSpeed;
            PlanetVertices = MakeCube(color1, color2);
            Origin = null;
            RotationCenter = Vector3.Zero;
            RotationSelf = Vector3.Zero;

        }
        public Cube(Vector3 position, Vector3 scale, float rotSpeed, Color color1, Color color2, ref Cube origin)
        {
            Position = position;
            Scale = scale;
            RotSpeed = rotSpeed;
            PlanetVertices = MakeCube(color1, color2);
            Origin = origin;
            RotationCenter = Vector3.Zero;
            RotationSelf = Vector3.Zero;
        }

        private VertexPositionColor[] MakeCube(Color color1, Color color2)
        {
            VertexPositionColor[] vertices = new VertexPositionColor[36];
            // trójkąt dolny:
            vertices[0] = new VertexPositionColor(new Vector3(-1, -1, 1), color2);
            vertices[1] = new VertexPositionColor(new Vector3(-1, 1, 1), color1);
            vertices[2] = new VertexPositionColor(new Vector3(1, -1, 1), color1);
            // trójkąt górny:
            vertices[3] = new VertexPositionColor(new Vector3(-1, 1, 1), color1);
            vertices[4] = new VertexPositionColor(new Vector3(1, 1, 1), color2);
            vertices[5] = new VertexPositionColor(new Vector3(1, -1, 1), color1);

            // trójkąt dolny:
            vertices[6] = new VertexPositionColor(new Vector3(1, -1, 1), color1);
            vertices[7] = new VertexPositionColor(new Vector3(1, 1, 1), color1);
            vertices[8] = new VertexPositionColor(new Vector3(1, -1, -1), color1);
            // trójkąt górny:
            vertices[9] = new VertexPositionColor(new Vector3(1, 1, 1), color1);
            vertices[10] = new VertexPositionColor(new Vector3(1, 1, -1), color1);
            vertices[11] = new VertexPositionColor(new Vector3(1, -1, -1), color1);

            // trójkąt dolny:
            vertices[12] = new VertexPositionColor(new Vector3(1, -1, -1), color1);
            vertices[13] = new VertexPositionColor(new Vector3(1, 1, -1), color1);
            vertices[14] = new VertexPositionColor(new Vector3(-1, -1, -1), color1);
            //trójkąt górny:
            vertices[15] = new VertexPositionColor(new Vector3(1, 1, -1), color1);
            vertices[16] = new VertexPositionColor(new Vector3(-1, 1, -1), color1);
            vertices[17] = new VertexPositionColor(new Vector3(-1, -1, -1), color1);

            // trójkąt dolny:
            vertices[18] = new VertexPositionColor(new Vector3(-1, -1, -1), color1);
            vertices[19] = new VertexPositionColor(new Vector3(-1, 1, -1), color1);
            vertices[20] = new VertexPositionColor(new Vector3(-1, -1, 1), color1);
            //trójkąt górny:
            vertices[21] = new VertexPositionColor(new Vector3(-1, 1, -1), color1);
            vertices[22] = new VertexPositionColor(new Vector3(-1, 1, 1), color1);
            vertices[23] = new VertexPositionColor(new Vector3(-1, -1, 1), color1);

            //góra
            // trójkąt dolny:
            vertices[24] = new VertexPositionColor(new Vector3(-1, 1, 1), color1);
            vertices[25] = new VertexPositionColor(new Vector3(-1, 1, -1), color1);
            vertices[26] = new VertexPositionColor(new Vector3(1, 1, 1), color1);
            //trójkąt górny:
            vertices[27] = new VertexPositionColor(new Vector3(-1, 1, -1), color1);
            vertices[28] = new VertexPositionColor(new Vector3(1, 1, -1), color1);
            vertices[29] = new VertexPositionColor(new Vector3(1, 1, 1), color1);

            //dół
            // trójkąt dolny:
            vertices[30] = new VertexPositionColor(new Vector3(-1, -1, -1), color1);
            vertices[31] = new VertexPositionColor(new Vector3(-1, -1, 1), color1);
            vertices[32] = new VertexPositionColor(new Vector3(1, -1, 1), color1);
            //trójkąt górny:
            vertices[33] = new VertexPositionColor(new Vector3(-1, -1, -1), color1);
            vertices[34] = new VertexPositionColor(new Vector3(1, -1, 1), color1);
            vertices[35] = new VertexPositionColor(new Vector3(1, -1, -1), color1);

            return vertices;
        }


        public void Update()
        {
            RotationSelf += Vector3.UnitY * rotSpeed;
            if (origin != null)
                RotationCenter += Vector3.UnitY * rotSpeed;

            WorldMatrix = Matrix.Identity;
            WorldMatrix = Matrix.Multiply(WorldMatrix, Matrix.CreateScale(Scale));
            WorldMatrix = Matrix.Multiply(WorldMatrix, Matrix.CreateRotationY(MathHelper.ToRadians(RotationSelf.Y)));
            WorldMatrix = Matrix.Multiply(WorldMatrix, Matrix.CreateTranslation(Position));
            WorldMatrix = Matrix.Multiply(WorldMatrix, Matrix.CreateRotationY(MathHelper.ToRadians(RotationCenter.Y)));

            if (Origin != null)
            {
                WorldMatrix = Matrix.Multiply(WorldMatrix, Matrix.CreateTranslation(Origin.Position));
                WorldMatrix = Matrix.Multiply(WorldMatrix, Matrix.CreateRotationY(MathHelper.ToRadians(Origin.RotationCenter.Y)));
            }
        }

        public void Draw(GraphicsDevice device)
        {
            device.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleList,
                planetVertices,
                0,
                planetVertices.Length / 3);
        }

    }


public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BasicEffect basicEffect;
        Matrix worldMatrix;         // macierz świata
        Matrix viewMatrix;          // macierz widoku
        Matrix projectionMatrix;
        private List<Cube> planets;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            basicEffect = new BasicEffect(GraphicsDevice);

            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);
            viewMatrix = Matrix.CreateLookAt(Vector3.One * 10f, Vector3.Zero, Vector3.Up);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60f), GraphicsDevice.Viewport.AspectRatio, 1.0f, 1000f);


            planets = new List<Cube>();
            Cube cube1 = new Cube(Vector3.Zero, Vector3.One, 5f, Color.Yellow, Color.Red);
            planets.Add(cube1);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Cube cube in planets)
            {
                cube.Update();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;
            basicEffect.VertexColorEnabled = true;
            basicEffect.CurrentTechnique.Passes[0].Apply();

            foreach (Cube cube in planets)
            {
                basicEffect.World = cube.WorldMatrix * worldMatrix;
                foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    cube.Draw(GraphicsDevice);

                }
            }

            base.Draw(gameTime);
        }
    }
}
