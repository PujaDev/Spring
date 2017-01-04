using System;
using System.Collections;
using System.Collections.Generic;

public class ForestSSC : SceneSwitchControler
{
    public enum Direction
    {
        Left = 0,
        Right
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // We just came to ritual site
        if (newState.HubaForest.IsOnSite && (oldState == null || !oldState.HubaForest.IsOnSite))
        {
            string mssg = oldState == null ? "GoSiteSwitch" : "GoSite";
            Flowchart.SendFungusMessage(mssg);
        }
        else if ((newState.HubaForest.IsInForest && (oldState == null || !oldState.HubaForest.IsInForest))                      // We just came to forest
            || (oldState != null && oldState.HubaForest.CurrentForestWay.Count < newState.HubaForest.CurrentForestWay.Count))   // We went further to the forest
        {
            string mssg = oldState == null ? "GoForestSwitch" : "GoForest";
            Flowchart.SendFungusMessage(mssg);
        }
    }

    public static List<int> GenerateNewPath(int depth)
    {
        Random rnd = new Random();
        List<int> path = new List<int>();
        for (int i = 0; i < depth; i++)
        {
            path.Add(rnd.Next((int)Direction.Left, (int)Direction.Right + 1));
        }

        return path;
    }
}
