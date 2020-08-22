using Microsoft.Xna.Framework;
using System;

namespace Frog.Engine
{
    public enum Direction
    {
        Front = 0,
        Back = 1
    }
    public class GameObject
    {
        public Direction Direction { get; set; }
        public Game _game { get; private set; }

        public BoxCollider _collider { get; set; }
        protected BallPhysics _physics;
        public Timer _jumpTimer { get; set; }

        public AnchoredSprite _sprite { get; set; }

        public Boolean ShowBounds { get; set; }

        public Int32 WindowSizeWidth
        {
            get { return _game.Window.ClientBounds.Width; }
        }

        public Int32 WindowSizeHeight
        {
            get { return _game.Window.ClientBounds.Height; }
        }
        
        public GameObject(Game game)
        {
            _game = game;
            ShowBounds = false;

            Direction = Direction.Front;
        }

        public virtual void Load() { }
        public virtual void Update() { }
    }
}
