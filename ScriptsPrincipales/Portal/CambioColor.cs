using UnityEngine;

public class CambioColor : MonoBehaviour
{
    public Material luzMaterial; // Material que controla el color de la luz de la lámpara
    public Color colorValido; // Color a aplicar cuando los objetos están colocados correctamente

    private Color colorOriginal; // Almacena el color original del material
    private bool colorCambiado = false; // Variable para indicar si el color ha cambiado o no

    void Start()
    {
        if (luzMaterial == null)
        {
            Debug.LogError("El material de la lámpara no está asignado en el Inspector.");
        }
        else
        {
            // Guarda el color original del material
            colorOriginal = luzMaterial.color;
        }
    }

    public void CambiarColorValido()
    {
        if (luzMaterial != null)
        {
            // Asigna el nuevo color al material de la lámpara
            luzMaterial.color = colorValido;
            colorCambiado = true; // Marca el color como cambiado
        }
    }

    public void RestaurarColorPredeterminado()
    {
        if (luzMaterial != null)
        {
            // Restaura el color original del material
            luzMaterial.color = colorOriginal;
            colorCambiado = false; // Marca el color como no cambiado
        }
    }

    public bool ColorCambiado()
    {
        // Devuelve el estado del color cambiado
        return colorCambiado;
    }

    void OnDisable()
    {
        RestaurarColorPredeterminado();
    }
}
