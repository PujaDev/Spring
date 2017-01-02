using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryData
{
    private static InventoryData instance;
    public static InventoryData Instance
    {
        get
        {
            if (instance == null)
                instance = new InventoryData();
            return instance;
        }
    }

    // Put any cross scene inventory data here
    
}
