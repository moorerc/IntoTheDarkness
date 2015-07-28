using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace EECS494.IntoTheDarkness
{
    /*
     * Lock
     * 
     * Last Modified : Decembr 9, 2012
     * v1.0 : Bryan - First implementation.
     * v1.1 : Bryan - Integrating into inheritance hierarchy.
     * v1.2 : Bryan - Overloaded collision detection.
     * v1.3 : Bryan - Small changes.
     * v1.4 : Bryan - Draw.
     * 
     * Lock class.
     */
    class Lock : Interactable
    {
        private const float LOCK_SIZE = (50.0f);

        public Lock(Vector3 position, float orientation)
        : base(position)
        {
            base.Bound = new BoundingSphere(position, LOCK_SIZE);

            mIsLocked = true;

            mOrientation = orientation;
            m_sound = SoundMaster.GetSound("door_unlock");
        }


        // Status of Lock.
        private bool mIsLocked;
        public bool IsLocked
        {
            get
            {
                return mIsLocked;
            }
        }

        // Orientation of Lock.
        private float mOrientation;
        public float Orientation
        {
            get
            {
                return mOrientation;
            }
        }

        // Sound effect for lock
        private SoundEffect m_sound;

        // Dictates interaction between Player and Lock.
        public override void Interact(GameTime gameTime)
        {
            if (mIsLocked && Player.HasItem("Key"))
            {
                mIsLocked = false;

                m_sound.Play();
                Player.AdjustQuantity("Key", -1);
            }
        }


        static public Model mLockModel;

        // Draws Lock.
        public override void Draw()
        {
            Matrix[] mTransforms = new Matrix[mLockModel.Bones.Count];
            mLockModel.CopyAbsoluteBoneTransformsTo(mTransforms);

            if(!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mLockModel.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.World      = mTransforms[mesh.ParentBone.Index] *
                                            Matrix.CreateRotationY(mOrientation) *
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
                foreach (ModelMesh mesh in mLockModel.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = GameplayScreen.mEffect;

                        GameplayScreen.mEffect.Parameters["World"].SetValue(mTransforms[mesh.ParentBone.Index] *
                                                                            Matrix.CreateRotationY(mOrientation) *
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
