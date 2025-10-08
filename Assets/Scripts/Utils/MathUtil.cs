using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtil
{
    public static float TaxicabDistance(Vector3 vector)
    {
        float suma = 0;
        for (int i = 0; i < 3; i++)
        {
            suma += Mathf.Abs(vector[i]);
        }
        return suma;
    }
}
