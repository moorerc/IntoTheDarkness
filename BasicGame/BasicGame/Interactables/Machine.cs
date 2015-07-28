using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace EECS494.IntoTheDarkness
{
    /*
     * Machine
     * 
     * Last modified : December 10, 2012
     * v1.0 : Max   - First implementation.
     * v1.1 : Bryan - Draw.
     * 
     * Machine class.
     * 
     * Creates pulsiing vibrations when associated Switch is flipped.
     */
    class Machine : Interactable
    {
        private const float MACHINE_SIZE = 50.0f;

        public Machine(Vector3 position, bool playSound)
        : base(position)
        {
            base.Bound = new BoundingSphere(position, MACHINE_SIZE);
        
            mSound                  = SoundMaster.GetSound("machine_hum");
            mSoundInstance          = mSound.CreateInstance();
            mSoundInstance.IsLooped = true;
            mPlaySound              = playSound;
        }


        // Status of Machine.
        private bool mIsOn;
        public bool IsOn
        {
            get
            {
                return mIsOn;
            }
            set
            {
                mIsOn = value;
            }
        }

        // Machine sound effect.

        private SoundEffect mSound;
        private SoundEffectInstance mSoundInstance;
        bool mPlaySound;

        // Toggles Machine on and off.
        public virtual void Toggle(GameTime gameTime)
        {
            mIsOn = !mIsOn;

            Vector4 mContact = new Vector4(base.Position, 0.0f);
            if (mIsOn)
            {
                // radius = 200 for machines
                PointManagerRemake.add_Pulse_Point(mContact, gameTime.TotalGameTime.Milliseconds, 200.0f);

                // Sound effect.
                if(mPlaySound)
                {
                    if (mSoundInstance.State == SoundState.Paused || mSoundInstance.State == SoundState.Stopped)
                        mSoundInstance.Play();
                }
            }
            else
            {
                PointManagerRemake.remove_Pulse_Point(mContact);

                // Sound effect.
                if(mPlaySound)
                {
                    if (mSoundInstance.State == SoundState.Playing)
                        mSoundInstance.Pause();
                }
            }
        }


        static public Model mMachineModel;

        // Draws Machine.
        public override void Draw()
        {
            Matrix[] mTransforms = new Matrix[mMachineModel.Bones.Count];
            mMachineModel.CopyAbsoluteBoneTransformsTo(mTransforms);

            if(!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mMachineModel.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.World      = mTransforms[mesh.ParentBone.Index] *
                                            Matrix.CreateRotationY(0.0f) *
                                            Matrix.CreateTranslation(base.Position);
                        effect.View       = GameplayScreen.mPlayer.PlayerCamera.ViewMatrix;
                        effect.Projection = GameplayScreen.mPlayer.PlayerCamera.ProjectionMatrix;
                    }
                    mesh.Draw();
                }
                #endregion
            }
            else
            {
                #region Shader
                foreach (ModelMesh mesh in mMachineModel.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = GameplayScreen.mEffect;

                        GameplayScreen.mEffect.Parameters["World"].SetValue(mTransforms[mesh.ParentBone.Index] *
                                                                            Matrix.CreateRotationY(0.0f) *
                                                                            Matrix.CreateTranslation(base.Position));
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


        public override void CleanUp()
        {
            // stop the machine sound
            mSoundInstance.Stop();
        }
    }

    class Fake_Machine : Machine
    {
        public Fake_Machine(Vector3 position, bool playSound)
        : base(position, playSound)
        {
            // Does nothing different    
        }

        public override void Toggle(GameTime gameTime)
        {
            // This machine is ALWAYS off!
            base.IsOn = false;
        }
    }
}
