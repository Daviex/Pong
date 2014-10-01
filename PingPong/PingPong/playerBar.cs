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
    public class playerBar : SpriteManager
    {
        const int BAR_SPEED = 500;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        KeyboardState previousKeyboardState;

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
}
*/