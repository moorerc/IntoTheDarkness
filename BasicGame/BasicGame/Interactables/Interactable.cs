using Microsoft.Xna.Framework;

namespace EECS494.IntoTheDarkness
{
    /*
     * Interactable
     * 
     * Last Update : December 9, 2012
     * v1.0 : Max - First implementation.
     * v1.1 : Bryan - Refigured to mesh with inheritance hierarchy.
     * v1.2 : Bryan - Overloaded collision detection.
     * v1.3 : Bryan - Draw, Update.
     * 
     * Abstract base class representing any object the Player may interact
     * with.
     */
    abstract class Interactable
    {
        public Interactable(Vector3 position)
        {
            mPosition = position;

            mBound = new BoundingSphere(position, 0.0f);
        }


        // Interactable position.
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

        // Interactable bound.
        private BoundingSphere mBound;
        public BoundingSphere Bound
        {
            get
            {
                return mBound;
            }
            set
            {
                mBound = value;
            }
        }


        // Logic upon interacting with Interactable. Deafult is no action.
        public virtual void Interact(GameTime gameTime) { }


        // Collison detection for Interactable.

        public virtual bool HasCollided(BoundingSphere bSphere)
        {
            return mBound.Intersects(bSphere);
        }

        public virtual bool HasCollided(BoundingBox bBox)
        {
            return mBound.Intersects(bBox);
        }


        // Updates Interactable. Default is no action.
        virtual public void Update(GameTime gameTime) { }


        // Draws Interactable. Must be overridden in derived classes.
        abstract public void Draw();

        abstract public void CleanUp();
    }
}
