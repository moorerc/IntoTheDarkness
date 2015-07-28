#region File Description
//-----------------------------------------------------------------------------
// InstructionsMenuScreen.cs
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
    class InstructionsMenuScreen : MenuScreen
    {
        Texture2D controlsTexture;

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public InstructionsMenuScreen()
            : base("")
        {

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            back.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(back);
        }

        #endregion

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            controlsTexture = content.Load<Texture2D>("MenuContent//controls_screen");
        }

        #region Handle Input

        #endregion

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

            Rectangle backgroundRectangle = new Rectangle(0,
                                                          0,
                                                          viewport.Width,
                                                          viewport.Height);

            // Fade the popup alpha during transitions.
            Color color = Color.White * TransitionAlpha;

            spriteBatch.Begin();

            spriteBatch.Draw(controlsTexture, backgroundRectangle, color);

            spriteBatch.End();

        }

    }
}