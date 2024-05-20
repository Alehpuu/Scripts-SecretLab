using UnityEngine;
using UnityEngine.SceneManagement;

public class PuntoFinal : MonoBehaviour
{
    [SerializeField] bool goNextLevel = true;
    [SerializeField] float distanceThreshold = 3f; // Umbral de distancia para considerar que el jugador está cerca del portal
    private bool canTeleport = false; // Variable para controlar si el jugador puede teletransportarse

    void Update()
    {
        // Verifica si se presiona el botón de clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0) && canTeleport)
        {
            // Verifica si el jugador está lo suficientemente cerca del portal
            if (goNextLevel)
            {
                PortalController.instance.NextLevel();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Si el objeto que entra en el trigger es el jugador, activa el teletransporte
        if (other.CompareTag("Player"))
        {
            canTeleport = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Si el objeto que sale del trigger es el jugador, desactiva el teletransporte
        if (other.CompareTag("Player"))
        {
            canTeleport = false;
        }
    }
}
