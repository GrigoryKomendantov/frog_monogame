using Microsoft.Xna.Framework;
using System;

namespace Frog.Engine
{
    public class BoxCollider
    {
        private GameObject _gameObject;

        public Rectangle Collider;

        public Vector2 Coord
        {
            get { return new Vector2(Collider.X, Collider.Y); }
        }

        public BoxCollider(GameObject gameObject, Vector2 coord, Int32 width, Int32 height)
        {
            _gameObject = gameObject;
            Collider = new Rectangle((int)coord.X, (int)coord.Y, width, height);
        }

        public void SetData(GameObject gameObject, Vector2 coord, Int32 width, Int32 height)
        {
            _gameObject = gameObject;
            Collider = new Rectangle((int)coord.X, (int)coord.Y, width, height);
        }
    }
}
