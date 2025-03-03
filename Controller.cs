using Microsoft.Xna.Framework;
using System;

namespace ChiayinYang_Assignment3
{
    internal class Controller
    {
        private TimeSpan elapsedTime; // Tracks the elapsed time for the timer
        private int secondsElapsed; // The total seconds that have passed
        public string gameEndScript = ""; // Message to display when the game ends

        // Constructor initializes the elapsed time and the seconds elapsed counter
        public Controller()
        {
            elapsedTime = TimeSpan.Zero; // Initialize elapsed time to zero
            secondsElapsed = 0; // Initialize seconds elapsed to zero
        }

        // Updates the timer and returns the number of seconds elapsed
        public int updateTime(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime; // Add the elapsed game time to the total elapsed time

            // Check if 1 second has passed
            if (elapsedTime.TotalSeconds >= 1)
            {
                secondsElapsed++;
                elapsedTime = TimeSpan.Zero; // Reset elapsed time to zero for the next second
            }

            return secondsElapsed; // Return the total number of seconds elapsed
        }

        // Checks for collision between the player ship and an asteroid
        public bool didCollisionHappen(Ship player, Asteroid asteroid)
        {
            int playerRadius = player.getRadius(); // Get the radius of the player ship
            int asteroidRadius = Asteroid.radius; // Get the radius of the asteroid
            int distance = playerRadius + asteroidRadius; // Calculate the sum of their radii

            // Check if the distance between the player and asteroid is less than the sum of their radii (collision)
            return Vector2.Distance(player.position, asteroid.position) < distance;
        }
    }
}
