using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStateMachine : MonoBehaviour
{
    private enum HeroStates { Idle, Healing, Attack, Block, Walk, Roll, Run, Dead };
    private HeroStates currentState;

    [SerializeField]
    private int maxHealth;
    private int currentHealth;

    [SerializeField]
    private int maxStamina;
    private int currentStamina;

    [SerializeField]
    private int maxNumOfHealthPotion;
    private int numOfHealthPotion;

    void Start()
    {
        currentState = HeroStates.Idle;
        currentHealth = maxHealth;
        numOfHealthPotion = maxNumOfHealthPotion;
    }


    void Update()
    {
        switch (currentState)
        {
            case HeroStates.Idle:
                HeroIdle();
                break;
            case HeroStates.Healing:
                HeroHeal();
                break;
            case HeroStates.Attack:
                HeroAttack();
                break;
            case HeroStates.Block:
                HeroBlock();
                break;
            case HeroStates.Walk:
                HeroWalk();
                break;
            case HeroStates.Roll:
                HeroRoll();
                break;
            case HeroStates.Run:
                HeroRun();
                break;
            case HeroStates.Dead:
                Death();
                break;
            default:
                HeroIdle();
                break;
        }
    }

    private void HeroIdle()
    {
        //If dead do death animation

        //Go to walk
    }

    private void HeroWalk()
    {
        //Check Health and decide to go towards boss or away
            //If in range of attack from boss, SWITCH to roll or run, attempt to dodge attack || if Close enough to attack SWITCH to attack
        
            //If low health attempt to get away and heal
                //Check if enough health potion in inventory

    }

    private void HeroBlock()
    {
        //Do Block animation
        //Drain some stamina on impact

        //SWITCH to idle when finished
    }

    private void HeroRoll()
    {
        //Check Stamina if not enough SWITCH to walk

        //pick direction to roll(away from damage most likely)
        //Do anmiation while moving character in direction
        //Drain some stamina on use

        //SWITCH to idle when finished
    }

    private void HeroRun()
    {
        //If zero stamina SWITCH to walk

        //Pick where to run
        //Play run animation
        //Move hero
        //Drain stamina continously

        //When done running SWITCH to walk
    }

    private void HeroAttack()
    {
        //Check Stamina if not enough SWTICH to walk

        //Swing at the boss
        //Drain Stamina

        //Animation done SWITCH to walk
    }

    private void HeroHeal()
    {
        //Use heal potion

        //SWITCH to walk on animation done
    }

    private void Death()
    {
        //Show death animation
    }
}
