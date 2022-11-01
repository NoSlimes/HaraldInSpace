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
    [SerializeField] private float LerpScaleDurationHarvest = 0.5f;
    [SerializeField] private float LerpScaleDurationRegrow = 0.5f;

    private float regrowTimeRemaining;

    private Vector3 berriesScale;
    private Vector3 berriesScaleZero = new Vector3(0, 0, 0);

    private void Start()
    {
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

            RpcHarvest(regrowTimeRemaining);
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
        Debug.Log(harvestable);
    }

    [ClientRpc]
    private void RpcHarvest(float regrowTime)
    {
        StartCoroutine(HarvestCooldown(regrowTime));
        StartCoroutine(ScaleBerries(berriesScale, berriesScaleZero, LerpScaleDurationHarvest));
       // berries.SetActive(false);
    }

    [ClientRpc]
    private void RpcRegrow()
    {
        //RpcScaleBerriesLerp(berriesScaleZero, berriesScale);
        //berries.SetActive(true);
        StartCoroutine(ScaleBerries(berriesScaleZero, berriesScale, LerpScaleDurationRegrow));
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

    private IEnumerator ScaleBerries(Vector3 startValue, Vector3 endValue, float LerpDuration)
    {
        float timeElapsed = 0;
        while (timeElapsed < LerpDuration)
        {

            berries.transform.localScale = Vector3.Lerp(startValue, endValue, timeElapsed / LerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        berries.transform.localScale = endValue;
        

    }

    #endregion

}
