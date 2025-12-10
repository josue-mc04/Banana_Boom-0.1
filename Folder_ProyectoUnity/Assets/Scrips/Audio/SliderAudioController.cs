using UnityEngine;
using UnityEngine.UI;

public class SliderAudioController : MonoBehaviour
{
    public Slider sliderMaster;
    public Slider sliderMusic;
    public Slider sliderSFX;
    public Slider sliderAmbient;
    public AudioData audioData;

    private void OnEnable()
    {
        sliderMaster.value = audioData.GetMaster();
        sliderMusic.value = audioData.GetMusic();
        sliderSFX.value = audioData.GetSFX();
        sliderAmbient.value = audioData.GetAmbient();

        sliderMaster.onValueChanged.AddListener(audioData.SetMaster);
        sliderMusic.onValueChanged.AddListener(audioData.SetMusic);
        sliderSFX.onValueChanged.AddListener(audioData.SetSFX);
        sliderAmbient.onValueChanged.AddListener(audioData.SetAmbient);
    }

    private void OnDisable()
    {
        sliderMaster.onValueChanged.RemoveListener(audioData.SetMaster);
        sliderMusic.onValueChanged.RemoveListener(audioData.SetMusic);
        sliderSFX.onValueChanged.RemoveListener(audioData.SetSFX);
        sliderAmbient.onValueChanged.RemoveListener(audioData.SetAmbient);
    }
}
