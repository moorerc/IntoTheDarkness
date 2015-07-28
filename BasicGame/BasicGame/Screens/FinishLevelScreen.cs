#region File Description
//-----------------------------------------------------------------------------
// MessageBoxScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace EECS494.IntoTheDarkness
{
    /// <summary>
    /// A popup message box screen, used to display "are you sure?"
    /// confirmation messages.
    /// </summary>
    class FinishLevelScreen : MessageBoxScreen
    {
        #region Fields

        string level;
        string message;
        Texture2D gradientTexture;
        Texture2D descriptionTexture;
        Texture2D controlsTexture;

        Texture2D win;
        Texture2D fail;

        Texture2D l1;
        Texture2D l2;
        Texture2D l3;
        Texture2D l4;
        Texture2D l5;
        Texture2D l6;
        Texture2D l7;

        string[] levelTitles = { "Find the Crystal", 
                                 "Locked In", 
                                 "Don't Flip Out", 
                                 "Aaahh!!! Real...", 
                                 "Stone Thrower", 
                                 "The Great Maze", 
                                 "End Game"};

        #endregion

        #region Events

        public event EventHandler<PlayerIndexEventArgs> A;
        public event EventHandler<PlayerIndexEventArgs> C;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor automatically includes the standard "A=ok, B=cancel"
        /// usage text prompt.
        /// </summary>
        /*public MessageBoxScreen(string message)
            : this(message, true)
        { }*/


        /// <summary>
        /// Constructor lets the caller specify whether to include the standard
        /// "A=ok, B=cancel" usage text prompt.
        /// </summary>
        public FinishLevelScreen(string message, bool includeUsageText)
            : base (message, includeUsageText)
        {
            GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
            
            level = message;
            this.message = string.Empty;

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }


        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded data.
        /// </summary>
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            gradientTexture = content.Load<Texture2D>("MenuContent//levelSwitchBackground");
            descriptionTexture = content.Load<Texture2D>("MenuContent//story_description");
            controlsTexture = content.Load<Texture2D>("MenuContent//controls_screen");

            win = content.Load<Texture2D>("MenuContent//wingamescreen");
            fail = content.Load<Texture2D>("MenuContent//failurescreen");

            l1 = content.Load<Texture2D>("MenuContent//levelonescreen");
            l2 = content.Load<Texture2D>("MenuContent//leveltwoscreen");
            l3 = content.Load<Texture2D>("MenuContent//levelthreescreen");
            l4 = content.Load<Texture2D>("MenuContent//levelfourscreen");
            l5 = content.Load<Texture2D>("MenuContent//levelfivescreen");
            l6 = content.Load<Texture2D>("MenuContent//levelsixscreen");
            l7 = content.Load<Texture2D>("MenuContent//levelsevenscreen");
        }


        #endregion

        #region Handle Input


        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;

            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                if (C != null)
                    C(this, new PlayerIndexEventArgs(playerIndex));

                ExitScreen();
            }
        }


        #endregion

        #region Draw


        /// <summary>
        /// Draws the message box.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
            
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            // Center the message text in the viewport.
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSize = font.MeasureString(message);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            // The background includes a border somewhat larger than the text itself.
            //const int hPad = 32;
            //const int vPad = 16;

            //Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
            //                                              (int)textPosition.Y - vPad,
            //                                              (int)textSize.X + hPad * 2,
            //                                              (int)textSize.Y + vPad * 2);

            Rectangle backgroundRectangle = new Rectangle(0,
                                                          0,
                                                          viewport.Width,
                                                          viewport.Height);

            // Fade the popup alpha during transitions.
            Color color = Color.White * TransitionAlpha;

            spriteBatch.Begin();

            // Draw the background rectangle.
            //spriteBatch.Draw(gradientTexture, backgroundRectangle, color);

            // Draw the message box text.
            spriteBatch.DrawString(font, message, textPosition, color);

            if (level.Equals("begin3"))
            {
                spriteBatch.Draw(descriptionTexture, backgroundRectangle, color);
            }
            else if (level.Equals("begin2"))
            {
                spriteBatch.Draw(controlsTexture, backgroundRectangle, color);
            }
            else if (level.Equals("begin"))
            {
                spriteBatch.Draw(l1, backgroundRectangle, color);
            }
            else if (level.Equals("1"))
            {
                spriteBatch.Draw(l2, backgroundRectangle, color);
            }
            else if (level.Equals("2"))
            {
                spriteBatch.Draw(l3, backgroundRectangle, color);
            }
            else if (level.Equals("3"))
            {
                spriteBatch.Draw(l4, backgroundRectangle, color);
            }
            else if (level.Equals("4"))
            {
                spriteBatch.Draw(l5, backgroundRectangle, color);
            }
            else if (level.Equals("5"))
            {
                spriteBatch.Draw(l6, backgroundRectangle, color);
            }
            else if (level.Equals("6"))
            {
                spriteBatch.Draw(l7, backgroundRectangle, color);
            }
            else if (level.Equals("7"))
            {
                spriteBatch.Draw(win, backgroundRectangle, color);
            }
            else if (level.Equals("Fail"))
            {
                spriteBatch.Draw(fail, backgroundRectangle, color);
            }
            spriteBatch.End();

        }


        #endregion
    }
}
