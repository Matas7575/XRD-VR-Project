using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonVR : MonoBehaviour
{
    public void ResetScene()
    {
        // Get the active scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}