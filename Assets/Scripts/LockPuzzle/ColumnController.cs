using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColumnController : MonoBehaviour
{
    public int SwitchCount;
    public int SwitchOffset;
    public int CorrectAnswer;
    public BoxCollider2D ButtonArea;
    public BoxCollider2D ColumnArea;
    public GameObject StepPrefab;
    public GameObject PointerPrefab;

    public bool IsCorrect { get { return CorrectAnswer == CurrentAnswer; } }
    public bool ReadyToUnlock { get { return !Moving; } }
    public bool IsZero { get { return CurrentAnswer == 0; } }

    private List<Switch> Switches;
    private int CurrentAnswer;
    private GameObject Pointer;
    private List<Vector3> StepPositions;
    private Vector3 PtrShift;
    private Coroutine PtrMovement;
    private bool Moving;

    static bool Loaded;
    static Sprite CorrectSelected;
    static Sprite CorrectDeselected;

    void Awake()
    {
        if (!Loaded)
        {
            CorrectSelected = Resources.Load<Sprite>("Sprites/LockPuzzle/green_on");
            CorrectDeselected = Resources.Load<Sprite>("Sprites/LockPuzzle/green_off");
        }

        Switches = new List<Switch>();

        int value = 1;
        var switchPositions = GeneratePositions(ButtonArea, SwitchCount, true);
        for (int i = 0; i < SwitchCount; i++)
        {
            GameObject s = Switch.Create(value, OnClickCallback, switchPositions[i]);
            s.transform.parent = transform;

            var swit = s.GetComponent<Switch>();
            Switches.Add(swit);

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
            {
                rend.sprite = CorrectDeselected;
                var step = rend.GetComponent<Step>();
                step.Selected = CorrectSelected;
                step.Deselected = CorrectDeselected;
            }
            else if (i == 0)
                rend.color = Color.black;
        }
        PtrShift = new Vector3(0.35f, 0);
        Pointer = Instantiate(PointerPrefab, StepPositions[0] + PtrShift, Quaternion.identity) as GameObject;
    }

    void OnClickCallback(bool isOn, int value)
    {
        if (isOn)
        {
            CurrentAnswer |= value;
        }
        else
        {
            CurrentAnswer &= ~value;
        }
    }

    public void Reset()
    {
        foreach (var s in Switches)
        {
            s.Reset();
        }

        CurrentAnswer = 0;
        MovePointer(0, 0.3f);
    }

    public void PrepareToUnlock()
    {
        MovePointer(CurrentAnswer);
    }

    private void MovePointer(int index, float time = 1f)
    {
        if (PtrMovement != null)
            StopCoroutine(PtrMovement);

        PtrMovement = StartCoroutine(MovePointerCoroutine(StepPositions[index] + PtrShift, time));
    }

    IEnumerator MovePointerCoroutine(Vector3 target, float time)
    {
        Moving = true;
        float speed = Vector3.Distance(Pointer.transform.position, target) / time;
        while (Vector3.Distance(Pointer.transform.position, target) > 0.005f)
        {
            Pointer.transform.position = Vector3.MoveTowards(Pointer.transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        Moving = false;
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
