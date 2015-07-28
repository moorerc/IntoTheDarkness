#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace EECS494.IntoTheDarkness
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry soundMenuEntry;
        MenuEntry screenMenuEntry;
        MenuEntry controlsMenuEntry;

        public static bool playSound = true;
        public static bool fullScreen = false;
        public static bool controlsInvert = false;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("")
        {
            // Create our menu entries.
            soundMenuEntry = new MenuEntry(string.Empty);
            screenMenuEntry = new MenuEntry(string.Empty);
            controlsMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            soundMenuEntry.Selected += SoundMenuEntrySelected;
            screenMenuEntry.Selected += ScreenMenuEntrySelected;
            controlsMenuEntry.Selected += ControlsMenuEntrySelected;
            back.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(soundMenuEntry);
            MenuEntries.Add(screenMenuEntry);
            MenuEntries.Add(controlsMenuEntry);
            MenuEntries.Add(back);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            soundMenuEntry.Text = "Sound: " + (playSound ? "On" : "Off");
            screenMenuEntry.Text = "Full Screen: " + (fullScreen ? "On" : "Off");
            controlsMenuEntry.Text = "Controls: " + (controlsInvert ? "Inverted" : "Normal");
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Sound menu entry is selected.
        /// </summary>
        void SoundMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            playSound = !playSound;
            SetMenuEntryText();
        }
        
        /// <summary>
        /// Event handler for when the Screen menu entry is selected.
        /// </summary>
        void ScreenMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            fullScreen = !fullScreen;
            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the Control menu entry is selected.
        /// </summary>
        void ControlsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            controlsInvert = !controlsInvert;
            SetMenuEntryText();
        }

        #endregion
    }
}