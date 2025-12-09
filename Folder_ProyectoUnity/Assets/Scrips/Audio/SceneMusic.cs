using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public AudioClip musicClip;

    private void Start()
    {
        if (AudioManager.Instance.musicSource.clip != musicClip)
        {
            AudioManager.Instance.musicSource.clip = musicClip;
            AudioManager.Instance.musicSource.loop = true;
            AudioManager.Instance.PlayMusic();
        }
    }
}
