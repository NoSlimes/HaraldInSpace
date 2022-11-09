using UnityEngine;

public class HatScript : MonoBehaviour
{
    [SerializeField] private Hats hats;
    [SerializeField] private Transform hatHolder;

    private GameObject hat;

    private void SetHat(string hatName)
    {
        for (int i = 0; i < hats.hatArray.Length; i++)
        {
            if(hatName == hats.hatArray[i].hatName)
            {
                hat = Instantiate(hats.hatArray[i].hatPrefab);
                hat.transform.SetParent(this.transform);
            }
        }
    }
}
