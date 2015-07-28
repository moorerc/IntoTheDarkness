using Microsoft.Xna.Framework;

namespace EECS494.IntoTheDarkness
{
    /* 
     * Camera
     * 
     * Last Updated : December 2, 2012
     * v1.0 : Bryan - First implementation.
     * v1.1 : Bryan - AdjustYaw, AdjustPitch, AdjustRoll added.
     * v1.2 : Bryan - Small adjustments.
     * 
     * Provides access to Camera axes and allows for movement, rotation (via
     * Quaternions) of the Camera.
     * 
     * IMPORTANT:
     * 
     * Call Update within Game::Update to ensure ViewMatrix is up to date!
    */
    class Camera
    {
        public Camera(Vector3    position,
                      Quaternion orientation)
        {
            mPosition    = position;
            mOrientation = orientation;

            // Compute projection matrix.
            mProjectionMatrix = Matrix.CreatePerspectiveFieldOfView((MathHelper.Pi / 3.0f), // FOV Angle
                                                                    (4.0f / 3.0f),          // Aspect Ratio
                                                                    (1.0f),                 // Near Clipping Plane
                                                                    (10000000.0f));         // Far Clipping Plane
        }


        // Camera position.
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

        // Camera orientation.
        private Quaternion mOrientation;
        public Quaternion Orientation
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

        // View Matrix.
        private Matrix mViewMatrix;
        public Matrix ViewMatrix
        {
            get
            {
                return mViewMatrix;
            }
        }

        // Projection Matrix.
        private Matrix mProjectionMatrix;
        public Matrix ProjectionMatrix
        {
            get
            {
                return mProjectionMatrix;
            }
            // BRYAN : If we want to adjust those immutable parameters, we can
            //         add a public method here for users.
        }


        // Returns normal in direction Camera is facing.
        public Vector3 GetForward()
        {
            // Transform Forward according to current orientation.
            Vector3 vForward = Vector3.Transform(Vector3.Forward, mOrientation);

            return vForward;
        }

        // Returns normal in direction "left" from Camera's perspective.
        public Vector3 GetLeft()
        {
            // Transform Left according to current orientation.
            Vector3 vLeft = Vector3.Transform(Vector3.Left, mOrientation);

            return vLeft;
        }

        // Returns normal in direction "up" from Camera's perspective.
        public Vector3 GetUp()
        {
            return Vector3.Cross(GetForward(), GetLeft());
        }


        // Moves Camera according to the given displacement.
        public void Move(Vector3 displacement)
        {
            mPosition += displacement;
        }

        // Rotates Camera according the given rotation.
        private void Rotate(Quaternion rotation)
        {
            mOrientation = rotation * mOrientation;
        }

        // Adjusts Camera orientation in the local Forward-Left plane. (Right
        // is positive)
        public void AdjustYaw(float theta)
        {
            Rotate(Quaternion.CreateFromAxisAngle(Vector3.Up, theta)); // Note Vector3.Up, not GetUp()
        }

        // Adjusts Camera orientation in the local Up-Forward plane. (Forward
        // is positive)
        public void AdjustPitch(float phi)
        {
            Rotate(Quaternion.CreateFromAxisAngle(GetLeft(), phi));
        }

        // Adjusts Camera orientation in the local Left-Up plane. (Right is 
        // positive)
        public void AdjustRoll(float rho)
        {
            Rotate(Quaternion.CreateFromAxisAngle(GetForward(), rho));
        }


        // Updates Camera.
        public void Update()
        {
            mViewMatrix = Matrix.CreateLookAt(mPosition, mPosition + GetForward(), GetUp());
        }
    }
}
