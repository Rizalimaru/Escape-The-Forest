using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartSceneButton : MonoBehaviour
{
    void Start()
    {
        // Get the button component and add a listener to it
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(RestartScene);
        }
    }

    public void RestartScene()
    {
        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ButtonMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
