using Microsoft.Xna.Framework;

namespace ChiayinYang_Assignment3
{
    internal class Asteroid
    {
        public Vector2 position; // Position of the asteroid on the screen
        public int speed;
        public static int radius = 60; // Radius of the asteroid, used for collision detection
        public bool HasScored { get; set; } = false; // Flag to track if the asteroid has been counted as passed by the player

        // Constructor to initialize the asteroid with a speed and a vertical position
        public Asteroid(int speed, int yPosition)
        {
            this.speed = speed;
            this.position = new Vector2(1300, yPosition); // Start the asteroid from the right side of the screen (x = 1300)
        }

        // Updates the position of the asteroid, moving it left across the screen
        public void updateAsteroid()
        {
            this.position.X -= this.speed; // Move the asteroid leftward at its assigned speed
        }
    }
}
