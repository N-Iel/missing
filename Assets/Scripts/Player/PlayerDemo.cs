using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemo : MonoBehaviour
{
    public float speed,
                 distRaycast;//variable que tendrá en cuenta al chocarse

    float h,
          v;

    Vector2 destination;

    public LayerMask notWalkableLayer;//capa con la que chocará el player

    bool isWalking;

    void Start()
    {
        //inicializo la variable a la posición actual del player
        destination = transform.position;

    }

    void Update()
    {
        //si hay un dialogo activo, no camines
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        PlayerMovement();
        SpeedRunner();
    }

    public void PlayerMovement()
    {
        if (!isWalking)//sino está andando
        {
            isWalking = true;

            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            //si la velocidad horizontal es distinta de 0 y no choca con nada que tenga esa capa
            if (h != 0 && !Physics2D.Raycast(transform.position, h * Vector2.right, distRaycast, notWalkableLayer))
            {
                destination = (Vector2)transform.position + (h * Vector2.right);
            }
            //si la velocidad vertical es distinta de 0 y no choca con nada que tenga esa capa
            else if (v != 0 && !Physics2D.Raycast(transform.position, v * Vector2.up, distRaycast, notWalkableLayer))
            {
                destination = (Vector2)transform.position + (v * Vector2.up);
            }
        }
        else //si está caminando
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            //hacemos que el personaje se mueva por cuadrículas
            if (Vector2.Distance(transform.position, destination) < 0.01f)
            {
                //actualizamos la posicion del jugador en destination y confirmamos que ha dejado de andar
                //(sino lo hacemos puede haber imprecisiones xikitas pero mejor evitarlo)
                transform.position = destination;
                isWalking = false;
            }
        }
    }
    //método de gestion de las animaciones asignando las variables H y V al parametro velocity

    //método para aumentar la velocidad del player x2 al pulsar shift
    private void SpeedRunner()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 2;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1;
        }
    }
}
