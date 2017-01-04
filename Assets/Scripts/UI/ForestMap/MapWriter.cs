using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class MapWriter : MonoBehaviour
{
    public Text Instructions;

    void Start()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var d in StateManager.Instance.State.HubaForest.CorrectForestWay)
        {
            if (d == (int)ForestSSC.Direction.Left)
            {
                sb.Append("<~ Left\n");
            }
            else
            {
                sb.Append("~> Right\n");
            }
        }

        Instructions.text = sb.ToString();
    }
}
