using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotaActiva : MonoBehaviour
{
    public GameObject notaVisual;
    public bool activa;
    public AudioClip sonidoActivacion; // Agrega la variable para el sonido de activación
    private AudioSource audioSource; // Referencia al AudioSource para reproducir el sonido
    private bool sonidoReproducido; // Variable para controlar si el sonido ya se ha reproducido

    [Space(10)]
    [SerializeField] private UnityEvent openEvent;
    private bool isOpen = false;

    void Start()
    {
        // Configurar el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Agregar un AudioSource si no lo hay
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = sonidoActivacion;
        audioSource.playOnAwake = false; // No reproducir automáticamente al inicio
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && activa == true) //si se cumple Player + clic derecho ---> muestra la nota.
        {
            notaVisual.SetActive(true);
            if (!sonidoReproducido) // Verificar si el sonido aún no se ha reproducido
            {
                ReproducirSonidoActivacion(); // Reproducir el sonido de activación
                sonidoReproducido = true; // Marcar que el sonido se ha reproducido
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && activa == true) //si se cumple Player + Esc ---> La nota se deja de mostrar.
        {
            notaVisual.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) //si activa = true significa que el Player esta dentro del collider.
    {
        if (other.CompareTag("Player"))
        {
            activa = true;
            if (!sonidoReproducido) // Verificar si el sonido aún no se ha reproducido
            {
                ReproducirSonidoActivacion(); // Reproducir el sonido de activación
                sonidoReproducido = true; // Marcar que el sonido se ha reproducido
            }
        }
    }

    private void OnTriggerExit(Collider other) // si activa = false, el Player ha salido por lo tanto deja de mostrar la nota.
    {
        if (other.CompareTag("Player"))
        {
            activa = false;
            notaVisual.SetActive(false);
            // Restablecer la variable sonidoReproducido para que el sonido se reproduzca nuevamente cuando el jugador vuelva a entrar
            sonidoReproducido = false;
        }
    }

    private void ReproducirSonidoActivacion()
    {
        if (sonidoActivacion != null && audioSource != null)
        {
            audioSource.Play(); // Reproducir el sonido de activación
        }
    }
}
