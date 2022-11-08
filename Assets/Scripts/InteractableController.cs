using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class InteractableController : NetworkBehaviour
{
    [SerializeField] private float interactableRange=5f;
    [SerializeField] private LayerMask InteractableLayer;
    //[SerializeField] private GameObject Cursor;

    public Transform rayOrigin;
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }


    private void Update()
    {
        if(!hasAuthority) { return; }

        bool interact = Input.GetButtonDown("Interact");
        Debug.DrawRay(rayOrigin.position, rayOrigin.transform.forward * interactableRange, Color.white);

        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out RaycastHit hit, interactableRange, InteractableLayer))
        {
            if (interact)
            {
                string hitTag = hit.transform.tag;

                switch (hitTag)
                {
                    case "Berrybush":
                        Berrybush berrybush = hit.transform.GetComponent<Berrybush>();
                        berrybush.CmdHarvest();
                        break;
                    case "Door":
                        NetworkDoor networkDoor = hit.transform.GetComponentInParent<NetworkDoor>();
                        networkDoor.ToggleDoor();
                        break;
                    case "Follower":
                        FollowerController controller = hit.transform.GetComponent<FollowerController>();
                        controller.CMDSetFollowTarget(transform);
                        break;
                }
            }
            

        }
        //else Cursor.SetActive(false);
    }
}
