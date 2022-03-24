using System.ComponentModel;
using engenious;
using engenious.Graphics;
using engenious.UserDefined;

namespace engenious.UserDefined
{
    public partial class simple : IModelEffect
    {
        private Texture _texture;
        private Matrix _view;
        private Matrix _world;
        private Matrix _projection;

        public Matrix Projection
        {
            get { return _projection; }
            set
            {
                _projection = value; 
                UpdateMatrix();
            }
        }

        public Matrix View
        {
            get { return _view; }
            set
            {
                _view = value;
                UpdateMatrix();
            }
        }

        public Matrix World
        {
            get { return _world; }
            set
            {
                _world = value;
                Ambient.World = value;
            }
        }

        public Texture Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
            }
        }

        private void UpdateMatrix()
        {
            Ambient.Pass1.World = World;
            Ambient.ViewProj = Projection * View;
        }
    }
}