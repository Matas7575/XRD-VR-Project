using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject settingsPanel;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToggleSettingsPanel()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
        else
        {
            Debug.LogWarning("Settings Panel is not assigned.");
        }
    }

    // Function to quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // For testing in Unity editor
        #else
            Application.Quit(); // For actual build
        #endif
    }
}
