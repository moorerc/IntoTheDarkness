using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace EECS494.IntoTheDarkness
{
    static class SoundMaster
    {
        // class Backpack
        // {

        /*
         * Backpack
         * 
         * Last Updated : December 1, 2012
         * v1.0 : Max - First implementation.
         * 
         * Simple wrapper around Dictionary data type, used as an a master lookup
         * for all sound files
         * 
         * Backpack holds <String, SoundEffect> pairs, consisting of an sound effect
         * name and the SoundEffect. The sound effect will have already loaded
         * the .wav file.
         * 
         * All of the sounds are loaded into this class during the LoadContent
         * function of Game1.
         */

        ///////////////
        // Accessors //
        ///////////////

        // Returns true if item is contained in Backpack, false otherwise.
        static public bool HasSound(String soundName)
        {
            return mSounds.ContainsKey(soundName);
        }

        ///////////////
        // Add Sound //
        ///////////////

        // Adds specified sound to Backpack. Throws an Exception if Backpack
        // already contains item.
        static public void AddSound(String soundName, SoundEffect soundEffect)
        {
            // SoundMaster already contains sound!
            if (HasSound(soundName))
                throw new Exception(soundName + " is already in SoundMaster!");

            // Place sound into SoundMaster
            mSounds.Add(soundName, soundEffect);
        }

        ///////////////
        // Get Sound //
        ///////////////

        // Adds specified sound to SoundMaster. Throws an Exception if SoundMaster
        // already contains sound.
        static public SoundEffect GetSound(String soundName)
        {
            if (!OptionsMenuScreen.playSound)
                return mSounds["button_beep"];

            // SoundMaster doesn't contain sound!
            if (!HasSound(soundName))
                throw new Exception(soundName + " is not in SoundMaster!");

            // Return the sound effect for the given sound name
            return mSounds[soundName];
        }

        ////////////////
        // Attributes //
        ////////////////

        /* SoundMaster */

        // Container of item name / quantity pairs.
        static private Dictionary<String, SoundEffect> mSounds = new Dictionary<String, SoundEffect>();

        // }
    }
}