using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject MainButtons;
    public GameObject OptionSliders;

    [SerializeField]
    UnityEngine.UI.Slider slider;

    private void Start()
    {

    }

    public void StartGameButtonPressed()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Main"))
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void OptionButtonPressed()
    {
        MainButtons.SetActive(false);
        OptionSliders.SetActive(true);
    }

    public void OptionBackButtonPressed()
    {
        OptionSliders.SetActive(false);
        MainButtons.SetActive(true);
    }

    public void VolumeAdjustment()
    {
        GameManager.instance.audioVolume = slider.value;
    }

    public void ExitButtonPressed()
    {
        GameManager.instance.QuitGame();
    }
}
