using Microsoft.Xna.Framework;
using System;

namespace Frog.Engine
{
    public class BallPhysics
    {
        private Direction _direction;
        
        private GameObject _gameObject;
        private BoxCollider _boxCollider;


        private Double _angle;
        private Int32 _gravity;

        private Vector2 Velosity;

        private Int32 _jumpingForce;

        private Int32 GroundCoord(Int32 coordX)
        {
            Game1 game = _gameObject._game as Game1;
            return (Int32)game.Ground[coordX]._collider.Coord.Y;
        }
        
        public Boolean IsResting { get; set; }
        

        public delegate void OnCollisionHandler(Vector2 normal, Single andgle);
        public event OnCollisionHandler OnCollision;

        public BallPhysics(GameObject gameObject, BoxCollider boxCollider)
        {
            _gameObject = gameObject;
            _boxCollider = boxCollider;

            Velosity = new Vector2(10, 10);
            IsResting = true;

            _gravity = -1;
        }

        public void CommitJump()
        {
            Velosity.Y = _jumpingForce;
            Velosity.Y *= -1;

            Double cosAngle = Math.Cos(_angle);
            Double sinAngle = Math.Sin(_angle);

            _boxCollider.Collider.X += (Int32)(Velosity.X * cosAngle);
            _boxCollider.Collider.Y += (Int32)(Velosity.Y * sinAngle);

            _jumpingForce += _gravity;




            Vector2 normal = new Vector2();
            Single angle = 0;
            Boolean isCollided = false;


            if (_boxCollider.Collider.Bottom >= GroundCoord((Int32)_boxCollider.Collider.Left))
            {
                _boxCollider.Collider.Y = GroundCoord((Int32)_boxCollider.Coord.X) - _boxCollider.Collider.Height;
                isCollided = true;
            }
            else if (_boxCollider.Collider.Bottom >= GroundCoord((Int32)_boxCollider.Collider.Right))
            {
                _boxCollider.Collider.Y = GroundCoord((Int32)_boxCollider.Collider.Right) - _boxCollider.Collider.Height;
                isCollided = true;
            }


            if (_boxCollider.Collider.Bottom >= GroundCoord((Int32)_boxCollider.Collider.Right) &&
                 _direction == Direction.Front)
            {
                normal = new Vector2(_boxCollider.Collider.Right, _boxCollider.Collider.Bottom);
                angle = SetAngle(normal) * -1;
            }
            else if (_boxCollider.Collider.Bottom >= GroundCoord((Int32)_boxCollider.Collider.Left) &&
                    _direction == Direction.Front)
            {
                normal = new Vector2(_boxCollider.Collider.Left, _boxCollider.Collider.Bottom);
                angle = SetAngle(normal);
            }
            else if (_boxCollider.Collider.Bottom >= GroundCoord((Int32)_boxCollider.Collider.Left) &&
                    _direction == Direction.Back)
            {
                normal = new Vector2(_boxCollider.Collider.Left, _boxCollider.Collider.Bottom);
                angle = SetAngle(normal);
            }
            else if (_boxCollider.Collider.Bottom >= GroundCoord((Int32)_boxCollider.Collider.Right) &&
                     _direction == Direction.Back)
            {
                normal = new Vector2(_boxCollider.Collider.Right, _boxCollider.Collider.Bottom);
                angle = SetAngle(normal) * -1;
            }


            if (isCollided)
            {                
                OnCollision(normal, angle);
            }
        }

        public void InitJump()
        {
            Velosity = new Vector2(10, 10);
            
            _angle = new Random().Next(35, 55) * (Math.PI / 180);

            _jumpingForce = 16;

            Int32 randomNumber = new Random().Next(0, 2);
            _direction = randomNumber == 0 ? Direction.Back : Direction.Front;
            
            _gameObject.Direction = _direction;
            if (_direction == Direction.Back)
            {
                Velosity.X *= -1;
            }
            
            
            
            if (_boxCollider.Coord.X + 400 > _gameObject.WindowSizeWidth && _direction == Direction.Front)
            {
                _direction = Direction.Back;
                _gameObject.Direction = _direction;
                Velosity.X *= -1;
            }
            else if(_boxCollider.Coord.X - 400 < 0 && _direction == Direction.Back)
            {
                _direction = Direction.Front;
                _gameObject.Direction = _direction;   
                Velosity.X *= -1;
            }
        }

        private Single SetAngle(Vector2 normal)
        {
            Single angle = 0;


            Single oppositeCoordX = normal.X == _boxCollider.Collider.Right ? _boxCollider.Collider.Left : _boxCollider.Collider.Right;
            Vector2 oppositePoint = new Vector2(oppositeCoordX, _boxCollider.Collider.Bottom);


            Int32 groundCoordY = GroundCoord((Int32)oppositePoint.X);
            Vector2 coordX2Y2 = new Vector2(oppositeCoordX, groundCoordY);


            Vector2 X0Y0 = new Vector2(coordX2Y2.X, coordX2Y2.Y - oppositePoint.Y);



            angle = (Single)(Math.PI / 2 - Math.Atan(_boxCollider.Collider.Width / (Double)X0Y0.Y));

            return angle;
        }
    }
}