using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public abstract class Bar : SpriteManager
    {
        protected int BAR_SPEED = 0;
        protected const int MOVE_UP = -1;
        protected const int MOVE_DOWN = 1;

        protected Vector2 mDirection = Vector2.Zero;
        protected Vector2 mSpeed = Vector2.Zero;
    }

    public class playerBar : Bar
    {
        KeyboardState previousKeyboardState;

        public playerBar()
        {
            BAR_SPEED = 300;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            UpdateMovement(aCurrentKeyboardState);

            previousKeyboardState = aCurrentKeyboardState;

            Position.Y += mDirection.Y * mSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void UpdateMovement(KeyboardState currentKeyboardState)
        {
            mSpeed = Vector2.Zero;
            mDirection = Vector2.Zero;

            if (currentKeyboardState.IsKeyDown(Keys.W) == true)
            {
                mSpeed.Y = BAR_SPEED;
                mDirection.Y = MOVE_UP;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.S) == true)
            {
                mSpeed.Y = BAR_SPEED;
                mDirection.Y = MOVE_DOWN;
            }
        }
    }

    public class enemyBar : Bar
    {
        //KeyboardState previousKeyboardState;

        public enemyBar()
        {
            BAR_SPEED = 300;
            mSpeed.Y = 300;
        }

        public void Update(GameTime gameTime, Vector2 ballPosition)
        {
            /* For Second Player
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            UpdateMovement(aCurrentKeyboardState);

            previousKeyboardState = aCurrentKeyboardState;
            */

            //AI
            if (ballPosition.Y < Position.Y)
                mDirection.Y = -1;
            else
                mDirection.Y = 1;

            if (Math.Abs(Position.Y - ballPosition.Y) <= 100)
            {
                mSpeed.Y = 80 + Math.Abs(ballPosition.Y - Position.Y) * (float)2;

                if (mSpeed.Y > 300)
                    mSpeed.Y = 300;
            }
            else
                mSpeed.Y = 300;

            if (Math.Abs(ballPosition.Y - Position.Y - 32) > 20 )
                Position.Y += mDirection.Y * mSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //END AI

            //For second Player
            //Position.Y += mDirection.Y * mSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /*
        private void UpdateMovement(KeyboardState currentKeyboardState)
        {
            mSpeed = Vector2.Zero;
            mDirection = Vector2.Zero;

            if (currentKeyboardState.IsKeyDown(Keys.Up) == true)
            {
                mSpeed.Y = BAR_SPEED;
                mDirection.Y = MOVE_UP;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Down) == true)
            {
                mSpeed.Y = BAR_SPEED;
                mDirection.Y = MOVE_DOWN;
            }
        }*/
    }
}
