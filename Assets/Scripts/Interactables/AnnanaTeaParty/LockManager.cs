using UnityEngine;
using System.Collections;

public class LockManager : MonoBehaviour
{
    public GameObject Lock;

    public static LockManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Close();
    }

    public void Close()
    {
        Lock.SetActive(false);
        GameController.Instance.isUI = false;
    }
    public void Open()
    {
        Lock.SetActive(true);
        Lock.GetComponent<LockController>().Initialize();
        GameController.Instance.isUI = true;
    }

    public void Unlock()
    {
        StateManager.Instance.DispatchAction(new SpringAction(ActionType.FRIDGE_UNLOCKED));
        Close();
    }
}
