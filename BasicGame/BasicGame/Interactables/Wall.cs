using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EECS494.IntoTheDarkness
{
    /*
     * Wall
     * 
     * Last modified : December 10, 2012
     * v1.0 : Bryan - First implementation.
     */
    class Wall
    {
        public Wall(Vector3 position,
                    float   orientation)
        {
            mPosition    = position;
            mOrientation = orientation;
        }


        // Wall position.
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

        // Wall orientation.
        private float mOrientation;
        public float Orientation
        {
            get
            {
                return mOrientation;
            }
            set
            {
                mOrientation = value;
            }
        }


        public static Model mWallModel;

        // Draws Wall.
        public void Draw()
        {
            Matrix[] mTransforms = new Matrix[mWallModel.Bones.Count];
            mWallModel.CopyAbsoluteBoneTransformsTo(mTransforms);

            if(!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mWallModel.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.World      = mTransforms[mesh.ParentBone.Index] *
                                            Matrix.CreateRotationY(mOrientation) *
                                            Matrix.CreateTranslation(mPosition);
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
                foreach (ModelMesh mesh in mWallModel.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = GameplayScreen.mEffect;

                        GameplayScreen.mEffect.Parameters["World"].SetValue(mTransforms[mesh.ParentBone.Index] *
                                                                            Matrix.CreateRotationY(mOrientation) *
                                                                            Matrix.CreateTranslation(mPosition));
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
    }
}
