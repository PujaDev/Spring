using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LockController : MonoBehaviour
{
    public ToggleLight WrongLight;
    public ToggleLight RightLight;

    private ColumnController[] Columns;
    private Coroutine UnlockCr;
    private bool Unlocking;

    void Awake()
    {
        Columns = GetComponentsInChildren<ColumnController>();
    }

    /// <summary>
    /// Try to unlock the lock
    /// </summary>
    public void Unlock()
    {
        if (Unlocking || IsAllZero())
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
            WrongLight.Toggle(true);
            float time = 3f;
            CameraManager.Instance.Shake(time, magnitude: 0.3f);
            while (time > 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            ForceReset();
        }
        else
        {
            RightLight.Toggle(true);
        }

        Unlocking = false;
    }


    /// <summary>
    /// Check if lock is unlocked and can be open
    /// </summary>
    /// <returns></returns>
    private bool TryToOpen()
    {
        bool unlocked = true;
        foreach (var col in Columns)
        {
            unlocked &= col.IsCorrect;
        }

        // Insert other unlock logic here e.g. dispatch unlock action

        return unlocked;
    }


    /// <summary>
    /// Returns whether all columns are in default state 
    /// </summary>
    /// <returns></returns>
    private bool IsAllZero()
    {
        bool zero = true;
        foreach (var col in Columns)
        {
            zero &= col.IsZero;
        }
        return zero;
    }


    /// <summary>
    /// Resets at all circumstances
    /// </summary>
    private void ForceReset()
    {
        foreach (var col in Columns)
        {
            col.Reset();
        }

        WrongLight.Toggle(false);
        RightLight.Toggle(false);
    }

    /// <summary>
    /// Resets only if user can reset the lock
    /// </summary>
    public void Reset()
    {
        if (Unlocking)
            return;

        ForceReset();
    }
}
