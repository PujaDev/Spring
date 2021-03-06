﻿using UnityEngine;
using System.Collections;

public class ActionWheel : MonoBehaviour
{
    // TODO add comments and unfuck

    public float scale = 0.5f;
    public float iconScale = 2f;
    public static ActionWheel Instance { get; private set; }
    const int MAX_ACTIONS = 6;
    /// <summary>
    /// Index of hovered action, -1 if cancel button, -2 if outside of wheel
    /// </summary>
    private int actionNumber = -1;
    private Sprite[,] spritesIdle;
    private Sprite[,] spritesHover;
    private SpringAction[] actions;
    private IInteractable actionSource;
    private GameObject cancelButton;
    private GameObject[] children;
    private TextMesh actionLabel;

    void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
            Instance.Load();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Load()
    {
        actionLabel = gameObject.transform.GetChild(0).GetComponent<TextMesh>();

        Rescale(scale, iconScale);

        children = new GameObject[0];
        spritesIdle = new Sprite[MAX_ACTIONS + 1, MAX_ACTIONS];
        spritesHover = new Sprite[MAX_ACTIONS + 1, MAX_ACTIONS];

        for (int length = 0; length <= MAX_ACTIONS; length++)
        {
            for (int i = 0; i < length || i == 0; i++)
            {
                spritesIdle[length, i] = Resources.Load<Sprite>(string.Format("Sprites/ActionWheel/{0}{1}", length, i));
                spritesHover[length, i] = Resources.Load<Sprite>(string.Format("Sprites/ActionWheel/{0}{1}h", length, i));
            }
        }

    }

    void OnMouseDown()
    {

        if (actionNumber >= 0)
        {
            //StateManager.Instance.DispatchAction(actions[actionNumber], actionSource);
            actionSource.ComeCloser(actions[actionNumber]);
        }
        killAndBuryChildren();

    }

    void OnMouseExit()
    {
        actionNumber = -2;
        resetHover();
    }

    void OnMouseOver()
    {
        if (actions == null)
            return;
        resetHover();
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var wheelPos = gameObject.transform.position;
        mousePos.z = wheelPos.z;
        var distance = Vector3.Distance(mousePos, wheelPos);
        if (distance < (1.19f * scale) * (1.19f * scale)) // It's a kind of magic
        {
            cancelButton.GetComponent<SpriteRenderer>().sprite = spritesHover[0, 0];
            actionNumber = -1;
        }
        else
        {
            var dx = (mousePos.x - wheelPos.x);
            var dy = (mousePos.y - wheelPos.y);
            var angle = Mathf.Atan2(dx, dy);
            if (angle < 0)
                angle += 2 * Mathf.PI;
            angle += Mathf.PI / actions.Length;
            var n = (int)(angle / (2 * Mathf.PI / actions.Length));
            n %= actions.Length;
            children[n].GetComponent<SpriteRenderer>().sprite = spritesHover[actions.Length, n];
            actionLabel.text = actions[n].Label;
            actionNumber = n;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            actions != null &&
            actionNumber == -2)
        {
            killAndBuryChildren();
        }
    }

    public void ShowActions(SpringAction[] actions, IInteractable actionSource = null)
    {
        killAndBuryChildren();

        this.actions = actions;
        this.actionSource = actionSource;
        var length = actions.Length;

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var wheelPosition = new Vector3(mousePos.x, mousePos.y, -0.5f);
        gameObject.transform.position = wheelPosition;
        children = new GameObject[length];
        SpriteRenderer spriteRenderer;
        for (int i = 0; i < length; i++)
        {
            var child = new GameObject("Action" + i);
            spriteRenderer = child.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = spritesIdle[length, i];
            spriteRenderer.sortingOrder = 6;
            child.transform.parent = gameObject.transform;
            child.transform.position = wheelPosition;
            child.transform.localScale = gameObject.transform.localScale;
            children[i] = child;

            var icon = new GameObject("ActionIcon" + i);
            spriteRenderer = icon.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = actions[i].Icon;
            spriteRenderer.sortingOrder = 7;
            icon.transform.parent = child.transform;
            var ix = Mathf.Sin(i * 2 * Mathf.PI / length) * 1f;
            //           if (i > length / 2)
            //             ix *= -1;
            var iy = Mathf.Cos(i * 2 * Mathf.PI / length) * 1f;
            //            if (i < length / 2)
            //              iy = -1;
            icon.transform.position = wheelPosition + new Vector3(ix, iy)*scale*scale/0.36f;
            icon.transform.localScale = gameObject.transform.localScale * iconScale;
        }

        cancelButton = new GameObject("CancelActionButton");
        spriteRenderer = cancelButton.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = spritesIdle[0, 0];
        spriteRenderer.sortingOrder = 6;
        cancelButton.transform.parent = gameObject.transform;
        cancelButton.transform.position = wheelPosition;
        cancelButton.transform.localScale = gameObject.transform.localScale;
    }

    public void Rescale(float scale, float iconScale)
    {
        this.scale = scale;
        this.iconScale = iconScale;

        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        gameObject.GetComponent<CircleCollider2D>().radius = 4.5f * scale;
    }

    private void resetHover()
    {
        if (actions == null)
            return;

        actionLabel.text = "";

        for (int i = 0; i < children.Length; i++)
        {
            children[i].GetComponent<SpriteRenderer>().sprite = spritesIdle[actions.Length, i];
        }
        cancelButton.GetComponent<SpriteRenderer>().sprite = spritesIdle[0, 0];

    }

    private void killAndBuryChildren()
    {
        for (int i = 0; i < children.Length; i++) // take all of your children
        {
            Destroy(children[i]); // kill them one by one
        }

        Destroy(cancelButton); // kill their mother as well

        gameObject.transform.position = Vector3.left * 500; // flee the crime scene

        actions = null; // deny everything

        actionNumber = -1;
    }
}
