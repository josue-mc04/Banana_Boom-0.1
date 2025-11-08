using UnityEngine;

public class ButtonScene : MonoBehaviour
{
    public enum TipoBoton
    {
        Jugar,
        MenuPrincipal
    }

    public TipoBoton tipo;

    public void EjecutarAccion()
    {
        switch (tipo)
        {
            case TipoBoton.Jugar:
                SceneEvents.Jugar();
                break;

            case TipoBoton.MenuPrincipal:
                SceneEvents.VolverMenu();
                break;

            default:
                Debug.LogWarning("El tipo de boton no esta configurado");
                break;
        }
    }
}
