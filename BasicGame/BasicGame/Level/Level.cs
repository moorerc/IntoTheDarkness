using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace EECS494.IntoTheDarkness
{
    /*
     * Level
     * 
     * Last Updated : December 9, 2012
     * v1.0 : Bryan - First implementation.
     * v2.0 : Bryan - Second implementation.
     * v2.1 : Bryan - Draw.
     *  
     * Provides framework for a level.
     * 
     * Derive from here to create levels.
     */
    abstract class Level
    {
        public Level(Dictionary<String, BoundingBox>  levelBounds,
                     Vector3                          levelStart,
                     float                            levelStartDirection,
                     BoundingSphere                   levelFinish,
                     Dictionary<String, Interactable> levelInteractables)
        {
            mLevelBounds         = levelBounds;
            mLevelStart          = levelStart;
            mLevelStartDirection = levelStartDirection;
            mLevelFinish         = levelFinish;
            mLevelInteractables  = levelInteractables;

            mIsCompleted = false;
        }

        #region Level Attributes
        // Level boundaries.
        private Dictionary<String, BoundingBox> mLevelBounds;
        public Dictionary<String, BoundingBox> LevelBounds
        {
            get
            {
                return mLevelBounds;
            }
            protected set
            {
                mLevelBounds = value;
            }
        }

        // Starting point for Level.
        private Vector3 mLevelStart;
        public Vector3 LevelStart
        {
            get
            {
                return mLevelStart;
            }
        }

        // Initial direction for Player in Level.
        private float mLevelStartDirection;
        public float LevelStartDirection
        {
            get
            {
                return mLevelStartDirection;
            }
        }

        // Finishing point for Level.
        private BoundingSphere mLevelFinish;
        public BoundingSphere LevelFinish
        {
            get
            {
                return mLevelFinish;
            }
        }

        // Level Interactactables.
        private Dictionary<String, Interactable> mLevelInteractables;
        public Dictionary<String, Interactable> LevelInteractables
        {
            get
            {
                return mLevelInteractables;
            }
            protected set
            {
                mLevelInteractables = value;
            }
        }

        // Status of Level.
        private bool mIsCompleted;
        public bool IsCompleted
        {
            get
            {
                return mIsCompleted;
            }
            set
            {
                mIsCompleted = value;
            }
        }
        #endregion


        // Removes given Bound from Level.
        public void RemoveWall(String str)
        {
            mLevelBounds.Remove(str);
        }

        // Removes given Interactable from Level.
        public void RemoveInteractable(String str)
        {
            mLevelInteractables.Remove(str);
        }


        #region Collision Detection
        // Returns true if given BoundingBox intersects any of the Level
        // bounds, false otherwise.
        public bool HitWall(BoundingBox bBoxPlayer, ref BoundingBox BoundHit)
        {
            foreach (BoundingBox bBoxWall in mLevelBounds.Values)
            {
                ContainmentType bContains = bBoxWall.Contains(bBoxPlayer);
                if ((bContains == ContainmentType.Intersects) && (bContains != ContainmentType.Contains))
                {
                    BoundHit = bBoxWall;
                    return true;
                }
            }
            return false;
        }

        public bool HitWallProj(BoundingSphere bObject, ref Vector3 vNormal)
        {
            foreach (BoundingBox bBoxWall in mLevelBounds.Values)
            {
                ContainmentType bContains = bBoxWall.Contains(bObject);
                if ((bContains == ContainmentType.Intersects) && (bContains != ContainmentType.Contains))
                {
                    if (bObject.Center.X <= bBoxWall.Min.X)
                        vNormal = Vector3.Left;
                    if (bObject.Center.X > bBoxWall.Max.X)
                        vNormal = Vector3.Right;
                    if (bObject.Center.Z <= bBoxWall.Min.Z)
                        vNormal = Vector3.Forward;
                    if (bObject.Center.Z > bBoxWall.Max.Z)
                        vNormal = Vector3.Backward;
                    return true;
                }
            }
            return false;
        }

        // Determines if Player is in position to interact with the various
        // Interactables within Level.
        public void Check(BoundingBox bBoxPlayer)
        {
            TextMaster.DisplayString = string.Empty;

            foreach (Interactable i in mLevelInteractables.Values)
            {
                if (i.Bound.Intersects(bBoxPlayer))
                {
                    if (i is Switch)
                    {
                        TextMaster.DisplayString = "A large Switch.";
                        break;
                    }
                    else if (i is Machine)
                    {
                        TextMaster.DisplayString = "An odd-looking Machine.";
                        break;
                    }
                    else if (i is Key)
                    {
                        TextMaster.DisplayString = "A small Key.";
                        break;
                    }
                    else if (i is Lock)
                    {
                        if (Player.HasItem("Key"))
                            TextMaster.DisplayString = "An old door. Use a Key to unlock.";
                        else
                            TextMaster.DisplayString = "An old door. It's locked, but perhaps there is a Key nearby?";

                        break;
                    }
                    else if ((i is Stone) && !(((Stone)i).IsMoving))
                    {
                        TextMaster.DisplayString = "A small Stone.";
                        break;
                    }
                    else
                        TextMaster.DisplayString = string.Empty;
                }
            }
        }
        #endregion

        // Attempts to have Player interact with the various Interactables
        // within Level. If possible, does so.
        public void Interact(BoundingBox bBoxPlayer, GameTime gameTime)
        {
            foreach (String key in mLevelInteractables.Keys)
            {
                // Fetch Interactable corresponding to String.
                Interactable i = mLevelInteractables[key];

                if (i.HasCollided(bBoxPlayer))
                {
                    if (i is Lock)
                    {
                        if (Player.HasItem("Key"))
                        {
                            i.Interact(gameTime);

                            mLevelInteractables.Remove(key);
                            mLevelBounds.Remove(key);
                        }
                    }
                    else if ((i is Stone) || (i is Key))
                    {
                        i.Interact(gameTime);

                        mLevelInteractables.Remove(key);
                    }
                    else
                        i.Interact(gameTime);

                    break; // Only interact with one object at a time.
                }
            }
        }


        // Updates Level. Returns true if Player has died, false otherwise.
        public virtual bool Update(BoundingBox bBoxPlayer, GameTime gameTime)
        {
            if (bBoxPlayer.Intersects(mLevelFinish))
            {
                mIsCompleted = true;
            }

            foreach (Interactable i in mLevelInteractables.Values)
                i.Update(gameTime);

            // Check for possible interactions with Level Interactables.
            Check(bBoxPlayer);

            return false;
        }


        // Draws the Level and its contents.
        public abstract void Draw();

        public abstract void CleanUp();
    }
}