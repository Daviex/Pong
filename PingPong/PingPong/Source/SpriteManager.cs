using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    public class SpriteManager
    {
        Texture2D SpriteTextures;
        public Vector2 Position;
        public Vector2 Velocity;

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    SpriteTextures.Width,
                    SpriteTextures.Height);
            }

        }

        public void loadContent(Texture2D texture, Vector2 position)
        {
            SpriteTextures = texture;
            Position = position;
        }

        public void loadContent(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            SpriteTextures = texture;
            Position = position;
            Velocity = velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTextures, Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
