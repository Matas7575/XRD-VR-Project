using UnityEngine;

public class Process_Ingredients : MonoBehaviour
{
    public CookingStation station1;
    public CookingStation station2;

    public GameObject chemicalA;
    public GameObject chemicalB;

    private void Start()
    {
        if (chemicalA != null) chemicalA.SetActive(false);
        if (chemicalB != null) chemicalB.SetActive(false);
    }

    private void Update()
    {
        CheckIngredients();
    }

    private void CheckIngredients()
    {
        string ingredient1 = station1.CurrentIngredient;
        string ingredient2 = station2.CurrentIngredient;

        if ((ingredient1 == "muriatic_acid" && ingredient2 == "caustic_soda") ||
            (ingredient1 == "caustic_soda" && ingredient2 == "muriatic_acid"))
        {
            // Activate chemicalA if muriatic acid and caustic soda are being cooked
            ActivateResult(chemicalA);
        }
        else
        {
            DeactivateResults();
        }
    }

    private void ActivateResult(GameObject result)
    {
        // Deactivate all results first
        DeactivateResults();

        // Activate the specified result
        if (result != null) result.SetActive(true);
    }

    private void DeactivateResults()
    {
        // Deactivate all potential results
        if (chemicalA != null) chemicalA.SetActive(false);
        if (chemicalB != null) chemicalB.SetActive(false);
    }
}
