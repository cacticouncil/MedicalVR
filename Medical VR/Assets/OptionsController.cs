using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public string text;
    public TMPro.TextMeshPro subtitles;
    public Toggle subEnabled;
    public Slider textSpeed, textSize;

    public AudioSource sfx;
    public Slider BGMSlider, SFXSlider;

    public Image reticleImage;
    public Material reticleMaterial;
    public GameObject reticle;

    float InitialSFXVolume, InitialBGMVolume;

    void OnEnable()
    {
        //Set initial text
        if (GlobalVariables.subtitles == 0)
        {
            subEnabled.isOn = false;
        }
        else
        {
            subEnabled.isOn = true;
            subtitles.gameObject.SetActive(true);
        }
        textSpeed.value = GlobalVariables.textDelay;
        textSize.value = GlobalVariables.textSize;

        // Set initial volumes
        BGMSlider.value = InitialSFXVolume = PlayerPrefs.GetFloat("InitialSFXVolume", 1);
        SFXSlider.value = InitialBGMVolume = PlayerPrefs.GetFloat("InitialBGMVolume", 1);
    }

    void OnDisable()
    {
        subtitles.gameObject.SetActive(false);
    }

    public void OnEnabledChanged()
    {
        if (subEnabled.isOn)
        {
            subtitles.gameObject.SetActive(true);
        }
        else
        {
            subtitles.gameObject.SetActive(false);
        }
    }

    public void OnSpeedChanged()
    {
        StopAllCoroutines();
        StartCoroutine(Draw());
    }

    public void OnSizeChanged()
    {
        subtitles.fontSize = textSize.value;
    }

    public void OnSFXChanged()
    {
        sfx.volume = SFXSlider.value;
        if (!sfx.isPlaying)
        {
            sfx.Play();
        }
        SoundManager.MaxSFXVolume = SFXSlider.value;
    }

    public void OnBGMChanged()
    {
        SoundManager.MaxBGMVolume = BGMSlider.value;
    }

    public void OnSettingsOK()
    {
        SoundManager.PlaySFX("MenuEnter");

        if (subEnabled.isOn)
            GlobalVariables.subtitles = 1;
        else
            GlobalVariables.subtitles = 0;
        GlobalVariables.textDelay = textSpeed.value;
        GlobalVariables.textSize = textSize.value;

        InitialSFXVolume = SoundManager.MaxSFXVolume;
        InitialBGMVolume = SoundManager.MaxBGMVolume;
        PlayerPrefs.SetFloat("InitialSFXVolume", SoundManager.MaxSFXVolume);
        PlayerPrefs.SetFloat("InitialBGMVolume", SoundManager.MaxBGMVolume);

        reticle.GetComponent<Renderer>().sharedMaterial.color = reticleMaterial.color = reticleImage.color;
    }

    public void Exit()
    {
        SoundManager.MaxSFXVolume = InitialSFXVolume;
        SoundManager.MaxBGMVolume = InitialBGMVolume;
    }

    IEnumerator Draw()
    {
        subtitles.text = "_";
        while (subtitles.text.Length < text.Length)
        {
            yield return new WaitForSeconds(textSpeed.value);
            subtitles.text = subtitles.text.Insert(subtitles.text.Length - 1, text[subtitles.text.Length - 1].ToString());
        }
        yield return new WaitForSeconds(textSpeed.value);
        subtitles.text = text;
    }
}