using UnityEngine;

public class HCLProcessor : MonoBehaviour
{

     public GameObject hydrogenChloride;

    public string CurrentIngredient { get; private set; }

    private void Start()
    {
        if (hydrogenChloride != null)
        {
            hydrogenChloride.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Hydrogen container object not assigned in the inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hydrogen_chloride_raw"))
        {
            if (hydrogenChloride != null)
            {
                hydrogenChloride.SetActive(true);
                        CurrentIngredient = "hydrogen_chloride";
            }

            other.gameObject.SetActive(false);
        }
    }
}
