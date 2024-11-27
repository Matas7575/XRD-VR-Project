using TMPro;
using Unity.Mathematics.Geometry;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WinningScript : MonoBehaviour
{
    [SerializeField] private CookingStation cookingStation1;
    [SerializeField] private CookingStation cookingStation2;
    [SerializeField] private GameObject trayLiquid;
    [SerializeField] private float minFill = 0.13f;
    [SerializeField] private float maxFill = 0.302f;
    [SerializeField] private Slider ingredient1Slider;
    [SerializeField] private Slider ingredient2Slider;
    [SerializeField] private TextMeshProUGUI ingredient1Value;
    [SerializeField] private TextMeshProUGUI ingredient2Value;
    [SerializeField] private TextMeshProUGUI ingredient1Text;
    [SerializeField] private TextMeshProUGUI ingredient2Text;
    private Renderer liquidRenderer;
    private float fillValue;
    private bool latch = false;
    private GameObject winningPanel;

    void Start()
    {
        liquidRenderer = trayLiquid.GetComponent<Renderer>();
        liquidRenderer.material = Resources.Load<Material>("Materials/BakedProduct");
        fillValue = liquidRenderer.material.GetFloat("_Fill");
        fillValue = 0.13f;
        liquidRenderer.material.SetFloat("_Fill", fillValue);
        winningPanel = GameObject.FindWithTag("WinningPanel");
        winningPanel.SetActive(false);
    }

    void Update()
    {
        if (cookingStation1 is null || cookingStation2 is null)
            return;

        fillValue = 0.13f;
        var station1 = cookingStation1.GetAmountRemoved();
        var station2 = cookingStation2.GetAmountRemoved();

        fillValue = RemapToRange(station1 + station2, 0, 1, minFill, maxFill);
        liquidRenderer.material.SetFloat("_Fill", fillValue);
        if (fillValue >= maxFill && !latch)
        {
            cookingStation1.SetShouldRemoveLiquid(false);
            cookingStation2.SetShouldRemoveLiquid(false);
            ingredient1Value.text = GetPercentageFilled(station1).ToString();
            ingredient2Value.text = GetPercentageFilled(station2).ToString();
            ingredient1Text.text = cookingStation1.GetLiquidType();
            ingredient2Text.text = cookingStation2.GetLiquidType();
            ingredient1Slider.value = station1;
            ingredient2Slider.value = station2;
            winningPanel.SetActive(true);
            latch = true;
        }
    }

    float RemapToRange(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return toMin + ((value - fromMin) * (toMax - toMin) / (fromMax - fromMin));
    }
    
    public float GetPercentageFilled(float value)
    {
        return Mathf.Clamp(value, 0, 1) * 100;
    }

    public void ResetFillValue()
    {
        fillValue = 0.13f;
        liquidRenderer.material.SetFloat("_Fill", fillValue);
        HidePanel();
        latch = false;
    }
    
    void HidePanel()
    {
        cookingStation1.SetAmountRemoved(0);
        cookingStation2.SetAmountRemoved(0);
        cookingStation1.SetShouldRemoveLiquid(true);
        cookingStation2.SetShouldRemoveLiquid(true);
        ingredient1Value.text = "";
        ingredient2Value.text = "";
        ingredient1Text.text = "";
        ingredient2Text.text = "";
        ingredient1Slider.value = 0;
        ingredient2Slider.value = 0;
        winningPanel.SetActive(false);
    }
}