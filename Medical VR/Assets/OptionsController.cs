using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    AudioSource sfx;
    public Slider BGMSlider, SFXSlider;
    [SerializeField]
    Selectable[] Elements;
    [SerializeField]
    int ElementIndex;

    public Image reticleImage;
    public Material reticleMaterial;
    public GameObject reticle;

    float InitialSFXVolume, InitialBGMVolume;


    // Use this for initialization
    void Start()
    {
        // Get audio component for the SFX sound test.
        sfx = GetComponent<AudioSource>();
        ElementIndex = 0;

        // Set initial volumes
        BGMSlider.value = InitialSFXVolume = PlayerPrefs.GetFloat("InitialSFXVolume", 1);
        SFXSlider.value = InitialBGMVolume = PlayerPrefs.GetFloat("InitialBGMVolume", 1);
        
        SFXSlider.onValueChanged.AddListener(delegate { OnSFXChanged(SFXSlider.value); });
    }

    // Update is called once per frame
    void Update()
    {
        //Index.text = ResolutionIndex.ToString();
        if (Input.GetKeyUp(KeyCode.Tab) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (ElementIndex - 1 < 0)
            {
                ElementIndex = Elements.Length - 1;
            }
            else
            {
                ElementIndex--;
            }
            Elements[ElementIndex].Select();
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (ElementIndex + 1 >= Elements.Length)
            {
                ElementIndex = 0;
            }
            else
            {
                ElementIndex++;
            }
            Elements[ElementIndex].Select();
        }
    }

    public void OnSFXChanged(float v)
    {
        if (sfx == null)
        {
            return;
        }
        sfx.volume = v;
        if (!sfx.isPlaying)
        {
            sfx.Play();
        }
        SoundManager.MaxSFXVolume = v;
    }

    public void OnBGMChanged(float v)
    {
        SoundManager.MaxBGMVolume = v;
    }

    public void OnSettingsOK()
    {
        SoundManager.PlaySFX("MenuEnter");
        InitialSFXVolume = SoundManager.MaxSFXVolume;
        InitialBGMVolume = SoundManager.MaxBGMVolume;
        PlayerPrefs.SetFloat("InitialSFXVolume", SoundManager.MaxSFXVolume);
        PlayerPrefs.SetFloat("InitialBGMVolume", SoundManager.MaxBGMVolume);
        reticle.GetComponent<Renderer>().material.color = reticleImage.color;
        reticleMaterial.color = reticleImage.color;
    }

    public void Exit()
    {
        SoundManager.MaxSFXVolume = InitialSFXVolume;
        SoundManager.MaxBGMVolume = InitialBGMVolume;
    }
}
