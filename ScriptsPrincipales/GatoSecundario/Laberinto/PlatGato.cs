using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;

public class PlatGato : MonoBehaviour
{
    public GameObject[] objetosRequeridos; // objetos requeridos para activar la plataforma
    public Animator puerta;
    public Animator puertaR; 
    private List<GameObject> objetosColocados; // lista de objetos que se deben colocar
    public NavMeshSurface navSurface;

    void Start()
    {
        puerta.SetBool("Abrir", false); // animacion false ---> puerta cerrada
        puertaR.SetBool("AbrirR", false);
        objetosColocados = new List<GameObject>();
    }

    void OnTriggerEnter(Collider other)
    {
        // si el objeto que entra en la plataforma esta en la lista de objetos requeridos, agregarlo a la lista de objetos colocados
        if (System.Array.Exists(objetosRequeridos, element => element == other.gameObject))
        {
            objetosColocados.Add(other.gameObject);
            
            bool objetosCorrectos = VerificarObjetosRequeridos(); // comprueba objetos colocados
            if (objetosCorrectos) {
                
                puerta.SetBool("Abrir", true); // si son correctos, entonces activa la puerta
                puertaR.SetBool("AbrirR", true);
                navSurface.BuildNavMesh();
            }
        }
    }

    //------ Comprueba si estan sobre la plataforma los obejtos
    public bool VerificarObjetosRequeridos()
    {
        
        foreach (GameObject objetoRequerido in objetosRequeridos) // si los objetos requeridos estan en la lista de objetos 
        {
            if (!objetosColocados.Contains(objetoRequerido))
            {
                return false;
            }
        }
        return true;
    }
}
