using UnityEngine;
using System.Collections;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder Instance { get; private set; }
    public bool IsHolding { get; private set; }

    private int CurrentItemId;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CurrentItemId = -1;
            transform.position = new Vector3(-5000, -5000, 5000);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartHolding(int index, Sprite sprite)
    {
        IsHolding = true;
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
                GetComponent<SpriteRenderer>().color = Color.white;

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
                            GetComponent<SpriteRenderer>().color = Color.yellow;

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
            }

            MoveToCursor();
        }
    }
}
