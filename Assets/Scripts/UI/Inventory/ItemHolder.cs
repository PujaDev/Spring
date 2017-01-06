using UnityEngine;
using System.Collections;
using System;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder Instance { get; private set; }
    public bool IsHolding { get; private set; }

    private int CurrentItemId;

    private GameObject HighlightPrefab;
    private ParticleSystem HighlightEffect;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CurrentItemId = -1;
            transform.position = new Vector3(-5000, -5000, 5000);
            transform.localScale = new Vector3(4f, 4f, 1f);

            // TODO move following condition to resource manager
            if (HighlightPrefab == null)
                HighlightPrefab = Resources.Load<GameObject>("Prefabs/InteractableEffect");

            var highlightGameObject = GameObject.Instantiate(HighlightPrefab);
            highlightGameObject.transform.parent = transform;
            highlightGameObject.transform.position = transform.position;
            highlightGameObject.transform.localScale = new Vector3(1f, 1f, 1f);

            HighlightEffect = highlightGameObject.GetComponent<ParticleSystem>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartHolding(int index, Sprite sprite)
    {
        IsHolding = true;

        // Scale sprite so it looks consistent on screen
        float scale = ComputeScale(sprite);
        transform.localScale = new Vector3(scale, scale, 1f);

        GetComponent<SpriteRenderer>().sprite = sprite;
        CurrentItemId = index;
        gameObject.AddComponent<BoxCollider2D>();
        MoveToCursor();
    }

    public void StopHolding()
    {
        Inventory.Instance.AddItem(CurrentItemId);
        IsHolding = false;
        CurrentItemId = -1;
        transform.position = new Vector3(-5000, -5000, 5000);
        GetComponent<SpriteRenderer>().sprite = null;
        Destroy(gameObject.GetComponent<BoxCollider2D>());

        ToggleHighlight(false);
    }

    void MoveToCursor()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldMousePos.x, worldMousePos.y, -2.1f);
    }

    void Update()
    {
        if (IsHolding)
        {
            // Don't care where we are, right click returns item to inventory
            if (Input.GetMouseButtonDown(1))
            {
                StopHolding();
                return;
            }
            else
            {
                // No effect - item cannot be used
                bool isHighlight = false;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] objects = Physics2D.RaycastAll(ray.origin, ray.direction);

                foreach (var obj in objects)
                {
                    IItemUsable u = obj.collider.gameObject.GetComponent<IItemUsable>();
                    if (u != null)
                    {
                        if (u.CanUseOnSelf(CurrentItemId))
                        {
                            // Effect to show that the item can be used
                            isHighlight = true;

                            if (Input.GetMouseButton(0))
                            {
                                // Stop holding before usage to return item to inventory and then remove it via action
                                // sometimes use on self invokes coroutine move closer - then we would be fine - item returns and is later removed
                                // but other times there is no coroutine involved, item is in hand so action cannot remove it from inventory
                                // and StopHolding just returns it to inventory for no reason

                                int itemId = CurrentItemId;
                                StopHolding();
                                u.UseOnSelf(itemId);
                                return;
                            }
                        }
                    }
                }

                ToggleHighlight(isHighlight);
            }

            MoveToCursor();
        }
    }

    private void ToggleHighlight(bool isOn)
    {
        var em = HighlightEffect.emission;
        em.enabled = isOn;
    }

    private float ComputeScale(Sprite sprite)
    {
        // Size constants
        const float MIN_SIZE = 32f;
        const float MAX_SIZE = 128f;

        // Scale constants
        const float MIN_SCALE = 1f;
        const float MAX_SCALE = 4f;

        float size = Math.Max(sprite.rect.height, sprite.rect.width);

        // Handle edge cases
        // MAX size should be potentially downscaled
        if (size >= MAX_SIZE)
            return MIN_SCALE;
        if (size <= MIN_SIZE)
            return MAX_SCALE;

        float amount = (size - MIN_SIZE) / (MAX_SIZE - MIN_SIZE);
        float scale = Mathf.Lerp(MAX_SCALE, MIN_SCALE, amount);

        return scale;
    }
}
