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

namespace EECS494.IntoTheDarkness
{

    /*
     * Monster
     * 
     * Last Updated : December 9, 2012
     * v1.0 : Max   - First implementation.
     * v1.1 : Max   - Added rumble feature.
     * v1.2 : Max   - Added sounds.
     * v1.3 : Bryan - Draw.
     * 
     * USAGE :
     * 
     * This is the class for the Monster object. It contains a model and
     * bounding sphere. The main function is Update(). This function moves the 
     * monster and recalculates where the player is in the level. 
     * 
     * The monster can create contact points. It creates one every time
     * it steps
     * 
     * IMPORTANT :
     * 
     * The bounding sphere should become a bounding box. We will decide that
     * once the model is made and we don't know the deminsions of it yet
     * 
     */
    class Monster
    {
        private const float MONSTER_SIZE = 50.0f;

        public Monster(Vector3 position)
        {
            m_position = position;
            m_velocity = Vector3.Zero;
            mOrientation = 0.0f;

            m_bound = new BoundingSphere(m_position, MONSTER_SIZE);

            m_target = Vector3.Zero;
            m_stepping = false;
            mCounter = 0.0f;

            m_step_sound = SoundMaster.GetSound("monster_step");
            m_roar_sound = SoundMaster.GetSound("monster_phrase");
            m_roar_sound_instance = m_roar_sound.CreateInstance();

            mIsStunned = false;
            mTimeStunned = 0.0f;
        }


        #region Monster Attributes
        // Monster position.
        private Vector3 m_position;
        public Vector3 Position
        {
            get
            {
                return m_position;
            }
        }

        // Monster velocity.
        private Vector3 m_velocity;
        public Vector3 Velocity
        {
            get
            {
                return m_velocity;
            }
        }

        // Monster orientation.
        private float mOrientation;
        public float Orientation
        {
            get
            {
                return mOrientation;
            }
        }

        // Monster bound.
        private BoundingSphere m_bound;
        public BoundingSphere Bound
        {
            get
            {
                return m_bound;
            }
        }

        // Monster target.
        private Vector3 m_target; // The point Monster is walking
        // towards. Is distance Monster
        // will travel in one step.

        // Status of Monster step.
        private bool m_stepping;

        // Monster bob counter.
        private float mCounter = 0.0f;

        /* Rumble */

        float m_rumble_dist_max = 2000.0f;

        /* Sound Effects */

        private SoundEffect m_step_sound;
        private SoundEffect m_roar_sound;
        private SoundEffectInstance m_roar_sound_instance;
        float m_sound_dist_max = 5000.0f;

        private bool mIsStunned;
        private float mTimeStunned;
        public bool isStunned
        {
            get
            {
                return isStunned;
            }
            set
            {
                mIsStunned = value;
            }
        }

        #endregion

        // Updates Monster.
        public void Update(Vector3 player_position, GameTime gameTime)
        {
            if (!m_stepping)
            {
                FindPlayer(player_position);
                m_stepping = true;
            }

            Walk(gameTime, player_position);

            // Rumble.
            Rumble(player_position);
        }

        // Produces rumble effect.
        private void Rumble(Vector3 player_pos)
        {
            float dist = (player_pos - m_position).Length();
            if (dist > m_rumble_dist_max)
                return;
            float intensity = dist / m_rumble_dist_max;
            GamePad.SetVibration(PlayerIndex.One, 1 - intensity, 1 - intensity);

        }

        private const float MONSTER_SPEED = 0.3f;
        private float MONSTER_STEP = 250.0f;
        public float MonsterStep
        {
            set
            {
                MONSTER_STEP = value;
            }
        }

        // Finds Player and orients Monster in that direction.
        private void FindPlayer(Vector3 player_position)
        {
            // Create velocity vector towards Player.
            Vector3 vel = player_position - m_position;
            vel.Normalize();
            m_velocity = MONSTER_SPEED * vel;
            m_target = m_position + (MONSTER_STEP * m_velocity);

            // Update orientation to face forward.
            Vector3 mVelNorm = m_velocity;
            if (mVelNorm.Length() > 0)
                mVelNorm.Normalize();

            float angle = (float)Math.Acos(Vector3.Dot(Vector3.Forward, mVelNorm));
            if (mVelNorm.X > 0)
                angle = -angle;

            mOrientation = angle;
        }

        // Moves Monster.
        private void Walk(GameTime gameTime, Vector3 playerPos)
        {
            if (mIsStunned)
            {
                // update stunned timer
                UpdateStunnedTimer(gameTime);
                return;
            }
             

            m_position += m_velocity;
            m_position.Y += (float)Math.Sin(mCounter);
            m_bound.Center = m_position;
            mCounter += 0.1f;

            if (AtTarget())
            {
                m_stepping = false;

                // Create contact point. Radius of 100.0f
                PointManagerRemake.add_Point(new Vector4(m_position, 0.0f), gameTime.TotalGameTime.Milliseconds, 100.0f);

                // Sound effect.
                float dist = (m_position - playerPos).Length();
                m_step_sound.Play(1.0f - dist / m_sound_dist_max, 0.0f, 0.0f); // I think works?
            }

            // Sound effect.
            if ((m_position - playerPos).Length() < (75.0f) && m_roar_sound_instance.State != SoundState.Playing)
                m_roar_sound_instance.Play();
        }

        // Returns true if Monster has reached target, false otherwise.
        private bool AtTarget()
        {
            if (m_position == m_target)
                return true;

            double X = m_position.X - m_target.X;
            double Z = m_position.Z - m_target.Z;

            if (X > -1 && X < 1 && Z > -1 && Z < 1)
                return true;

            return false;
        }


        public static Model mMonsterModel;

        // Draws Monster.
        public void Draw()
        {
            Matrix[] mTransforms = new Matrix[mMonsterModel.Bones.Count];
            mMonsterModel.CopyAbsoluteBoneTransformsTo(mTransforms);

            if (!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mMonsterModel.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.World = mTransforms[mesh.ParentBone.Index] *
                                            Matrix.CreateRotationY(mOrientation) *
                                            Matrix.CreateTranslation(m_position);
                        effect.View = GameplayScreen.mPlayer.PlayerCamera.ViewMatrix;
                        effect.Projection = GameplayScreen.mPlayer.PlayerCamera.ProjectionMatrix;
                    }
                    mesh.Draw();
                }
                #endregion
            }
            else
            {
                #region Shader
                foreach (ModelMesh mesh in mMonsterModel.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = GameplayScreen.mEffect;

                        GameplayScreen.mEffect.Parameters["World"].SetValue(mTransforms[mesh.ParentBone.Index] *
                                                                            Matrix.CreateRotationY(mOrientation) *
                                                                            Matrix.CreateTranslation(m_position));
                        GameplayScreen.mEffect.Parameters["View"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ViewMatrix);
                        GameplayScreen.mEffect.Parameters["Projection"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ProjectionMatrix);
                        GameplayScreen.mEffect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
                        GameplayScreen.mEffect.Parameters["points"].SetValue(PointManagerRemake.ContactPoints);
                        GameplayScreen.mEffect.Parameters["time_passed"].SetValue(GameplayScreen.total_time_passed % 1000);
                        GameplayScreen.mEffect.Parameters["times"].SetValue(PointManagerRemake.Times);
                    }
                    mesh.Draw();
                }
                #endregion
            }
        }

        public void CleanUp()
        {
            // Stop Vibration
            GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
            // Stop Sounds
            m_roar_sound_instance.Stop();
        }

        private void UpdateStunnedTimer(GameTime gameTime)
        {
            mTimeStunned += gameTime.ElapsedGameTime.Milliseconds;
            if (mTimeStunned > 1500)
            {
                mTimeStunned = 0.0f;
                mIsStunned = false;
            }
        }
    }
}