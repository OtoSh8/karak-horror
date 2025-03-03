using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class scr_cutscene : MonoBehaviour
{
    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("SampleScene");
    }

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }
}
