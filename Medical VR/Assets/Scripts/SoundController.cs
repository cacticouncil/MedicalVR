using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoundController : MonoBehaviour
{
    public string MainMenu, CellStory, Memory, VirusStory, Dodge, Simon, ATP, Throphy, DestroyCell, DestroyVirus, Cgamp, Strategy, credits;
    public int Samples;

    void Start()
    {
        if (SoundManager.Initialized)
        {
            DestroyImmediate(gameObject);
            return;
        }
        SoundManager.MaxBGMVolume = PlayerPrefs.GetFloat("InitialBGMVolume", 1);
        SoundManager.MaxSFXVolume = PlayerPrefs.GetFloat("InitialSFXVolume", 1);
        DontDestroyOnLoad(gameObject);
        SoundManager.Init(gameObject);
        OnSceneLoaded(SceneManager.GetSceneAt(0), LoadSceneMode.Single);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string name = SceneManager.GetActiveScene().name;
        switch (name)
        {
            case "MainMenu":
                if (MainMenu != null)
                {
                    SoundManager.FadeInBGM(MainMenu);
                }
                break;
            case "CellGameplay":
                if (CellStory != null)
                {
                    SoundManager.FadeInBGM(CellStory);
                }
                break;
            case "DestroyTheCell":
                if (DestroyCell != null)
                {
                    SoundManager.FadeInBGM(DestroyCell);
                }
                break;
            case "DodgeAntibodies":
                if (Dodge != null)
                {
                    SoundManager.FadeInBGM(Dodge);
                }
                break;
            case "FightVirus":
                if (DestroyVirus != null)
                {
                    SoundManager.FadeInBGM(DestroyVirus);
                }
                break;
            case "MemoryGame":
                if (Memory != null)
                {
                    SoundManager.FadeInBGM(Memory);
                }
                break;
            case "VirusGameplay":
                if (VirusStory != null)
                {
                    SoundManager.FadeInBGM(VirusStory);
                }
                break;
            case "SimonDNA":
                if (Simon != null)
                {
                    SoundManager.FadeInBGM(Simon);
                }
                break;
            case "Strategy":
                if (Strategy != null)
                {
                    SoundManager.FadeInBGM(Strategy);
                }
                break;
            case "TrophyRoom":
                if (Throphy != null)
                {
                    SoundManager.FadeInBGM(Throphy);
                }
                break;
            case "CGampSnatcher":
                if (Cgamp != null)
                {
                    SoundManager.FadeInBGM(Cgamp);
                }
                break;
            case "ATPGTPShooter":
                if (ATP != null)
                {
                    SoundManager.FadeInBGM(ATP);
                }
                break;
            case "Credits":
                if (credits != null)
                {
                    SoundManager.FadeInBGM(credits);
                }
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SoundManager.Update();
        Samples = SoundManager.BGM.timeSamples;
    }
}
