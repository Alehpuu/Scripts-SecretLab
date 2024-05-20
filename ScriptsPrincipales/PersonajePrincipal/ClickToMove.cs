using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public LayerMask detectLayer;
    private NavMeshAgent agent;
    private Animator animator; // Referencia al Animator
    public bool movementEnabled = true; // Variable para controlar si el movimiento está habilitado
    public Camera mainCamera; // Referencia a la cámara del jugador principal
    public Camera catCamera; // Referencia a la cámara del gato
    bool cursorOnGround;

    // Variable estática para almacenar la instancia actual de ClickToMove
    public static ClickToMove instance;

    void Start()
    {
        // Asignar la instancia actual a la variable 'instance' cuando se inicializa el objeto
        instance = this;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Inicializar la referencia al Animator
    }

    void Update()
    {
        // Verificar si el movimiento está habilitado
        if (!movementEnabled)
            return;

        // Cambiar el objetivo de la cámara al jugador actual al hacer clic con la rueda del ratón
        if (Input.GetMouseButtonDown(2) && ClickToMoveGato.instance != null)
        {
            // Cambiar a la cámara del gato
            catCamera.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            // Cambiar el target de la cámara al gato
            Camera.main.GetComponent<CameraFollow>().ChangeTarget(ClickToMoveGato.instance.transform);
        }

        // Permitir que el jugador principal se mueva si el gato no está siguiendo al jugador principal
        if (ClickToMoveGato.instance == null || ClickToMoveGato.instance.isFollowingPlayer)
        {
            // Cambiar a la cámara del jugador principal
            mainCamera.gameObject.SetActive(true);
            catCamera.gameObject.SetActive(false);

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                cursorOnGround = Physics.Raycast(camRay, out hit, Mathf.Infinity, detectLayer);

                if (cursorOnGround)
                {
                    // Actualizar la posición del destino del agente
                    agent.SetDestination(hit.point);
                }
            }
        }

        // Actualizar el parámetro 'running' del Animator
        animator.SetBool("running", IsPlayerMoving());
    }

    public bool CursorOnGround()
    {
        return cursorOnGround;
    }

    public bool IsPlayerMoving()
    {
        return agent.velocity.magnitude > 0.1f;
    }
}

