using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberBehaviour : MonoBehaviour
{
    BehaviourTree tree;


    void Start()
    {
        tree = new BehaviourTree();
        Node steal = new Node("Steal Something");
        Node goToDiamond = new Node("Go To Diamond");
        Node goTovan = new Node("Go To Van");

        steal.AddChild(goToDiamond);
        steal.AddChild(goTovan);
        tree.AddChild(steal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
