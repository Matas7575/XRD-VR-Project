using UnityEngine;

public class IngredientProcessor : MonoBehaviour
{
    public GameObject flaskLiquid;

    public Color muriaticAcidColor = Color.green;
    public Color causticSodaColor = Color.white;

    private void Start()
    {
        if (GetComponent<Collider>() == null)
    {
        Debug.LogError("No collider attached to the object.");
    }
    else
    {
        Debug.Log("Collider is present and active.");
    }

    if (flaskLiquid != null)
    {
        flaskLiquid.SetActive(false);
    }
    else
    {
        Debug.LogWarning("Flask liquid object not assigned in the inspector.");
    }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a raw ingredient
        if (other.CompareTag("muriatic_acid_raw"))
        {
            ApplyIngredient("muriatic_acid", muriaticAcidColor);
        }
        else if (other.CompareTag("caustic_soda_raw"))
        {
            ApplyIngredient("caustic_soda", causticSodaColor);
        }
    }

    private void ApplyIngredient(string newTag, Color liquidColor)
    {
        gameObject.tag = newTag;

        if (flaskLiquid != null)
        {
            flaskLiquid.SetActive(true);

            Renderer liquidRenderer = flaskLiquid.GetComponent<Renderer>();
            if (liquidRenderer != null)
            {
                liquidRenderer.material.color = liquidColor;
            }
            else
            {
                Debug.LogWarning("issue with texture");
            }
        }
    }
}
