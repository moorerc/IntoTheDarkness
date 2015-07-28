using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace EECS494.IntoTheDarkness
{
    class Level_01 : Level
    {
        public Level_01(Dictionary<String, BoundingBox> levelBounds,
                       Vector3                          levelStart,
                       float                            levelStartDirection,
                       BoundingSphere                   levelFinish,
                       Dictionary<String, Interactable> levelInteractables)
        : base(levelBounds, levelStart, levelStartDirection, levelFinish, levelInteractables) { }


        public static Model mLevel_01_Model;

        // Draws Level_01 and its contents.
        public override void Draw()
        {
            /* Draw Level_01 */

            Matrix[] mTransforms = new Matrix[mLevel_01_Model.Bones.Count];
            mLevel_01_Model.CopyAbsoluteBoneTransformsTo(mTransforms);

            if(!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mLevel_01_Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.World      = mTransforms[mesh.ParentBone.Index] *
                                            Matrix.CreateRotationY(0.0f) *
                                            Matrix.CreateTranslation(Vector3.Zero);
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
                foreach (ModelMesh mesh in mLevel_01_Model.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = GameplayScreen.mEffect;

                        GameplayScreen.mEffect.Parameters["World"].SetValue(mTransforms[mesh.ParentBone.Index] *
                                                                            Matrix.CreateRotationY(0.0f) *
                                                                            Matrix.CreateTranslation(Vector3.Zero));
                        GameplayScreen.mEffect.Parameters["View"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ViewMatrix);
                        GameplayScreen.mEffect.Parameters["Projection"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ProjectionMatrix);
                        GameplayScreen.mEffect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
                        GameplayScreen.mEffect.Parameters["points"].SetValue(PointManagerRemake.ContactPoints);
                        GameplayScreen.mEffect.Parameters["time_passed"].SetValue(GameplayScreen.total_time_passed % 1000);
                        GameplayScreen.mEffect.Parameters["times"].SetValue(PointManagerRemake.Times);
                        GameplayScreen.mEffect.Parameters["radiuses"].SetValue(PointManagerRemake.Radiuses);
                    }
                    mesh.Draw();
                }
                #endregion   
            }

            /* Draw Level_01 Interactables */

            foreach(Interactable i in base.LevelInteractables.Values)
                i.Draw();
        }

        public override void CleanUp()
        {
            foreach (Interactable i in base.LevelInteractables.Values)
                i.CleanUp();
        }
    }

    class Level_02 : Level
    {
        public Level_02(Dictionary<String, BoundingBox> levelBounds,
                       Vector3                          levelStart,
                       float                            levelStartDirection,
                       BoundingSphere                   levelFinish,
                       Dictionary<String, Interactable> levelInteractables)
        : base(levelBounds, levelStart, levelStartDirection, levelFinish, levelInteractables) { }


        public static Model mLevel_02_Model;

        // Draws Level_02 and its contents.
        public override void Draw()
        {
            /* Draw Level_02 */

            Matrix[] mTransforms = new Matrix[mLevel_02_Model.Bones.Count];
            mLevel_02_Model.CopyAbsoluteBoneTransformsTo(mTransforms);

            if(!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mLevel_02_Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.World      = mTransforms[mesh.ParentBone.Index] *
                                            Matrix.CreateRotationY(0.0f) *
                                            Matrix.CreateTranslation(Vector3.Zero);
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
                foreach (ModelMesh mesh in mLevel_02_Model.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = GameplayScreen.mEffect;

                        GameplayScreen.mEffect.Parameters["World"].SetValue(mTransforms[mesh.ParentBone.Index] *
                                                                            Matrix.CreateRotationY(0.0f) *
                                                                            Matrix.CreateTranslation(Vector3.Zero));
                        GameplayScreen.mEffect.Parameters["View"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ViewMatrix);
                        GameplayScreen.mEffect.Parameters["Projection"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ProjectionMatrix);
                        GameplayScreen.mEffect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
                        GameplayScreen.mEffect.Parameters["points"].SetValue(PointManagerRemake.ContactPoints);
                        GameplayScreen.mEffect.Parameters["time_passed"].SetValue(GameplayScreen.total_time_passed % 1000);
                        GameplayScreen.mEffect.Parameters["times"].SetValue(PointManagerRemake.Times);
                        GameplayScreen.mEffect.Parameters["radiuses"].SetValue(PointManagerRemake.Radiuses);
                    }
                    mesh.Draw();
                }
                #endregion   
            }

            /* Draw Level_02 Interactables */

            foreach(Interactable i in base.LevelInteractables.Values)
                i.Draw();
        }

        public override void CleanUp()
        {
            foreach (Interactable i in base.LevelInteractables.Values)
                i.CleanUp();
        }
    }

    class Level_03 : Level
    {
        public Level_03(Dictionary<String, BoundingBox> levelBounds,
                       Vector3                          levelStart,
                       float                            levelStartDirection,
                       BoundingSphere                   levelFinish,
                       Dictionary<String, Interactable> levelInteractables)
        : base(levelBounds, levelStart, levelStartDirection, levelFinish, levelInteractables) { }


        public static Model mLevel_03_Model;

        // Draws Level_03 and its contents.
        public override void Draw()
        {
            /* Draw Level_03 */

            Matrix[] mTransforms = new Matrix[mLevel_03_Model.Bones.Count];
            mLevel_03_Model.CopyAbsoluteBoneTransformsTo(mTransforms);

            if(!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mLevel_03_Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.World      = mTransforms[mesh.ParentBone.Index] *
                                            Matrix.CreateRotationY(0.0f) *
                                            Matrix.CreateTranslation(Vector3.Zero);
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
                foreach (ModelMesh mesh in mLevel_03_Model.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = GameplayScreen.mEffect;

                        GameplayScreen.mEffect.Parameters["World"].SetValue(mTransforms[mesh.ParentBone.Index] *
                                                                            Matrix.CreateRotationY(0.0f) *
                                                                            Matrix.CreateTranslation(Vector3.Zero));
                        GameplayScreen.mEffect.Parameters["View"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ViewMatrix);
                        GameplayScreen.mEffect.Parameters["Projection"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ProjectionMatrix);
                        GameplayScreen.mEffect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
                        GameplayScreen.mEffect.Parameters["points"].SetValue(PointManagerRemake.ContactPoints);
                        GameplayScreen.mEffect.Parameters["time_passed"].SetValue(GameplayScreen.total_time_passed % 1000);
                        GameplayScreen.mEffect.Parameters["times"].SetValue(PointManagerRemake.Times);
                        GameplayScreen.mEffect.Parameters["radiuses"].SetValue(PointManagerRemake.Radiuses);
                    }
                    mesh.Draw();
                }
                #endregion   
            }

            /* Draw Level_03 Interactables */

            foreach(Interactable i in base.LevelInteractables.Values)
                i.Draw();
        }

        public override void CleanUp()
        {
            foreach (Interactable i in base.LevelInteractables.Values)
                i.CleanUp();
        }
    }

    class Level_04 : Level
    {
        public Level_04(Dictionary<String, BoundingBox> levelBounds,
                       Vector3                          levelStart,
                       float                            levelStartDirection,
                       BoundingSphere                   levelFinish,
                       Dictionary<String, Interactable> levelInteractables)
        : base(levelBounds, levelStart, levelStartDirection, levelFinish, levelInteractables)
        {
            mMonster = new Monster(new Vector3(300.0f, 35.0f, -300.0f));
            mSound = SoundMaster.GetSound("monster_grunt");
        }

        
        // Level_04 Monster.
        private Monster mMonster;
        private SoundEffect mSound;


        // Updates Level_04.
        public override bool Update(BoundingBox bBoxPlayer, GameTime gameTime)
        {
            base.Update(bBoxPlayer, gameTime);

            // Update Monster.
            mMonster.Update((bBoxPlayer.Min + bBoxPlayer.Max) / 2.0f, gameTime);

            // Interaction between Stones and Switches within Level_05.
            List<string> removables = new List<string>();
            foreach (string str in LevelInteractables.Keys)
            {
                Interactable i = LevelInteractables[str];
                // Grab Stones.
                if (i is Stone)
                {
                    // Does it collide with the Monster
                    if (i.Bound.Intersects(mMonster.Bound))
                    {
                        removables.Add(str);

                        // stun the monster
                        mMonster.isStunned = true;
                        // play sound effect
                        mSound.Play();
                    }
                }
            }

            // collision. Remove stone
            foreach (string str in removables)
                LevelInteractables.Remove(str);


            return mMonster.Bound.Intersects(bBoxPlayer);
        }

        
        public static Model mLevel_04_Model;

        // Draws Level_04 and its contents.
        public override void Draw()
        {
            /* Draw Level_04 */

            Matrix[] mTransforms = new Matrix[mLevel_04_Model.Bones.Count];
            mLevel_04_Model.CopyAbsoluteBoneTransformsTo(mTransforms);

            if(!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mLevel_04_Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.World      = mTransforms[mesh.ParentBone.Index] *
                                            Matrix.CreateRotationY(0.0f) *
                                            Matrix.CreateTranslation(Vector3.Zero);
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
                foreach (ModelMesh mesh in mLevel_04_Model.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = GameplayScreen.mEffect;

                        GameplayScreen.mEffect.Parameters["World"].SetValue(mTransforms[mesh.ParentBone.Index] *
                                                                            Matrix.CreateRotationY(0.0f) *
                                                                            Matrix.CreateTranslation(Vector3.Zero));
                        GameplayScreen.mEffect.Parameters["View"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ViewMatrix);
                        GameplayScreen.mEffect.Parameters["Projection"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ProjectionMatrix);
                        GameplayScreen.mEffect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
                        GameplayScreen.mEffect.Parameters["points"].SetValue(PointManagerRemake.ContactPoints);
                        GameplayScreen.mEffect.Parameters["time_passed"].SetValue(GameplayScreen.total_time_passed % 1000);
                        GameplayScreen.mEffect.Parameters["times"].SetValue(PointManagerRemake.Times);
                        GameplayScreen.mEffect.Parameters["radiuses"].SetValue(PointManagerRemake.Radiuses);
                    }
                    mesh.Draw();
                }
                #endregion   
            }

            /* Draw Level_04 Interactables */

            foreach(Interactable i in base.LevelInteractables.Values)
                i.Draw();

            /* Draw Monster */

            mMonster.Draw(); 
        }

        public override void CleanUp()
        {
            mMonster.CleanUp();

            foreach (Interactable i in base.LevelInteractables.Values)
                i.CleanUp();
        }
    }

    class Level_05 : Level
    {
        public Level_05(Dictionary<String, BoundingBox> levelBounds,
                       Vector3                          levelStart,
                       float                            levelStartDirection,
                       BoundingSphere                   levelFinish,
                       Dictionary<String, Interactable> levelInteractables)
        : base(levelBounds, levelStart, levelStartDirection, levelFinish, levelInteractables)
        {
            spikes_1 = new BoundingBox(new Vector3(440.0f,   0.0f, -650.0f),
                                       new Vector3(600.0f, 200.0f, -470.0f));
            spikes_2 = new BoundingBox(new Vector3(  0.0f,   0.0f, -690.0f),
                                       new Vector3(145.0f, 200.0f, -650.0f));

            wall_1 = new Wall(new Vector3(550.0f, 0.0f, -150.0f), 0.0f);
            drawWall_1 = true;

            wall_2 = new Wall(new Vector3(200.0f, 0.0f, -650.0f), 0.0f);
            drawWall_2 = true;

            sound = SoundMaster.GetSound("wall_move");
        }


        // Level_05 Spike Bounds.

        private BoundingBox spikes_1;
        private BoundingBox spikes_2;

        // Level_05 Walls, status.

        private Wall wall_1;
        private bool drawWall_1;

        private Wall wall_2;
        private bool drawWall_2;

        private SoundEffect sound;
        // Updates Level_05.
        public override bool Update(BoundingBox bBoxPlayer, GameTime gameTime)
        {
            // Collision detection with Level_05 Spikes.
            if (bBoxPlayer.Intersects(spikes_1))
                return true;
            if (bBoxPlayer.Intersects(spikes_2))
                return true;

            // Interaction between Stones and Switches within Level_05.
            foreach (Interactable i_1 in LevelInteractables.Values)
            {
                // Grab Stones.
                if (i_1 is Stone)
                {
                    // Grab Switches.
                    foreach (String str in LevelInteractables.Keys)
                    {
                        Interactable i_2 = LevelInteractables[str];

                        // Check for collision.
                        if ((i_2 is Switch) && (i_2.HasCollided(i_1.Bound)))
                        {
                            // Flip Switch, remove Wall.
                            if (!((Switch) i_2).isOn)
                            {
                                i_2.Interact(gameTime);
                                RemoveWall(str);

                                if (str == "Switch 1")
                                {
                                    drawWall_1 = false;
                                    sound.Play();
                                }

                                if (str == "Switch 2")
                                {
                                    drawWall_2 = false;
                                    sound.Play();
                                }
                            }
                        }
                    }
                }
            }

            return base.Update(bBoxPlayer, gameTime);
        }


        public static Model mLevel_05_Model;

        // Draws Level_05 and its contents.
        public override void Draw()
        {
            /* Draw Level_05 */

            Matrix[] mTransforms = new Matrix[mLevel_05_Model.Bones.Count];
            mLevel_05_Model.CopyAbsoluteBoneTransformsTo(mTransforms);

            if (!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mLevel_05_Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.World = mTransforms[mesh.ParentBone.Index] *
                                            Matrix.CreateRotationY(0.0f) *
                                            Matrix.CreateTranslation(Vector3.Zero);
                        effect.View = GameplayScreen.mPlayer.PlayerCamera.ViewMatrix;
                        effect.Projection = GameplayScreen.mPlayer.PlayerCamera.ProjectionMatrix;
                    }
                    mesh.Draw();
                }
                #endregion
            }
            else
            {
                #region Shader
                foreach (ModelMesh mesh in mLevel_05_Model.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = GameplayScreen.mEffect;

                        GameplayScreen.mEffect.Parameters["World"].SetValue(mTransforms[mesh.ParentBone.Index] *
                                                                            Matrix.CreateRotationY(0.0f) *
                                                                            Matrix.CreateTranslation(Vector3.Zero));
                        GameplayScreen.mEffect.Parameters["View"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ViewMatrix);
                        GameplayScreen.mEffect.Parameters["Projection"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ProjectionMatrix);
                        GameplayScreen.mEffect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
                        GameplayScreen.mEffect.Parameters["points"].SetValue(PointManagerRemake.ContactPoints);
                        GameplayScreen.mEffect.Parameters["time_passed"].SetValue(GameplayScreen.total_time_passed % 1000);
                        GameplayScreen.mEffect.Parameters["times"].SetValue(PointManagerRemake.Times);
                        GameplayScreen.mEffect.Parameters["radiuses"].SetValue(PointManagerRemake.Radiuses);
                    }
                    mesh.Draw();
                }
                #endregion
            }

            /* Draw Level_05 Interactables */

            foreach (Interactable i in base.LevelInteractables.Values)
                i.Draw();

            /* Draw Level_05 Walls */

            if(drawWall_1)
                wall_1.Draw();
            if(drawWall_2)
                wall_2.Draw();
        }

        public override void CleanUp()
        {
            foreach (Interactable i in base.LevelInteractables.Values)
                i.CleanUp();
        }
    }

    class Level_06 : Level
    {
        public Level_06(Dictionary<String, BoundingBox> levelBounds,
                       Vector3                          levelStart,
                       float                            levelStartDirection,
                       BoundingSphere                   levelFinish,
                       Dictionary<String, Interactable> levelInteractables)
        : base(levelBounds, levelStart, levelStartDirection, levelFinish, levelInteractables)
        {
            mWall_01 = new Wall(new Vector3(200.0f, 0.0f, -350.0f), MathHelper.Pi/2);
            mBound_01 = new BoundingBox(new Vector3(195.0f, 0.0f, -400.0f), new Vector3(205.0f, 200.0f, -300.0f));
            base.LevelBounds.Add("Moving Wall 01", mBound_01);
            mDrawWall_01 = true;
            mWall_02 = new Wall(new Vector3(350.0f, 0.0f, -400.0f), 0.0f);
            mBound_02 = new BoundingBox(new Vector3(300.0f, 0.0f, -405.0f), new Vector3(400.0f, 200.0f, -395.0f));
            base.LevelBounds.Add("Moving Wall 02", mBound_02);
            mDrawWall_02 = true;

            mWall_03 = new Wall(new Vector3(350.0f, 0.0f, -100.0f), 0.0f);
            mBound_03 = new BoundingBox(new Vector3(300.0f, 0.0f, -105.0f), new Vector3(400.0f, 200.0f, -95.0f));
            base.LevelBounds.Add("Moving Wall 03", mBound_03);
            mDrawWall_03 = true;
            mWall_04 = new Wall(new Vector3(500.0f, 0.0f, -350.0f), MathHelper.Pi/2);
            mBound_04 = new BoundingBox(new Vector3(495.0f, 0.0f, -400.0f), new Vector3(505.0f, 200.0f, -300.0f));
            base.LevelBounds.Add("Moving Wall 04", mBound_04);
            mDrawWall_04 = true;

            mWall_05 = new Wall(new Vector3(450.0f, 0.0f, -200.0f), 0.0f);
            mBound_05 = new BoundingBox(new Vector3(400.0f, 0.0f, -205.0f), new Vector3(500.0f, 200.0f, -195.0f));
            base.LevelBounds.Add("Moving Wall 05", mBound_05);
            mDrawWall_05 = true;
            mWall_06 = new Wall(new Vector3(350.0f, 0.0f, -600.0f), 0.0f);
            mBound_06 = new BoundingBox(new Vector3(300.0f, 0.0f, -605.0f), new Vector3(400.0f, 200.0f, -595.0f));
            base.LevelBounds.Add("Moving Wall 06", mBound_06);
            mDrawWall_06 = true;

            mWall_07 = new Wall(new Vector3(300.0f, 0.0f, -250.0f), MathHelper.Pi/2);
            mBound_07 = new BoundingBox(new Vector3(295.0f, 0.0f, -300.0f), new Vector3(305.0f, 200.0f, -200.0f));
            //base.LevelBounds.Add("Moving Wall 07", mBound_07);
            mDrawWall_07 = true;
            mWall_08 = new Wall(new Vector3(700.0f, 0.0f, -550.0f), MathHelper.Pi/2);
            mBound_08 = new BoundingBox(new Vector3(695.0f, 0.0f, -600.0f), new Vector3(705.0f, 200.0f, -500.0f));
            //base.LevelBounds.Add("Moving Wall 08", mBound_08);
            mDrawWall_08 = true;

            mCurrentCycle = 0;
            mCycleCounter = 0;
            mCycleUpdated = false;

            mSound = SoundMaster.GetSound("wall_move");
        }


        #region Level_06 Attributes
        /* Level_06 Walls */

        // Group One

        private Wall mWall_01;
        private BoundingBox mBound_01;
        private bool mDrawWall_01;

        private Wall mWall_02;
        private BoundingBox mBound_02;
        private bool mDrawWall_02;

        // Group Two

        private Wall mWall_03;
        private BoundingBox mBound_03;
        private bool mDrawWall_03;

        private Wall mWall_04;
        private BoundingBox mBound_04;
        private bool mDrawWall_04;

        // Group Three
        
        private Wall mWall_05;
        private BoundingBox mBound_05;
        private bool mDrawWall_05;

        private Wall mWall_06;
        private BoundingBox mBound_06;
        private bool mDrawWall_06;

        // Group Four

        private Wall mWall_07;
        private BoundingBox mBound_07;
        private bool mDrawWall_07;

        private Wall mWall_08;
        private BoundingBox mBound_08;
        private bool mDrawWall_08;

        // Current cycle.
        Int32 mCurrentCycle;

        // Cycle counter.
        Int32 mCycleCounter;

        // Status of cycle update.
        bool mCycleUpdated;

        // Wall moving sound.
        SoundEffect mSound;
        #endregion


        // Updates Level_06
        public override bool Update(BoundingBox bBoxPlayer, GameTime gameTime)
        {
            // Do we need to update?
            if(mCycleCounter == 600)
            {
                mCurrentCycle = ((mCurrentCycle + 1) % 4);
                mCycleUpdated = false;
                mCycleCounter = 0;
                mSound.Play();
            }
            else
                mCycleCounter++;

            switch(mCurrentCycle)
            {
                case 0:
                    if(!mCycleUpdated)
                    {
                        base.LevelBounds.Add("Moving Wall 07", mBound_07);
                        mDrawWall_07 = true;
                        base.LevelBounds.Add("Moving Wall 08", mBound_08);
                        mDrawWall_08 = true;

                        if(GameplayScreen.mPlayer.Bound.Intersects(mBound_07) || GameplayScreen.mPlayer.Bound.Intersects(mBound_08))
                        {
                            GameplayScreen.mPlayer.Position += new Vector3(50.0f, 0.0f, 50.0f);
                            GameplayScreen.mPlayer.PlayerCamera.Move(new Vector3(50.0f, 0.0f, 50.0f));
                            GameplayScreen.mPlayer.PlayerCamera.Update();
                        }

                        base.LevelBounds.Remove("Moving Wall 01");
                        mDrawWall_01 = false;
                        base.LevelBounds.Remove("Moving Wall 02");
                        mDrawWall_02 = false;

                        mCycleUpdated = true;
                    }
                    break;
                case 1:
                    if(!mCycleUpdated)
                    {
                        base.LevelBounds.Add("Moving Wall 01", mBound_01);
                        mDrawWall_01 = true;
                        base.LevelBounds.Add("Moving Wall 02", mBound_02);
                        mDrawWall_02 = true;

                        if(GameplayScreen.mPlayer.Bound.Intersects(mBound_01) || GameplayScreen.mPlayer.Bound.Intersects(mBound_02))
                        {
                            GameplayScreen.mPlayer.Position += new Vector3(50.0f, 0.0f, 50.0f);
                            GameplayScreen.mPlayer.PlayerCamera.Move(new Vector3(50.0f, 0.0f, 50.0f));
                            GameplayScreen.mPlayer.PlayerCamera.Update();
                        }

                        base.LevelBounds.Remove("Moving Wall 03");
                        mDrawWall_03 = false;
                        base.LevelBounds.Remove("Moving Wall 04");
                        mDrawWall_04 = false;

                        mCycleUpdated = true;
                    }
                    break;
                case 2:
                    if(!mCycleUpdated)
                    {
                        base.LevelBounds.Add("Moving Wall 03", mBound_03);
                        mDrawWall_03 = true;
                        base.LevelBounds.Add("Moving Wall 04", mBound_04);
                        mDrawWall_04 = true;

                        if(GameplayScreen.mPlayer.Bound.Intersects(mBound_03) || GameplayScreen.mPlayer.Bound.Intersects(mBound_04))
                        {
                            GameplayScreen.mPlayer.Position += new Vector3(50.0f, 0.0f, 50.0f);
                            GameplayScreen.mPlayer.PlayerCamera.Move(new Vector3(50.0f, 0.0f, 50.0f));
                            GameplayScreen.mPlayer.PlayerCamera.Update();
                        }

                        base.LevelBounds.Remove("Moving Wall 05");
                        mDrawWall_05 = false;
                        base.LevelBounds.Remove("Moving Wall 06");
                        mDrawWall_06 = false;

                        mCycleUpdated = true;
                    }
                    break;
                case 3:
                    if(!mCycleUpdated)
                    {
                        base.LevelBounds.Add("Moving Wall 05", mBound_05);
                        mDrawWall_05 = true;
                        base.LevelBounds.Add("Moving Wall 06", mBound_06);
                        mDrawWall_06 = true;

                        if(GameplayScreen.mPlayer.Bound.Intersects(mBound_05) || GameplayScreen.mPlayer.Bound.Intersects(mBound_06))
                        {
                            GameplayScreen.mPlayer.Position += new Vector3(50.0f, 0.0f, 50.0f);
                            GameplayScreen.mPlayer.PlayerCamera.Move(new Vector3(50.0f, 0.0f, 50.0f));
                            GameplayScreen.mPlayer.PlayerCamera.Update();
                        }

                        base.LevelBounds.Remove("Moving Wall 07");
                        mDrawWall_07 = false;
                        base.LevelBounds.Remove("Moving Wall 08");
                        mDrawWall_08 = false;

                        mCycleUpdated = true;
                    }
                    break;
                default:
                    throw new Exception();
            }

            return base.Update(bBoxPlayer, gameTime);
        }


        public static Model mLevel_06_Model;

        // Draws Level_06 and its contents.
        public override void Draw()
        {
            /* Draw Level_06 */

            Matrix[] mTransforms = new Matrix[mLevel_06_Model.Bones.Count];
            mLevel_06_Model.CopyAbsoluteBoneTransformsTo(mTransforms);

            if(!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mLevel_06_Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.World      = mTransforms[mesh.ParentBone.Index] *
                                            Matrix.CreateRotationY(0.0f) *
                                            Matrix.CreateTranslation(Vector3.Zero);
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
                foreach (ModelMesh mesh in mLevel_06_Model.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = GameplayScreen.mEffect;

                        GameplayScreen.mEffect.Parameters["World"].SetValue(mTransforms[mesh.ParentBone.Index] *
                                                                            Matrix.CreateRotationY(0.0f) *
                                                                            Matrix.CreateTranslation(Vector3.Zero));
                        GameplayScreen.mEffect.Parameters["View"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ViewMatrix);
                        GameplayScreen.mEffect.Parameters["Projection"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ProjectionMatrix);
                        GameplayScreen.mEffect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
                        GameplayScreen.mEffect.Parameters["points"].SetValue(PointManagerRemake.ContactPoints);
                        GameplayScreen.mEffect.Parameters["time_passed"].SetValue(GameplayScreen.total_time_passed % 1000);
                        GameplayScreen.mEffect.Parameters["times"].SetValue(PointManagerRemake.Times);
                        GameplayScreen.mEffect.Parameters["radiuses"].SetValue(PointManagerRemake.Radiuses);
                    }
                    mesh.Draw();
                }
                #endregion
            }

            /* Draw Level_06 Interactables */

            foreach(Interactable i in base.LevelInteractables.Values)
                i.Draw();

            /* Draw Level_06 Walls */

            if(mDrawWall_01)
                mWall_01.Draw();
            if(mDrawWall_02)
                mWall_02.Draw();
            if(mDrawWall_03)
                mWall_03.Draw();
            if(mDrawWall_04)
                mWall_04.Draw();
            if(mDrawWall_05)
                mWall_05.Draw();
            if(mDrawWall_06)
                mWall_06.Draw();
            if(mDrawWall_07)
                mWall_07.Draw();
            if(mDrawWall_08)
                mWall_08.Draw();
            }

        public override void CleanUp()
        {
            foreach (Interactable i in base.LevelInteractables.Values)
                i.CleanUp();
        }
    }

    class Level_07 : Level
    {
        public Level_07(Dictionary<String, BoundingBox> levelBounds,
                       Vector3                          levelStart,
                       float                            levelStartDirection,
                       BoundingSphere                   levelFinish,
                       Dictionary<String, Interactable> levelInteractables)
        : base(levelBounds, levelStart, levelStartDirection, levelFinish, levelInteractables)
        {
            mMonster = new Monster(new Vector3(0.0f, 35.0f, -650.0f));
            mMonster.MonsterStep = 400.0f;
            show_monster = false;
            count = 0;
            mSound = SoundMaster.GetSound("monster_grunt");
        }

        // logic variables
        private bool show_monster;
        private int count;

        // Level_07 Monster.
        private Monster mMonster;
        private SoundEffect mSound;

        // Updates Level_07.
        public override bool Update(BoundingBox bBoxPlayer, GameTime gameTime)
        {
            base.Update(bBoxPlayer, gameTime);

            // Update Monster.
            if (show_monster)
            {
                if (count == 0)
                    count++;
                mMonster.Update((bBoxPlayer.Min + bBoxPlayer.Max) / 2.0f, gameTime);

                // Interaction between Stones and Switches within Level_05.
                List<string> removables = new List<string>();
                foreach (string str in LevelInteractables.Keys)
                {
                    Interactable i = LevelInteractables[str];
                    // Grab Stones.
                    if (i is Stone)
                    {
                        // Does it collide with the Monster
                        if (i.Bound.Intersects(mMonster.Bound))
                        {
                            removables.Add(str);

                            // stun the monster
                            mMonster.isStunned = true;
                            // play sound effect
                            mSound.Play();
                        }
                    }
                }

                // collision. Remove stone
                foreach (string str in removables)
                    LevelInteractables.Remove(str);

                if (LevelInteractables.ContainsKey("Key2"))
                {
                    Key k = (Key)LevelInteractables["Key2"];
                    LevelInteractables.Remove("Key2");
                    Vector3 v = mMonster.Velocity;
                    v.Normalize();
                    k.Position = mMonster.Position - (75.0f * v);
                    BoundingSphere b = new BoundingSphere(k.Position, 12.5f);
                    k.Bound = b;
                    LevelInteractables.Add("Key2", k);
                }
            }
            else
            {
                if (!LevelBounds.ContainsKey("Lock1"))
                {
                    show_monster = true;
                }
            }

            if (count == 1)
            {
                // Play sound
                
                // increase count
                count++;
            }

            
            return mMonster.Bound.Intersects(bBoxPlayer);
        }


        public static Model mLevel_07_Model;

        // Draws Level_07 and its contents.
        public override void Draw()
        {
            /* Draw Level_07 */

            Matrix[] mTransforms = new Matrix[mLevel_07_Model.Bones.Count];
            mLevel_07_Model.CopyAbsoluteBoneTransformsTo(mTransforms);

            if (!GameplayScreen.mEffectOn)
            {
                #region Default
                foreach (ModelMesh mesh in mLevel_07_Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.World = mTransforms[mesh.ParentBone.Index] *
                                            Matrix.CreateRotationY(0.0f) *
                                            Matrix.CreateTranslation(Vector3.Zero);
                        effect.View = GameplayScreen.mPlayer.PlayerCamera.ViewMatrix;
                        effect.Projection = GameplayScreen.mPlayer.PlayerCamera.ProjectionMatrix;
                    }
                    mesh.Draw();
                }
                #endregion
            }
            else
            {
                #region Shader
                foreach (ModelMesh mesh in mLevel_07_Model.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = GameplayScreen.mEffect;

                        GameplayScreen.mEffect.Parameters["World"].SetValue(mTransforms[mesh.ParentBone.Index] *
                                                                            Matrix.CreateRotationY(0.0f) *
                                                                            Matrix.CreateTranslation(Vector3.Zero));
                        GameplayScreen.mEffect.Parameters["View"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ViewMatrix);
                        GameplayScreen.mEffect.Parameters["Projection"].SetValue(GameplayScreen.mPlayer.PlayerCamera.ProjectionMatrix);
                        GameplayScreen.mEffect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
                        GameplayScreen.mEffect.Parameters["points"].SetValue(PointManagerRemake.ContactPoints);
                        GameplayScreen.mEffect.Parameters["time_passed"].SetValue(GameplayScreen.total_time_passed % 1000);
                        GameplayScreen.mEffect.Parameters["times"].SetValue(PointManagerRemake.Times);
                        GameplayScreen.mEffect.Parameters["radiuses"].SetValue(PointManagerRemake.Radiuses);
                    }
                    mesh.Draw();
                }
                #endregion
            }

            /* Draw Level_07 Interactables */

            foreach (Interactable i in base.LevelInteractables.Values)
                i.Draw();

            /* Draw Monster */

            mMonster.Draw();
        }

        public override void CleanUp()
        {
            mMonster.CleanUp();

            foreach (Interactable i in base.LevelInteractables.Values)
                i.CleanUp();
        }
    }
}
