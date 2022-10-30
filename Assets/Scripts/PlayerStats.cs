using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerStats : NetworkBehaviour
{
    [SyncVar] public int HP = 10;
    [SyncVar] public int Stamina = 20;
    [SyncVar] public int Berries = 10;
    [SyncVar] public int SpeedMultiplier = 1;

    private void HandleHPUpdated(int oldHP, int newHP)
    {
        HP = newHP;
    }


}
