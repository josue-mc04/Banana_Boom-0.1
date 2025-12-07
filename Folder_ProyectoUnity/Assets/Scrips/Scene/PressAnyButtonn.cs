using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyButtonn : MonoBehaviour
{
    private bool keyPressed = false;
    void Update()
    {
        if (!keyPressed  && Input.anyKeyDown)
        {
            keyPressed = true;
            SceneManager.LoadScene("Looby");
        }
    }
}
