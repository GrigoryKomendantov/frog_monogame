using Microsoft.Xna.Framework;
using System;

namespace Frog.Engine
{
    public class Frog : GameObject
    {
        private Boolean _isJumping;
        public bool IsJumping
        {
            get => _isJumping;
            set
            {
                _isJumping = value;

                if (_isJumping)
                {
                    _sprite.LoadTexture("Sprites/FrogJump", new Rectangle(0,0, 133,110),  1, 12);
                    _sprite.Scale = new Vector2(1.18f, 1.18f);
                }
                else
                {
                    _sprite.LoadTexture("Sprites/FrogSit", new Rectangle(0,0, 140,140), 1, 8);
                    _sprite.Scale = new Vector2(1.0f, 1.0f);
                }
            }
        }
        public Frog(Game game) : base(game)
        {
            _isJumping = false;
            
            _collider = new BoxCollider(this, new Vector2(50,280), 10, 20);

            _physics = new BallPhysics(this, _collider);
            _physics.OnCollision += (Vector2 normal, Single angle) =>
            {
                _sprite.Angle = angle;

                IsJumping = false;
                _jumpTimer.Start();
            };
            
            
            _jumpTimer = new Timer(this, 5000);
            _jumpTimer.OnTimer += (Single elapsed) =>
            {
                if (!IsJumping)
                {
                    _jumpTimer.Stop();

                    _physics.InitJump();

                    _sprite.Angle = 0;

                    IsJumping = true;
                }
            };
            _jumpTimer.Start();


            _sprite = new AnchoredSprite(this, _collider);
            _sprite.OnInit += (Sprite sprite) =>
            {
                sprite.LoadTexture("Sprites/FrogSit", new Rectangle(0, 0, 140, 140), 1, 8);
                sprite.Frame = 0;
            };
            _sprite.OnEachFrame += (Sprite sprite, Single elapsed) =>
            {
                sprite.LoopAllFrames(elapsed);
            };
            _sprite.OnLoadSprite += (Sprite sprite, String filePath, Rectangle rect, 
                                            Int32 animationRowCount, Int32 animationColumnCount) =>
            {
                sprite.LoadTexture(filePath, rect, animationRowCount, animationColumnCount);
            };
                

            _sprite.InitTexture();
        }

        public override void Update()
        {
            if (IsJumping)
            {
                _physics.CommitJump();
            }
        }
    }
}
