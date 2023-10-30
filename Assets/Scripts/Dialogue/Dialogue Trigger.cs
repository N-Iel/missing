using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header ("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    private bool playerInRange;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;


    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive (false);
    }

    private void Update()
    {
        //si el jugador está en rango y no hay un texto activo
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            //si el jugador pulsa el botón izq del ratón, despliega el texto (provisional)
            if (Input.GetMouseButtonDown(0)) 
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else 
        {
            visualCue.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;

        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;

        }
    }
}
