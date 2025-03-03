using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ChiayinYang_Assignment3
{
    internal class Ship
    {
        public Vector2 position = new Vector2(100, 100); // Initial position of the ship on the screen
        private int speed = 2;
        private int radius; // The radius of the ship, used for collision detection and boundary control

        // Sets the radius of the ship (used for collision detection and keeping the ship within screen bounds)
        public void setRadius(int radius)
        {
            this.radius = radius;
        }

        // Returns the current radius of the ship
        public int getRadius()
        {
            return this.radius;
        }

        // Updates the ship's position based on keyboard input
        public void updateShip()
        {
            KeyboardState state = Keyboard.GetState(); // Get the current state of the keyboard

            // Move speed is doubled if the Right arrow key and Enter key are pressed simultaneously
            int moveSpeed = (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Enter)) ? speed * 2 : speed;

            // Update the ship's position based on arrow key input
            if (state.IsKeyDown(Keys.Left)) position.X -= moveSpeed;
            if (state.IsKeyDown(Keys.Right)) position.X += moveSpeed;
            if (state.IsKeyDown(Keys.Up)) position.Y -= speed;
            if (state.IsKeyDown(Keys.Down)) position.Y += speed;

            // Ensure the spaceship stays within the screen bounds, adjusting position if necessary
            position.X = MathHelper.Clamp(position.X, radius, 1200 - radius); // Prevent going out of the left or right side
            position.Y = MathHelper.Clamp(position.Y, radius, 900 - radius); // Prevent going out of the top or bottom
        }
    }
}
