using UnityEngine;
using System.Collections;

public class PageTurner : MonoBehaviour
{
    public GameObject[] LeftPages;
    public GameObject[] RightPages;
    public GameObject NextButton;
    public GameObject PreviousButton;

    private int TotalPages;
    private int CurrentPageIdx;
    private bool Opened;
    private Action StopReading;

    void Awake()
    {
        if (LeftPages.Length != RightPages.Length)
        {
            throw new System.Exception("Left and right pages' lengths do not match!");
        }
        TotalPages = LeftPages.Length;

        StopReading = new Action(ActionType.STOP_READING_VEGAN_BOOK, null, null);
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
    }

    public void CloseBook()
    {
        StateManager.Instance.DispatchAction(StopReading, GetComponentInParent<IInteractable>());
    }

    public void NextPage()
    {
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
