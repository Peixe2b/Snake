using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake.Scripts
{
    class Fruit
    {
        private Texture2D img;
        public Rectangle rect;
        private global::System.Random random = new System.Random();
        private Color Color { get; set; }
        public Fruit()
        {
            rect = new Rectangle(random.Next(0, 600), random.Next(0, 600), 30, 30);
            img = new Texture2D(Game1.Game.GraphicsDevice, 30, 30);
            Color = Color.Red;
            Game1.SetColor(img, Color);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, rect, Color);
        }
    }
}
