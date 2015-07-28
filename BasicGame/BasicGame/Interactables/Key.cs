using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace EECS494.IntoTheDarkness
{
    /*
     * Key
     * 
     * Last Modified : December 9, 2012
     * v1.0 : Bryan - First implementation.
     * v1.1 : Bryan - Integrating into inheritance hierarchy.
     * v1.2 : Bryan - Overloaded collision detection.
     * v1.3 : Bryan - Small changes.
     * v1.4 : Bryan - Draw.
     * 
     * Key class.
     */
    class Key : Interactable
    {
        private const float KEY_SIZE = (15.0f);

        public Key(Vector3 position)
        : base(position)
        {
            base.Bound = new BoundingSphere(position, KEY_SIZE);

            mIsFound = false;
            m_sound = SoundMaster.GetSound("key_pickup");
        }


        // Status of Key.
        private bool mIsFound;
        public bool IsFound
        {
            get
            {
                return mIsFound;
            }
            set
            {
                mIsFound = value;
            }
        }

        // Sound effect
        private SoundEffect m_sound;

        // Dictates interaction between Player and Key.
        public override void Interact(GameTime gameTime)
        {
            if(!Player.HasItem("Key"))
                Player.AddItem("Key");
            else
                Player.AdjustQuantity("Key", 1);

            m_sound.Play();

        }


        static public Model mKeyModel;

        // Draws Key.
        public override void Draw()
        {
            Matrix[] mTransforms = new Matrix[mKeyModel.Bones.Count];
            mKeyModel.CopyAbsoluteBoneTransformsTo(mTransforms);

            if(!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mKeyModel.Meshes)
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
                foreach (ModelMesh mesh in mKeyModel.Meshes)
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
