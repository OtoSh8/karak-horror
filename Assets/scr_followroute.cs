using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_followroute : MonoBehaviour
{

[SerializeField]
    private List<Transform> routes = new List<Transform>();
    [SerializeField] Transform objplayer;
    public float verticalrange = 20f;

    public bool isMoving = false;

    private int prev;

    private int routeToGo;

    private float tParam;

    private Vector3 objectPosition;

    public float speedModifier;

    private bool coroutineAllowed;

    public bool Collided = false;


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("NPCCar").Length >= 2)
        {
            Destroy(this.gameObject);
        }
        else
        {
            objplayer = GameObject.FindGameObjectWithTag("Player").transform;
            routeToGo = 1;
            prev = 0;
            tParam = 0f;
            speedModifier = 0.05f;
            coroutineAllowed = true;

            this.transform.parent = null;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(this.transform.position, objplayer.transform.position);
/*        Debug.Log("distance: "+ dist);
*/        if (dist < 100)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        

        if (coroutineAllowed && routeToGo > prev)
        {
            StartCoroutine(GoByTheRoute(0));
        }
    }

    public void AddRoute(Transform road)
    {
        routes.Add(road);
        routeToGo++;
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNum].GetChild(0).position;
        Vector3 p1 = routes[routeNum].GetChild(1).position;
        Vector3 p2 = routes[routeNum].GetChild(2).position;
        Vector3 p3 = routes[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            if (isMoving)
            {
                tParam += Time.deltaTime * speedModifier;
            }
            

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
            transform.LookAt(objectPosition);
            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0;
        speedModifier = speedModifier * 0.90f;
        prev += 1;
        routes.RemoveAt(0);

        coroutineAllowed = true;

    }


    public void UnColl()
    {
        Collided = false;
    }

    public void OnColl()
    {
        Collided = true;
        StartCoroutine(CHeck());
    }

    IEnumerator CHeck()
    {
        yield return new WaitForSeconds(20f);

        if (Collided)
        {
            GameObject.Find("obj_controller").GetComponent<scr_controller>().isEscaped = true;
            Destroy(gameObject);
            Debug.Log("LEVEL 2 DONE");
        }

        yield return null;
    }


}
