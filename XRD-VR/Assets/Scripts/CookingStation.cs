using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CookingStation : MonoBehaviour
{
    [SerializeField] private Transform beakerLocation;
    [SerializeField] private GameObject fireVFX; // Reference to the fire VFX GameObject
    private bool isInsideCollider;
    private GameObject beaker;
    private XRGrabInteractable grabInteractable;
    [SerializeField] private XRKnob knob;
    private IngredientProcessor ingredientProcessor;
    private float amountRemoved = 0.0f;
    [SerializeField] private HCLProcessor hclProcessor;
    private bool shouldRemoveLiquid = true;

    private void Start()
    {
        if (beakerLocation == null)
        {
            beakerLocation = transform.Find("BeakerLocation");
            if (beakerLocation == null)
                Debug.LogWarning("Beaker location not assigned in the inspector.");
        }

        if (fireVFX != null)
        {
            fireVFX.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Fire VFX object not assigned in the inspector.");
        }
    }
    
    private void Update()
    {
        if (knob is null)
            return;
        var amountToRemove = knob.value / 100;
        if (beaker is null || !isInsideCollider)
            return;
        if (ingredientProcessor is null)
            return;
        if (ingredientProcessor.GetFillValue() <= 0.0f)
            return;
        if (!hclProcessor.IsInsideCollider())
            return;
        if (!shouldRemoveLiquid)
            return;
        amountRemoved += amountToRemove;
        ingredientProcessor.
            RemoveIngredient(amountToRemove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("muriatic_acid") || other.CompareTag("caustic_soda"))
        {
            beaker = other.gameObject;
            grabInteractable = beaker.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.selectExited.AddListener(OnGrabReleased);
            }
            ingredientProcessor = beaker.GetComponent<IngredientProcessor>();
            isInsideCollider = true;
            if (fireVFX != null)
            {
                fireVFX.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("muriatic_acid") || other.CompareTag("caustic_soda"))
        {
            beaker = null;
            if (grabInteractable != null)
            {
                grabInteractable.selectExited.RemoveListener(OnGrabReleased);
            }
            grabInteractable = null;
            isInsideCollider = false;   
            if (fireVFX != null)
            {
                fireVFX.SetActive(false);
            }
        }
    }

    private void OnGrabReleased(SelectExitEventArgs args)
    {
        if (beaker != null && isInsideCollider)
        {
            beaker.transform.position = beakerLocation.position;
            beaker.transform.rotation = beakerLocation.rotation;
            if (fireVFX != null)
            {
                fireVFX.SetActive(true);
            }
        }
    }
    
    public float GetAmountRemoved()
    {
        return amountRemoved;
    }
    
    public string GetLiquidType()
    {
        if (beaker is null)
            return "";
        if (beaker.CompareTag("muriatic_acid"))
            return "Muriatic Acid";
        if (beaker.CompareTag("caustic_soda"))
            return "Caustic Soda";
        return "";
    }
    
    public void SetAmountRemoved(float value)
    {
        amountRemoved = value;
    }
    
    public void SetShouldRemoveLiquid(bool value)
    {
        shouldRemoveLiquid = value;
    }
    
    public bool GetShouldRemoveLiquid()
    {
        return shouldRemoveLiquid;
    }
}