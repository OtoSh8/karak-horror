using UnityEngine;
using UnityEngine.SceneManagement;

public class gameControlload : MonoBehaviour
{
    public string gameControl; // Name of the scene to load

    public void LoadScene()
    {
        SceneManager.LoadScene(gameControl);
    }
}

