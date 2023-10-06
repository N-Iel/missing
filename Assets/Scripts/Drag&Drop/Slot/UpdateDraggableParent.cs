using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpdateDraggableParent
{
    public static void setParent(GameObject parent, GameObject child)
    {
        DraggableItem draggableItem = child.GetComponent<DraggableItem>();
        draggableItem.parentAfterDrag = parent.transform;
    }
}
