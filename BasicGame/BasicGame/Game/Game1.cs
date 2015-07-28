#region File Description
//-----------------------------------------------------------------------------
// Game.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
#endregion

namespace EECS494.IntoTheDarkness
{
    /// <summary>
    /// Sample showing how to manage different game states, with transitions
    /// between menu screens, a loading screen, the game itself, and a pause
    /// menu. This main game class is extremely simple: all the interesting
    /// stuff happens in the ScreenManager component.
    /// </summary>
    public class GameOne : Game
    {
        #region Fields

        GraphicsDeviceManager graphics;
        ScreenManager screenManager;
        Song backgroundMusic;


        // By preloading any assets used by UI rendering, we avoid framerate glitches
        // when they suddenly need to be loaded in the middle of a menu transition.
        static readonly string[] preloadAssets =
        {
            "MenuContent//gradient",
        };


        #endregion
        
        #region Initialization


        /// <summary>
        /// The main game constructor.
        /// </summary>
        public GameOne()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 853;
            graphics.PreferredBackBufferHeight = 480;

            graphics.IsFullScreen = true;

            // Create the screen manager component.
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }


        /// <summary>
        /// Loads graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            loadSounds();
            backgroundMusic = Content.Load<Song>("GameMusic/Haunted By Voices");  // Put the name of your song here instead of "song_title"
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.Play(backgroundMusic);
            foreach (string asset in preloadAssets)
            {
                Content.Load<object>(asset);
            }
        }

        private void loadSounds()
        {
            // for Player
            // wall thud is done in GameplayScreen
            SoundEffect a;
            a = Content.Load<SoundEffect>("Sounds/Thud");
            SoundMaster.AddSound("jump_thud", a);
            SoundEffect b;
            b = Content.Load<SoundEffect>("Sounds/ThrowSound");
            SoundMaster.AddSound("throw_sound", b);

            // Monster
            SoundEffect c;
            c = Content.Load<SoundEffect>("Sounds/Monster_There_You_Are");
            SoundMaster.AddSound("monster_phrase", c);
            SoundEffect d;
            d = Content.Load<SoundEffect>("Sounds/Monster_Step");
            SoundMaster.AddSound("monster_step", d);
            SoundEffect e;
            e = Content.Load<SoundEffect>("Sounds/Monster_Youre_Mine");
            SoundMaster.AddSound("monster_game_over", e);

            // Machine
            SoundEffect f;
            f = Content.Load<SoundEffect>("Sounds/Machine_Hum_2");
            SoundMaster.AddSound("machine_hum", f);

            // Switch
            SoundEffect g;
            g = Content.Load<SoundEffect>("Sounds/Switch_Click");
            SoundMaster.AddSound("switch_click", g);
            
            // Lock
            SoundEffect h;
            h = Content.Load<SoundEffect>("Sounds/Door_Unlock");
            SoundMaster.AddSound("door_unlock", h);

            // Key
            SoundEffect i;
            i = Content.Load<SoundEffect>("Sounds/Key_Pickup");
            SoundMaster.AddSound("key_pickup", i);
            
            // Stone
            SoundEffect j;
            j = Content.Load<SoundEffect>("Sounds/Stone_Ground");
            SoundMaster.AddSound("stone_ground", e);
            SoundEffect k;
            k = Content.Load<SoundEffect>("Sounds/Stone_Wall");
            SoundMaster.AddSound("stone_wall", k);
            // use key pickup for stone pickup
            
            SoundEffect l;
            l = Content.Load<SoundEffect>("Sounds/Monster_Grunt");
            SoundMaster.AddSound("monster_grunt", l);

            SoundEffect m;
            m = Content.Load<SoundEffect>("Sounds/Wall_Move");
            SoundMaster.AddSound("wall_move", m);


            SoundEffect z;
            z = Content.Load<SoundEffect>("Sounds/button-3");
            SoundMaster.AddSound("button_beep", z);
        }
        #endregion

        #region Draw


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // The real drawing happens inside the screen manager component.
            base.Draw(gameTime);
        }


        #endregion
    }
}
