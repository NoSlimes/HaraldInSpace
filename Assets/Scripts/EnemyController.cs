using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]private float lookDistance = 1f;
    [SerializeField]private float wanderRadius = 100f;
    public static bool isPlayerDetected;
    [SerializeField] GameObject rayOrigin;

    public LayerMask ignore;
    //private Transform _target;


    //bool _stuck = false;
    //[SerializeField]private bool stunned = false;
    private AIEngine _engine;
    void Start()
    {
        //_target = FindObjectOfType<NetworkPlayer>().transform;
        _engine = GetComponent<AIEngine>();

    }

    #region Triggers
    /* 
        //When enemy enters a trigger
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Door"))
            {
                Door door = other.GetComponentInChildren<Door>();
                Animator anim = other.GetComponentInChildren<Animator>(); //Set the animator to the animator of the gameObject the enemy currently is at
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen")) //Checks the state of the animator, returns if the door is open
                    return;

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorClose") | anim.GetCurrentAnimatorStateInfo(0).IsName("Start")) //Checks the state of the animator, opens the door if the door is already closed
                    door.LockedCheck(); 
            }
        }

        //When enemy exits a trigger
       private void OnTriggerExit(Collider other)
        {
            Door door = other.GetComponentInChildren<Door>(); 
            Animator anim = other.GetComponentInChildren<Animator>(); //Set the animator to the animator of the gameObject the enemy currently is at
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorClose")) //Checks the state of the animator, returns if the door is closed
                return;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen")) //Checks the state of the animator, closes the door if the door is already open
                door.LockedCheck();
        }
    */
    #endregion

    private void FixedUpdate()
    {
        
        Vector3 origin = rayOrigin.transform.position;
      //  Vector3 targetPos = _target.position;
       // Vector3 dir = (targetPos - origin);
       // float distance = Vector3.Distance(targetPos, transform.position);
        int layerMask = 1 << 31;
        layerMask = ~layerMask;
        
        //Draws a raycast towards the player
     //   if (Physics.Raycast(origin, dir, out RaycastHit hit, lookDistance, layerMask))
    //    {
     //       if (hit.transform.CompareTag("Player"))
     //       {
      //          _engine.agent.SetDestination(hit.transform.position);
                //if (distance <= _engine.agent.stoppingDistance)
                    //AttackPlayer(); isPlayerDetected = true; Debug.Log("Hit the player!");
     //       }
    //        else isPlayerDetected = false;

       // }

    //    Debug.DrawRay(origin, dir * hit.distance, Color.red);
        
        //Sets the enemy to roam around randomly when player is not hit by the raycast
        if(!isPlayerDetected)
        {
            if (_engine.agent.remainingDistance <= _engine.agent.stoppingDistance)
            {
                RandomRoam();
            }
        }
    }

    void RandomRoam()
    {
        _engine.agent.SetDestination(_engine.agent.RandomPosition(wanderRadius));
    }

}
