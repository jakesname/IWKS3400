using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace IWKS_3400_Lab4
{
    class pongAI
    {
        int ballPos;
        int myPos; //star position
        int myHeight; // number of pixels in sprite to aim for center
         //  int Y; //Y axis position
        float WindowHeight;// size of game window
        public float AIMoveSpeed = 2; // move speed of AI opponent
       //###### WindowHeight = Rectangle(GraphicsDevice.Viewport.Height);  //Not sure how to declare this to make it work
       //####### ballPos = clsSprite mySprite3; //not sure how to make mySprite3 get called to do the calculation. 

        #regionAIposCalc
        public void UpdateAI()
        {
            Vector2 position = ballPos;
            if (ballPos.Y > (myPos.Y + myHeight / 2)) //  aim at the center so top left + 1/2 height makes it center of the star 
            {
                if (CanMoveDown())
                {
                    myPos.Y += AIMoveSpeed;
                }
            }
            if (ballPos.Y < (myPos.Y + myHeight / 2)) // aim at the center so top left + 1/2 height makes it center of the star  
            {
                if (CanMoveUp())
                {
                    myPos.Y -= AIMoveSpeed;
                }
            }
        }
        //end AI move calculation
# endregionAIposCalc

 #regionCanMoveY
        //check to see if can move Y/N
        public bool CanMoveUp()

        {
            //check to see if sprite at TOP of screen. Prevent going off screeen
            // if the paddle position is greater than max move can move up
            if (myPos.Y > AIMoveSpeed) 
                return true;
            else //prevent clipping  
                return false;
        }

        public bool CanMoveDown()
        {
            //check to see if sprite at BOTTOM of screen. Prevent going off screen
            // if the paddle position + paddle height + AIMoveSpeed is less than screen heigh can move  
            if (myPos.Y + myHeight + AIMoveSpeed < WindowHeight) // there is space below us so we can move down   
                return true;
            else
            {// prevent clipping
                return false;
            }
        }
        //end check to see move Y/N
#endregionCanMoveY

    }
}




    

