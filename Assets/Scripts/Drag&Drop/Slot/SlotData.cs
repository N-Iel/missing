using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SlotData : ScriptableObject
{
    [field: SerializeField]
    public Sprite sprite { get; private set; }
}
