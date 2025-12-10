using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorEscena : MonoBehaviour
{
    private void OnEnable()
    {
        SceneEvents.OnJugar += CargarJuego;
        SceneEvents.OnLobby += CargarLobby;
        SceneEvents.OnVolverMenu += CargarMenu;
        SceneEvents.OnPlayer1Win += CargarEscenaP1;
        SceneEvents.OnPlayer2Win += CargarEscenaP2;
        SceneEvents.OnExit += CargarSalida;
    }
    private void OnDisable()
    {
        SceneEvents.OnJugar -= CargarJuego;
        SceneEvents.OnLobby -= CargarJuego;
        SceneEvents.OnVolverMenu -= CargarMenu;
        SceneEvents.OnPlayer1Win -= CargarEscenaP1;
        SceneEvents.OnPlayer2Win -= CargarEscenaP2;
        SceneEvents.OnExit -= CargarSalida;
    }
    void CargarJuego()
    {
        SceneManager.LoadScene("GamePlay");
    }
    void CargarLobby()
    {
        SceneManager.LoadScene("Looby");
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
    void CargarSalida()
    {
        Application.Quit();
        Debug.Log("Salio de banana bugs TT");
    }
}
