using UnityEngine;
using UnityEngine.AI;

public class EnemyEngine : MonoBehaviour
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
        
        /*
 Mathf.Clamp(TimeOutTime, 0, TimeOutTime);
 if (_engine.velocity < 0.1 && TimeOutTime > 0 && distance > 1)
     TimeOutTime -= Time.deltaTime;
 else TimeOutTime = .1f;

 if(TimeOutTime <= 0f)
     _stuck = true;
 */
        
        /*if(_stuck && !stunned)
        {
            RandomRoam();
            if(isPlayerDetected)
                isPlayerDetected = false;
            Debug.Log("stuck, setting new destination");
            _stuck = false;
            TimeOutTime = .1f;
        }*/
    }
}
