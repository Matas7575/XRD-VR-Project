using System;
using UnityEngine;

public class IngredientProcessor : MonoBehaviour
{
    [SerializeField] private Renderer liquidRenderer;
    [SerializeField] private float fillAmountPerSecond = 0.25f;
    [SerializeField] private float fillValue;
    private int liquidType = 0;
    private bool isFilling = false;

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

        fillValue = 0.0f;
    }

    private void FixedUpdate()
    {
        if (isFilling && fillValue < 1.0f && liquidRenderer is not null)
        {
            AddIngredient(liquidType == 1 ? "muriatic_acid" : "caustic_soda");
        }
        if (fillValue >= 1.0f)
        {
            isFilling = false;
            fillValue = 1.0f;
            if (liquidRenderer is not null)
            {
                liquidRenderer.material.SetFloat("_Fill", fillValue);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("muriatic_acid_raw"))
        {
            if(liquidType != 1)
            {
                Debug.Log("Liquid type" + liquidType);
                liquidRenderer.material = Resources.Load<Material>("Materials/Acid");
                fillValue = liquidRenderer.material.GetFloat("_Fill");
                fillValue = 0.0f;
                liquidType = 1;
            }
            isFilling = true;
        }
        else if (other.CompareTag("caustic_soda_raw"))
        {
            if (liquidType != 2)
            {
                liquidRenderer.material = Resources.Load<Material>("Materials/CausticSoda");
                fillValue = liquidRenderer.material.GetFloat("_Fill");
                fillValue = 0.0f;
                liquidType = 2;
            }
            isFilling = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isFilling = false;
    }

    private void AddIngredient(string newTag)
    {
        gameObject.tag = newTag;
        fillValue += fillAmountPerSecond;
        liquidRenderer.material.SetFloat("_Fill", fillValue);
    }
    
    public void RemoveIngredient(float amount)
    {
        if(fillValue > 0.0f)
        {
            fillValue -= amount;
            if (fillValue < 0.0f)
            {
                fillValue = 0.0f;
            }
            liquidRenderer.material.SetFloat("_Fill", fillValue);
        }
    }
    
    public float GetFillValue()
    {
        return fillValue;
    }
}
