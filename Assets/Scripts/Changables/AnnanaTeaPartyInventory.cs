using System;
using System.Collections.Generic;
using System.Linq;

public class AnnanaTeaPartyInventory : Inventory
{
    protected override Inventory GetInstance()
    {
        return this;
    }

    // This has to accord to indices in the Unity inspector for inventory
    public enum ItemIds
    {
        None = -1,
        PotEmpty = 0,
        PotColdWater = 1,
        PotHotWater = 2,
        TeaBag = 3
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        var pickedUpItems = newState.AnnanaTeaParty.PickedUpItems;
        // This order matters so we do not end up in state where we should not have certain item but we have it
        // First check states that add items
        #region AddItems

        // See what's been added from the last time
        HashSet<int> addDiff = null;
        if (oldState == null)
        {
            addDiff = pickedUpItems;
        }
        else if (!pickedUpItems.SetEquals(oldState.AnnanaTeaParty.PickedUpItems))
        {
            addDiff = new HashSet<int>(pickedUpItems);
            addDiff.ExceptWith(oldState.AnnanaTeaParty.PickedUpItems);
        }

        // If there are differences
        if (addDiff != null)
        {
            // Add new items
            foreach (var item in addDiff)
            {
                AddItem(item);
            }
        }

        #endregion

        // Then check states that remove items
        #region RemoveItems

        if (oldState == null)
        {
            return;
        }

        // See what's been used from the last time
        var removeDiff = new HashSet<int>( oldState.AnnanaTeaParty.PickedUpItems);
        removeDiff.ExceptWith(pickedUpItems);

        // If there are any differences
        if (removeDiff != null)
        {
            // Remove used items
            foreach (var item in removeDiff)
            {
                RemoveItem(item);
            }
        }

        #endregion
    }

    private HashSet<int> getAllItems()
    {
        var result = new System.Collections.Generic.HashSet<int>();
        foreach (var id in Enum.GetValues(typeof(ItemIds)))
        {
            result.Add((int)id);
        }
        return result;
    }
}
