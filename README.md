# Spaceship Mono-Game App

## Overview
This is a game where you control a spaceship and navigate through multiple asteroids. The goal is to avoid colliding with the asteroids, and the game ends either when the timer runs out or when you hit an asteroid. You score points for each asteroid you avoid, but lose points for collisions.

## Features
1. **Multiple Asteroids**: Asteroids spawn at random positions and move horizontally from right to left at different speeds.
2. **Score**: Every time an asteroid passes the spaceship without collision, the score increases by one.
3. **Running Timer**: The game has a timer that runs from 1 to 15 seconds. The timer stops at 15, marking the end of the game.
4. **Game Win**: If the timer reaches 15, the game ends successfully and displays the number of asteroids you surpassed.
5. **Game Loss**: If the spaceship collides with an asteroid or if the score goes negative, the game ends with a failure message.
6. **Movement**: The spaceship can move in all directions (up, down, left, right) using arrow keys. Pressing the 'right arrow key' + 'enter key' boosts the spaceship's forward speed.
7. **Sound Effects**: The game includes sound and/or music effects for actions such as collisions and game events.

## How to Play
- **Move**: Use the arrow keys to move the spaceship (up, down, left, or right).
- **Boost**: Press the 'right arrow key' + 'enter key' to boost your spaceship’s forward speed.
- **Avoid Asteroids**: Avoid colliding with asteroids that move horizontally from right to left.
- **Timer**: The timer counts from 1 to 15 seconds. If you surpass 15 seconds, you win!
- **Score**: Your score increases when you avoid asteroids and decreases if you collide with them.

## Game Logic
- **Asteroid Spawning**: Multiple asteroids are spawned from random positions at designated time intervals.
- **Score Calculation**: Every asteroid passed without collision increases the score by 1 point. If the spaceship collides with an asteroid, the score decreases by 3 points.
- **Game End**: 
  - **Success**: When the timer reaches 15, the game ends and displays a success message: "Game Won, You surpassed <x> asteroids".
  - **Failure**: If the spaceship collides with any asteroid or if the score goes negative, the game ends with the message: "Game Lost!! Spaceship hit by Asteroid".
