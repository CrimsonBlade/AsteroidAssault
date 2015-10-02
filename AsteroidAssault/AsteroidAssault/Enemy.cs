using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace AsteroidAssault
{
    class Enemy
    {

        //Declarations
        public Sprite EnemySprite;
        public Vector2 gunOffset = new Vector2(25, 25);
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        private Vector2 currentWaypoint = Vector2.Zero;
        private float speed = 120f;
        public bool Destroyed = false;
        private int enemyRadius = 15;
        private Vector2 previousLocation = Vector2.Zero;

        //Constructor
        public Enemy(Texture2D texture, Vector2 location, Rectangle initialFrame, int frameCount)
        {
            EnemySprite = new Sprite(location, texture, initialFrame, Vector2.Zero);
            for (int x = 1; x < frameCount; x++)
            {
                EnemySprite.AddFrame(new Rectangle(initialFrame.X = (initialFrame.Width * x),
                    initialFrame.Y, initialFrame.Width, initialFrame.Height));
            }
            previousLocation = location;
            currentWaypoint = location;
            EnemySprite.CollisionRadius = enemyRadius;
        }

        // Add Waypoint Method
        public void AddWaypoint(Vector2 waypoint)
        {
            waypoints.Enqueue(waypoint);
        }

        // Waypoint reached Method
        public bool WaypointReached()
        {
            if (Vector2.Distance(EnemySprite.Location, currentWaypoint) < (float)EnemySprite.Source.Width / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //IsActive Method
        public bool IsActive()
        {
            if (Destroyed)
            {
                return false;
            }
            if (waypoints.Count > 0)
            {
                return true;
            }
            if (WaypointReached())
            {
                return false;
            }

            return true;
        }

        //Update Method
        public void Update(GameTime gameTime)
        {
            if (IsActive())
            {
                Vector2 heading = currentWaypoint - EnemySprite.Location;
                if (heading != Vector2.Zero)
                {
                    heading.Normalize();
                }
                heading *= speed;
                EnemySprite.Velocity = heading;
                previousLocation = EnemySprite.Location;
                EnemySprite.Update(gameTime);
                EnemySprite.Rotaton = (float)Math.Atan2(EnemySprite.Location.Y - previousLocation.Y,
                    EnemySprite.Location.X - previousLocation.X);

                if (WaypointReached())
                {
                    if (waypoints.Count > 0)
                    {
                        currentWaypoint = waypoints.Dequeue();
                    }
                }
            }
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive())
            {
                EnemySprite.Draw(spriteBatch);
            }
        }
    }
}
