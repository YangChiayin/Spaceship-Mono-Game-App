using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ChiayinYang_Assignment3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Texture and font resources
        Texture2D shipSprite, asteroidSprite, spaceSprite;
        SpriteFont gameFont, timerFont;

        // Game entities
        Ship player = new Ship();
        List<Asteroid> asteroids = new List<Asteroid>();
        Random random = new Random();

        // Sound effects
        SoundEffect collisionSound, scoreSound, bgm, successSound, gameoverSound;
        SoundEffectInstance bgmInstance;

        // Game variables
        private int score = 0;                    // Keeps track of the score
        private int asteroidsPassed = 0;          // Counts asteroids that successfully passed the spaceship
        private TimeSpan elapsedTime;             // Tracks the elapsed time for the timer
        private int secondsElapsed;               // Stores elapsed seconds for display
        private double spawnInterval = 1.5;       // Interval in seconds between asteroid spawns
        private double spawnTimer = 0;            // Timer for spawning asteroids
        private bool inGame = true;               // Flag to check if the game is ongoing
        private bool gameWon = false;             // Flag to check if the game was won

        // Game controller for managing interactions
        Controller controller = new Controller();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Set game window size
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            // Initialize elapsed time and seconds
            elapsedTime = TimeSpan.Zero;
            secondsElapsed = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load the sprite batch for drawing
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load textures
            shipSprite = Content.Load<Texture2D>("ship");
            asteroidSprite = Content.Load<Texture2D>("asteroid");
            spaceSprite = Content.Load<Texture2D>("space");

            // Load fonts
            gameFont = Content.Load<SpriteFont>("spaceFont");
            timerFont = Content.Load<SpriteFont>("timerFont");

            // Load sound effects and music
            collisionSound = Content.Load<SoundEffect>("collision");
            scoreSound = Content.Load<SoundEffect>("score");
            successSound = Content.Load<SoundEffect>("success");
            gameoverSound = Content.Load<SoundEffect>("gameover");


            // Load and start background music in a loop
            bgm = Content.Load<SoundEffect>("bgm");
            bgmInstance = bgm.CreateInstance();
            bgmInstance.IsLooped = true;
            bgmInstance.Play();
        }

        protected override void Update(GameTime gameTime)
        {
            // End the game if the Back button (controller) or Escape key (keyboard) is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (inGame) // Update logic only if the game is ongoing
            {
                // Update the timer and check for game win condition
                secondsElapsed = controller.updateTime(gameTime);

                // Check if the game win condition (timer reaches 15 seconds) is met
                if (secondsElapsed >= 15)
                {
                    inGame = false;
                    gameWon = true;
                    controller.gameEndScript = "Game Won! You surpassed " + asteroidsPassed + " asteroids";

                    // Stop background music and play success sound
                    bgmInstance.Stop();
                    successSound.Play();
                }

                // Spawn asteroids at defined intervals
                spawnTimer += (double)gameTime.ElapsedGameTime.TotalSeconds; //ensures that the value of TotalSeconds is (double)
                if (spawnTimer >= spawnInterval)
                {
                    int speed = random.Next(2, 6); // Random speed for the asteroid
                    int yPosition = random.Next(100, _graphics.PreferredBackBufferHeight - 100); // Random position on the Y-axis
                    asteroids.Add(new Asteroid(speed, yPosition));
                    spawnTimer = 0;
                }

                // Update player and asteroids
                player.setRadius(shipSprite.Width); // Set the collision radius for the player
                player.updateShip(); // Update player position based on input

                // Iterate through asteroids in reverse order to safely remove items from the list
                for (int i = asteroids.Count - 1; i >= 0; i--)
                {
                    asteroids[i].updateAsteroid(); // Move the asteroid

                    // Check if asteroid has passed the spaceship
                    if (asteroids[i].position.X < player.position.X && !asteroids[i].HasScored)
                    {
                        score++;
                        asteroidsPassed++; // Increment passed asteroid count
                        scoreSound.Play();
                        asteroids[i].HasScored = true; // Mark asteroid as "scored" to avoid double counting
                    }

                    // Check for collision between player and asteroid
                    if (controller.didCollisionHappen(player, asteroids[i]))
                    {
                        score -= 3;
                        collisionSound.Play();
                        asteroids.RemoveAt(i); // Remove collided asteroid

                        // Check if the score drops below zero, which ends the game
                        if (score < 0)
                        {
                            inGame = false;
                            controller.gameEndScript = "Game Lost! Spaceship hit by Asteroid";
                            bgmInstance.Stop(); // Stop background music
                            gameoverSound.Play(); // Play gameover sound
                            break;
                        }
                    }
                    else if (asteroids[i].position.X < -Asteroid.radius)
                    {
                        // Remove asteroids that have moved completely off the screen to the left
                        asteroids.RemoveAt(i);
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Clear the screen with a background color
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Draw the background, spaceship, and asteroids
            _spriteBatch.Draw(spaceSprite, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(shipSprite, new Vector2(player.position.X - shipSprite.Width / 2, player.position.Y - shipSprite.Height / 2), Color.White);

            foreach (var asteroid in asteroids)
            {
                _spriteBatch.Draw(asteroidSprite, new Vector2(asteroid.position.X - Asteroid.radius, asteroid.position.Y - Asteroid.radius), Color.White);
            }

            // Display timer and score on the screen
            _spriteBatch.DrawString(timerFont, "Time: " + secondsElapsed, new Vector2(_graphics.PreferredBackBufferWidth / 2, 30), Color.White);
            _spriteBatch.DrawString(gameFont, "Score: " + score, new Vector2(10, 10), Color.White);

            if (!inGame)
            {
                // Calculate the position to center the text on the screen
                Vector2 position = new Vector2(
                    // Horizontally center the text by subtracting the text width from the screen width, then dividing by 2
                    (_graphics.PreferredBackBufferWidth - gameFont.MeasureString(controller.gameEndScript).X) / 2,

                    // Vertically center the text by subtracting the text height from the screen height, then dividing by 2
                    (_graphics.PreferredBackBufferHeight - gameFont.MeasureString(controller.gameEndScript).Y) / 2
                );

                // Draw the message at the centered position using the calculated position and white color
                _spriteBatch.DrawString(gameFont, controller.gameEndScript, position, Color.White);
            }



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
