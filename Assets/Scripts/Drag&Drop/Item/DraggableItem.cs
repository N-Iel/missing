using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    ItemData itemData;

    Image image;

    public Transform parentAfterDrag { private get; set; }

    #region LifeCycle
    void Awake()
    {
        image = GetComponent<Image>();
        UpdateSpriteUI.Update(gameObject, itemData.background);
    }
    #endregion

    #region Events
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        transform.position = parentAfterDrag.position;
        image.raycastTarget = true;
    }
    #endregion
}
