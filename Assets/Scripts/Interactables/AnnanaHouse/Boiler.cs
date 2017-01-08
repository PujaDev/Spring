using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Boiler : IInteractable, IItemUsable
{
    public Color WaterColor;
    public Color SoupColor;
    public GameObject ElixirObject;
    public GameObject ParticlerSystemObject;
    public float RefillSpeed  = 0.004f;
    public float RefillDistance = 0.7f;
    public int RefillWait = 20;
    public float ColorChangeSpeed = 0.01f;
    SpriteRenderer Elixir;
    ParticleSystem ParticleSystem;
    HashSet<int> UsableItems;
    bool isRefilling;
    Color ElixirColor;
    HashSet<int> lastContents;
    

    protected override void Awake()
    {
        base.Awake();
        Elixir = ElixirObject.GetComponent<SpriteRenderer>();
        ParticleSystem = ParticlerSystemObject.GetComponent<ParticleSystem>();
        ColorChangeSpeed = Mathf.Clamp(ColorChangeSpeed, 0.001f, 0.1f);
        UsableItems = new HashSet<int>()
        {
            (int)AnnanaInventory.ItemIds.EmptyVial,
            (int)AnnanaInventory.ItemIds.Flower,
            (int)AnnanaInventory.ItemIds.Berry,
            (int)AnnanaInventory.ItemIds.Leaf
        };
    }

    protected override SpringAction[] GetActionList()
    {
        return new SpringAction[]
        {
            new SpringAction(ActionType.EMPTY_BOILER, "Empty boiler", icons[0])
        };
    }
    
    public bool CanUseOnSelf(int itemId)
    {
        // Check if we can use it and disallow duplicates
        if (UsableItems.Contains(itemId) &&
            !StateManager.Instance.State.AnnanaHouse.BoilerContents.Contains(itemId))
            return true;
        return false;
    }

    public void UseOnSelf(int itemId)
    {
        if (CanUseOnSelf(itemId))
        {
            if (itemId == (int)AnnanaInventory.ItemIds.EmptyVial) // Fill current elixir
            {
                ComeCloser(new SpringAction(ActionType.FILL_ELIXIR, "", null));
            }
            else // Add ingredient
            {
                ComeCloser(new SpringAction(ActionType.THROW_TO_BOILER, "", null, itemId));
            }
        }
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {

        var contents = newState.AnnanaHouse.BoilerContents;
        lastContents = contents;

        // play "refill" animation if boiler state is reset
        if (contents.Count == 0 &&
            oldState != null &&
            contents != oldState.AnnanaHouse.BoilerContents)
            StartCoroutine("Refill");

        var defaultColor = new Color(0.55f, 0.55f, 0.55f, 1);
        Color color = defaultColor;

        // calculate new color from ingredients

        if (contents.Contains((int)AnnanaInventory.ItemIds.Flower))
            color.r = 0.8f;

        if (contents.Contains((int)AnnanaInventory.ItemIds.Berry))
            color.b = 0.8f;

        if (contents.Contains((int)AnnanaInventory.ItemIds.Leaf))
            color.g = 0.8f;

        // if there are no ingredients and it is start of the game set the elixir color to water
        // if the game is already in progress and we need to set the color to water,
        // we will do so during refill animation
        if (contents.Count == 0 &&
            oldState == null)
        {
            Elixir.color = WaterColor;
            return;
        }

        // if there are way too many ingredients
        if (contents.Count >= 3)
            color = SoupColor;

        // should happen only if game is in progress and we emptied some ingredients,
        // in that case we will set elixir color during refill animation
        if (!color.Equals(defaultColor))
        {
            if(oldState== null)
            {
                Elixir.color = ElixirColor;
            }
            else
            {
                ElixirColor = color;
                StopCoroutine("ChangeElixirColor");
                StartCoroutine("ChangeElixirColor");
            }
        }
    }

    IEnumerator Refill()
    {
        isRefilling = true;
        ParticleSystem.Stop();
        var pos = ElixirObject.transform.position;
        var newY = pos.y;

        // move elixir down
        while (pos.y - newY < RefillDistance)
        {
            ElixirObject.transform.position = new Vector3(pos.x, newY, pos.z);
            newY -= RefillSpeed;
            yield return new WaitForFixedUpdate();
        }

        StopCoroutine("ChangeElixirColor");
        // reset elixir color only if player didn't manage to throw something inside
        // while elixir was refilling
        if (lastContents.Count == 0)
            Elixir.color = WaterColor;

        // wait
        for (int i = 0; i < RefillWait; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        // move elixir up
        while (pos.y > newY)
        {
            ElixirObject.transform.position = new Vector3(pos.x, newY, pos.z);
            newY += RefillSpeed;
            yield return new WaitForFixedUpdate();
        }

        // reset elixir position
        ElixirObject.transform.position = pos;
        isRefilling = false;

        ParticleSystem.Play();
    }

    IEnumerator ChangeElixirColor()
    {
        for (;;)
        {
            var color = Elixir.color;
            var diffR = color.r - ElixirColor.r;
            var r = -Mathf.Sign(diffR) * ColorChangeSpeed;
            var diffG = color.g - ElixirColor.g;
            var g = -Mathf.Sign(color.g - ElixirColor.g) * ColorChangeSpeed;
            var diffB = color.b - ElixirColor.b;
            var b = -Mathf.Sign(color.b - ElixirColor.b) * ColorChangeSpeed;
            Elixir.color = new Color(color.r + r, color.g + g, color.b + b);
            if (
                Mathf.Abs(diffR) < ColorChangeSpeed  &&
                Mathf.Abs(diffG) < ColorChangeSpeed  &&
                Mathf.Abs(diffB) < ColorChangeSpeed
                )
                break;
            yield return new WaitForFixedUpdate();
        }
        Elixir.color = ElixirColor;
    }
}
