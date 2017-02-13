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

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;       // SpriteBatch which will draw (render) the sprite
        clsSprite mySprite1;        //we are setting up the class for redStar
        clsSprite mySprite2;        //we are setting up the class for greenStar
        clsSprite mySprite3;        //we are setting up the class for ball

        //contain the background 
        Texture2D background;
        //conatin the mianFrme
        Rectangle mainFrame;
        //for the fonts that we will be using
        SpriteFont Font1;

        //*************************************************
        //need to change for the pong game
        // Create a SoundEffect resource
        SoundEffect soundEffect1;
        SoundEffect soundEffect2;
        AudioEngine audioEngine;            //using the the audioEngine
        SoundBank soundBank;                //using the soundbank from XACT
        WaveBank waveBank;                  //using the waveBank from XACT
        Cue cue;                            //using the cue from XACT
        WaveBank streamingWaveBank;         //this wavebank is for the background music
        Cue musicCue;                       //this cue is for the background music
        //********************************************************************************
   
        //i just hard coded the this but we can move this around
        public int scoreComputer = 0;
        public int scorePlayer =0;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            // changing the back buffer size changes the window size (in windowed mode) 

            graphics.PreferredBackBufferWidth = 1800;            //this changes the width
            graphics.PreferredBackBufferHeight = 500;              //this changes the height

            Content.RootDirectory = "Content";
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }



        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            //load content for the background
            background = Content.Load<Texture2D>("spatiul-cosmic");

            //set the rectangle parameters
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);


            //**********************************************sounds
            // Load files built from XACT project
            //these will allow us to pull the music from the labs3sounds to play in the game
            audioEngine = new AudioEngine("Content\\Labs3Sounds.xgs");
            waveBank = new WaveBank(audioEngine, "Content\\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, "Content\\Sound Bank.xsb");
            // Load streaming wave bank
            streamingWaveBank = new WaveBank(audioEngine, "Content\\Music.xwb", 0, 4);
            // The audio engine must be updated before the streaming cue is ready
            audioEngine.Update();
            // Get cue for streaming music
            //got this .wav file from this website
            //https://www.dl-sounds.com/royalty-free/loading-loop/
            musicCue = soundBank.GetCue("Loop");
            // Start the background music
            musicCue.Play();
            // Load the SoundEffect resource
            soundEffect1 = Content.Load<SoundEffect>("chord");
            soundEffect2 = Content.Load<SoundEffect>("metal");
            //***************************************************sounds

            // Load a 2D texture sprite 
            //setting the object of the sprite with the image, start position, the image size, the width of the screen and the height of the screen
            mySprite1 = new clsSprite(Content.Load<Texture2D>("redstar"), new Vector2(0f, 0f), new Vector2(64f, 64f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            mySprite2 = new clsSprite(Content.Load<Texture2D>("greenstar"), new Vector2(1700f, 0f), new Vector2(64f, 64f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            mySprite3 = new clsSprite(Content.Load<Texture2D>("ball"), new Vector2(340, 300f), new Vector2(64f, 64f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);


            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

     

            //mySprite1.velocity = new Vector2(5, 5);      //sets the velcoity of the sprite
            //mySprite2.velocity = new Vector2(3, -3);     //sets the velcoity of the sprite
            mySprite3.velocity = new Vector2(5,5);     //sets the velcoity of the sprite

            //for the fonts
            Font1 = Content.Load<SpriteFont>("Courier New");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            // Free the previously allocated resources 
            mySprite1.texture.Dispose();            //this is where the image is disposed
            mySprite2.texture.Dispose();            //this is where the image is disposed
            mySprite3.texture.Dispose();            //this is where the image is disposed
            spriteBatch.Dispose();                     //this is where the object gets disposed

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
           

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();

            // Move the sprite by itself
            mySprite1.Move();
            mySprite2.Move();

            //this allows the sprite to make a noise when it hits a baoundary line
            if (mySprite3.MovePointLeft())
            {
                scoreComputer++;
                mySprite3.position = new Vector2(500,250);
                
                    }




            if (mySprite3.MovePointRight())
            {
                scorePlayer++;
                mySprite3.position = new Vector2(500, 250);
            }

            //if (mySprite2.Move()) soundEffect1.Play(0.5f, 0.5f, 0.0f);

            // Change the sprite3 position using the keyboard
            // i liked this movement for my sprite since it felt better for me
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
                mySprite2.position += new Vector2(0, -5);
            if (keyboardState.IsKeyDown(Keys.Down))
                mySprite2.position += new Vector2(0, 5);

            KeyboardState keyboardState1 = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W))
                mySprite1.position += new Vector2(0, -5);
            if (keyboardState.IsKeyDown(Keys.S))
                mySprite1.position += new Vector2(0, 5);


            /* 
             * for the sprite to move with a controller
            // Change the sprite 2 position using the left thumbstick
            Vector2 LeftThumb = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
            mySprite2.position += new Vector2(LeftThumb.X, -LeftThumb.Y) * 5;
            */

            /*
            //Make sprite 3 follow the mouse
            if (mySprite3.position.X < Mouse.GetState().X)
                mySprite3.position += new Vector2(5, 0);
            if (mySprite3.position.X > Mouse.GetState().X)
                mySprite3.position += new Vector2(-5, 0);
            if (mySprite3.position.Y < Mouse.GetState().Y)
                mySprite3.position += new Vector2(0, 5);
            if (mySprite3.position.Y > Mouse.GetState().Y)
                mySprite3.position += new Vector2(0, -5);
            */


            /*
            //checks on the collitions of the two sprites
            if (mySprite1.Collides(mySprite2))
            {
                Vector2 tempVelocity = mySprite1.velocity;
                mySprite1.velocity = mySprite2.velocity;
                mySprite2.velocity = tempVelocity;
            }
            */
            //when sprite1 hits sprite2 then the velocity wil transfer
            if (mySprite1.CircleCollides(mySprite2))
            {
                soundEffect2.Play(1.0f, 1.0f, 1.0f);
                mySprite1.velocity *= -1;
                mySprite2.velocity *= -1;

            }
            //when sprite2 hits sprite3 then the velocity wil transfer
            if (mySprite2.CircleCollides(mySprite3))
            {
                soundEffect1.Play(1.0f, 1.0f, 0.5f);
                mySprite2.velocity *= -1;
                mySprite3.velocity *= -1;

            }
            //when sprite3 hits sprite1 then the velocity wil transfer
            if (mySprite3.CircleCollides(mySprite1))
            {
                soundEffect1.Play(1.0f, 1.0f, 0.5f);
                mySprite3.velocity *= -1;
                mySprite1.velocity *= -1;
                //this sound was too low to listen for with the background music
                // Get an instance of the cue from the XACT project
                //cue = soundBank.GetCue("chord");
                //cue.Play();
            }

            /*
            if (mySprite1.CircleCollides(mySprite2))
            {
                mySprite1.velocity *= -1;
                GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);
            }
            else
                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            */

            // TODO: Add your update logic here


            // Update the audio engine
            audioEngine.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // Draw the sprite using Alpha Blend, which uses transparency information if available
            // In 4.0, this behavior is the default; in XNA 3.1, it is not 
            spriteBatch.Begin();
            // Draw running score string
                     spriteBatch.Draw(background, mainFrame, Color.White);
            spriteBatch.DrawString(Font1, "Player: " + scorePlayer, new Vector2(5, 10),Color.White);
            spriteBatch.DrawString(Font1, "Computer: " + scoreComputer, new Vector2(1550, 10), Color.White);
   

            mySprite1.Draw(spriteBatch);    //this draws the sprite
            mySprite2.Draw(spriteBatch);    //this draws the sprite

            mySprite3.Draw(spriteBatch);    //this draws the sprite
            spriteBatch.End();          //the end of the drawing of the sprite object

            base.Draw(gameTime);
        }
    }
}
