using System;
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
    public class HUD
    {
        public SpriteFont Font { get; set; }
        public int Score;

        public HUD()
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 scorePos)
        {
            spriteBatch.DrawString(Font, "Score: " + Score, scorePos, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }
    }
}
