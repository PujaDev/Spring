using System;

public class AnnanaInventory : Inventory
{
    public enum ItemIds
    {
        CrystalBall = 5,
        EmptyVial = 6
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // This order matters so we do not end up in state where we should not have certain item but we have it
        // First check states that add items

        // Add crystal ball
        if (newState.AnnanaHouse.IsCrystalBallPickedUp && (oldState == null || !oldState.AnnanaHouse.IsCrystalBallPickedUp))
        {
            AddItem((int)ItemIds.CrystalBall);
        }

        // Add/remove vials
        if (oldState == null || newState.AnnanaHouse.EmptyVialPickedUpCount != oldState.AnnanaHouse.EmptyVialPickedUpCount)
        {
            AddRemoveVials(newState, oldState);
        }

        // Then check states that remove items

    }

    private void AddRemoveVials(GameState newState, GameState oldState)
    {
        // How many vials were changed
        int newCount = newState.AnnanaHouse.EmptyVialPickedUpCount;
        int oldCount = oldState == null ? 0 : oldState.AnnanaHouse.EmptyVialPickedUpCount;
        int vialCount = newCount - oldCount;

        // Add or remove?
        Action<int> modifyInventory;
        if (vialCount > 0)
            modifyInventory = AddItem;
        else
            modifyInventory = RemoveItem;
        vialCount = Math.Abs(vialCount);

        // Do your job
        for (int i = 0; i < vialCount; i++)
        {
            modifyInventory((int)ItemIds.EmptyVial);
        }
    }
}
