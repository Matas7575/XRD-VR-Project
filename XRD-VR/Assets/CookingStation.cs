using UnityEngine;

public class CookingStation : MonoBehaviour
{
    public GameObject beakerWater;

    public Color muriaticAcidColor = Color.green;
    public Color causticSodaColor = Color.white;

    private void Start()
    {
        if (beakerWater != null)
        {
            beakerWater.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Beaker water object not assigned in the inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("muriatic_acid") || other.CompareTag("caustic_soda"))
        {
            if (beakerWater != null)
            {
                beakerWater.SetActive(true);

                Renderer liquidRenderer = beakerWater.GetComponent<Renderer>();
                if (liquidRenderer != null)
                {
                    if (other.CompareTag("muriatic_acid"))
                    {
                        liquidRenderer.material.color = muriaticAcidColor;
                    }
                    else if (other.CompareTag("caustic_soda"))
                    {
                        liquidRenderer.material.color = causticSodaColor;
                    }
                }
                else
                {
                    Debug.LogWarning("No texture found");
                }
            }

            other.gameObject.SetActive(false);
        }
    }
}
