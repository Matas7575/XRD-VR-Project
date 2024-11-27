using System.Globalization;
using UnityEngine;
using TMPro;

public class ViewObjectName : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tagDisplayText;

    private GameObject textHolder;

    private float defaultOpacity = 0.6f;

    private float raycastDistance = 20f;
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, raycastDistance))
        {
            tagDisplayText.text = "";
            if (!string.IsNullOrEmpty(hitInfo.collider.tag))
            {
                if (!hitInfo.collider.CompareTag("Untagged") && !hitInfo.collider.CompareTag("WinningPanel"))
                {
                    tagDisplayText.text = ToPrettyTag(hitInfo.collider.tag);
                }
            }
        }
        else
        {
            tagDisplayText.text = "";
        }
    }

    string ToPrettyTag(string tag)
    {
        string[] words = tag.Split('_');
        // Join them back without underscores
        string formattedTag = string.Join(" ", words);
        return formattedTag;
    }
}
