using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LockController : MonoBehaviour
{
    public int SwitchCount;
    public int SwitchOffset;
    public int CorrectAnswer;
    public BoxCollider2D ButtonArea;
    public BoxCollider2D ColumnArea;
    public GameObject StepPrefab;
    public GameObject PointerPrefab;

    public bool IsCorrect { get { return CorrectAnswer == CurrentAnswer; } }

    private int CurrentAnswer;
    private GameObject Pointer;
    private List<Vector3> StepPositions;
    private Vector3 PtrShift;
    private Coroutine PtrMovement;

    void Awake()
    {
        int value = 1;
        var switchPositions = GeneratePositions(ButtonArea, SwitchCount, true);
        for (int i = 0; i < SwitchCount; i++)
        {
            GameObject s = Switch.Create(value, ClickCallback, switchPositions[i]);
            s.transform.parent = transform;

            value <<= 1;
        }

        int stepCount = 1 << SwitchCount;
        StepPositions = GeneratePositions(ColumnArea, stepCount, false);
        for (int i = 0; i < stepCount; i++)
        {
            GameObject s = Instantiate(StepPrefab, StepPositions[i], Quaternion.identity) as GameObject;
            s.transform.parent = transform;
            var rend = s.GetComponent<SpriteRenderer>();

            if (i == CorrectAnswer)
                rend.color = Color.green;
            else if (i == 0)
                rend.color = Color.black;
            else
                rend.color = Color.yellow;
        }
        PtrShift = new Vector3(0.35f, 0);
        Pointer = Instantiate(PointerPrefab, StepPositions[0] + PtrShift, Quaternion.identity) as GameObject;
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
        MovePointer(CurrentAnswer);
    }

    private void MovePointer(int index)
    {
        if (PtrMovement != null)
            StopCoroutine(PtrMovement);

        PtrMovement = StartCoroutine(MovePointerCoroutine(StepPositions[index] + PtrShift));
    }

    IEnumerator MovePointerCoroutine(Vector3 target)
    {
        while(Vector3.Distance(Pointer.transform.position, target) > 0.005f)
        {
            Pointer.transform.position = Vector3.MoveTowards(Pointer.transform.position, target, 0.05f);
            yield return null;
        }
    }

    private List<Vector3> GeneratePositions(BoxCollider2D area, int count, bool horizontal)
    {
        area.enabled = true;
        List<Vector3> positions = new List<Vector3>();

        float offset;
        if (horizontal)
            offset = area.bounds.size.x / (count - 1);
        else
            offset = area.bounds.size.y / (count - 1);

        if (horizontal)
        {
            for (int i = 0; i < count; i++)
            {
                positions.Add(new Vector3(area.bounds.min.x + i * offset, area.bounds.center.y, area.bounds.center.z));
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                positions.Add(new Vector3(area.bounds.center.x, area.bounds.min.y + i * offset, area.bounds.center.z));
            }
        }

        area.enabled = false;
        return positions;
    }
}
