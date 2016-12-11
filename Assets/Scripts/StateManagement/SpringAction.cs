using UnityEngine;
using System.Collections;

public class SpringAction {

    public readonly string Label;
    public readonly ActionType Type;
    public readonly object Payload;
    public readonly Sprite Icon;

    public SpringAction(ActionType type, string label, Sprite icon)
    {
        Type = type;
        Label = label;
        Icon = icon;
    }

    public SpringAction(ActionType type, string label, Sprite icon, object payload)
    {
        Type = type;
        Payload = payload;
        Label = label;
        Icon = icon;
    }

    public override string ToString()
    {
        return string.Format("[ACTION - {0}]", Type);
    }
}