using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Microsoft.Win32.SafeHandles;  

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 scorePlayer = new Vector2(0, 0);
        Vector2 scoreEnemy = new Vector2(0, 0);

        int[] randomNumbers = new int[] { 4, 4, -4, -4};

        playerBar player;
        SpriteManager ball;
        enemyBar enemy;
        SpriteManager centralLine;

        emergencyShield shield;

        HUD hudPlayer;
        HUD hudEnemy;

        int whoPoint;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferMultiSampling = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            player = new playerBar();
            enemy = new enemyBar();
            ball = new SpriteManager();
            centralLine = new SpriteManager();

            hudPlayer = new HUD();
            hudEnemy = new HUD();

            scorePlayer = new Vector2(200, 40);
            scoreEnemy = new Vector2(880, 40);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D playerTexture = Content.Load<Texture2D>("img/Sprite/bar");
            Texture2D enemyTexture = Content.Load<Texture2D>("img/Sprite/bar");
            Texture2D ballTexture = Content.Load<Texture2D>("img/Sprite/ball");
            Texture2D centralLineTexture = Content.Load<Texture2D>("img/Sprite/centralBar");
            Texture2D emergencyShield = Content.Load<Texture2D>("img/Sprite/emergencyShield");

            hudPlayer.Font = Content.Load<SpriteFont>("myFont");
            hudEnemy.Font = Content.Load<SpriteFont>("myFont");

            player.loadContent(playerTexture, new Vector2(50, 340));
            enemy.loadContent(enemyTexture, new Vector2(1230, 340));

            
            Random rand = new Random(Environment.TickCount);
            double grade = 0.0;
            bool Found = false;

            do
            {
                grade = rand.Next(0, 360);
                if ((grade >= 30 && grade <= 60) || (grade >= 120 && grade <= 150) || (grade >= 210 && grade <= 240) || (grade >= 300 && grade <= 330))
                {
#if DEBUG
                    Debug.WriteLine("Degrees: " + grade);
                    Debug.WriteLine("Radians: " + DegreesToRadians(grade));
                    Debug.WriteLine("Sin: " + Math.Sin(DegreesToRadians(grade)));
                    Debug.WriteLine("Sin: " + Math.Sin(DegreesToRadians(grade)));
                    Debug.WriteLine("Cos: " + Math.Cos(DegreesToRadians(grade)));
#endif
                    Found = true;
                }
            }
            while (!Found);
            ball.loadContent(ballTexture, new Vector2(640, 360), fromDegrees(DegreesToRadians(grade)));

            centralLine.loadContent(centralLineTexture, new Vector2(640, 0));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>w
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            player.Update(gameTime);
            enemy.Update(gameTime, ball.Position);

            ball.Position += ball.Velocity;

            #region Player Collision with Client Bounds
            if (player.Position.Y <= 0)
                player.Position.Y = 0;
            if (player.Position.Y >= Window.ClientBounds.Height - player.BoundingBox.Height)
                player.Position.Y = Window.ClientBounds.Height - player.BoundingBox.Height;
            #endregion

            #region Enemy Collision with Client Bounds
            if (enemy.Position.Y <= 0)
                enemy.Position.Y = 0;
            if (enemy.Position.Y >= Window.ClientBounds.Height - enemy.BoundingBox.Height)
                enemy.Position.Y = Window.ClientBounds.Height - enemy.BoundingBox.Height;
            #endregion

            #region Ball Collision with Client Bounds
            if (ball.Position.X <= 0)
            {
                whoPoint = 1;
                ball.Velocity.X += 0.5f;
                ball.Velocity.Y += 0.5f;
                hudEnemy.Score++;
                resetBall();
            }
            if (ball.Position.Y <= 0)
            {
                ball.Velocity.Y = -ball.Velocity.Y;
                ball.Position += ball.Velocity;
            }

            if (ball.Position.X >= Window.ClientBounds.Width - ball.BoundingBox.Width)
            {
                whoPoint = 0;
                ball.Velocity.X += 0.5f;
                ball.Velocity.Y += 0.5f;
                hudPlayer.Score++;
                resetBall();
            }
            if (ball.Position.Y >= Window.ClientBounds.Height - ball.BoundingBox.Height)
            {
                ball.Velocity.Y = -ball.Velocity.Y;
                ball.Position += ball.Velocity;
            }

            if (ball.BoundingBox.Intersects(player.BoundingBox) || ball.BoundingBox.Intersects(enemy.BoundingBox))
            {
                if (BallFrontHit())
                {
                    ball.Velocity.X = -ball.Velocity.X;
                    ball.Velocity.X *= (float)1.2;
                    ball.Velocity.Y *= (float)1.2;
                    ball.Position += ball.Velocity;
                }
            }

            #endregion

            #region Emergency Shield Activation
            if (player.emergencyShieldActive)
            {
                
            }
            #endregion

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            player.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            hudPlayer.Draw(spriteBatch, scorePlayer);
            hudEnemy.Draw(spriteBatch, scoreEnemy);

            if (player.emergencyShieldActive)
            {
                shield.Draw(spriteBatch);
            }

            centralLine.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void resetBall()
        {
            ball.Position = new Vector2(640, 360);
            Random rand = new Random(Environment.TickCount);
            double grade = 0.0;
            bool Found = false;

            do
            {
                grade = rand.Next(0, 360);
                if ((grade >= 30 && grade <= 60) || (grade >= 120 && grade <= 150) || (grade >= 210 && grade <= 240) || (grade >= 300 && grade <= 330))
                {
#if DEBUG
                    Debug.WriteLine("Degrees: " + grade);
                    Debug.WriteLine("Radians: " + DegreesToRadians(grade));
                    Debug.WriteLine("Sin: " + Math.Sin(DegreesToRadians(grade)));
                    Debug.WriteLine("Sin: " + Math.Sin(DegreesToRadians(grade)));
                    Debug.WriteLine("Cos: " + Math.Cos(DegreesToRadians(grade)));
#endif
                    Found = true;
                }
            }
            while (!Found);
            ball.Velocity = fromDegrees(DegreesToRadians(grade));

            player.Position = new Vector2(50, 340);
            enemy.Position = new Vector2(1230, 340);
        }

        public bool BallFrontHit()
        {
            if (ball.Position.X > player.Position.X && ball.Position.X < enemy.Position.X)
                return true;
            else
                return false;
        }

        public double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        private Vector2 fromDegrees(double value)
        {
            Vector2 newVector = new Vector2();

            double x = Math.Sin(value);
            double y = Math.Cos(value);
            float mult = 4;

            newVector.X = (float)x * mult;
            newVector.Y = (float)y * mult;

            return newVector;
        }
    }
}
