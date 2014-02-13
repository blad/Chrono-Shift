using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS113_Game
{
    public class Camera2D
    {
        protected float zoom; //camera zoom
        public Matrix transform; //matrix transformation
        public Vector2 position; //camera position
        protected float rotation; //camera rotation

        public Camera2D()
        {
            zoom = 1.0f;
            rotation = 0.0f;
            position = Vector2.Zero;
        }

        //sets and gets zoom
        public float Zoom
        {
            get { return zoom; }

            set 
            { 
                zoom = value;
                if (zoom < 0.1f)
                    zoom = 0.1f;
            }
        }

        public float Rotation
        {
            get { return rotation; }

            set 
            {
                rotation = value;
            }
        }

        //Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            position += amount;
        }

        //get and set the position of the camera
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        //function that calculates all the transformations
        public Matrix getTransformation(GraphicsDevice graphicsDevice)
        {
            Viewport viewPort = graphicsDevice.Viewport;

            transform = Matrix.CreateTranslation(new Vector3(position.X, position.Y, 0)) *
                                                    Matrix.CreateRotationZ(Rotation) *
                                                    Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                                    Matrix.CreateTranslation(new Vector3(viewPort.Width * 0.5f, viewPort.Height * 0.5f, 0));

            return transform;
        }
    }
}
