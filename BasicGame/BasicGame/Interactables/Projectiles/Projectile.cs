using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace EECS494.IntoTheDarkness
{
    /*
     * Projectile
     * 
     * Last Modified : December 9, 2012
     * v1.0 : Bryan - First implementation.
     * v1.1 : Bryan - Small changes.
     * v1.2 : Bryan - Draw.
     * 
     * Abstract base class for all projectiles. Handles basic kinematics.
     */
    abstract class Projectile : Interactable
    {
        public Projectile(Vector3 position,
                          Vector3 velocity)
        : base(position)
        {
            mVelocity = velocity;

            mIsMoving = true;

            mSound = SoundMaster.GetSound("stone_wall");
            
        }


        // Projectile velocity.
        protected Vector3 mVelocity;
        public Vector3 Velocity
        {
            get
            {
                return mVelocity;
            }
        }

        // Status of Projectile.
        private bool mIsMoving;
        public bool IsMoving
        {
            get
            {
                return mIsMoving;
            }
        }

        // Projectile sound effect.
        private SoundEffect mSound;


        private const float GRAVITY = -0.15f;

        // Updates Projectile.
        public override void Update(GameTime gameTime)
        {
            if (mIsMoving)
            {
                // Account for gravity.
                mVelocity.Y += GRAVITY;

                base.Position += mVelocity;

                if (base.Position.Y < 0.0f)
                {
                    mIsMoving = false;
                    mVelocity = Vector3.Zero;
                    base.Position = new Vector3(base.Position.X, 0.0f, base.Position.Z);
                    
                    // Create contact point. Radius = 200.0f
                    PointManagerRemake.add_Point(new Vector4(base.Position, 0.0f), gameTime.TotalGameTime.Milliseconds, 200.0f);

                    // Sound effect
                    mSound.Play();
                }
            }    
        }
    }
}
