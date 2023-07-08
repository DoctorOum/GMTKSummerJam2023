using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;

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
}