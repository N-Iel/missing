using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    PlayerDirection dir;
    float speed;

    public PlayerMovement(PlayerDirection _dir, float _speed)
    {
        dir = _dir;
        speed = _speed;
    }

    public Vector2 Get(Vector2 currentPos)
    {
        Vector2 position = currentPos + dir.Get() * speed * Time.deltaTime;
        return position;
    }
}
