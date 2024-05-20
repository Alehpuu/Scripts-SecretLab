using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMoveGato : MonoBehaviour
{
    public LayerMask detectLayer;
    private NavMeshAgent agent;
    public bool isFollowingPlayer = true;
    public Transform player; // Se agrega para referencia a la cámara
    public Camera mainCamera; // Referencia a la cámara del jugador principal
    public Camera catCamera; // Referencia a la cámara del gato

    // Variable estática para almacenar la instancia actual de ClickToMoveGato
    public static ClickToMoveGato instance;

    void Start()
    {
        instance = this; // Asignar la instancia actual cuando se inicializa el objeto
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Cambiar entre seguir al jugador principal y moverse independientemente al presionar la rueda del ratón
        if (Input.GetMouseButtonDown(2)) // Botón de la rueda del ratón
        {
            ToggleFollowingPlayer();
        }

        if (isFollowingPlayer)
        {
            mainCamera.gameObject.SetActive(true);
            catCamera.gameObject.SetActive(false);
            FollowPlayer();
        }
        else
        {
            mainCamera.gameObject.SetActive(false);
            catCamera.gameObject.SetActive(true);
            MoveIndependently();
        }

        // Cambiar la posición de la cámara al personaje que se mueve
        if (Input.GetKeyDown(KeyCode.E)) // Botón de la rueda del ratón
        {
            ChangeCameraTarget();
        }
    }

void FollowPlayer()
{
    // Mover hacia la posición del gato
    if (transform != null)
    {
        agent.SetDestination(transform.position);
    }
}

    void MoveIndependently()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Rayo desde la posición del ratón hacia el suelo
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Si el rayo choca con el suelo
            if (Physics.Raycast(camRay, out hit, Mathf.Infinity, detectLayer))
            {
                // Establecer la posición objetivo para moverse
                Vector3 targetPosition = hit.point;

                // Mover al gato hacia la posición objetivo
                agent.SetDestination(targetPosition);
            }
        }
    }
    

    void ToggleFollowingPlayer()
    {
        isFollowingPlayer = !isFollowingPlayer;
    }

    // Función para cambiar la posición de la cámara al personaje que se mueve
    void ChangeCameraTarget()
    {
        Camera.main.GetComponent<CameraFollow>().ChangeTarget(transform);
    }
}
