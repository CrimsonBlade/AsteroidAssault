﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace AsteroidAssault
{
    class CollisionManager
    {
        //Declarations
        private AsteroidManager asteroidManager;
        private PlayerManager playerManager;
        private EnemyManager enemyManager;
        private ExplosionManager explosionManager;
        private Vector2 offScreen = new Vector2(-500, -500);
        private Vector2 shotToAsteroidImpact = new Vector2(0, -20);
        private int enemyPointValue = 100;

        //Constructor
        public CollisionManager( AsteroidManager asteroidManager, PlayerManager playerManager, EnemyManager enemyManager, ExplosionManager explosionManager)
        {
            this.asteroidManager = asteroidManager;
            this.playerManager = playerManager;
            this.enemyManager = enemyManager;
            this.explosionManager = explosionManager;
        }

        //Check Shot to Enemy Method
        private void checkShotToEnemyCollisions()
        {
            foreach (Sprite shot in playerManager.PlayerShotManager.Shots)
            {
                foreach (Enemy enemy in enemyManager.Enemies)
                {
                    if (shot.IsCircleColliding(enemy.EnemySprite.Center, enemy.EnemySprite.CollisionRadius))
                    {
                        shot.Location = offScreen;
                        enemy.Destroyed = true;
                        playerManager.PlayerScore += enemyPointValue;
                        explosionManager.AddExplosion(enemy.EnemySprite.Center, enemy.EnemySprite.Velocity / 10);
                    }
                }
            }
        }

        // Check Shot to Asteroids Method
        private void checkShotToAsteroidCollisions()
        {
            foreach (Sprite shot in playerManager.PlayerShotManager.Shots)
            {
                foreach (Sprite asteroid in asteroidManager.Asteroids)
                {
                    if (shot.IsCircleColliding(asteroid.Center, asteroid.CollisionRadius))
                    {
                        shot.Location = offScreen;
                        asteroid.Velocity += shotToAsteroidImpact;
                    }
                }
            }
        }

        // Check Shot to Player Method
        private void checkShotToPlayerCollisions()
        {
            foreach (Sprite shot in enemyManager.EnemyShotManager.Shots)
            {
                if (shot.IsCircleColliding(playerManager.playerSprite.Center, playerManager.playerSprite.CollisionRadius))
                {
                    shot.Location = offScreen;
                    playerManager.Destroyed = true;
                    explosionManager.AddExplosion(playerManager.playerSprite.Center, Vector2.Zero);
                }
            }
        }

        //CHECK ENEMY TO PLAYER COLLISIONS
        private void checkEnemyToPlayerCollisions()
        {
            foreach (Enemy enemy in enemyManager.Enemies)
            {
                if (enemy.EnemySprite.IsCircleColliding(playerManager.playerSprite.Center, playerManager.playerSprite.CollisionRadius))
                {
                    enemy.Destroyed = true;
                    explosionManager.AddExplosion(enemy.EnemySprite.Center, enemy.EnemySprite.Velocity / 10);

                    playerManager.Destroyed = true;

                    explosionManager.AddExplosion(playerManager.playerSprite.Center, Vector2.Zero);
                }
            }
        }

        //CHECK ASTEROID TO PLAYER COLLISONS
        private void checkAsteroidToPlayerCollisions()
        {
            foreach (Sprite asteroid in asteroidManager.Asteroids)
            {
                if (asteroid.IsCircleColliding(playerManager.playerSprite.Center, playerManager.playerSprite.CollisionRadius))
                {
                    explosionManager.AddExplosion(asteroid.Center, asteroid.Velocity / 10);

                    asteroid.Location = offScreen;

                    playerManager.Destroyed = true;
                    explosionManager.AddExplosion(playerManager.playerSprite.Center, Vector2.Zero);
                }
            }
        }

        //Check Collisions Method
        public void CheckCollisions()
        {
            checkShotToEnemyCollisions();
            checkShotToAsteroidCollisions();
            if (!playerManager.Destroyed)
            {
                checkShotToPlayerCollisions();
                checkShotToEnemyCollisions();
                checkShotToAsteroidCollisions();
            }
        }

    }
}