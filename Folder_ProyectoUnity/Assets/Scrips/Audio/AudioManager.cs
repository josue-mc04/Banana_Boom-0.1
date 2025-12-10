using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource ambientSource;

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

    public void PlayMusic() => musicSource.Play();
    public void StopMusic() => musicSource.Stop();

    public void PlaySFX() => sfxSource.PlayOneShot(sfxSource.clip);
    public void StopSFX() => sfxSource.Stop();

    public void PlayAmbient() => ambientSource.Play();
    public void StopAmbient() => ambientSource.Stop();
}
