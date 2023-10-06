using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IsometrycVector2
{
    public static Vector2 AdjustIsomertycDir(Vector2 _dir)
    {
        Vector2 dir = _dir;

        if (0.70 <= Mathf.Abs(dir.x) && Mathf.Abs(dir.x) <= 0.71)
            dir = new Vector2(dir.x + (Mathf.Sign(dir.x) * .7f), dir.y).normalized;

        return dir;
    }
}
