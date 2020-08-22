using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Frog.Engine
{
    public class Sprite
    {
        private GameObject _gameObject;
        public Texture2D Texture { get; set; }
        
        private Rectangle _rectangle;
        public Rectangle Rectangle
        {
            get { return _rectangle; }
            private set { _rectangle = value; }
        }

        public Vector2 Scale { get; set; }


        public Single Angle { get; set; }

        private Single _frame;
        public Single Frame
        {
            get => _frame;
            set
            {
                _frame += 0.015f * value;

                if ((Int32) _frame >= ColumnCount * RowCount)
                {
                    _frame = 0;
                    _rectangle.X = 0;
                }
                else
                {
                    _rectangle.X = Rectangle.Width * (Int32)_frame;
                }
            }
        }
        public Int32 MaxFrames
        {
            get { return ColumnCount * RowCount; }
        }

        public Int32 RowCount { get; private set; }
        public Int32 ColumnCount { get; private set; }


        public Sprite(GameObject gameObject)
        {
            _gameObject = gameObject;

            //_frame = 1;
            Angle = 0;
            Scale = new Vector2(1, 1);
        }
        public void LoadTexture(String texturePath, Rectangle rect, Int32 animationRowCount, Int32 animationColumnCount)
        {
            RowCount = animationRowCount;
            ColumnCount = animationColumnCount;

            Rectangle = rect;

            Texture = _gameObject._game.Content.Load<Texture2D>(texturePath);

            _frame = 1;
        }

        public void LoopAllFrames(Single elapsed)
        {
            Frame += elapsed;
        }

        public void PlayAllFrames()
        {
            Frame++;
        }
    }
}