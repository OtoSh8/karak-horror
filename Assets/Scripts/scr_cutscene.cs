using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class scr_cutscene : MonoBehaviour
{
    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(20f);
        SceneManager.LoadScene("gameControl");
    }

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }
}
