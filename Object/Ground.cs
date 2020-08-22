using Frog.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Frog.Object
{
    public class Ground : GameObject
    {
        public Ground(Game game, Vector2 coord, Texture2D texture) : base(game)
        {
            _collider = new BoxCollider(this, coord, 1, 1);

            _sprite = new AnchoredSprite(this, _collider);
            _sprite.OnInit += (Sprite sprite) =>
            {
                sprite.Texture = texture;
            };
            _sprite.OnEachFrame += (Sprite sprite, Single elapsed) =>
            {               
            };

            _sprite.InitTexture();
        }
    }
}
