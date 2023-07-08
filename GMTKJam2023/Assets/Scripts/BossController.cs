using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    private void OnEnable()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        Destroy(instance);
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
