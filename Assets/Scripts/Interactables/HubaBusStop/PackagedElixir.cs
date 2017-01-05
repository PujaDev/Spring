using UnityEngine;
using System.Collections;

public class PackagedElixir : IInteractable
{
    public Transform touchDownPoint;
    private SpringAction[] Actions;
    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    protected override void Awake()
    {
        base.Awake();

        Actions = new SpringAction[]
        {
            new SpringAction(ActionType.GET_ELIXIR, "Open package", icons[0], (int)HubaBusInventory.ItemIds.Elixir)
        };
    }
    public void TogglePackage(bool On) {
        GetComponent<Rigidbody2D>().isKinematic = !On;
        GetComponent<Collider2D>().enabled = On;
        GetComponent<SpriteRenderer>().enabled = On;
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.HubaBus.isDelivered
           && (oldState == null || !oldState.HubaBus.isDelivered) && !(newState.HubaBus.PickedUpItems.Contains((int)HubaBusInventory.ItemIds.Elixir) || newState.HubaBus.UsedItems.Contains((int)HubaBusInventory.ItemIds.Elixir)))
        {
            TogglePackage(true);
            transform.position = touchDownPoint.position;
        }
        else {
            TogglePackage(false);
        }
    }
}
