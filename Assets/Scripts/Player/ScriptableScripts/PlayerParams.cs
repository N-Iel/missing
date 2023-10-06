using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class PlayerParams: ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed = 1f;

    [Header("Inputs")]
    public InputActionReference move;
    public InputActionReference interact;

}
