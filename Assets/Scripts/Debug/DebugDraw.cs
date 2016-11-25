using UnityEngine;
using System.Collections;

public static class DebugDraw
{
    public static void DrawCircle(Vector3 position, float radius)
    {
        position.z -= 1;
        const int segments = 64;
        float step = Mathf.PI / (segments / 2);
        Vector3 lastPoint = new Vector3(Mathf.Cos(0), Mathf.Sin(0)) * radius + position;

        // Go from step to 2pi
        for (float i = 1; i <= segments; i++)
        {
            float angle = i * step;
            Vector3 currentPoint = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius + position;
            Debug.DrawLine(lastPoint, currentPoint, Color.magenta, 0.01f);
            lastPoint = currentPoint;
        }
    }
}
