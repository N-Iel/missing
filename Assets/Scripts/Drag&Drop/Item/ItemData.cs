using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [field: SerializeField]
    public Sprite background { get; private set; }

    [field: SerializeField]
    public Sprite portrait { get; private set; }
}
