using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Frog.Engine
{
    public class AnchoredSprite
    {
        private Timer _updateTimer;
        
        private GameObject _gameObject;
        private BoxCollider _boxCollider;

        private Sprite _sprite;
        public Texture2D Texture
        {
            get
            {
                return _sprite.Texture;
            }
        }
        public Rectangle TextureRectangle
        {
            get
            {
                return _sprite.Rectangle;
            }
        }
        public Vector2 CenterCoord
        {
            get
            {
                Int32 offsetY = 30;

                Vector2 center = _boxCollider.Collider.Center.ToVector2();
                center.Y -= offsetY;

                return center;
            }
        }

        public Single Angle
        {
            get
            {
                return _sprite.Angle;
            }
            set
            {
                _sprite.Angle = value;
            }
        }

        public Vector2 Scale
        {
            get
            {
                return _sprite.Scale;
            }
            set
            {
                _sprite.Scale = value;
            }
        }

        public Boolean Visible { get; set; }


        public delegate void OnInitHandler(Sprite sprite);
        public event OnInitHandler OnInit;

        public delegate void OnEachFrameHandler(Sprite sprite, float elapsed);
        public event OnEachFrameHandler OnEachFrame;

        public delegate void OnLoadSpriteHandler(Sprite sprite, String filePath, Rectangle rect, Int32 animationRowCount, Int32 animationColumnCount);
        public event OnLoadSpriteHandler OnLoadSprite;

        public AnchoredSprite(GameObject gameObject, BoxCollider boxCollider)
        {
            _gameObject = gameObject;
            _boxCollider = boxCollider;

            _sprite = new Sprite(_gameObject);
            
            _updateTimer = new Timer(_gameObject, 100);
            _updateTimer.OnTimer += (Single elapsed) =>
            {
                OnEachFrame(_sprite, elapsed);
            };
            _updateTimer.Start();
        }

        public void InitTexture()
        {
            OnInit(_sprite);
        }

        public void LoadTexture(String filePath, Rectangle rect, Int32 animationRowCount, Int32 animationColumnCount)
        {
            OnLoadSprite(_sprite, filePath, rect, animationRowCount, animationColumnCount);
        }
    }
}
