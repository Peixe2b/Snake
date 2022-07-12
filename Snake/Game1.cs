using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Snake.Scripts;

namespace Snake
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Game1 game;
        public static Game1 Game { get { return game; } }
        private GameState GameState { get; set; }
        private SpriteFont font;
        private List<Player> snakeParts;
        private Fruit fruit;
        private global::System.Random random = new System.Random();
        private int score;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Window
            _graphics.PreferredBackBufferWidth = 630;
            _graphics.PreferredBackBufferHeight = 630;
            _graphics.ApplyChanges();

            // Initialize
            game = this;
            GameState = GameState.Play;
            score = 0;
            fruit = new Fruit();
            snakeParts = new List<Player>();
            snakeParts.Add(new Player());
            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Font");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GameState == GameState.Play)
            {
                snakeParts[0].Update(gameTime);

                // Collision Fruit
                if (snakeParts[0].rect.Intersects(fruit.rect))
                {
                    // Add Snake Part
                    var tail = new Player();
                    snakeParts.Add(tail);

                    // Fruit
                    fruit.rect.X = random.Next(0, 630);
                    fruit.rect.Y = random.Next(0, 630);
                    score++;
                }

                // Move Snake
                if (snakeParts.Count > 1)
                {
                    for (int i = snakeParts.Count - 1; i > 0; i--)
                    {
                        if (snakeParts[0].rect == snakeParts[i].rect)
                        {
                            ResetGame();
                            GameState = GameState.GameOver;
                            return;
                        }
                        snakeParts[i].rect.X = snakeParts[i - 1].rect.X;
                        snakeParts[i].rect.Y = snakeParts[i - 1].rect.Y;
                    }
                }
                snakeParts[0].SnakeController();
            }
            else if (GameState == GameState.GameOver)
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    ResetGame();
                    GameState = GameState.Play;
                }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
            switch (GameState)
            {
                case GameState.Play:
                    fruit.Draw(_spriteBatch);
                    foreach (var sPart in snakeParts)
                        sPart.Draw(_spriteBatch);
                    _spriteBatch.DrawString(font, $"Score = {score}", new Vector2(12, 12), Color.Green);
                    break;
                case GameState.GameOver:
                    _spriteBatch.DrawString(font, "Press Space", new Vector2(
                        _graphics.PreferredBackBufferWidth / 2 - 80,
                        _graphics.PreferredBackBufferHeight / 2 - 20), Color.Green);
                    break;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public static Texture2D SetColor(Texture2D img, Color colorTexture)
        {
            var color = new Color[img.Width * img.Height];
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = colorTexture;
            }
            img.SetData<Color>(color);
            return img;
        }

        private void ResetGame()
        {
            // Restart Snake
            for (int i = snakeParts.Count - 1; i > 0; i--)
                snakeParts.RemoveAt(i);
            snakeParts[0].rect.X = _graphics.PreferredBackBufferWidth / 2 - 30;
            snakeParts[0].rect.Y = _graphics.PreferredBackBufferHeight / 2 - 30;
            snakeParts[0].direction = Direction.Right;
            score = 0;
            return;
        }
    }
}
