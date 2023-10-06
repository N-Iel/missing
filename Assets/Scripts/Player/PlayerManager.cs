using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("Logic")]
    [SerializeField]
    PlayerDirection direction;
    PlayerMovement movement;

    [SerializeField]
    PlayerParams playerParams;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new PlayerDirection(playerParams.move, true);
        movement = new PlayerMovement(direction, playerParams.movementSpeed);
    }

    void FixedUpdate()
    {
        rb.MovePosition(movement.Get(transform.position));
    }
}
