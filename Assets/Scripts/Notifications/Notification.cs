using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Notification : MonoBehaviour
{
    [field: SerializeField]
    public GameObject target { private get; set; }
    [field: SerializeField]
    public float offScreenThreshold { get; private set; }

    float xAxisCorrection = 1.1f;
    Camera mainCamera;
    GameObject sprite;

    void Start()
    {
        mainCamera = Camera.main;
        sprite = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (target)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            float distanceToTarget = targetDirection.magnitude;
            if (distanceToTarget < offScreenThreshold)
            {
                sprite.SetActive(false);
            }
            else
            {
                Vector3 targetViewportPosition = mainCamera.WorldToViewportPoint(target.transform.position);
                if (targetViewportPosition.z > 0 && targetViewportPosition.x > 0 && targetViewportPosition.x < 1 && targetViewportPosition.y > 0 && targetViewportPosition.y < 1)
                {
                    // The target is on screen, hide the indicator
                    sprite.SetActive(false);
                }
                else
                {
                    // The target is off screen, show the indicator
                    sprite.SetActive(true);

 
                    Vector3 screenEdge = mainCamera.ViewportToWorldPoint(new Vector3(Mathf.Clamp(targetViewportPosition.x, 0.1f, 0.9f), Mathf.Clamp(targetViewportPosition.y, 0.1f, 0.9f), mainCamera.nearClipPlane));
                    transform.position = new Vector3(screenEdge.x * xAxisCorrection, screenEdge.y, 0);
                    Vector3 direction = target.transform.position - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, 0, angle);

                    if (Mathf.Abs(angle) >= 10 || 180 >= Mathf.Abs(angle)) xAxisCorrection = 1.1f; else xAxisCorrection = 0f;
                }
            }
        }
    }
}
