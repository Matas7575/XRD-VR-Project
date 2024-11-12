using UnityEngine;

public class CookingStation : MonoBehaviour
{
    public GameObject beakerWater;

    public Color muriaticAcidColor = Color.green;
    public Color causticSodaColor = Color.white;

    public string CurrentIngredient { get; private set; }

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
                        CurrentIngredient = "muriatic_acid";
                    }
                    else if (other.CompareTag("caustic_soda"))
                    {
                        liquidRenderer.material.color = causticSodaColor;
                        CurrentIngredient = "caustic_soda";
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
