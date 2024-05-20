using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using UnityEngine.Events;

public class ActivaRejilla : MonoBehaviour
{
    private Animator anim;
    private bool activa;
    private bool sonidoReproducido; // Variable para controlar si el sonido ya se ha reproducido
    public NavMeshSurface navMeshSurface;
    public AudioClip sonidoActivacion; // Agrega el AudioClip para el sonido de activación
    private AudioSource audioSource; // Referencia al AudioSource para reproducir el sonido

    [Space (10)]
    [SerializeField] private UnityEvent openEvent;
    private bool isOpen = false;

    void Start()
    {
        anim = GetComponent<Animator>();

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

    private void Update()
    {
        if (activa && Input.GetMouseButtonDown(0)) // si el jugador esta dentro y se presiona clic izquierdo, entonces se instancia anim.
        {
            anim.SetTrigger("Abrir");
            navMeshSurface.BuildNavMesh();

            if (!sonidoReproducido) // Verificar si el sonido aún no se ha reproducido
            {
                ReproducirSonidoActivacion(); // Reproducir el sonido de activación
                sonidoReproducido = true; // Marcar que el sonido se ha reproducido
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //si activa = true significa que el Player esta dentro del collider.
        {
            activa = true;
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
