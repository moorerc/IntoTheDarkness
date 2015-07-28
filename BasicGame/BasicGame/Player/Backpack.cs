using System;
using System.Collections.Generic;

namespace EECS494.IntoTheDarkness
{
    partial class Player
    {
        /*
         * Backpack
         * 
         * Last Updated : December 2, 2012
         * v1.0 : Bryan - First implementation.
         * v2.0 : Bryan - Second implementation. I was being stupid.
         * v2.1 : Bryan - Converted to a static class.
         * v2.2 : Bryan - Now integrated with Player class.
         * v2.3 : Bryan - Small changes.
         * 
         * Simple wrapper around Dictionary data type, used as an inventory for
         * Players. Integrated with Player.
         * 
         * Backpack holds <String, Int32> pairs, consisting of an item name and
         * item quantity.
         */

        // Backpack. Container of item name / quantity pairs.
        static public Dictionary<String, Int32> mBackpack = new Dictionary<String, Int32>();


        // Returns true if item is contained in Backpack, false otherwise.
        static public bool HasItem(String itemName)
        {
            return mBackpack.ContainsKey(itemName);
        }


        // Returns the quantity of the given item in Backpack. Throws an
        // Exception if it is not contained in Backpack.
        static public Int32 QuantityItem(String itemName)
        {
            // Backpack doesn't contain given item!
            if (!HasItem(itemName))
                return 0;

            return mBackpack[itemName];
        }


        // Adds specified item to Backpack. Throws an Exception if Backpack
        // already contains item.
        static public void AddItem(String itemName)
        {
            // Backpack already contains item!
            if (HasItem(itemName))
                throw new Exception(itemName + " already contained in Backpack!");

            // Place given quantity of item in Backpack.
            mBackpack.Add(itemName, 1);
        }


        // Adjusts the quantity of the given item by the given amount. Throws
        // an Exception if Backpack doesn't contain item, or adjustment will
        // render the item quantity invalid.
        static public void AdjustQuantity(String itemName, Int32 itemAmount)
        {
            // Backpack doesn't contain item!
            if (!HasItem(itemName))
                throw new Exception(itemName + " not contained in Backpack!");

            mBackpack[itemName] += itemAmount;

            if(mBackpack[itemName] == 0)
                mBackpack.Remove(itemName);
        }
    }
}
