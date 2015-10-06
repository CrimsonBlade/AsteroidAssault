using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace AsteroidAssault
{
    public static class SoundManager
    {

        //Declarations
        public static List<SoundEffect> explosions = new List<SoundEffect>();
        private static int explosionCount = 4;
        private static SoundEffect playerShot;
        private static SoundEffect enemyShot;
        private static Random rand = new Random();

        //Initialize method (does work of constructor in this case / static class)
        public static void Initialize(ContentManager content)
        {
            try
            {
                playerShot = content.Load<SoundEffect>(@"Sounds\Shot1");
                enemyShot = content.Load<SoundEffect>(@"Sounds\Shot2");

                for (int x = 1; x <= explosionCount; x++)
                {
                    explosions.Add(content.Load<SoundEffect>(@"Sounds\Explosion" + x.ToString()));
                }
            }
            catch
            {
                Debug.Write("SoundManager Initialization Failed");
            }
        }

        //Play Explosion Method
        public static void PlayExplosion()
        {
            try
            {
                explosions[rand.Next(0, explosionCount)].Play();
            }
            catch
            {
                Debug.Write("PlayExplosion Failed");
            }
        }

        // Play Player Shot Method
        public static void PlayPlayerShot()
        {
            try
            {
                playerShot.Play();
            }
            catch
            {
                Debug.Write("PlayPlayerShot Failed");
            }
        }

        // Play emeny shot
        public static void PlayEnemyShot()
        {
            try
            {
                enemyShot.Play();
            }
            catch
            {
                Debug.Write("PlayEnemyShot Failed");
            }
        } 

    }
}
