using Microsoft.Xna.Framework; //for Vector2
using Microsoft.Xna.Framework.Graphics; //for Texture 2D
namespace IWKS_3400_Lab4        //namespace for IWKS_3400_Lab3
{
    class clsSprite
    {
        public Texture2D texture { get; set; } // sprite texture, read-only property
        public Vector2 position { get; set; } // sprite position on screen
        public Vector2 size { get; set; } // sprite size in pixels
        public Vector2 velocity { get; set; } // sprite velocity 
        private Vector2 screenSize { get; set; } // screen size
        public Vector2 center { get { return position + (size / 2); } } // sprite center
        public float radius { get { return size.X / 2; } } // sprite radius
           


        //setting the object of the sprite with the image, start position, the image size, the width of the screen and the height of the screen
        public clsSprite(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, int ScreenWidth, int ScreenHeight)
        {
            texture = newTexture;                   //sets the texture
            position = newPosition;                 //sets the position
            size = newSize;                         //sets the size
            screenSize = new Vector2(ScreenWidth, ScreenHeight);     //sets the vector    
        }
        //this draws the sprite on the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
        //this checks the center of the image for the point to collid away
        public bool CircleCollides(clsSprite otherSprite)
        {
            // Check if two circle sprites collided
            return (Vector2.Distance(this.center, otherSprite.center) < this.radius +
            otherSprite.radius);
        }


        public bool Collides(clsSprite otherSprite)
        {
            // check if two sprites intersect
            //when ever the sprite gets close to the walls then it will bounce off of the wall
            if (this.position.X + this.size.X > otherSprite.position.X &&
            this.position.X < otherSprite.position.X + otherSprite.size.X &&
            this.position.Y + this.size.Y > otherSprite.position.Y &&
            this.position.Y < otherSprite.position.Y + otherSprite.size.Y) return true;
            else
                return false;
        }

       
        public bool MovePointRight()      //moving for the sprite, added the bool for the sound when it hits the screen wall 
        {
            bool point = false;

            // if we´ll move out of the screen, invert velocity
            // checking right boundary, when it hits the boundary it will set it to true for the sound
            if (position.X + size.X + velocity.X > screenSize.X)
            {
                velocity = new Vector2(-velocity.X, velocity.Y);
                point = true;
            }
            // checking bottom boundary, when it hits the boundary it will set it to true for the sound
            if (position.Y + size.Y + velocity.Y > screenSize.Y)
            {
                velocity = new Vector2(velocity.X, -velocity.Y);
                point = false;
            }
            // checking left boundary, when it hits the boundary it will set it to true for the sound
            if (position.X + velocity.X < 0)
            {
                velocity = new Vector2(-velocity.X, velocity.Y);
                point = false;
            }
            // checking top boundary, when it hits the boundary it will set it to true for the sound
            if (position.Y + velocity.Y < 0)
            {
                velocity = new Vector2(velocity.X, -velocity.Y);
                point = false;
            }
            // since we adjusted the velocity, just add it to the current position
            position += velocity;
            return (point);
        }

        public bool MovePointLeft()      //moving for the sprite, added the bool for the sound when it hits the screen wall 
        {
            bool point = false;

            // if we´ll move out of the screen, invert velocity
            // checking right boundary, when it hits the boundary it will set it to true for the sound
            if (position.X + size.X + velocity.X > screenSize.X)
            {
                velocity = new Vector2(-velocity.X, velocity.Y);
                point = false;
            }
            // checking bottom boundary, when it hits the boundary it will set it to true for the sound
            if (position.Y + size.Y + velocity.Y > screenSize.Y)
            {
                velocity = new Vector2(velocity.X, -velocity.Y);
                point = false;
            }
            // checking left boundary, when it hits the boundary it will set it to true for the sound
            if (position.X + velocity.X < 0)
            {
               velocity = new Vector2(-velocity.X, velocity.Y);
                point = true;
            }
            // checking top boundary, when it hits the boundary it will set it to true for the sound
            if (position.Y + velocity.Y < 0)
            {
                velocity = new Vector2(velocity.X, -velocity.Y);
                point = false;
            }
            // since we adjusted the velocity, just add it to the current position
            position += velocity;
            return (point);
        }

        public void Move()
        { 
            //right boundary
            if (position.X + size.X + velocity.X >= screenSize.X)
                position = new Vector2((screenSize.X -size.X), velocity.Y);
            // checking left boundary
            if (position.X + velocity.X <= 0)
                position = new Vector2(0, position.Y);
            // checking top boundary
            if (position.Y + velocity.Y <= 0)
                position = new Vector2(position.X, 0);
            // checking bottom boundary
            if (position.Y + size.Y + velocity.Y >= screenSize.Y)
                position = new Vector2(position.X, (screenSize.Y - size.Y));
            // since we adjusted the velocity, just add it to the current position
            position += velocity;
        }

        //public void computerMove(clsSprite movingBall)
        //{
        //    if (movingBall.velocity.X > 0 && movingBall.velocity.Y < 0)
        //        position = new Point(screenSize.X - (size.X + size.X / position.Y - size.Y / 2));
        //}
        
    }
}