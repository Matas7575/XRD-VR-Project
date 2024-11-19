using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{

    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;

    public GameObject cookingLabPrefab; 
    public Transform cookingLabParent; 
    private GameObject currentCookingLab;

     private Vector3 originalPosition; 
    private Quaternion originalRotation; 

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;

        if (currentCookingLab == null)
        {
            currentCookingLab = GameObject.FindWithTag("CookingLab");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed) { 
            button.transform.localPosition = new Vector3 (0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke ();
            sound.Play ();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser) {
            button.transform.localPosition = new Vector3(0, 0.05f, 0);
            onRelease.Invoke ();
            isPressed=false;
        }
    }

     public void ResetLab()
    {
        if (currentCookingLab != null)
        {
            Destroy(currentCookingLab);
        }

        if (cookingLabPrefab != null)
        {
            currentCookingLab = Instantiate(cookingLabPrefab, originalPosition, originalRotation);
            Debug.Log("Cooking Lab has been reset to its original position and rotation.");
        }
        else
        {
            Debug.LogWarning("Cooking Lab Prefab is not assigned.");
        }
    }

}