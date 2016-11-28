using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PageTurner : MonoBehaviour
{
    public float FadeInTime;

    public GameObject[] LeftPages;
    public GameObject[] RightPages;
    public GameObject NextButton;
    public GameObject PreviousButton;

    private int TotalPages;
    private int CurrentPageIdx;
    private bool Opened;
    private Action StopReading;

    private List<Coroutine> FadeIn;

    void Awake()
    {
        if (LeftPages.Length != RightPages.Length)
        {
            throw new System.Exception("Number of left and right pages must be equal.");
        }
        TotalPages = LeftPages.Length;

        StopReading = new Action(ActionType.STOP_READING_VEGAN_BOOK, null, null);
        FadeIn = new List<Coroutine>();
    }

    /// <summary>
    /// Initializes the book
    /// </summary>
    public void OpenBook()
    {
        // First time open - initialize
        if (!Opened)
        {
            Opened = true;

            LeftPages[0].SetActive(true);
            RightPages[0].SetActive(true);

            for (int i = 1; i < LeftPages.Length; i++)
            {
                LeftPages[i].SetActive(false);
            }
            for (int i = 1; i < RightPages.Length; i++)
            {
                RightPages[i].SetActive(false);
            }

            PreviousButton.SetActive(false);
            NextButton.SetActive(true);
        }

        var graphics = GetComponentsInChildren<Graphic>();

        foreach (var g in graphics)
        {
            if (g.IsActive())
            {
                var col = g.color;
                col.a = 0.0001f;
                g.color = col;

                FadeIn.Add(StartCoroutine(FadeInVisibleCoroutine(g, FadeInTime)));
            }
        }
    }

    void StopFadeIn()
    {
        foreach (var c in FadeIn)
        {
            StopCoroutine(c);
        }
        FadeIn.Clear();
    }

    void MakeAllVisible()
    {
        var graphics = GetComponentsInChildren<Graphic>();

        foreach (var g in graphics)
        {
            var col = g.color;
            col.a = 1f;
            g.color = col;
        }
    }

    IEnumerator FadeInVisibleCoroutine(Graphic g, float duration)
    {
        Color currentColor = g.color;
        currentColor.a = 0f;
        float speed = 1 / duration;

        while(currentColor.a < 1f)
        {
            currentColor.a += speed * Time.deltaTime;
            g.color = currentColor;
            yield return null;
        }

        if (currentColor.a > 1f)
        {
            currentColor.a = 1f;
            g.color = currentColor;
        }
    }

    public void CloseBook()
    {
        StopFadeIn();
        StateManager.Instance.DispatchAction(StopReading, GetComponentInParent<IInteractable>());
    }

    public void NextPage()
    {
        StopFadeIn();
        MakeAllVisible();

        // We are not at the end - turn it
        if (CurrentPageIdx < TotalPages - 1)
        {
            LeftPages[CurrentPageIdx].SetActive(false);
            RightPages[CurrentPageIdx].SetActive(false);

            CurrentPageIdx++;

            LeftPages[CurrentPageIdx].SetActive(true);
            RightPages[CurrentPageIdx].SetActive(true);

            PreviousButton.SetActive(true);
        }

        // We are at the end - disable further turning
        if(CurrentPageIdx >= TotalPages - 1)
        {
            CurrentPageIdx = TotalPages - 1;
            NextButton.SetActive(false);
        }
    }

    public void PreviousPage()
    {
        StopFadeIn();
        MakeAllVisible();

        // We are not at the beginning - turn it
        if (CurrentPageIdx > 0)
        {
            LeftPages[CurrentPageIdx].SetActive(false);
            RightPages[CurrentPageIdx].SetActive(false);

            CurrentPageIdx--;

            LeftPages[CurrentPageIdx].SetActive(true);
            RightPages[CurrentPageIdx].SetActive(true);

            NextButton.SetActive(true);
        }

        // We are at the beginning - disable further turning
        if (CurrentPageIdx <= 0)
        {
            CurrentPageIdx = 0;
            PreviousButton.SetActive(false);
        }
    }
}
