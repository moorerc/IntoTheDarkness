#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace EECS494.IntoTheDarkness
{
    partial class GameplayScreen : GameScreen
    {
        #region GameplayScreen Attributes
        private ContentManager content;

        public static bool mEffectOn = true;
        public bool finishGame = false;
        public bool finishLevel = false;
        public bool beginGame = true;

        /* Fonts */

        private SpriteFont gameFont;
        private SpriteFont m_font;
        private SpriteFont m_text_font;
        private SpriteFont iFont;

        /* Effects */

        public static Effect mEffect;

        /* Textures */

        Texture2D backpackTexture;


        Random random = new Random();
        float pauseAlpha;
        bool drawBackpack = false;

        // Player
        public static Player mPlayer;

        // Point Manager
        private float time_passed;
        public static float total_time_passed;

        // Fonts.
        private SpriteBatch m_sprite_batch;

        // Level
        public static int currentLevel;

        // Sounds
        private SoundEffect m_player_wall;

        /* Input Devices */
        
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        GamePadState  gamepadState;
        GamePadState  oldGamepadState;
        #endregion

        #region Initialization
        public GameplayScreen()
        {
            TransitionOnTime  = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            time_passed       = 0.0f;
            total_time_passed = 0.0f;

            currentLevel = 1;

            InitializeLevels();

            // sounds
            m_player_wall = SoundMaster.GetSound("jump_thud");

            // Initialize Player.
            mPlayer     = new Player(GetLevel(currentLevel).LevelStart, Vector3.Zero);
            mPlayer.Yaw = GetLevel(currentLevel).LevelStartDirection;
            mPlayer.PlayerCamera.Orientation = Quaternion.CreateFromAxisAngle(Vector3.Up, GetLevel(currentLevel).LevelStartDirection);

            // Initialize input device states.
            keyboardState    = Keyboard.GetState();
            gamepadState     = GamePad.GetState(PlayerIndex.One);
            oldKeyboardState = keyboardState;
            oldGamepadState  = gamepadState;
        }

        // Loads game content.
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            m_sprite_batch = new SpriteBatch(ScreenManager.GraphicsDevice);

            #region Load Font Assets
            m_font   = content.Load<SpriteFont>("Fonts//GameFont");
            m_text_font = content.Load<SpriteFont>("Fonts//SpriteFont1");
            gameFont = content.Load<SpriteFont>("Fonts//gamefont2");
            iFont    = content.Load<SpriteFont>("Fonts//InventoryFont");
            #endregion

            #region Load Effect Assets
            mEffect = content.Load<Effect>("Effects//VibrationShader");
            #endregion

            #region Load Texture Assets
            backpackTexture = content.Load<Texture2D>("MenuContent//inventory");
            #endregion

            #region Load Model Assets
            LoadLevelContent();

            Lock.mLockModel        = content.Load<Model>("Models//Lock");
            Key.mKeyModel          = content.Load<Model>("Models//Key");
            Switch.mSwitchOnModel  = content.Load<Model>("Models//Switch_On");
            Switch.mSwitchOffModel = content.Load<Model>("Models//Switch_Off");
            Machine.mMachineModel  = content.Load<Model>("Models//Machine");
            Stone.mStoneModel      = content.Load<Model>("Models//Stone");
            Wall.mWallModel        = content.Load<Model>("Models//Wall");
            Monster.mMonsterModel  = content.Load<Model>("Models//Monster");
            #endregion

            Thread.Sleep(1000);
            ScreenManager.Game.ResetElapsedTime();
        }

        // Unloads game content.
        public override void UnloadContent()
        {
            content.Unload();
        }
        #endregion

        #region Update and Draw
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                // Get new input states.
                keyboardState = Keyboard.GetState();
                gamepadState  = GamePad.GetState(PlayerIndex.One);

                #region Controls
                #region Keyboard
                /* Space */

                // Jump.
                if (keyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space))
                    mPlayer.Jump();

                /* WASD */

                // Move forward.
                if (keyboardState.IsKeyDown(Keys.W))
                    mPlayer.Move(0.0f, 1.0f);

                // Move backward.
                if (keyboardState.IsKeyDown(Keys.S))
                    mPlayer.Move(0.0f, -1.0f);

                // Move left.
                if (keyboardState.IsKeyDown(Keys.A))
                    mPlayer.Move(-1.0f, 0.0f);

                // Move right.
                if (keyboardState.IsKeyDown(Keys.D))
                    mPlayer.Move(1.0f, 0.0f);

                /* Arrow Keys */

                // Look up.
                if (keyboardState.IsKeyDown(Keys.Up))
                    mPlayer.AdjustPitch(1.0f);

                // Look down.
                if (keyboardState.IsKeyDown(Keys.Down))
                    mPlayer.AdjustPitch(-1.0f);

                // Look left.
                if (keyboardState.IsKeyDown(Keys.Left))
                    mPlayer.AdjustYaw(-1.0f);

                // Look right.
                if (keyboardState.IsKeyDown(Keys.Right))
                    mPlayer.AdjustYaw(1.0f);

                /* I */

                // Interact
                if(keyboardState.IsKeyDown(Keys.I) && oldKeyboardState.IsKeyUp(Keys.I))
                    GetLevel(currentLevel).Interact(mPlayer.Bound, gameTime);

                /* T */

                // Throw
                if(keyboardState.IsKeyDown(Keys.T) && oldKeyboardState.IsKeyUp(Keys.T))
                    mPlayer.Throw(gameTime);

                // Display Backpack
                if ((keyboardState.IsKeyDown(Keys.B) && oldKeyboardState.IsKeyUp(Keys.B)))
                    drawBackpack = true;
                else
                    drawBackpack = false;
                #endregion

                #region Controller
                if (gamepadState.IsConnected)
                {
                    /* Right Shoulder or A Button */

                    // Jump.
                    if (((gamepadState.Buttons.RightShoulder == ButtonState.Pressed) && (oldGamepadState.Buttons.RightShoulder == ButtonState.Released))
                        || ((gamepadState.Buttons.A == ButtonState.Pressed) && (oldGamepadState.Buttons.A == ButtonState.Released)))
                        mPlayer.Jump();

                    /* Y Button */

                    // Display Backpack
                    if ((gamepadState.Buttons.Y == ButtonState.Pressed))
                        drawBackpack = true;
                    else
                        drawBackpack = false;

                    /* B Button */

                    // Throw Projectile.
                    if ((gamepadState.Buttons.B == ButtonState.Pressed) && (oldGamepadState.Buttons.B == ButtonState.Released))
                        mPlayer.Throw(gameTime);

                    /* X Button */

                    // Interact.
                    if ((gamepadState.Buttons.X == ButtonState.Pressed) && (oldGamepadState.Buttons.X == ButtonState.Released))
                        GetLevel(currentLevel).Interact(mPlayer.Bound, gameTime);

                    /* Left Shoulder Button */

                    // Complete current Level.
                    if ((gamepadState.Buttons.LeftShoulder == ButtonState.Pressed) && (oldGamepadState.Buttons.LeftShoulder == ButtonState.Released))
                        GetLevel(currentLevel).IsCompleted = true;

                    /* Right Joystick */

                    // Rotate.
                    mPlayer.AdjustYaw(gamepadState.ThumbSticks.Right.X);
                    mPlayer.AdjustPitch(gamepadState.ThumbSticks.Right.Y);

                    /* Left Joystick */

                    // Move.
                    mPlayer.Move(gamepadState.ThumbSticks.Left.X, gamepadState.ThumbSticks.Left.Y);
                }
                #endregion
                #endregion

                #region Collision Detection
                // Update Player.
                mPlayer.Update(gameTime);

                /* Collision detection */

                BoundingBox BoundHit = new BoundingBox();
                if (GetLevel(currentLevel).HitWall(mPlayer.Bound, ref BoundHit))
                {
                    // Create contact point. Radius = 100 for player wall collisions
                    PointManagerRemake.add_Point(new Vector4(mPlayer.Position, 0.0f), gameTime.TotalGameTime.Milliseconds, 100.0f);

                    /* Compute collision normal. */

                    Vector3 vNorm = new Vector3();

                    if (mPlayer.Position.X <= BoundHit.Min.X)
                        vNorm = Vector3.Left;
                    if (mPlayer.Position.X > BoundHit.Max.X)
                        vNorm = Vector3.Right;
                    if (mPlayer.Position.Z <= BoundHit.Min.Z)
                        vNorm = Vector3.Forward;
                    if (mPlayer.Position.Z > BoundHit.Max.Z)
                        vNorm = Vector3.Backward;

                    // Get vector projection.
                    Vector3 vProj = Vector3.Dot(mPlayer.Velocity, vNorm) * vNorm;
                    vProj.Normalize();

                    // Store Player position.
                    Vector3 oldPos = mPlayer.Position;

                    // Revert Player position.
                    Vector3 newPos = mPlayer.Position + (2.0f) * vNorm;
                    newPos.Y = mPlayer.Position.Y;
                    mPlayer.Position = newPos;

                    // Update Player velocity.
                    mPlayer.Velocity = (-1.0f) * vProj * (new Vector3(mPlayer.Velocity.X, 0.0f, mPlayer.Velocity.Z).Length());

                    // Update Camera position.
                    mPlayer.PlayerCamera.Move(newPos - oldPos);

                    // play sound effect for player
                    m_player_wall.Play();
                }
                #endregion

                if (beginGame)
                {
                    ScreenManager.AddScreen(new FinishLevelScreen("begin", true), ControllingPlayer);
                    ScreenManager.AddScreen(new FinishLevelScreen("begin2", true), ControllingPlayer);
                    ScreenManager.AddScreen(new FinishLevelScreen("begin3", true), ControllingPlayer);
                    beginGame = false;
                }

                #region Level Complete
                // Level Completed.
                if (GetLevel(currentLevel).IsCompleted)
                {
                    ScreenManager.AddScreen(new FinishLevelScreen(currentLevel.ToString(), true), ControllingPlayer);
                    CleanUp();
                    currentLevel++;
                    PointManagerRemake.ClearAll();

                    if (finishGame)
                    {
                        this.ExitScreen();
                        ScreenManager.AddScreen(new BackgroundScreen(), null);
                        ScreenManager.AddScreen(new MainMenuScreen(), null);
                    }

                    if (currentLevel > NUMLEVELS)
                    {
                        currentLevel = NUMLEVELS;
                        finishGame = true;
                    }

                    mPlayer                          = new Player(Vector3.Zero, Vector3.Zero);
                    mPlayer.Position                 = GetLevel(currentLevel).LevelStart;
                    mPlayer.PlayerCamera.Position    = mPlayer.Position;
                    mPlayer.Yaw                      = GetLevel(currentLevel).LevelStartDirection;
                    mPlayer.PlayerCamera.Orientation = Quaternion.CreateFromAxisAngle(Vector3.Up, GetLevel(currentLevel).LevelStartDirection);

                    mPlayer.PlayerCamera.Update();
                }
                #endregion

                #region Game Over
                // Game Over.
                if (GetLevel(currentLevel).Update(mPlayer.Bound, gameTime))
                {
                    Exception ex = new Exception("add level to switch statement");

                    CleanUp();

                    ScreenManager.AddScreen(new FinishLevelScreen("Fail", true), ControllingPlayer);
                    switch (currentLevel)
                    {
                        case 1:
                            InitializeLevel_01();
                            break;
                        case 2:
                            InitializeLevel_02();
                            break;
                        case 3:
                            InitializeLevel_03();
                            break;
                        case 4:
                            InitializeLevel_04();
                            break;
                        case 5:
                            InitializeLevel_05();
                            break;
                        case 6:
                            InitializeLevel_06();
                            break;
                        case 7:
                            InitializeLevel_07();
                            break;
                        default:
                            throw ex;
                    }

                    mPlayer                          = new Player(Vector3.Zero, Vector3.Zero);
                    mPlayer.Position                 = GetLevel(currentLevel).LevelStart;
                    mPlayer.PlayerCamera.Position    = mPlayer.Position;
                    mPlayer.Yaw                      = GetLevel(currentLevel).LevelStartDirection;
                    mPlayer.PlayerCamera.Orientation = Quaternion.CreateFromAxisAngle(Vector3.Up, GetLevel(currentLevel).LevelStartDirection);

                    mPlayer.PlayerCamera.Update();
                }
                    //LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
                #endregion

                #region Point Manager
                // Update Point Manager.
                float temp = gameTime.TotalGameTime.Milliseconds;
                if ((temp - time_passed) > 0)
                {
                    total_time_passed += (temp - time_passed);
                    PointManagerRemake.updateTime(temp - time_passed);
                }
                else
                {
                    total_time_passed += temp;
                    PointManagerRemake.updateTime(temp);
                }
                #endregion

                // Set old input states.
                oldKeyboardState = keyboardState;
                oldGamepadState  = gamepadState;
            }
        }

        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState  gamePadState   = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }

            // Reset setting for 3D.
            ScreenManager.GraphicsDevice.BlendState = BlendState.Opaque;
            ScreenManager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Set Clear color.
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            // Draw current Level.
            GetLevel(currentLevel).Draw();

            // Draw text if necessary
            Rectangle backgroundRectangle = new Rectangle(0,
                                                          0,
                                                          ScreenManager.GraphicsDevice.Viewport.Width,
                                                          ScreenManager.GraphicsDevice.Viewport.Height);

            m_sprite_batch.Begin();
            #region Text
            DrawText();
            #endregion

            #region Backpack
            if (drawBackpack)
            {
                m_sprite_batch.Draw(backpackTexture, backgroundRectangle, Color.White * TransitionAlpha);
                m_sprite_batch.DrawString(iFont, Player.QuantityItem("Stone").ToString(), new Vector2(ScreenManager.GraphicsDevice.Viewport.Width/4.0f, ScreenManager.GraphicsDevice.Viewport.Height / 2.0f), Color.White);
                m_sprite_batch.DrawString(iFont, Player.QuantityItem("Key").ToString(), new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 3.0f / 4.0f, ScreenManager.GraphicsDevice.Viewport.Height / 2.0f), Color.White);

            }
            #endregion
            m_sprite_batch.End();

            base.Draw(gameTime);
        }

        // Displays current level.
        private void DrawText()
        {
            //m_sprite_batch.DrawString(m_font, "Current Level : " + currentLevel, new Vector2(0.0f, 0.0f), Color.White);
            if (TextMaster.DisplayString != string.Empty)
            {
                // measure the size of the string
                Vector2 size = m_text_font.MeasureString(TextMaster.DisplayString);

                float height = ScreenManager.GraphicsDevice.Viewport.Height;
                float width = ScreenManager.GraphicsDevice.Viewport.Width;

                height = height * 0.75f;
                
                // draw width of string based on the size of the string
                // find middle of the screen, then subtract half the size of the string
                width = (width / 2.0f) - (size.X / 2.0f);

                m_sprite_batch.DrawString(m_text_font, TextMaster.DisplayString, new Vector2(width, height), Color.White);
            }
            else
            {
                m_sprite_batch.DrawString(m_font, "", new Vector2(0.0f, 100.0f), Color.White);
            }
        }
        #endregion

        #region Clean Up

        public void CleanUp()
        {
            // clean up Player
            mPlayer.CleanUp();
            // clean up Monster and Interactables
            GetLevel(currentLevel).CleanUp();
            // clean up Interactables
        }

        #endregion
        
    }
}
