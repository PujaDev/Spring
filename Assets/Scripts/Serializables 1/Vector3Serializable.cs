using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Serializable version of Vector3 class
/// </summary>
[Serializable]
public struct Vector3S
{
    readonly public float x;
    readonly public float y;
    readonly public float z;

    public Vector3S(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3S(float tmp_x, float tmp_y, float tmp_z)
    {
        x = tmp_x;
        y = tmp_y;
        z = tmp_z;
    }


    public Vector3 GetVector3()
    {  
        return new Vector3(x, y, z);
    }

    public static Vector3S Load(Vector3 vector)
    {
        return new Vector3S(vector);
    }
}
