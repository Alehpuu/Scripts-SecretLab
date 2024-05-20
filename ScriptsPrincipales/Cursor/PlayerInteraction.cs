using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    ClickToMove playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<ClickToMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(camRay, out hit))
        {
            Interactable interactable;
            hit.collider.TryGetComponent<Interactable>(out interactable);
            if (interactable != null)
            {
                UIManager.SetCursors(interactable.objectType); // Intenta llamar a SetCursors
            }
            else if (playerMovement.CursorOnGround())
            {

            }
            else
            {

            }
        }
    }
}
