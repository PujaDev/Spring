using UnityEngine;
using System.Collections;

public class Action {

    public readonly ActionType Type;
    public readonly object Payload;

    public Action(ActionType type)
    {
        Type = type;
    }

    public Action(ActionType type, object payload)
    {
        Type = type;
        Payload = payload;
    }

    public override string ToString()
    {
        return string.Format("[ACTION - {0}]", Type);
    }
}