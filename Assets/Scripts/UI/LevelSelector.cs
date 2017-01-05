using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Spine.Unity;

public class LevelSelector : MonoBehaviour {
    public static LevelSelector selector;
    public GameObject loadingPanel;
    public GameObject backInTimePanel;
    public string[] scenes;
    public GameObject[] clocks;
    public int[] timeRanges;
    public int[] timeRangeValues; //in 8 minutes rate (7:04 == 7 * 60 + 4 == 424 => 424/8 = 53), min =  0 (0:00)  max = 180 (12:00) middle = 90
    public int pastIndexToLoad;
    public Image timeFill;
    public int lastTimeRange;

    void Start()
    {
        if (selector == null)
        {
            selector = this;
            lastTimeRange = GameController.Instance.LastPlayedTimeRange;
        }
        else if (selector != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void OnEnable () {
        SkeletonGraphic skeletonGraphic;

        for (int i = 0; i < clocks.Length; i++)
        {
            if (timeRanges[i] < lastTimeRange)
            {
                clocks[i].GetComponentsInChildren<Image>()[1].enabled = true;
            }
            else
            {
                clocks[i].GetComponentsInChildren<Image>()[1].enabled = false;
            }
            int frame = timeRangeValues[timeRanges[i]];
            if (frame < 90)
            {
                skeletonGraphic = clocks[i].GetComponentInChildren<SkeletonGraphic>();
                skeletonGraphic.AnimationState.SetAnimation(0, "clock", false).timeScale = 3f;
                Spine.TrackEntry track = skeletonGraphic.AnimationState.GetCurrent(0);
                track.animationEnd = (frame * 2) / 30f;
            }
            else {
                skeletonGraphic = clocks[i].GetComponentInChildren<SkeletonGraphic>();
                skeletonGraphic.AnimationState.SetAnimation(0, "clock", false).timeScale = 3f;
                Spine.TrackEntry track = skeletonGraphic.AnimationState.AddAnimation(0, "clock", false, 0);
                track.timeScale = 3f;
                track.animationEnd = ((frame - 90) * 2) / 30f;
            }
        }
        timeFill.fillAmount = 0.33f * (lastTimeRange + 1);
	}

    public void GoBackInTime() {
        //discard all progress in the following scenes --TODO--

        lastTimeRange = timeRanges[pastIndexToLoad];
        StateManager.Instance.SetAsLastState(GameController.Instance.stateNums[lastTimeRange]);
        LoadScene(pastIndexToLoad);
    }

    public void LoadScene(int id)
    {
        if (timeRanges[id] < lastTimeRange)
        {
            pastIndexToLoad = id;
            GameObject child = (GameObject)GameObject.Instantiate(backInTimePanel);
            RectTransform parent = (RectTransform)(GetComponentInParent<Canvas>().gameObject.transform);
            child.transform.SetParent(parent, false);
        }
        else
        {
            GameObject child = (GameObject)GameObject.Instantiate(loadingPanel);
            RectTransform parent = (RectTransform)(GetComponentInParent<Canvas>().gameObject.transform);
            child.transform.SetParent(parent, false);
            SceneManager.LoadScene(scenes[id]);
        }
        lastTimeRange = timeRanges[id];
        GameController.Instance.LastPlayedTimeRange = lastTimeRange;
    }


    // Update is called once per frame
    void Update () {
	
	}
}
