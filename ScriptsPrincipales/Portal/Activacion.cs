using UnityEngine;

public class Activacion : MonoBehaviour
{
    public Plataforma[] plataformas; // Referencia a los scripts de las plataformas
    public CambioColor ultimaLampara; // Referencia al script de cambio de color de la última lámpara
    public GameObject portal; // Referencia al portal GameObject que quieres activar
    public AudioClip sonidoActivacionPortal; // Sonido a reproducir cuando se activa el portal

    private bool portalActivado = false; // Variable para controlar si el portal ha sido activado
    private AudioSource audioSource; // Referencia al AudioSource para reproducir el sonido de activación del portal

    void Start()
    {
        // Obtener o agregar el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Agregar un componente AudioSource si no existe
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false; // No reproducir automáticamente al inicio
    }

    void Update()
    {
        // Verificar si todas las plataformas tienen todos los objetos requeridos colocados correctamente
        bool todasPlataformasActivadas = true;
        foreach (Plataforma plataforma in plataformas)
        {
            if (!plataforma.VerificarObjetosRequeridos())
            {
                todasPlataformasActivadas = false;
                break;
            }
        }

        // Si todas las plataformas están activadas, cambiar el color de la última lámpara
        if (todasPlataformasActivadas)
        {
            ultimaLampara.CambiarColorValido();

            // Si la última lámpara ha cambiado de color y el portal no ha sido activado aún
            if (!portalActivado && ultimaLampara.ColorCambiado())
            {
                // Activa el portal
                portal.SetActive(true);
                portalActivado = true; // Marca el portal como activado para evitar activaciones múltiples

                // Reproducir el sonido de activación del portal si está asignado
                if (sonidoActivacionPortal != null)
                {
                    audioSource.PlayOneShot(sonidoActivacionPortal);
                }
            }
        }
        else
        {
            // Restaurar el color predeterminado de la última lámpara si no se cumplen las condiciones
            ultimaLampara.RestaurarColorPredeterminado();

            // Desactivar el portal si no todas las plataformas están activadas
            if (portalActivado)
            {
                portal.SetActive(false);
                portalActivado = false; // Marca el portal como desactivado
            }
        }
    }
}
