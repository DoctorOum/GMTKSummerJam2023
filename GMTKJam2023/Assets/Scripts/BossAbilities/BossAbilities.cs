using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbilities : MonoBehaviour
{
    public float currentCooldownTime;
    [SerializeField]
    private float maxCooldownTime;

    [SerializeField]
    private UnityEngine.UI.Image cooldownImage;

    public int damage;

    [SerializeField]
    protected GameObject AoeIndicators;

    void Start()
    {
        currentCooldownTime = 0;
        cooldownImage.fillAmount = 0;
    }

    public void ResetCooldown()
    {
        currentCooldownTime = maxCooldownTime;
    }

    public virtual void UseAbility()
    {
        //Reset cooldown
        ResetCooldown();
        
        //Start Timer
        StartCoroutine(TimerStart());
    }

    IEnumerator TimerStart()
    {
        cooldownImage.fillAmount = 1;

        for (int i = 0; i < maxCooldownTime; i++)
        {
            yield return new WaitForSeconds(1);
            currentCooldownTime -= 1;

            cooldownImage.fillAmount = currentCooldownTime / maxCooldownTime;
        }
    }
}
