using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Berrybush : NetworkBehaviour
{
    [SyncVar(hook = nameof(HandleHarvestableUpdated))] [SerializeField] bool harvestable = true;

    [Header("Cooldown")]
    [SerializeField] private float regrowTimeSeconds = 10f;
    [SerializeField] private float regrowTimeMinutes = 0f;
    [SerializeField] private float randomMultiplierAmount = 0.2f;

    [Header("Berries")]
    [SerializeField] private int berryAmount = 5;
    [SerializeField] private GameObject berries;
    [SerializeField] private float LerpScaleDuration = 2;

    private float regrowTimeRemaining;

    private Vector3 berriesScale;
    Vector3 endValue = new Vector3(0, 0, 0);

    private void Start()
    {
        harvestable = true;
        berriesScale = berries.transform.localScale;
    }

    #region Server

    [Command(requiresAuthority = false)]
    public void CmdHarvest()
    {
        if (harvestable)
        {
            regrowTimeRemaining = (regrowTimeSeconds + (regrowTimeMinutes * 60)) * (1 + Random.Range(-randomMultiplierAmount, randomMultiplierAmount));
            Debug.Log("Cooldown: " + regrowTimeRemaining);

            RpcHarvest();
            //RpcScaleBerriesLerp(berriesScale, endValue);

            //playerStats.Berries += berryAmount;
            harvestable = false;
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdRegrow()
    {
        RpcRegrow();
        harvestable = true;
    }


#endregion

    #region Client

    private void HandleHarvestableUpdated(bool oldValue, bool newValue)
    {
        harvestable = newValue;
    }

    [ClientRpc]
    private void RpcHarvest()
    {
        StartCoroutine(HarvestCooldown(regrowTimeRemaining));
    }

    [ClientRpc]
    private void RpcRegrow()
    {
        //RpcScaleBerriesLerp(endValue, berriesScale);
        berries.SetActive(true);
    }

    private void Update()
    {

    }

    private IEnumerator HarvestCooldown(float cooldownTime)
    {
        while(cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime;
            yield return null;
        }
        CmdRegrow();
    }


    private void RpcScaleBerriesLerp(Vector3 startValue, Vector3 endValue)
    {
        StartCoroutine(ScaleBerriesLerp(startValue, endValue));
    }

    private IEnumerator ScaleBerriesLerp(Vector3 startValue, Vector3 endValue)
    {
        float timeElapsed = 0;
        while (timeElapsed < LerpScaleDuration)
        {

            berriesScale.x = Mathf.Lerp(startValue.x, endValue.x, timeElapsed / LerpScaleDuration);
            berriesScale.y = Mathf.Lerp(startValue.y, endValue.y, timeElapsed / LerpScaleDuration);
            berriesScale.z = Mathf.Lerp(startValue.z, endValue.z, timeElapsed / LerpScaleDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        berriesScale.x = endValue.x;
        berriesScale.y = endValue.y;
        berriesScale.z = endValue.z;
        

    }

    #endregion

}
