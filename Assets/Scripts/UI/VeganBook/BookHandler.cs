using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BookHandler : MonoBehaviour
{
    //-- Editor fields --//
    public GameObject Book;
    public float OpenFadeInTime;
    public float TextFadeInTime;
    public GameObject[] LeftPages;
    public GameObject[] RightPages;
    public GameObject NextButton;
    public GameObject PreviousButton;


    //-- Private --//
    private int TotalPages;
    private int CurrentPageIdx;
    /// <summary>
    /// Only false before first openning
    /// </summary>
    private bool WasOpen;
    private SpringAction StopReading;

    private List<Coroutine> FadeIn;


    void Awake()
    {
        if (LeftPages.Length != RightPages.Length)
        {
            throw new System.Exception("Number of left and right pages must be equal.");
        }
        TotalPages = LeftPages.Length;

        StopReading = new SpringAction(ActionType.STOP_READING_VEGAN_BOOK, null, null);
        FadeIn = new List<Coroutine>();
    }


    #region API
    /// <summary>
    /// Initializes the book
    /// </summary>
    public void OpenBook()
    {
        // First time open - initialize
        if (!WasOpen)
        {
            WasOpen = true;

            // First time - show first page
            LeftPages[0].SetActive(true);
            RightPages[0].SetActive(true);

            // Hide the rest
            for (int i = 1; i < LeftPages.Length; i++)
            {
                LeftPages[i].SetActive(false);
            }
            for (int i = 1; i < RightPages.Length; i++)
            {
                RightPages[i].SetActive(false);
            }

            // Can go only forward from the first page
            PreviousButton.SetActive(false);
            NextButton.SetActive(true);
        }

        // Fade in all graphics on current book page
        var graphics = Book.GetComponentsInChildren<Graphic>();
        foreach (var g in graphics)
        {
            if (g.IsActive())
            {
                // Set the graphic transparent
                var col = g.color;
                col.a = 0.0001f; // Don't go from 0, there may be glitches apparently
                g.color = col;

                // Fade it in over time
                FadeIn.Add(StartCoroutine(FadeInVisibleCoroutine(g, OpenFadeInTime)));
            }
        }
    }

    public void CloseBook()
    {
        StopFadeIn();
        StateManager.Instance.DispatchAction(StopReading, GetComponentInParent<IInteractable>());
    }

    // Next and previous page methods are very similar
    // If you change one, you probably want to change the other as well
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

            FadeInActiveTexts();

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

            FadeInActiveTexts();

            NextButton.SetActive(true);
        }

        // We are at the beginning - disable further turning
        if (CurrentPageIdx <= 0)
        {
            CurrentPageIdx = 0;
            PreviousButton.SetActive(false);
        }
    }
    #endregion

    #region Helpers
    IEnumerator FadeInVisibleCoroutine(Graphic g, float duration)
    {
        Color currentColor = g.color;
        currentColor.a = 0f;
        float speed = 1 / duration;

        while (currentColor.a < 1f)
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

    void FadeInActiveTexts()
    {
        var texts = Book.GetComponentsInChildren<Text>();
        foreach (var t in texts)
        {
            if (t.IsActive())
            {
                var col = t.color;
                col.a = 0.0001f;
                t.color = col;
                // We should probably save these coroutines and stop them eventually
                // This is probably not necessary as long as the time is low enough
                StartCoroutine(FadeInVisibleCoroutine(t, TextFadeInTime));
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
        var graphics = Book.GetComponentsInChildren<Graphic>();

        foreach (var g in graphics)
        {
            var col = g.color;
            col.a = 1f;
            g.color = col;
        }
    }
    #endregion
}
