using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;

namespace EECS494.IntoTheDarkness
{
    partial class Player
    {
        public Player(Vector3 position,
                      Vector3 velocity)
        {
            mPosition = position;
            mVelocity = velocity;

            mBound = new BoundingBox(mPosition + new Vector3(-5.0f, -100.0f, -5.0f), mPosition + new Vector3(5.0f, 35.0f, 5.0f));

            mYaw   = 0.0f;
            mPitch = 0.0f;

            mPlayerCamera = new Camera(mPosition, Quaternion.CreateFromAxisAngle(Vector3.Up, mYaw));

            // Start Player with 3 Stones.
            mBackpack.Clear();
            Player.AddItem("Stone");
            Player.AdjustQuantity("Stone", 2);

            m_rumble_start  = 150.0f;
            m_rumble_finish = 150.0f;
            m_prev_time     = 0.0f;
            m_time          = 0.0f;

            mFinishJump = SoundMaster.GetSound("jump_thud");
            mThrowStone = SoundMaster.GetSound("throw_sound");
        }

        #region Player Attributes
        // Player position.
        private Vector3 mPosition;
        public Vector3 Position
        {
            get
            {
                return mPosition;
            }
            set
            {
                mPosition = value;
            }
        }

        // Player Velocity
        private Vector3 mVelocity;
        public Vector3 Velocity
        {
            get
            {
                return mVelocity;
            }
            set
            {
                mVelocity = value;
            }
        }

        // Player bound.
        private BoundingBox mBound;
        public BoundingBox Bound
        {
            get
            {
                return mBound;
            }
        }

        // Player Yaw
        private float mYaw;
        public float Yaw
        {
            get
            {
                return mYaw;
            }
            set
            {
                mYaw = value;
            }
        }

        // Player Pitch
        private float mPitch;
        public float Pitch
        {
            get
            {
                return mPitch;
            }
            set
            {
                mPitch = value;
            }
        }

        // Player Camera
        private Camera mPlayerCamera;
        public Camera PlayerCamera
        {
            get
            {
                return mPlayerCamera;
            }
        }

        // Rumble

        private float m_rumble_start;
        private float m_rumble_finish;
        private float m_prev_time;
        private float m_time;

        /* Sound effects */

        private SoundEffect mFinishJump;
        private SoundEffect mThrowStone;
        #endregion

        #region Player Actions
        #region Move
        private const float MOVEMENT_SPEED_FACTOR = (0.25f);

        // Move Player according to Controller input.
        public void Move(float joystick_XValue, float joystick_YValue)
        {
            // Project Player velocity onto xz-plane.
            Vector3 mVelocityAdd = new Vector3(mPlayerCamera.GetForward().X, 0.0f, mPlayerCamera.GetForward().Z);
            mVelocityAdd.Normalize();

            // Forward / Backward.
            if (joystick_YValue != 0)
            {
                mVelocityAdd.X = - (float) Math.Sin(mYaw) / 3.0f;
                mVelocityAdd.Z = - (float) Math.Cos(mYaw) / 3.0f;
                mVelocityAdd *= joystick_YValue * MOVEMENT_SPEED_FACTOR;
                mVelocity += mVelocityAdd;
            }

            // Left / Right.
            if (joystick_XValue != 0)
            {
                mVelocityAdd.X = (float) Math.Sin(mYaw + MathHelper.PiOver2) / 4.0f;
                mVelocityAdd.Z = (float) Math.Cos(mYaw + MathHelper.PiOver2) / 4.0f;
                mVelocityAdd *= joystick_XValue * MOVEMENT_SPEED_FACTOR;
                mVelocity += mVelocityAdd;
            }
        }
        #endregion

        #region Rotate
        private const float ROTATION_SPEED_FACTOR = (0.025f);

        // Rotates Player left or right according to Controller input.
        public void AdjustYaw(float joyValue)
        {
            mYaw -= joyValue * ROTATION_SPEED_FACTOR;
            mPlayerCamera.AdjustYaw(- joyValue * ROTATION_SPEED_FACTOR);
        }

        // Rotates Player up or down according to Controller input.
        public void AdjustPitch(float joyValue)
        {
            // Ensure within bounds.
            if (((mPitch - joyValue * ROTATION_SPEED_FACTOR) < MathHelper.PiOver4) && ((mPitch - joyValue * ROTATION_SPEED_FACTOR) > - MathHelper.PiOver4))
            {
                mPitch -= joyValue * ROTATION_SPEED_FACTOR;
                mPlayerCamera.AdjustPitch(- joyValue * ROTATION_SPEED_FACTOR);
            }
        }
        #endregion

        #region Throw
        const float THROW_POWER = 5.0f;

        // Stupid variable to make sure we can give Stones unique names.
        private static int numStones = 0;

        // Throw Stone.
        public void Throw(GameTime gameTime)
        {
            if(Player.HasItem("Stone"))
            {
                // Throw at a 45 degree angle upwards from Player.
                Vector3 tVel = THROW_POWER * Vector3.Transform(mPlayerCamera.GetUp(), Quaternion.CreateFromAxisAngle(mPlayerCamera.GetLeft(), MathHelper.PiOver4));
            
                GameplayScreen.GetLevel(GameplayScreen.currentLevel).LevelInteractables.Add("Stone" + Player.numStones, new Stone(mPosition, tVel));
                Player.numStones++;

                Player.AdjustQuantity("Stone", -1);

                // Sound effect.
                mThrowStone.Play();
            }
            //else
                // No Stones to throw!
        }
        #endregion

        #region Jump
        private const float JUMP_POWER = 8.0f;

        // Jump.
        public void Jump()
        {
            if (OnGround())
                mVelocity.Y = JUMP_POWER;
        }
        #endregion
        #endregion

        private const float GRAVITY     = -0.3f;
        private const float HEAD_HEIGHT = 35.0f;

        // Returns true if Player is on the ground, false otherwise.
        private bool OnGround()
        {
            return mPosition.Y == HEAD_HEIGHT;
        }

        // Updates Player.
        public void Update(GameTime gameTime)
        {
            // Account for gravity.
            if (!OnGround())
                mVelocity.Y += GRAVITY;

            mPosition += mVelocity;
            mPlayerCamera.Move(mVelocity);

            // Recover from jump.
            if (mPosition.Y < HEAD_HEIGHT)
            {
                mPlayerCamera.Move(new Vector3(0.0f, HEAD_HEIGHT - mPosition.Y, 0.0f));
                mPosition.Y = HEAD_HEIGHT;
                mVelocity.Y = 0.0f;

                // Create contact point. Radius of 300.0f
                PointManagerRemake.add_Point(new Vector4(mPosition, 0.0f), gameTime.TotalGameTime.Milliseconds, 300.0f);

                // Rumble.
                m_rumble_start = 0.0f;

                // Sound effect.
                mFinishJump.Play();
            }

            mBound = new BoundingBox(mPosition + new Vector3(-5.0f, -100.0f, -5.0f), mPosition + new Vector3(5.0f, 35.0f, 5.0f));

            // Slow Player down.
            mVelocity *= (0.9f);

            // Update View Matrix.
            mPlayerCamera.Update();

            #region Rumble
            m_prev_time = m_time;
            float t = gameTime.ElapsedGameTime.Milliseconds;

            if ((t - m_prev_time) > 0.0f)
                m_time += (t - m_prev_time);
            else
                m_time += t;

            if (m_rumble_start < m_rumble_finish)
            {
                GamePad.SetVibration(PlayerIndex.One, 0, 1);
                m_rumble_start += (m_time - m_prev_time);
            }
            else
                GamePad.SetVibration(PlayerIndex.One, 0, 0);
            #endregion
        }

        public void CleanUp()
        {
            // Stop Vibration
            GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
            // Stop Sounds
            // No sounds to stop
        }
    }
}