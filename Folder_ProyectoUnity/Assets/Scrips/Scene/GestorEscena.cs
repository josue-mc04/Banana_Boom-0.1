using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorEscena : MonoBehaviour
{
    private void OnEnable()
    {
        SceneEvents.OnJugar += CargarJuego;
        SceneEvents.OnVolverMenu += CargarMenu;
    }
    private void OnDisable()
    {
        SceneEvents.OnJugar -= CargarJuego;
        SceneEvents.OnVolverMenu -= CargarMenu;
    }
    void CargarJuego()
    {
        SceneManager.LoadScene("GamePlay");
    }
    void CargarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
