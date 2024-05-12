using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberBehaviour : MonoBehaviour
{
    BehaviourTree tree;
    public GameObject diamond;
    public GameObject van;
    
    NavMeshAgent agent;

    public enum ActionState { IDLE, WORKING};
    ActionState state = ActionState.IDLE;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        tree = new BehaviourTree();
        Node steal = new Node("Steal Something");
        Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
        Leaf goTovan = new Leaf("Go To Van", GoToDiamond);

        steal.AddChild(goToDiamond);
        steal.AddChild(goTovan);
        tree.AddChild(steal);

        tree.PrintTree();

        tree.Process();
    }

    public Node.Status GoToDiamond()
    {
        return GoToLocation(diamond.transform.position);
    }
    public Node.Status GoTovan()
    {
        return GoToLocation(van.transform.position);
    }
    Node.Status GoToLocation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);
        if (state == ActionState.IDLE)
        {
            agent.SetDestination(destination);
            state = ActionState.WORKING;
        }
        else if (Vector3.Distance(agent.pathEndPosition, destination) >= 2)
        {
            state = ActionState.IDLE;
            return Node.Status.FAILURE;
        }
        else if (distanceToTarget < 1)
        {
            state = ActionState.IDLE;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }
    
    void Update()
    {
        
    }
}
