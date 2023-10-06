using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Main script for objects capable of holding a draggable item in them
/// </summary>
public class SlotHandler : MonoBehaviour, IDropHandler
{
    [SerializeField]
    SlotData slotData;

    void Awake()
    {
        UpdateSpriteUI.Update(gameObject, slotData.sprite);
    }

    // Make's the draggableItem dropped inside a children object
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 1) return;

        UpdateDraggableParent.setParent(gameObject, eventData.pointerDrag);
    }   
}
