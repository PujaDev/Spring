using UnityEngine;
using System.Collections;
using Spine.Unity;
using System.Collections.Generic;

public class BubbleManager : MonoBehaviour {
    public enum BubbleType
    {
        SAY = 0,
        THINK = 1
    }

    public SpriteRenderer bubble_image;
    public SkeletonAnimation skeletonAnim;
    public string[] image_names;
    public List<Sprite> images = new List<Sprite>();
    public BubbleType type;
    public GameObject[] bubbles;

    //Debug
    public bool play = false;

    public void Start()
    {
        bubble_image.sprite = null;
        MinimizeBubbles();
        SwitchBubbles();
    }

    public void MinimizeBubbles()
    {
        for (int i = 0; i < bubbles.Length; i++)
        {
            bubbles[i].GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "grow", false).timeScale = 0f;
        }
    }

    public void SwitchBubbles() {
        skeletonAnim = bubbles[(int)type].GetComponent<SkeletonAnimation>();
    }

	// Use this for initialization
	public void LoadImages () {
        images.Clear();
        for(int i = 0; i < image_names.Length; i++)
            images.Add(Resources.Load<Sprite>(string.Format("Sprites/Bubbles/{0}", image_names[i])));
    }

    public void PlayImages(float seconds = 1f, BubbleType form = BubbleType.SAY)
    {
        type = form;
        SwitchBubbles();

        Spine.TrackEntry entry = skeletonAnim.AnimationState.SetAnimation(0, "grow", false);
        entry.mixDuration = 0f;
        entry.timeScale = 1f;
        skeletonAnim.skeleton.a = 1f;
        LoadImages();
        StartCoroutine(SwitchImagesCoroutine(seconds));
    }

    IEnumerator SwitchImagesCoroutine(float seconds)
    {
        yield return new WaitForSeconds(skeletonAnim.skeleton.data.FindAnimation("grow").duration * 1.2f);

        for (int i = 0; i < images.Count; i++) {
            bubble_image.sprite = images[i];
            yield return new WaitForSeconds(seconds);
        }
        bubble_image.sprite = null;

        skeletonAnim.AnimationState.SetAnimation(0, "disappear", false);

        yield return new WaitForSeconds(skeletonAnim.skeleton.data.FindAnimation("disappear").duration * 0.5f);

        DialogManager.Instance.Next();
    }

    private void Update()
    {
        if (play)
        {
            play = false;
            PlayImages();
        }
    }
}
