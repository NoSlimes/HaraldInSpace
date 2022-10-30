using UnityEngine;
using UnityEngine.AI;

public class AIEngine : MonoBehaviour
{
    
    static float _velocity;
    public NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _velocity = agent.velocity.magnitude;
    }
}
