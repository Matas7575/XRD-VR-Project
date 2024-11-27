using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HCLProcessor : MonoBehaviour
{
    private GameObject hydrogenChloride;
    private bool isInsideCollider;
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hydrogen_chloride_raw"))
        {
            hydrogenChloride = other.gameObject;
            rb = hydrogenChloride.GetComponent<Rigidbody>();
            grabInteractable = hydrogenChloride.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.selectExited.AddListener(OnGrabReleased);
            }
            isInsideCollider = true;
            rb.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("hydrogen_chloride_raw"))
        {
            isInsideCollider = false;
        }
    }

    private void OnGrabReleased(SelectExitEventArgs args)
    {
        if (hydrogenChloride != null && isInsideCollider)
        {
            hydrogenChloride.transform.position = transform.position;
            hydrogenChloride.transform.rotation = transform.rotation;
        }
        if (!isInsideCollider)
            rb.isKinematic = false;
    }
    
    public bool IsInsideCollider()
    {
        return isInsideCollider;
    }
}
