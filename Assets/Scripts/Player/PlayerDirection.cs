using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerDirection
{
    InputActionReference move;
    bool isIsometryc;

    public PlayerDirection(InputActionReference _move, bool _isIsometryc)
    {
        isIsometryc = _isIsometryc;
        move = _move;
    }

    public Vector2 Get()
    {
        Vector2 dir = move.action.ReadValue<Vector2>().normalized;

        if (isIsometryc) dir = IsometrycVector2.AdjustIsomertycDir(dir);

        return dir;
    }
}
