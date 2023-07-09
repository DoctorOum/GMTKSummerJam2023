using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HeroStateMachine : MonoBehaviour
{
    private enum HeroStates { Idle, Healing, Attack, Block, Walk, Roll, Run, Dead };
    private HeroStates currentState;

    private NavMeshAgent navAgent;

    [SerializeField]
    private int maxHealth;
    private int currentHealth;

    [SerializeField]
    private int maxStamina;
    private int currentStamina;

    [SerializeField]
    private int maxNumOfHealthPotion;
    private int numOfHealthPotion;

    [SerializeField]
    private int attackCost;
    [SerializeField]
    private int rollCost;
    [SerializeField]
    private int blockCost;

    private bool tryToHeal = false;
    private Rigidbody rb;

    void Start()
    {
        currentState = HeroStates.Idle;
        navAgent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        numOfHealthPotion = maxNumOfHealthPotion;

        rb = GetComponent<Rigidbody>();
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

    private void CheckAndSetDestination()
    {
        if (!navAgent.hasPath || navAgent.destination == BossController.instance.transform.position)
        {
            RaycastHit hit;
            int randX = Random.Range(-5, 5);
            int randZ = Random.Range(-5, 5);
            if (Physics.Raycast(transform.position, new Vector3(transform.forward.x + randX, -.5f, transform.forward.z + randZ), out hit, 25))
            {
                if (hit.collider.tag == "AoeAreaObj")
                {   
                    Debug.DrawRay(transform.position, new Vector3(transform.forward.x + randX, -.5f, transform.forward.z + randZ) * hit.distance, Color.red);
                    Debug.Log("Hit aoe area");
                    //Try to set a new destination if raycast hit a aoe area
                    CheckAndSetDestination();
                }
                else
                {
                    Debug.DrawRay(transform.position, new Vector3(transform.forward.x + randX, -.5f, transform.forward.z + randZ) * hit.distance, Color.green);
                    Debug.Log("Didn't hit aoe area");
                    navAgent.SetDestination(hit.point);
                }
            }
        }
        else
        {
            return;
        }
    }

    private void SetRollDestination()
    {
        navAgent.ResetPath();
        rb.Move(new Vector3(transform.position.x + 10, 0, transform.position.y), Quaternion.identity);
    }

    private void HeroIdle()
    {
        Debug.Log("In Idle state");
        //If dead do death animation
        if (currentHealth <= 0)
        {
            currentState = HeroStates.Dead;
        }

        //Go to walk
        currentState = HeroStates.Walk;
    }

    private void HeroWalk()
    {
        Debug.Log("In walk state");
        //Check Health and decide to go towards boss or away
        //Health is greater than quarter or if no more health potions left try fighting
        if (currentHealth > maxHealth * 0.25f || numOfHealthPotion <= 0)
        {
            RaycastHit hit;
            //Checks current floor hero is standing on
            if (Physics.Raycast(transform.position,Vector3.down, out hit, 25, ~LayerMask.NameToLayer("Ground")))
            {
                //Currently standing in a aoe area
                if (hit.collider.tag == "AoeAreaObj")
                {
                    Debug.Log("Standing in AOE area, lot of health");
                    //Try to roll away
                    if (currentStamina >= rollCost)
                    {
                        currentState = HeroStates.Roll;
                    }
                    else if (currentStamina >= blockCost)
                    {
                        //Not enough stamina for roll, have hero Block
                        currentState = HeroStates.Block;
                    }
                    //At least 5 percent of stamina left to increase
                    else if (currentStamina >= maxStamina * .05f)
                    {
                        currentState = HeroStates.Run;
                    }
                    else if (currentState != HeroStates.Roll || currentState != HeroStates.Block || currentState != HeroStates.Run)
                    {
                        //Walk to safe area
                        CheckAndSetDestination();
                    }
                }
                //Not in AOE Area so either attack if close enough or move towards the boss
                else
                {
                    //Check if in range to make own attack
                    if (Vector3.Distance(transform.position, BossController.instance.transform.position) < 5 && currentStamina >= attackCost)
                    {
                        currentState = HeroStates.Attack;
                    }
                    else
                    {
                        //Set destination to boss
                        navAgent.SetDestination(BossController.instance.transform.position);
                    }
                }
            }
        }
        //If low health attempt to get away and heal only if hero has potions in inventory
        else if (currentHealth < maxHealth * 0.25f && numOfHealthPotion > 0)
        {
            //If in range of attack, Switch to roll or run, 
            //Set destination
            RaycastHit hit;
            //Checks current floor hero is standing on
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 25))
            {
                //Currently standing in a aoe area
                if (hit.collider.tag == "AoeAreaObj")
                {
                    Debug.Log("Standing in AOE area, low health");
                    //Try to roll away
                    if (currentStamina >= rollCost)
                    {
                        currentState = HeroStates.Roll;
                    }
                    else if (currentStamina >= blockCost)
                    {
                        //Not enough stamina for roll, have hero Block
                        currentState = HeroStates.Block;
                    }
                    //At least 5 percent of stamina left to increase
                    else if (currentStamina >= maxStamina * .05f)
                    {
                        currentState = HeroStates.Run;
                    }
                    else
                    {
                        //Walk to safe area
                        CheckAndSetDestination();
                    }
                }
                //Not in AOE Area so either heal
                else
                {
                    currentState = HeroStates.Healing;
                }
            }
        }
        else if (currentHealth <= 0)
        {
            currentState = HeroStates.Dead;
        }
    }

    private void HeroBlock()
    {
        Debug.Log("In Block state");
        //TODO: Do Block animation

        //Drain some stamina on impact
        currentStamina -= blockCost;

        //SWITCH to idle when finished
            //TODO: Make sure to check block animation is done
        currentState = HeroStates.Idle;

    }

    private void HeroRoll()
    {
        Debug.Log("In Roll state");

        //pick direction to roll(away from damage most likely)
        SetRollDestination();
        //TODO: Do anmiation while moving character in direction

        //Drain some stamina on use
        currentStamina -= rollCost;

        //SWITCH to Idle when finished
        currentState = HeroStates.Idle;
    }

    private void HeroRun()
    {
        Debug.Log("In run state");

        //Pick where to run and move
        navAgent.speed = navAgent.speed * 1.5f;
        CheckAndSetDestination();
        //TODO: Play run animation

        //Drain stamina continously
        currentStamina -= 1;

        
        //When done running SWITCH to idle
            navAgent.speed = navAgent.speed * .5f;
            currentState = HeroStates.Idle;
    }

    private void HeroAttack()
    {
        Debug.Log("In Attack state");

        //TODO: Animation of Swing at the boss

        //TODO: Deal damage to boss

        //Drain Stamina
        currentStamina -= attackCost;

        //Animation done SWITCH to idle
        currentState = HeroStates.Idle;
    }

    private void HeroHeal()
    {
        Debug.Log("In Heal state");
        //Use heal potion
        numOfHealthPotion -= 1;
        currentHealth += 50;

        //SWITCH to idle on animation done
        currentState = HeroStates.Idle;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Taking {damage} damage, current health is {currentHealth}");

        currentState = HeroStates.Idle;
    }

    private void Death()
    {
        Debug.Log("In Death state");

        //Double checks if health is at zero
        if (currentHealth <= 0)
        {
            //TODO: Show death animation

            //Calls gamemanagers Hero died
            GameManager.instance.OnHeroDeath();
        }
    }
}
