using UnityEngine;
using System.Collections;
using System;

public class AnnanaHouseTips : TipManager
{
    protected override string GetTipText(GameState gameState)
    {
        var state = gameState.AnnanaHouse;

        if (!state.AlarmTurnedOff)
            return "Turn the alarm off";

        if (!state.DidReadFridgeNote)
            return "Read the note from the fridge";

        if (!state.IsAddressPickedUp)
            return "Take the address from the fridge";

        if (!state.ReadVeganBook)
            return "Read the cookbook book and remember the correct recipe";

        if (!state.IsEmptyVialPickedUp)
            return "Take empty vial from the cabinet";

        if (!state.IsBerryPickedUp && !state.IsFlowerPickedUp)
            return "Pick up berry & flower from ingredients stash";

        if (!state.IsBerryPickedUp)
            return "Pick up berry from ingredients stash";

        if (!state.IsFlowerPickedUp)
            return "Pick up flower from ingredients stash";

        if (!state.IsBerryUsed && !state.IsFlowerUsed)
            return "Throw berry and flower into boiler";

        if (!state.IsFlowerUsed)
            return "Throw flower into boiler";

        if (!state.IsBerryUsed)
            return "Throw berry into boiler";

        if (!state.IsEmptyVialUsed)
            return "Use vial to retrieve the elixir from the boiler";

        if (!state.IsOutside)
            return "Go to the balcony";

        if (!state.IsElixirUsed && !state.IsAddressUsed)
            return "Give elixir and note with the address to the owl";

        if (!state.IsElixirUsed)
            return "Give vial with the elixir to the owl";

        if (!state.IsAddressUsed)
            return "Give note with the address to the owl";

        if (!state.FlyAway)
            return "Send owl away";


        return ALL_DONE;
    }
}
