using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerObjeto : MonoBehaviour
{
    public GameObject manoPoint;
    private GameObject pickedObject = null;
    private bool isHoldingObject = false;
    public float maxDistance = 2f; // Distancia máxima de recogida

    void Update()
    {
        HandleObjectInteraction();
    }

    void HandleObjectInteraction()
    {
        // Si el jugador está sosteniendo un objeto, permitir soltarlo al hacer clic derecho
        if (isHoldingObject && Input.GetKeyDown(KeyCode.R))
        {
            SoltarObjeto();
            return;
        }

        // Si el jugador no está sosteniendo un objeto y presiona la tecla A, intentar recoger un objeto
        if (!isHoldingObject && Input.GetKeyDown(KeyCode.A))
        {
            RecogerObjeto();
        }
    }

    private void SoltarObjeto()
    {
        if (pickedObject != null)
        {
            Rigidbody pickedRigidbody = pickedObject.GetComponent<Rigidbody>();
            if (pickedRigidbody != null)
            {
                pickedRigidbody.useGravity = true;
                pickedRigidbody.isKinematic = false;
            }

            pickedObject.transform.SetParent(null);
            pickedObject = null;
            isHoldingObject = false;
        }
    }

    private void RecogerObjeto()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camRay, out RaycastHit _hit, maxDistance)) // Limitamos la distancia del raycast
        {
            if (_hit.collider.CompareTag("Objeto") || _hit.collider.CompareTag("Objeto2") || _hit.collider.CompareTag("Objeto3"))
            {
                pickedObject = _hit.collider.gameObject;
                Rigidbody pickedRigidbody = pickedObject.GetComponent<Rigidbody>();
                if (pickedRigidbody != null)
                {
                    pickedRigidbody.useGravity = false;
                    pickedRigidbody.isKinematic = true;
                }
                pickedObject.transform.position = manoPoint.transform.position; // Ajustar la posición donde se coloca el objeto recogido
                pickedObject.transform.SetParent(manoPoint.transform);
                isHoldingObject = true;
            }
        }
    }
}
