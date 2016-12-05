using UnityEngine;
using System.Collections;
using System;

public class LockController : MonoBehaviour
{

    public void Unlock()
    {
        var cols = GetComponentsInChildren<ColumnController>();
        bool unlocked = true;
        foreach (var col in cols)
        {
            unlocked &= col.IsCorrect;
        }

        Debug.Log(string.Format("Lock unlocked: {0}", unlocked));
    }

    public void Reset()
    {
        var cols = GetComponentsInChildren<ColumnController>();
        foreach (var col in cols)
        {
            col.Reset();
        }
    }
}
