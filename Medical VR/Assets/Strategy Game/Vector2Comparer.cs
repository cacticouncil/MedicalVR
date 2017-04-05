using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Vector2Comparer : IEqualityComparer<Vector2>
{
    public bool Equals(Vector2 x, Vector2 y)
    {
        if (x.x == y.x && x.y == y.y)
            return true;
        return false;
    }

    public int GetHashCode(Vector2 x)
    {
        return (int)x.x + (int)x.y * 1000000;
    }
}

