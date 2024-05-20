using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Plataforma : MonoBehaviour
{
    public GameObject[] objetosRequeridos; // Los objetos requeridos para activar la plataforma
    public CambioColor cambioColor; // Referencia al script de cambio de color de la lámpara
    public AudioClip sonidoActivacion; // Sonido a reproducir cuando se activa la plataforma

    private List<GameObject> objetosColocados; // Lista de los objetos colocados en la plataforma
    private AudioSource audioSource; // Referencia al AudioSource para reproducir el sonido
    private bool sonidoReproducido = false; // Variable para controlar si el sonido de activación ya se ha reproducido

    [Space(10)]
    [SerializeField] private UnityEvent openEvent;

    void Start()
    {
        objetosColocados = new List<GameObject>();

        // Obtener o agregar el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Agregar un componente AudioSource si no existe
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Verificar si todos los objetos requeridos están colocados encima de la plataforma
        bool objetosCorrectos = VerificarObjetosRequeridos();

        // Si todos los objetos están colocados correctamente y el sonido aún no se ha reproducido, cambiar el color de la lámpara y reproducir el sonido de activación
        if (objetosCorrectos && !sonidoReproducido)
        {
            cambioColor.CambiarColorValido();
            // Reproducir el sonido de activación si está asignado
            if (sonidoActivacion != null)
            {
                audioSource.PlayOneShot(sonidoActivacion);
                sonidoReproducido = true; // Marcar que el sonido se ha reproducido
            }
        }
        else if (!objetosCorrectos) // Si los objetos no están colocados correctamente, restaurar el color predeterminado de la lámpara y restablecer la variable sonidoReproducido
        {
            cambioColor.RestaurarColorPredeterminado();
            sonidoReproducido = false;
        }
    }

    // Método para verificar si todos los objetos requeridos están colocados encima de la plataforma
    public bool VerificarObjetosRequeridos()
    {
        // Verificar si todos los objetos requeridos están en la lista de objetos colocados en la plataforma
        foreach (GameObject objetoRequerido in objetosRequeridos)
        {
            if (!objetosColocados.Contains(objetoRequerido))
            {
                return false;
            }
        }
        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        // Si el objeto que entra en la plataforma está en la lista de objetos requeridos, agregarlo a la lista de objetos colocados
        if (System.Array.Exists(objetosRequeridos, element => element == other.gameObject))
        {
            objetosColocados.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Si el objeto que sale de la plataforma está en la lista de objetos requeridos, removerlo de la lista de objetos colocados
        if (System.Array.Exists(objetosRequeridos, element => element == other.gameObject))
        {
            objetosColocados.Remove(other.gameObject);
        }
    }
}
