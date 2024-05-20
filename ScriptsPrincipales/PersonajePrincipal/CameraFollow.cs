using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;
    public float rotationSpeed = 1f;
    public float minDistance = 2f; // Distancia mínima entre la cámara y el jugador
    public LayerMask ignoreLayer;
    public LayerMask collisionLayer;

    private Vector3 offset;
    private bool isRotating = false;

    void Start()
    {
        // Offset inicial
        offset = transform.position - target.position;
    }

    void Update()
    {
        // Rotación
        if (Input.GetMouseButtonDown(1)) // Botón derecho
        {
            isRotating = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            RotateCamera();
        }
    }

void LateUpdate()
{
    // Seguimiento personaje
    if (target != null)
    {
        Vector3 targetPosition = target.position + offset;

        // Realiza un spherecast desde el jugador a la posición de la cámara, ignorando la capa específica
        RaycastHit hit;
        if (Physics.SphereCast(target.position, minDistance, targetPosition - target.position, out hit, offset.magnitude - minDistance, collisionLayer, QueryTriggerInteraction.Ignore))
        {
            // Si la cámara colisiona con algo, ajusta la posición de la cámara
            targetPosition = hit.point + hit.normal * minDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.RotateAround(target.position, Vector3.up, mouseX);
        offset = Quaternion.Euler(0, mouseX, 0) * offset;
    }

    // Función para cambiar el objetivo de la cámara
    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
        offset = transform.position - target.position;
    }
}
