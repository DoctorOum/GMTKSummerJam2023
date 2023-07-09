using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAbility : BossAbilities
{
    public override void UseAbility()
    {
        base.UseAbility();

        //create AoeIndicator
        //AoeIndicator has own timer and deals the damage to hero
        //GameObject currentAoeIndiactor = Instantiate(AoeIndicators, BossController.instance.transform.position, BossController.instance.transform.rotation);
        //Destroy(currentAoeIndiactor, 3);
    }
}
