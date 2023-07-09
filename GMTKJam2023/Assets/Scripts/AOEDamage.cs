using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamage : MonoBehaviour
{
    [SerializeField,Tooltip("Only Pick one")]
    private bool Axe, Comet, Ice, Ult;
    private int attachedAbility;
    private void Start()
    {
        if (Axe)
        {
            attachedAbility = 0;
        }
        else if (Comet)
        {
            attachedAbility = 1;
        }
        else if (Ice)
        {
            attachedAbility = 2;
        }
        else if (Ult)
        {
            attachedAbility = 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            other.gameObject.GetComponent<HeroStateMachine>().TakeDamage(BossController.instance.abilities[attachedAbility].damage);
        }
    }
}
