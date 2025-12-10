using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorEscena : MonoBehaviour
{
    private void OnEnable()
    {
        SceneEvents.OnJugar += CargarJuego;
        SceneEvents.OnVolverMenu += CargarMenu;
        SceneEvents.OnPlayer1Win += CargarEscenaP1;
        SceneEvents.OnPlayer2Win += CargarEscenaP2;
    }
    private void OnDisable()
    {
        SceneEvents.OnJugar -= CargarJuego;
        SceneEvents.OnVolverMenu -= CargarMenu;
        SceneEvents.OnPlayer1Win -= CargarEscenaP1;
        SceneEvents.OnPlayer2Win -= CargarEscenaP2;
    }
    void CargarJuego()
    {
        SceneManager.LoadScene("GamePlay");
    }
    void CargarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    void CargarEscenaP1()
    {
        SceneManager.LoadScene("Win P1");
    }
    void CargarEscenaP2()
    {
        SceneManager.LoadScene("Win P2");
    }
}
