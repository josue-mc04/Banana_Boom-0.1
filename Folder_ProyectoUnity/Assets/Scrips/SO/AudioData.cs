using System;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/AudioData")]
public class AudioData : ScriptableObject
{
    public AudioMixer audioMixer;
    public string masterKeyVolume;
    public string musicKeyVolume;
    public string SfxKeyVolume;
    public string ambientKeyVolume;

    [Range(0f, 1f)] public float master = 1f;
    [Range(0f, 1f)] public float music = 1f;
    [Range(0f, 1f)] public float sfx = 1f;
    [Range(0f, 1f)] public float ambient = 1f;

    public void SetMaster(float value)
    {
        master = value;
        audioMixer.SetFloat(masterKeyVolume, VolumeToDB(value));
    }

    public void SetMusic(float value)
    {
        music = value;
        audioMixer.SetFloat(musicKeyVolume, VolumeToDB(value));
    }

    public void SetSFX(float value)
    {
        sfx = value;
        audioMixer.SetFloat(SfxKeyVolume, VolumeToDB(value));
    }

    public void SetAmbient(float value)
    {
        ambient = value;
        audioMixer.SetFloat(ambientKeyVolume, VolumeToDB(value));
    }

    public float GetMaster()
    {
        audioMixer.GetFloat(masterKeyVolume, out float v);
        return DBtoVolume(v);
    }

    public float GetMusic()
    {
        audioMixer.GetFloat(musicKeyVolume, out float v);
        return DBtoVolume(v);
    }

    public float GetSFX()
    {
        audioMixer.GetFloat(SfxKeyVolume, out float v);
        return DBtoVolume(v);
    }

    public float GetAmbient()
    {
        audioMixer.GetFloat(ambientKeyVolume, out float v);
        return DBtoVolume(v);
    }

    private float VolumeToDB(float f) => Mathf.Clamp(Mathf.Log10(Mathf.Max(f, 0.001f)) * 20f, -80f, 20f);
    private float DBtoVolume(float f) => Mathf.Clamp((float)Math.Pow(10, f / 20f), 0f, 1f);
}
