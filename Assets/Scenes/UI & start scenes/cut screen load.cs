using UnityEngine;
using UnityEngine.SceneManagement;

public class cutscreenload: MonoBehaviour
{
    public string cutScene; // Name of the scene to load

    public void LoadScene()
    {
        SceneManager.LoadScene(cutScene);
    }
}
