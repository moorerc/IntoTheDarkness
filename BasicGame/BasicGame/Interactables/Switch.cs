using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace EECS494.IntoTheDarkness
{
    /*
     * Switch
     * 
     * Last Modified : December 10, 2012
     * v1.0 : Max   - First implementation.
     * v1.1 : Bryan - Draw.
     * 
     * Switch class.
     * 
     * Controls associated Machine.
     */
    class Switch : Interactable
    {
        private const float SWITCH_SIZE = 50.0f;

        public Switch(Vector3 position, Machine machine)
        : base(position)
        {
            base.Bound = new BoundingSphere(position, SWITCH_SIZE);

            mMachine  = machine;
            switchOnOff = false;
            mSound     = SoundMaster.GetSound("switch_click");
        }


        // Machine associated with Switch.
        private Machine mMachine;
        public Machine getMachine()
        {
            return mMachine;
        }

        // Switch sound effect.
        private SoundEffect mSound;

        private bool switchOnOff;   // this toggles just like the machine, but is on or off
                                    // independent of the machine
        public bool isOn
        {
            get
            {
                return switchOnOff;
            }
        }
        // Dictates interaction between Player and Switch.
        public override void Interact(GameTime gameTime)
        {
            mMachine.Toggle(gameTime);
            switchOnOff = !switchOnOff;
            // Sound effect.
            mSound.Play();
        }


        static public Model mSwitchOnModel;
        static public Model mSwitchOffModel;

        // Draws Switch.
        public override void Draw()
        {
            Model mCurrentModel = switchOnOff ? mSwitchOnModel : mSwitchOffModel;

            Matrix[] mTransforms = new Matrix[mCurrentModel.Bones.Count];
            mCurrentModel.CopyAbsoluteBoneTransformsTo(mTransforms);

            if(!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mCurrentModel.Meshes)
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
                foreach (ModelMesh mesh in mCurrentModel.Meshes)
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
            // Do nothing. There is nothing to clean up
        }
    }
}
