using Frog.Engine;
using Frog.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Frog
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameObject _frog;
        public GameObject[] Ground { get; private set; }
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _frog = new Engine.Frog(this);
            

            Ground = new Ground[_graphics.PreferredBackBufferWidth];

            Vector2 coord = new Vector2(0, 300);
            Texture2D texture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            texture.SetData(new[] { Color.Black });


            for (int i = 0; i < _graphics.PreferredBackBufferWidth; i++)
            {
                Ground[i] = new Ground(this, coord, texture);

                coord.X++;

                if (i < Ground.Length / 3 && i > 200)
                {
                    coord.Y++;
                }
                else if (i > Ground.Length - Ground.Length / 3 && i < 900)
                {
                    coord.Y--;
                }
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _frog.Update();
            
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            for (int i = 0; i < Ground.Length; i++)
            {
                _spriteBatch.Draw(Ground[i]._sprite.Texture,
                                    Ground[i]._collider.Coord,
                                    Color.Black);
            }


            Vector2 rotatePoint = new Vector2(_frog._sprite.TextureRectangle.Width / 2, _frog._sprite.TextureRectangle.Height / 2);

            _spriteBatch.Draw(_frog._sprite.Texture,
                                _frog._sprite.CenterCoord, 
                                _frog._sprite.TextureRectangle, 
                                Color.White,
                                _frog._sprite.Angle,
                                rotatePoint,
                                _frog._sprite.Scale, 
                                _frog.Direction == Direction.Back ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 
                                1);



            if (_frog.ShowBounds)
            {
                Texture2D Bound = new Texture2D(_graphics.GraphicsDevice, _frog._collider.Collider.Width, _frog._collider.Collider.Height, false, SurfaceFormat.Color);
                Color[] data = new Color[Bound.Width * Bound.Height];
                for (int i = 0; i < data.Length; ++i) 
                {
                    data[i] = Color.Black;
                    Bound.SetData(data);
                }

                _spriteBatch.Draw(Bound, _frog._collider.Coord, Color.Black);
            }
             _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
