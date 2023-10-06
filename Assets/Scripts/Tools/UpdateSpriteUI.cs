using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UpdateSpriteUI
{
    public static void Update(GameObject target, Sprite sprite){
        target.GetComponent<Image>().sprite = sprite;
    }
}
