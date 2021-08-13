using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public StateAI currentState;
    private StateAI idleState;
    private StateAI walkState;
    public Animator animator;
    public static bool npcWalking = false;
    private AIController npcAIController;
    private NavMeshAgent agent;

    void Start()
    {
        idleState = GameObject.FindGameObjectWithTag("Idle").GetComponent<StateAI>();
        walkState = GameObject.FindGameObjectWithTag("Walk").GetComponent<StateAI>();
        npcAIController = GetComponent<AIController>();
        agent = GetComponent<NavMeshAgent>();
        currentState = idleState;
    }

    void Update()
    {
        RunCurrentStateAI();
        if(currentState == idleState)
        {
            animator.SetBool("isWalking", false);
            npcWalking = false;
            npcAIController.GetComponent<AIController>().enabled = false;
            agent.isStopped = true;
        }
        else if(currentState == walkState)
        {
            animator.SetBool("isWalking", true);
            npcWalking = true;
            npcAIController.GetComponent<AIController>().enabled = true;
            agent.isStopped = false;
        }
    }

    private void RunCurrentStateAI()
    {
        // "?" verifica se o estado não é nulo
        StateAI nextState = currentState?.RunCurrentStateAI();

        if(nextState != null)
        {
            //muda para o próximo estado
            SwitchState(nextState);
        }
        else
        {
            SwitchState(idleState);
        }
    }

    private void SwitchState(StateAI nextState)
    {
        currentState = nextState;
    }

}
