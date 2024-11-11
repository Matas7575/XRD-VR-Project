using UnityEngine;

public class CookingStation : MonoBehaviour
{
    // Reference to the Beaker water prop
    public GameObject beakerWater;

    private void Start()
    {
        // Ensure the Beaker water is initially disabled
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
        // Check if the colliding object has the tag "muriatic_acid"
        if (other.CompareTag("muriatic_acid"))
        {
            // Enable the Beaker water prop
            if (beakerWater != null)
            {
                beakerWater.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Beaker water object not assigned in the inspector.");
            }
        }
    }
}
