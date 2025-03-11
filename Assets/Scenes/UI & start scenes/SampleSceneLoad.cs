using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleSceneLoad : MonoBehaviour
{
    public string SampleScene; // Name of the scene to load

    public void LoadScene()
    {
        SceneManager.LoadScene(SampleScene);
    }
}
