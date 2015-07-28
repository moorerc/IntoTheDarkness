using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace EECS494.IntoTheDarkness
{
    /*
     * Stone
     * 
     * Last Modified : December 9, 2012
     * v1.0 : Bryan - First implementation.
     * v1.1 : Bryan - Some minor things.
     * v1.2 : Bryan - Overloaded collision detection.
     * v1.3 : Bryan - Small changes.
     * v1.4 : Bryan - Draw.
     * 
     * Stone class.
     */
    class Stone : Projectile
    {
        private const float STONE_SIZE = 5.0f;

        public Stone(Vector3 position,
                     Vector3 velocity)
        : base(position, velocity)
        {
            base.Bound = new BoundingSphere(position, STONE_SIZE);
            m_wall_sound = SoundMaster.GetSound("stone_wall");
            
            m_pickup_sound = SoundMaster.GetSound("key_pickup");
        }

        // Sound effects
        private SoundEffect m_wall_sound;
        //private SoundEffect m_ground_sound;
        private SoundEffect m_pickup_sound;

        // Dictates interaction between Player and Stone.
        public override void Interact(GameTime gameTime)
        {
            if(!Player.HasItem("Stone"))
                Player.AddItem("Stone");
            else if(Player.QuantityItem("Stone") < 3)
                Player.AdjustQuantity("Stone", 1);

            m_pickup_sound.Play();
        }


        /* Collision detection for Stone. Updates status if has collided. */

        // Logic upon hitting a wall.
        private void HitWall(GameTime gameTime)
        {
            // Calculate normal to wall, if a hit.
            Vector3 wallNormal = new Vector3();
            bool mIsHit = GameplayScreen.GetLevel(GameplayScreen.currentLevel).HitWallProj(base.Bound, ref wallNormal);
 
            /* 
             * Bounce Stone off the wall.
             *
             * R = V - 2 * (V.N) * N,
             * where V  is the vector, 
             *       N  is the surface normal and 
             *      '.' is the dot product.
             *      
             * Hope this helps... 
             */

            if(mIsHit)
            {
                if (Vector3.Equals(mVelocity, Vector3.Zero))
                {
                    // move stone away from wall
                    Vector3 pos = base.Position;
                    pos += wallNormal;
                    base.Position = pos;
                    BoundingSphere b = new BoundingSphere(pos, STONE_SIZE);
                    base.Bound = b;
                }
                else
                {
                    mVelocity -= (2 * (Vector3.Dot(mVelocity, wallNormal)) * wallNormal);

                    // Create contact point. Radius = 200
                    PointManagerRemake.add_Point(new Vector4(base.Position, 0.0f), gameTime.TotalGameTime.Milliseconds, 200.0f);
                    m_wall_sound.Play();
                }
                
            }
        }

        public override bool HasCollided(BoundingSphere bSphere)
        {
            //if(!base.IsMoving)
              //  return false;

            if (base.Bound.Intersects(bSphere))
                return true;

            return false;
        }

        public override bool HasCollided(BoundingBox    bBox)
        {
            //if(!base.IsMoving)
              //  return false;

            if (base.Bound.Intersects(bBox))
                return true;

            return false;
        }

        // Updates Stone.
        public override void Update(GameTime gameTime)
        {
            // Wall collision.
            HitWall(gameTime);

            // Update Projectile.
            base.Update(gameTime);
            
            // Update Bound.
            base.Bound = new BoundingSphere(base.Position, STONE_SIZE);
        }


        static public Model mStoneModel;

        // Draws Stone.
        public override void Draw()
        {
            Matrix[] mTransforms = new Matrix[mStoneModel.Bones.Count];
            mStoneModel.CopyAbsoluteBoneTransformsTo(mTransforms);

            if(!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mStoneModel.Meshes)
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
                foreach (ModelMesh mesh in mStoneModel.Meshes)
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
