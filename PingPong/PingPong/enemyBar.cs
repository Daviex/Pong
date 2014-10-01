/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class enemyBar : SpriteManager
    {
        const int BAR_SPEED = 200;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        KeyboardState previousKeyboardState;

        public void Update(GameTime gameTime, Vector2 ballPosition)
        {
            /* For Second Player
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            UpdateMovement(aCurrentKeyboardState);

            previousKeyboardState = aCurrentKeyboardState;
            

            //AI
            mSpeed.Y = BAR_SPEED;

            if (ballPosition.Y < Position.Y)
                mDirection.Y = -1;
            else
                mDirection.Y = 1;

            if (ballPosition.Y != Position.Y )
                Position.Y += mDirection.Y * mSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //END AI

            //For second Player
            //Position.Y += mDirection.Y * mSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

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
        }
    }
}
*/