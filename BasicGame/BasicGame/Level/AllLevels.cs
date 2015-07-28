using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace EECS494.IntoTheDarkness
{
    // This file only has one function. It contains all the code
    // for loading in the levels. Everything is written directly 
    // into the code, so there is no file reading. We specify where
    // each interactable object is.

    partial class GameplayScreen : GameScreen
    {
        const int NUMLEVELS = 7;

        /* Levels */

        private static Level_01 mLevel_01;
        private static Level_02 mLevel_02;
        private static Level_03 mLevel_03;
        private static Level_04 mLevel_04;
        private static Level_05 mLevel_05;
        private static Level_06 mLevel_06;
        private static Level_07 mLevel_07;

        // Initializes each Level.
        private void InitializeLevels()
        {
            InitializeLevel_01();
            InitializeLevel_02();
            InitializeLevel_03();
            InitializeLevel_04();
            InitializeLevel_05();
            InitializeLevel_06();
            InitializeLevel_07();
        }


        // Loads content for each Level.
        private void LoadLevelContent()
        {
            Level_01.mLevel_01_Model = content.Load<Model>("Models//Levels//Level 01 - Jump");
            Level_02.mLevel_02_Model = content.Load<Model>("Models//Levels//Level 02 - Lock and Key");
            Level_03.mLevel_03_Model = content.Load<Model>("Models//Levels//Level 03 - Switches and Machines");
            Level_04.mLevel_04_Model = content.Load<Model>("Models//Levels//Level 04 - Monster");
            Level_05.mLevel_05_Model = content.Load<Model>("Models//Levels//Level 05 - Stones");
            Level_06.mLevel_06_Model = content.Load<Model>("Models//Levels//Level 06 - Shifting Labyrinth");            
            Level_07.mLevel_07_Model = content.Load<Model>("Models//Levels//Level 07 - Find the Key");
        }


        // Returns the specified Level.
        public static Level GetLevel(Int32 levelNum)
        {
            switch(levelNum)
            {
                case 1:
                    return mLevel_01;
                case 2:
                    return mLevel_02;
                case 3:
                    return mLevel_03;
                case 4:
                    return mLevel_04;
                case 5:
                    return mLevel_05;
                case 6:
                    return mLevel_06;
                case 7:
                    return mLevel_07;
                default:
                    throw new Exception();
            }
        }


        // Initializes Level 01.
        private void InitializeLevel_01()
        {
            #region Level 01 Bounds
            Dictionary<String, BoundingBox> levelBounds = new Dictionary<String, BoundingBox>();

            levelBounds.Add("Wall #1",  new BoundingBox(new Vector3(   0.0f,   0.0f,   245.0f),
                                                        new Vector3( 100.0f, 200.0f,   255.0f)));
            levelBounds.Add("Wall #2",  new BoundingBox(new Vector3(  -5.0f,   0.0f,  -250.0f),
                                                        new Vector3(   5.0f, 200.0f,   250.0f)));
            levelBounds.Add("Wall #3",  new BoundingBox(new Vector3(  95.0f,   0.0f,  -250.0f),
                                                        new Vector3( 105.0f, 200.0f,   250.0f)));
            levelBounds.Add("Wall #4",  new BoundingBox(new Vector3(-200.0f,   0.0f,  -255.0f),
                                                        new Vector3(   0.0f, 200.0f,  -245.0f)));
            levelBounds.Add("Wall #5",  new BoundingBox(new Vector3( 100.0f,   0.0f,  -255.0f),
                                                        new Vector3( 300.0f, 200.0f,  -245.0f)));
            levelBounds.Add("Wall #6",  new BoundingBox(new Vector3(-205.0f,   0.0f,  -650.0f),
                                                        new Vector3(-195.0f, 200.0f,  -250.0f)));
            levelBounds.Add("Wall #7",  new BoundingBox(new Vector3( 295.0f,   0.0f,  -650.0f),
                                                        new Vector3( 305.0f, 200.0f,  -250.0f)));
            levelBounds.Add("Wall #8",  new BoundingBox(new Vector3(-200.0f,   0.0f,  -655.0f),
                                                        new Vector3(   0.0f, 200.0f,  -645.0f)));
            levelBounds.Add("Wall #9",  new BoundingBox(new Vector3( 100.0f,   0.0f,  -655.0f), 
                                                        new Vector3( 300.0f, 200.0f,  -645.0f)));
            levelBounds.Add("Wall #10", new BoundingBox(new Vector3(-100.0f,   0.0f,  -355.0f),
                                                        new Vector3( 200.0f, 200.0f,  -345.0f)));
            levelBounds.Add("Wall #11", new BoundingBox(new Vector3(-100.0f,   0.0f,  -555.0f),
                                                        new Vector3( 200.0f, 200.0f,  -545.0f)));
            levelBounds.Add("Wall #12", new BoundingBox(new Vector3(-105.0f,   0.0f,  -550.0f),
                                                        new Vector3( -95.0f, 200.0f,  -350.0f)));
            levelBounds.Add("Wall #13", new BoundingBox(new Vector3( 195.0f,   0.0f,  -550.0f),
                                                        new Vector3( 205.0f, 200.0f,  -350.0f)));
            levelBounds.Add("Wall #14", new BoundingBox(new Vector3(  -5.0f,   0.0f,  -800.0f),
                                                        new Vector3(   5.0f, 200.0f,  -650.0f)));
            levelBounds.Add("Wall #15", new BoundingBox(new Vector3(  95.0f,   0.0f,  -800.0f),
                                                        new Vector3( 105.0f, 200.0f,  -650.0f)));
            levelBounds.Add("Wall #16", new BoundingBox(new Vector3(   0.0f,   0.0f,  -805.0f),
                                                        new Vector3( 100.0f, 200.0f,  -795.0f)));
            
            levelBounds.Add("Ceiling",  new BoundingBox(new Vector3(-300.0f, 200.0f, -1000.0f),
                                                        new Vector3( 300.0f, 200.0f,   300.0f)));
            #endregion

            #region Level One Start
            Vector3 levelStart = new Vector3(50.0f, 35.0f, 200.0f);
            #endregion
            
            #region Level One Finish
            BoundingSphere levelFinish = new BoundingSphere(new Vector3(50.0f, 25.0f, -750.0f), 25.0f);
            #endregion
    
            #region Level One Interactables
            Dictionary<String, Interactable> levelInteractables = new Dictionary<String, Interactable>();
            #endregion

            mLevel_01 = new Level_01(levelBounds,
                                     levelStart,
                                     0.0f,
                                     levelFinish,
                                     levelInteractables);
        }

        // Initializes Level 02.
        private void InitializeLevel_02()
        {
            #region Level 02 Bounds
            Dictionary<String, BoundingBox> levelBounds = new Dictionary<String, BoundingBox>();
          
            levelBounds.Add("Wall #1",  new BoundingBox(new Vector3( -50.0f,   0.0f,   95.0f),
                                                        new Vector3(  50.0f, 200.0f,  105.0f)));
            levelBounds.Add("Wall #2",  new BoundingBox(new Vector3( -55.0f,   0.0f, -100.0f),
                                                        new Vector3( -45.0f, 200.0f,  100.0f)));
            levelBounds.Add("Wall #3",  new BoundingBox(new Vector3(  45.0f,   0.0f, -100.0f),
                                                        new Vector3(  55.0f, 200.0f,  100.0f)));
            levelBounds.Add("Wall #4",  new BoundingBox(new Vector3(-250.0f,   0.0f, -105.0f),
                                                        new Vector3( -50.0f, 200.0f,  -95.0f)));
            levelBounds.Add("Wall #5",  new BoundingBox(new Vector3(  50.0f,   0.0f, -105.0f),
                                                        new Vector3( 250.0f, 200.0f,  -95.0f)));
            levelBounds.Add("Wall #6",  new BoundingBox(new Vector3(-350.0f,   0.0f, -205.0f),
                                                        new Vector3( 250.0f, 200.0f, -195.0f)));
            levelBounds.Add("Wall #7",  new BoundingBox(new Vector3( 245.0f,   0.0f, -200.0f),
                                                        new Vector3( 255.0f, 200.0f, -100.0f)));
            levelBounds.Add("Wall #8",  new BoundingBox(new Vector3(-355.0f,   0.0f, -200.0f),
                                                        new Vector3(-345.0f, 200.0f,  100.0f)));
            levelBounds.Add("Wall #9",  new BoundingBox(new Vector3(-255.0f,   0.0f, -100.0f),
                                                        new Vector3(-245.0f, 200.0f,  100.0f)));
            levelBounds.Add("Wall #10", new BoundingBox(new Vector3(-350.0f,   0.0f,   95.0f),
                                                        new Vector3(-250.0f, 200.0f,  105.0f)));
    
            levelBounds.Add("Right of Door", new BoundingBox(new Vector3(-255.0f,   0.0f, -200.0f), 
                                                             new Vector3(-245.0f, 200.0f, -170.0f)));
            levelBounds.Add("Lock",          new BoundingBox(new Vector3(-255.0f,   0.0f, -170.0f),
                                                             new Vector3(-235.0f, 200.0f, -130.0f)));
            levelBounds.Add("Left of Door",  new BoundingBox(new Vector3(-255.0f,   0.0f, -130.0f),
                                                             new Vector3(-245.0f, 200.0f, -100.0f)));

            levelBounds.Add("Ceiling", new BoundingBox(new Vector3(-500.0f, 200.0f, -1000.0f),
                                                       new Vector3( 300.0f, 200.0f,   300.0f)));
            #endregion

            #region Level 02 Start
            Vector3 levelStart = new Vector3(0.0f, 35.0f, 0.0f);
            #endregion

            #region Level 02 Finish
            BoundingSphere levelFinish = new BoundingSphere(new Vector3(-300.0f, 25.0f, 50.0f), 25.0f);
            #endregion

            #region Level 02 Interactables
            Dictionary<String, Interactable> levelInteractables = new Dictionary<String, Interactable>();

            levelInteractables.Add("Key",  new Key( new Vector3( 200.0f, 12.5f, -150.0f)));
            levelInteractables.Add("Lock", new Lock(new Vector3(-250.0f,  0.0f, -150.0f), MathHelper.Pi/2));
            //levelInteractables.Add("Lock", new Lock(new Vector3(550.0f, 0.0f,  -150.0f), MathHelper.Pi));
            #endregion

            mLevel_02 = new Level_02(levelBounds,
                                     levelStart,
                                     0.0f,
                                     levelFinish,
                                     levelInteractables);
        }

        // Initializes Level 03.
        private void InitializeLevel_03()
        {
            #region Level 03 Bounds
            Dictionary<String, BoundingBox> levelBounds = new Dictionary<String, BoundingBox>();

            levelBounds.Add("Outer Front", new BoundingBox(new Vector3(  0.0f,   0.0f,    -5.0f),
                                                           new Vector3(600.0f, 200.0f,     5.0f)));
            levelBounds.Add("Outer Left",  new BoundingBox(new Vector3( -5.0f,   0.0f, -1200.0f),
                                                           new Vector3(  5.0f, 200.0f,     0.0f)));
            levelBounds.Add("Outer Back",  new BoundingBox(new Vector3(  0.0f,   0.0f, -1205.0f),
                                                           new Vector3(600.0f, 200.0f, -1195.0f)));
            levelBounds.Add("Outer Right", new BoundingBox(new Vector3(595.0f,   0.0f, -1200.0f),
                                                           new Vector3(605.0f, 200.0f,     0.0f)));
            levelBounds.Add("Outer Top",   new BoundingBox(new Vector3(  0.0f, 195.0f, -1200.0f),
                                                           new Vector3(600.0f, 205.0f,     0.0f)));

            levelBounds.Add("Exit Left", new BoundingBox(new Vector3(495.0f,   0.0f, -150.0f),
                                                         new Vector3(505.0f, 200.0f,    0.0f)));
            levelBounds.Add("Lock",      new BoundingBox(new Vector3(500.0f,   0.0f, -155.0f),
                                                         new Vector3(600.0f, 200.0f, -145.0f)));
            #endregion

            #region Level 03 Start
            Vector3 levelStart = new Vector3(550.0f, 35.0f, -350.0f);
            #endregion

            #region Level 03 Finish
            BoundingSphere levelFinish = new BoundingSphere(new Vector3(550.0f, 25.0f, -75.0f),
                                                                25.0f);
            #endregion

            #region Level 03 Interactables
            Dictionary<String, Interactable> levelInteractables = new Dictionary<String, Interactable>();

            levelInteractables.Add("Lock", new Lock(new Vector3(550.0f, 0.0f,  -150.0f), MathHelper.Pi));
            levelInteractables.Add("Key",  new Key( new Vector3(100.0f, 5.0f, -1100.0f)));

            Machine mMachine_01 = new Machine(new Vector3(150.0f, 0.0f, -50.0f), true);
            levelInteractables.Add("Machine 1", mMachine_01);
            levelInteractables.Add("Switch 1",  new Switch(new Vector3(550.0f, 0.0f, -300.0f), mMachine_01));

            Machine mMachine_02 = new Machine(new Vector3(50.0f, 0.0f, -600.0f), true);
            levelInteractables.Add("Machine 2", mMachine_02);
            levelInteractables.Add("Switch 2", new Switch(new Vector3(50.0f, 0.0f, -150.0f), mMachine_02));

            Machine mMachine_03 = new Machine(new Vector3(550.0f, 0.0f, -950.0f), true);
            levelInteractables.Add("Machine 3", mMachine_03);
            levelInteractables.Add("Switch 3", new Switch(new Vector3(150.0f, 0.0f, -700.0f), mMachine_03));

            Machine mMachine_04 = new Machine(new Vector3(50.0f, 0.0f, -1150.0f), true);
            levelInteractables.Add("Machine 4", mMachine_04);
            levelInteractables.Add("Switch 4", new Switch(new Vector3(450.0f, 0.0f, -1050.0f), mMachine_04));
            #endregion

            mLevel_03 = new Level_03(levelBounds,
                                     levelStart,
                                     MathHelper.Pi,
                                     levelFinish,
                                     levelInteractables);
        }

        // Initializes Level 04.
        private void InitializeLevel_04()
        {
            #region Level 04 Bounds
            Dictionary<String, BoundingBox> levelBounds = new Dictionary<String, BoundingBox>();

            levelBounds.Add("Outer Front", new BoundingBox(new Vector3(  0.0f,   0.0f,   -5.0f),
                                                           new Vector3(600.0f, 200.0f,    5.0f)));
            levelBounds.Add("Outer Left",  new BoundingBox(new Vector3( -5.0f,   0.0f, -600.0f),
                                                           new Vector3(  5.0f, 200.0f,    0.0f)));
            levelBounds.Add("Outer Back",  new BoundingBox(new Vector3(  0.0f,   0.0f, -605.0f),
                                                           new Vector3(600.0f, 200.0f, -595.0f)));
            levelBounds.Add("Outer Right", new BoundingBox(new Vector3(595.0f,   0.0f, -600.0f),
                                                           new Vector3(605.0f, 200.0f,    0.0f)));
            levelBounds.Add("Outer Top",   new BoundingBox(new Vector3(  0.0f, 195.0f, -600.0f),
                                                           new Vector3(600.0f, 205.0f,    0.0f)));

            levelBounds.Add("Exit Left", new BoundingBox(new Vector3(495.0f,   0.0f, -150.0f),
                                                         new Vector3(505.0f, 200.0f,    0.0f)));
            levelBounds.Add("Lock",      new BoundingBox(new Vector3(500.0f,   0.0f, -155.0f),
                                                         new Vector3(600.0f, 200.0f, -145.0f)));
            #endregion

            #region Level 04 Start
            Vector3 levelStart = new Vector3(550.0f, 35.0f, -550.0f);
            #endregion

            #region Level 04 Finish
            BoundingSphere levelFinish = new BoundingSphere(new Vector3(75.0f, 0.0f, -50.0f),
                                                                25.0f);
            #endregion

            #region Level 04 Interactables
            Dictionary<String, Interactable> levelInteractables = new Dictionary<String, Interactable>();

            levelInteractables.Add("Lock", new Lock(new Vector3(150.0f, 0.0f, -52.5f), MathHelper.PiOver2));
            levelInteractables.Add("Key",  new Key( new Vector3(300.0f, 5.0f, -300.0f)));
            #endregion

            mLevel_04 = new Level_04(levelBounds,
                                     levelStart,
                                     3.0f * MathHelper.PiOver4,
                                     levelFinish,
                                     levelInteractables);
        }

        // Initializes Level 05.
        private void InitializeLevel_05()
        {
            #region Level 05 Bounds
            Dictionary<String, BoundingBox> levelBounds = new Dictionary<String, BoundingBox>();

            levelBounds.Add("Outer Front", new BoundingBox(new Vector3(  0.0f,   0.0f,    -5.0f),
                                                           new Vector3(600.0f, 200.0f,     5.0f)));
            levelBounds.Add("Outer Left",  new BoundingBox(new Vector3( -5.0f,   0.0f, -1000.0f),
                                                           new Vector3(  5.0f, 200.0f,     0.0f)));
            levelBounds.Add("Outer Back",  new BoundingBox(new Vector3(  0.0f,   0.0f, -1005.0f),
                                                           new Vector3(600.0f, 200.0f,  -995.0f)));
            levelBounds.Add("Outer Right", new BoundingBox(new Vector3(595.0f,   0.0f, -1000.0f),
                                                           new Vector3(605.0f, 200.0f,     0.0f)));

            levelBounds.Add("Wall #1", new BoundingBox(new Vector3(150.0f,   0.0f, -155.0f),
                                                       new Vector3(500.0f, 200.0f, -145.0f)));
            levelBounds.Add("Wall #2", new BoundingBox(new Vector3(145.0f,   0.0f, -650.0f),
                                                       new Vector3(155.0f, 200.0f, -150.0f)));
            levelBounds.Add("Wall #3", new BoundingBox(new Vector3(250.0f,   0.0f, -655.0f),
                                                       new Vector3(600.0f, 200.0f, -645.0f)));
            levelBounds.Add("Wall #4", new BoundingBox(new Vector3(  0.0f,   0.0f, -905.0f),
                                                       new Vector3(500.0f, 200.0f, -895.0f)));


            levelBounds.Add("Switch 1", new BoundingBox(new Vector3(500.0f,   0.0f, -155.0f),
                                                        new Vector3(600.0f, 200.0f, -145.0f)));
            levelBounds.Add("Switch 2", new BoundingBox(new Vector3(150.0f,   0.0f, -655.0f),
                                                        new Vector3(250.0f, 200.0f, -645.0f)));
            #endregion

            #region Level 05 Start
            Vector3 levelStart = new Vector3(225.0f, 35.0f, -225.0f);
            #endregion

            #region Level 05 Finish
            BoundingSphere levelFinish = new BoundingSphere(new Vector3(50.0f, 0.0f, -950.0f), 25.0f);
            #endregion

            #region Level 05 Interactables
            Dictionary<String, Interactable> levelInteractables = new Dictionary<String, Interactable>();

            Fake_Machine mMachine_01 = new Fake_Machine(new Vector3(550.0f, 0.0f, -600.0f), false);
            levelInteractables.Add("Switch 1", new Switch(new Vector3(550.0f, 0.0f, -600.0f), mMachine_01));

            Fake_Machine mMachine_02 = new Fake_Machine(new Vector3(75.0f, 0.0f, -750.0f), false);
            levelInteractables.Add("Switch 2", new Switch(new Vector3(75.0f, 0.0f, -750.0f), mMachine_02));
            #endregion

            mLevel_05 = new Level_05(levelBounds,
                                     levelStart,
                                     0.0f,
                                     levelFinish,
                                     levelInteractables);
        }

        // Initializes Level 06.
        private void InitializeLevel_06()
        {
            #region Level 06 Bounds
            Dictionary<String, BoundingBox> levelBounds = new Dictionary<String, BoundingBox>();

            levelBounds.Add("Outer Front", new BoundingBox(new Vector3(  0.0f,   0.0f,   -5.0f),
                                                           new Vector3(800.0f, 200.0f,    5.0f)));
            levelBounds.Add("Outer Left",  new BoundingBox(new Vector3( -5.0f,   0.0f, -800.0f),
                                                           new Vector3(  5.0f, 200.0f,    0.0f)));
            levelBounds.Add("Outer Back",  new BoundingBox(new Vector3(  0.0f,   0.0f, -805.0f),
                                                           new Vector3(800.0f, 200.0f, -795.0f)));
            levelBounds.Add("Outer Right", new BoundingBox(new Vector3(795.0f,   0.0f, -800.0f),
                                                           new Vector3(805.0f, 200.0f,    0.0f)));
            levelBounds.Add("Outer Top",   new BoundingBox(new Vector3(  0.0f, 195.0f, -800.0f),
                                                           new Vector3(600.0f, 205.0f,    0.0f)));

            levelBounds.Add("Wall 01", new BoundingBox(new Vector3(  0.0f,   0.0f, -605.0f),
                                                       new Vector3(100.0f, 200.0f, -595.0f)));
            levelBounds.Add("Wall 02", new BoundingBox(new Vector3( 95.0f,   0.0f, -100.0f),
                                                       new Vector3(105.0f, 200.0f,    0.0f)));
            levelBounds.Add("Wall 03", new BoundingBox(new Vector3( 95.0f,   0.0f, -300.0f),
                                                       new Vector3(105.0f, 200.0f, -200.0f)));
            levelBounds.Add("Wall 04", new BoundingBox(new Vector3( 95.0f,   0.0f, -700.0f),
                                                       new Vector3(105.0f, 200.0f, -500.0f)));
            levelBounds.Add("Wall 05", new BoundingBox(new Vector3(100.0f,   0.0f, -205.0f),
                                                       new Vector3(200.0f, 200.0f, -195.0f)));
            levelBounds.Add("Wall 06", new BoundingBox(new Vector3(100.0f,   0.0f, -405.0f),
                                                       new Vector3(200.0f, 200.0f, -395.0f)));
            levelBounds.Add("Wall 07", new BoundingBox(new Vector3(100.0f,   0.0f, -705.0f),
                                                       new Vector3(200.0f, 200.0f, -695.0f)));
            levelBounds.Add("Wall 08", new BoundingBox(new Vector3(195.0f,   0.0f, -300.0f),
                                                       new Vector3(205.0f, 200.0f, -100.0f)));
            levelBounds.Add("Wall 09", new BoundingBox(new Vector3(195.0f,   0.0f, -600.0f),
                                                       new Vector3(205.0f, 200.0f, -400.0f)));
            levelBounds.Add("Wall 10", new BoundingBox(new Vector3(200.0f,   0.0f, -105.0f),
                                                       new Vector3(300.0f, 200.0f,  -95.0f)));
            levelBounds.Add("Wall 11", new BoundingBox(new Vector3(200.0f,   0.0f, -605.0f),
                                                       new Vector3(300.0f, 200.0f, -595.0f)));
            levelBounds.Add("Wall 12", new BoundingBox(new Vector3(295.0f,   0.0f, -500.0f),
                                                       new Vector3(305.0f, 200.0f, -300.0f)));
            levelBounds.Add("Wall 13", new BoundingBox(new Vector3(295.0f,   0.0f, -800.0f),
                                                       new Vector3(305.0f, 200.0f, -700.0f)));
            levelBounds.Add("Wall 14", new BoundingBox(new Vector3(300.0f,   0.0f, -205.0f),
                                                       new Vector3(400.0f, 200.0f, -195.0f)));
            levelBounds.Add("Wall 15", new BoundingBox(new Vector3(395.0f,   0.0f, -200.0f),
                                                       new Vector3(405.0f, 200.0f, -100.0f)));
            levelBounds.Add("Wall 16", new BoundingBox(new Vector3(395.0f,   0.0f, -700.0f),
                                                       new Vector3(405.0f, 200.0f, -600.0f)));
            levelBounds.Add("Wall 17", new BoundingBox(new Vector3(400.0f,   0.0f, -105.0f),
                                                       new Vector3(700.0f, 200.0f,  -95.0f)));
            levelBounds.Add("Wall 18", new BoundingBox(new Vector3(400.0f,   0.0f, -405.0f),
                                                       new Vector3(700.0f, 200.0f, -395.0f)));
            levelBounds.Add("Wall 19", new BoundingBox(new Vector3(400.0f,   0.0f, -505.0f),
                                                       new Vector3(500.0f, 200.0f, -495.0f)));
            levelBounds.Add("Wall 20", new BoundingBox(new Vector3(400.0f,   0.0f, -605.0f),
                                                       new Vector3(700.0f, 200.0f, -595.0f)));
            levelBounds.Add("Wall 21", new BoundingBox(new Vector3(400.0f,   0.0f, -705.0f),
                                                       new Vector3(600.0f, 200.0f, -695.0f)));
            levelBounds.Add("Wall 22", new BoundingBox(new Vector3(495.0f,   0.0f, -300.0f),
                                                       new Vector3(505.0f, 200.0f, -200.0f)));
            levelBounds.Add("Wall 23", new BoundingBox(new Vector3(495.0f,   0.0f, -600.0f),
                                                       new Vector3(505.0f, 200.0f, -500.0f)));
            levelBounds.Add("Wall 24", new BoundingBox(new Vector3(500.0f,   0.0f, -505.0f),
                                                       new Vector3(600.0f, 200.0f, -495.0f)));
            levelBounds.Add("Wall 25", new BoundingBox(new Vector3(595.0f,   0.0f, -300.0f),
                                                       new Vector3(605.0f, 200.0f, -200.0f)));
            levelBounds.Add("Wall 26", new BoundingBox(new Vector3(600.0f,   0.0f, -305.0f),
                                                       new Vector3(700.0f, 200.0f, -295.0f)));
            levelBounds.Add("Wall 27", new BoundingBox(new Vector3(695.0f,   0.0f, -500.0f),
                                                       new Vector3(705.0f, 200.0f, -100.0f)));
            levelBounds.Add("Wall 28", new BoundingBox(new Vector3(695.0f,   0.0f, -700.0f),
                                                       new Vector3(705.0f, 200.0f, -600.0f)));
            #endregion

            #region Level 06 Start
            Vector3 levelStart = new Vector3(50.0f, 35.0f, -50.0f);
            #endregion

            #region Level 06 Finish
            BoundingSphere levelFinish = new BoundingSphere(new Vector3(400.0f, 0.0f, -300.0f), 25.0f);
            #endregion

            #region Level 06 Interactables
            Dictionary<String, Interactable> levelInteractables = new Dictionary<String, Interactable>();

            Machine mMachine_01 = new Machine(new Vector3(150.0f, 0.0f, -150.0f), true);
            levelInteractables.Add("Machine 1", mMachine_01);
            levelInteractables.Add("Switch 1",  new Switch(new Vector3(150.0f, 0.0f, -50.0f), mMachine_01));

            Machine mMachine_02 = new Machine(new Vector3(150.0f, 0.0f, -650.0f), true);
            levelInteractables.Add("Machine 2", mMachine_02);
            levelInteractables.Add("Switch 2", new Switch(new Vector3(250.0f, 0.0f, -650.0f), mMachine_02));

            Machine mMachine_03 = new Machine(new Vector3(650.0f, 0.0f, -650.0f), true);
            levelInteractables.Add("Machine 3", mMachine_03);
            levelInteractables.Add("Switch 3", new Switch(new Vector3(550.0f, 0.0f, -650.0f), mMachine_03));

            Machine mMachine_04 = new Machine(new Vector3(650.0f, 0.0f, -150.0f), true);
            levelInteractables.Add("Machine 4", mMachine_04);
            levelInteractables.Add("Switch 4", new Switch(new Vector3(650.0f, 0.0f, -250.0f), mMachine_04));
            #endregion

            mLevel_06 = new Level_06(levelBounds,
                                     levelStart,
                                     0.0f,
                                     levelFinish,
                                     levelInteractables);
        }

        // Initializes Level 07
        private void InitializeLevel_07()
        {
            #region Level 07 Bounds
            Dictionary<String, BoundingBox> levelBounds = new Dictionary<String, BoundingBox>();
            
            levelBounds.Add("Wall #1", new BoundingBox(new Vector3(-600.0f, 0.0f, 595.0f),
                                                            new Vector3(600.0f, 200.0f, 605.0f)));
            levelBounds.Add("Wall #2", new BoundingBox(new Vector3(-605.0f, 0.0f, -600.0f),
                                                            new Vector3(-595.0f, 200.0f, 600.0f)));
            levelBounds.Add("Wall #3", new BoundingBox(new Vector3(595.0f, 0.0f, -600.0f),
                                                            new Vector3(605.0f, 200.0f, 600.0f)));
            
            levelBounds.Add("Wall #4", new BoundingBox(new Vector3(-600.0f, 0.0f, -605.0f),
                                                            new Vector3(-30.0f, 200.0f, -595.0f)));
            levelBounds.Add("Wall #9", new BoundingBox(new Vector3(30.0f, 0.0f, -605.0f),
                                                            new Vector3(600.0f, 200.0f, -595.0f)));

            levelBounds.Add("Lock1", new BoundingBox(new Vector3(-30.0f, 0.0f, -605.0f),
                                                            new Vector3(30.0f, 200.0f, -595.0f)));

            levelBounds.Add("Wall #6", new BoundingBox(new Vector3(-55.0f, 0.0f, -800.0f),
                                                            new Vector3(-45.0f, 200.0f, -600.0f)));
            levelBounds.Add("Wall #7", new BoundingBox(new Vector3(45.0f, 0.0f, -800.0f),
                                                            new Vector3(55.0f, 200.0f, -600.0f)));

            levelBounds.Add("Wall #8", new BoundingBox(new Vector3(-50.0f, 0.0f, -805.0f),
                                                            new Vector3(50.0f, 200.0f, -795.0f)));

            levelBounds.Add("Wall #10", new BoundingBox(new Vector3(-50.0f, 0.0f, -705.0f),
                                                            new Vector3(-30.0f, 200.0f, -695.0f)));
            levelBounds.Add("Wall #11", new BoundingBox(new Vector3(30.0f, 0.0f, -705.0f),
                                                            new Vector3(50.0f, 200.0f, -695.0f)));

            levelBounds.Add("Lock2", new BoundingBox(new Vector3(-30.0f, 0.0f, -705.0f),
                                                            new Vector3(30.0f, 200.0f, -695.0f)));

            #endregion

            #region Level 07 Start
            Vector3 levelStart = new Vector3(0.0f, 35.0f, 0.0f);
            #endregion

            #region Level 07 Finish
            BoundingSphere levelFinish = new BoundingSphere(new Vector3(0.0f, 25.0f, -750.0f),
                                                                25.0f);
            #endregion

            #region Level 07 Interactables
            Dictionary<String, Interactable> levelInteractables = new Dictionary<String, Interactable>();

            levelInteractables.Add("Key1", new Key(new Vector3(0.0f, 10.0f, -300.0f)));
            levelInteractables.Add("Lock1", new Lock(new Vector3(0.0f, 0.0f, -600.0f), 0.0f));
            levelInteractables.Add("Key2", new Key(new Vector3(0.0f, 10.0f, -650.0f)));
            levelInteractables.Add("Lock2", new Lock(new Vector3(00.0f, 0.0f, -700.0f), 0.0f));

            Machine mMachine_01 = new Machine(new Vector3(400.0f, 0.0f, 400.0f), true);
            levelInteractables.Add("Machine 1", mMachine_01);
            levelInteractables.Add("Switch 1", new Switch(new Vector3(500.0f, 0.0f, 500.0f), mMachine_01));

            Machine mMachine_02 = new Machine(new Vector3(400.0f, 0.0f, -400.0f), true);
            levelInteractables.Add("Machine 2", mMachine_02);
            levelInteractables.Add("Switch 2", new Switch(new Vector3(500.0f, 0.0f, -500.0f), mMachine_02));

            Machine mMachine_03 = new Machine(new Vector3(-400.0f, 0.0f, 400.0f), true);
            levelInteractables.Add("Machine 3", mMachine_03);
            levelInteractables.Add("Switch 3", new Switch(new Vector3(-500.0f, 0.0f, 500.0f), mMachine_03));

            Machine mMachine_04 = new Machine(new Vector3(-400.0f, 0.0f, -400.0f), true);
            levelInteractables.Add("Machine 4", mMachine_04);
            levelInteractables.Add("Switch 4", new Switch(new Vector3(-500.0f, 0.0f, -500.0f), mMachine_04));
            #endregion

            mLevel_07 = new Level_07(levelBounds,
                                     levelStart,
                                     0.0f,
                                     levelFinish,
                                     levelInteractables);
        }
    }
}