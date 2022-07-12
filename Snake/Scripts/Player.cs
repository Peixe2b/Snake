using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake.Scripts
{
    class Player
    {
        private Texture2D img;
        public Rectangle rect;
        public Direction direction = Direction.Right;
        private Vector2 vel;
        private readonly float speed = 7;
        public Player()
        {
            rect = new Rectangle(
                Game1.Game._graphics.PreferredBackBufferWidth / 2 - 30,
                Game1.Game._graphics.PreferredBackBufferHeight / 2 - 30, 30, 30);
            img = new Texture2D(Game1.Game.GraphicsDevice, 30, 30);
            Game1.SetColor(img, Color.Green);
        }

        public void Update(GameTime gameTime)
        {
            rect.X += (int)vel.X;
            rect.Y += (int)vel.Y;
            CrossScreen();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, rect, Color.White);
        }
        
        public void SnakeController()
        {
            switch (direction)
            {
                case Direction.Right:
                    vel = Vector2.UnitX * speed;
                    KeyPress(Direction.Up, Keys.Up);
                    KeyPress(Direction.Down, Keys.Down);
                    break;
                case Direction.Left:
                    vel = -Vector2.UnitX * speed;
                    KeyPress(Direction.Up, Keys.Up);
                    KeyPress(Direction.Down, Keys.Down);
                    break;
                case Direction.Up:
                    vel = -Vector2.UnitY * speed;
                    KeyPress(Direction.Left, Keys.Left);
                    KeyPress(Direction.Right, Keys.Right);
                    break;
                case Direction.Down:
                    vel = Vector2.UnitY * speed;
                    KeyPress(Direction.Left, Keys.Left);
                    KeyPress(Direction.Right, Keys.Right);
                    break;
            }
        }

        private void KeyPress(Direction direction, Keys keys)
        {
            var key = Keyboard.GetState();

            if (key.IsKeyDown(keys))
                this.direction = direction;

            return;
        }

        private void CrossScreen()
        {
            if (rect.X > 630) rect.X = 0;
            else if (rect.X < 0) rect.X = 630;
            else if (rect.Y > 630) rect.Y = 0;
            else if (rect.Y < 0) rect.Y = 630;
            return;
        }
    }
}
