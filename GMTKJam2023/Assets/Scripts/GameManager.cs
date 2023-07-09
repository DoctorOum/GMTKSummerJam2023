using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;

    [SerializeField]
    private AudioSource[] audioSources;

    public float audioVolume;
    private float currentAudioVolume;

    private void Start()
    {
        currentAudioVolume = audioVolume;
    }

    private void Update()
    {
        if (currentAudioVolume != audioVolume)
        {
            currentAudioVolume = audioVolume;

            foreach(AudioSource source in audioSources)
            {
                source.volume = currentAudioVolume;
            }
        }
    }

    private void OnEnable()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
    }

    public void OnHeroDeath()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}