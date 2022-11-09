using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using JetBrains.Annotations;

public class FriendlyController : NetworkBehaviour
{
    [SyncVar][HideInInspector] public Transform followTarget;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 7.5f;
    [SerializeField] private float stopDistance = 1f;
    [SerializeField] private float runDistance = 10f;

    [SerializeField] private float roamerDistance = 10f;

    

    private float speed;
    private AIEngine engine;
    private Animator animator;
    public FriendlyType type;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        engine = GetComponent<AIEngine>();
        speed = walkSpeed;
    }

    [Command]
    public void CMDSetFollowTarget(Transform target)
    {
        followTarget = transform;
    }

    private void FixedUpdate()
    {
        engine.agent.speed = speed;
        engine.agent.stoppingDistance = stopDistance;

        switch (type)
        {
            case FriendlyType.Follower:
                if (followTarget != null)
                {
                    engine.agent.SetDestination(followTarget.position);

                    if (engine.agent.remainingDistance > runDistance)
                    {
                        speed = runSpeed;
                    }
                    else
                    {
                        speed = walkSpeed;
                    }

                    if(engine.agent.velocity != Vector3.zero)
                    {
                        animator.SetBool("Walking", true);
                    }
                    else
                    {
                        animator.SetBool("Walking", false);
                    }

                }
                break;
            case FriendlyType.Partyer:
                animator.SetBool("Dance", true);
                break;
            default:
                {
                    if(engine.agent.remainingDistance <= engine.agent.stoppingDistance)
                    {
                        engine.agent.SetDestination(engine.agent.RandomPosition(roamerDistance));
                    }
                    if (engine.agent.velocity != Vector3.zero)
                    {
                        animator.SetBool("Walking", true);
                    }
                    else
                    {
                        animator.SetBool("Walking", false);
                    }

                    if (engine.agent.remainingDistance > runDistance)
                    {
                        speed = runSpeed;
                    }
                    else
                    {
                        speed = walkSpeed;
                    }
                }
                break;
        }
     
        
        

    }

    private void Update()
    {

    }
    public enum FriendlyType
    {
        Follower,
        Roamer,
        Partyer
    }

}
