using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool gameEnded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDied(int playerID)
    {
        if (gameEnded) return;
        gameEnded = true;

        if (playerID == 1)
            SceneManager.LoadScene("Win P1");
        else
            SceneManager.LoadScene("Win P2");
    }
}
