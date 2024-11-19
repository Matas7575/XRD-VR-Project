using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class ViewObjectName : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tagDisplayText;

    private GameObject textHolder;

    private UnityEngine.UI.Image imageHolder;

    private float defaultOpacity = 0.6f;

    private float raycastDistance = 20f;
    void Start()
    {
        if (textHolder != null)
        {
            imageHolder = textHolder.GetComponentInChildren<UnityEngine.UI.Image>();
        }

        // Optionally, set the default opacity for the image at the start
        SetImageOpacity(defaultOpacity);
    }
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, raycastDistance))
        {
            tagDisplayText.text = "";
            SetImageOpacity(0);
            if (!string.IsNullOrEmpty(hitInfo.collider.tag))
            {
                Debug.Log("Hit object with tag: " + hitInfo.collider.tag);

                if (!hitInfo.collider.CompareTag("Untagged"))
                {
                    tagDisplayText.text = hitInfo.collider.tag;
                    SetImageOpacity(defaultOpacity);
                }
            }
        }
        else
        {
            tagDisplayText.text = "";
            SetImageOpacity(0);
        }
    }

    private void SetImageOpacity(float opacity)
    {
        if (imageHolder != null)
        {
            // Get the current color of the Image component
            Color currentColor = imageHolder.color;

            // Modify the alpha (opacity) value
            currentColor.a = opacity;

            // Apply the new color with the updated alpha (opacity) to the Image
            imageHolder.color = currentColor;
        }
    }
}
