using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LockController : MonoBehaviour
{
    public int SwitchCount;
    public int SwitchOffset;
    public int CorrectAnswer;
    public BoxCollider2D ButtonArea;

    public bool IsCorrect { get { return CorrectAnswer == CurrentAnswer; } }

    private int CurrentAnswer;

    void Awake()
    {
        int value = 1;
        var positions = GenerateSwitchPositions();
        for (int i = 0; i < SwitchCount; i++)
        {
            GameObject s = Switch.Create(value, ClickCallback, positions[i]);
            s.transform.parent = transform;

            value <<= 1;
        }
    }

    void ClickCallback(bool isOn, int value)
    {
        if(isOn)
        {
            CurrentAnswer |= value;
        }
        else
        {
            CurrentAnswer &= ~value;
        }
        Debug.Log(CurrentAnswer);
    }

    private List<Vector3> GenerateSwitchPositions()
    {
        List<Vector3> positions = new List<Vector3>();

        float offset = ButtonArea.bounds.size.x / (SwitchCount - 1);

        for (int i = 0; i < SwitchCount; i++)
        {
            positions.Add(new Vector3(ButtonArea.bounds.min.x + i * offset, ButtonArea.bounds.center.y, ButtonArea.bounds.center.z));
        }

        return positions;
    }
}
