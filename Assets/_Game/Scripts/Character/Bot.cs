using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;

    private Vector3 destination;

    //Check diem den cua bot
    public bool IsDestination => Vector3.Distance(destination, Vector3.right * TF.position.x + Vector3.forward * TF.position.z) < 0.1f;

    //protected override void Start()
    //{
    //    base.Start();
    //    ChangeState(new PatrolState());
    //}

    public override void OnInit()
    {
        base.OnInit();
        ChangeAnim(Constants.ANIM_IDLE);
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destination = position;
        destination.y = 0;
        agent.SetDestination(position);
    }

    //Dieu khien luong cua state
    IState<Bot> currentState;

    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay) && currentState != null)
        {
            currentState.OnExecute(this);
            //Check stair
            CanMove(TF.position);
        }
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void StopMoving()
    {
        agent.enabled = false;
    }
}