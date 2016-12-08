using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LockController : MonoBehaviour
{
    private ColumnController[] Columns;
    private Coroutine UnlockCr;
    private bool Unlocking;

    void Awake()
    {
        Columns = GetComponentsInChildren<ColumnController>();
    }

    public void Unlock()
    {
        if (Unlocking || AllZero())
            return;

        foreach (var col in Columns)
        {
            col.PrepareToUnlock();
        }

        
        if (UnlockCr != null)
            StopCoroutine(UnlockCr);

        UnlockCr = StartCoroutine(UnlockCoroutine());
    }

    private IEnumerator UnlockCoroutine()
    {
        Unlocking = true;
        bool ready;
        while (true)
        {
            ready = true;
            foreach (var col in Columns)
            {
                ready &= col.ReadyToUnlock;
            }
            if (ready)
                break;

            yield return null;
        }

        // Reset only if the player did not manage to open the lock
        if (!TryToOpen())
        {
            float time = 3f;
            CameraManager.Instance.Shake(time, magnitude: 0.03f);
            while (time > 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            Reset();
        }
        Unlocking = false;
    }

    private bool TryToOpen()
    {
        bool unlocked = true;
        foreach (var col in Columns)
        {
            unlocked &= col.IsCorrect;
        }

        Debug.Log(string.Format("Lock unlocked: {0}", unlocked));
        return unlocked;
    }

    private bool AllZero()
    {
        bool zero = true;
        foreach (var col in Columns)
        {
            zero &= col.IsZero;
        }
        return zero;
    }

    public void Reset()
    {
        foreach (var col in Columns)
        {
            col.Reset();
        }
    }
}
