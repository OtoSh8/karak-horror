using UnityEngine;
using UnityEngine.SceneManagement;

public class backMainMenu : MonoBehaviour
{
    public string MainMenu; // Name of the scene to load

    public void LoadScene()
    {
        SceneManager.LoadScene(MainMenu);
    }
}

