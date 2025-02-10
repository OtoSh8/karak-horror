using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class scr_controller : MonoBehaviour
{
    [SerializeField] private GameObject objplayer;
    [SerializeField] private GameObject firsttile;
    [SerializeField] private GameObject secondtile;
    [SerializeField] private GameObject backbox;
    private int ogtile;
    public int currenttile;
    public List<GameObject> tileprefabs = new List<GameObject>();
    public GameObject[] SpawnedTiles;

    public List<GameObject> EmptyRoads;

    public GameObject Road;
    public GameObject Level1;
    public GameObject Level2;


    public GameObject crnt_level1_obj;


    private float dist = 25;

    public bool isHelp;
    public bool isEscaped;


    private void Start()
    {
        SpawnedTiles = new GameObject[6];
        //Spawn starting Tiles

        SpawnedTiles[0] = firsttile;
        SpawnedTiles[1] = secondtile;
/*        dist += firsttile.GetComponent<MeshRenderer>().bounds.size.z + secondtile.GetComponent<MeshRenderer>().bounds.size.z;
*/        for (int i = 1; i <= 4; i++)
        {
            dist += tileprefabs.ElementAt(i).GetComponent<MeshRenderer>().bounds.size.z/2;
            GameObject tile = Instantiate(tileprefabs.ElementAt(i), new Vector3(0, 0, dist) , Quaternion.identity);
            dist += tileprefabs.ElementAt(i).GetComponent<MeshRenderer>().bounds.size.z/2;
            SpawnedTiles[i+1] = tile;
        }
        currenttile = -1;
        ogtile = 0;

    }

    public void HelpChild()
    {
        isHelp = true;
    }

    private void Update()
    {
        /*        currenttile = (int)Mathf.Round(objplayer.transform.position.z/50f);
         *      
        */

        /*float totaldist = 0;
        for(int i = 0; i <= currenttile; i++)
        {
            totaldist += tileprefabs[currenttile].GetComponent<MeshRenderer>().bounds.size.z;
        }*/
        if (objplayer.transform.position.z > SpawnedTiles[2].gameObject.transform.position.z - SpawnedTiles[2].gameObject.GetComponent<MeshRenderer>().bounds.size.z / 2)
        {
            //player is over it bro
            currenttile++;
            Debug.Log(currenttile);
        }

        if (currenttile != ogtile)
        {
            TerrainUpdate();
            ogtile = currenttile;
        }
    }

    private void TerrainUpdate()
    {
        if(currenttile > ogtile)
        {
            //Check if tile i wnat to destroy is an empty road, if so, i dont destroy it hehe
            if (SpawnedTiles[0].tag == "road")
            {
                EmptyRoads.Add(SpawnedTiles[0]);
            }
            else
            {
                Destroy(SpawnedTiles[0]);
            }
            

            for (int i = 1; i < SpawnedTiles.Length; i++)
            {
                SpawnedTiles[i - 1] = SpawnedTiles[i];
            }


            if(((currenttile / 5f) - Mathf.FloorToInt((float)currenttile / 5f)) <= 0f)
            {
                if(isHelp == false)
                {
                    dist += Level1.GetComponent<MeshRenderer>().bounds.size.z / 2;

                    SpawnedTiles[5] = Instantiate(Level1, new Vector3(0, 0, dist), Quaternion.identity);

                    dist += Level1.GetComponent<MeshRenderer>().bounds.size.z / 2;
                }
                else if(isEscaped == false)
                {
                    dist += Level2.GetComponent<MeshRenderer>().bounds.size.z / 2;

                    SpawnedTiles[5] = Instantiate(Level2, new Vector3(0, 0, dist), Quaternion.identity);

                    dist += Level2.GetComponent<MeshRenderer>().bounds.size.z / 2;
                }
                else
                {
                    /*dist += tileprefabs[currenttile + 4].GetComponent<MeshRenderer>().bounds.size.z / 2;

                    SpawnedTiles[5] = Instantiate(tileprefabs[currenttile + 4], new Vector3(0, 0, dist), Quaternion.identity);

                    dist += tileprefabs[currenttile + 4].GetComponent<MeshRenderer>().bounds.size.z / 2;*/

                    if (EmptyRoads.Count() >= 2)
                    {
                        dist += EmptyRoads[0].GetComponent<MeshRenderer>().bounds.size.z / 2;
                        SpawnedTiles[5] = EmptyRoads[0];
                        EmptyRoads[0].transform.position = new Vector3(0, 0, dist);
                        dist += EmptyRoads[0].GetComponent<MeshRenderer>().bounds.size.z / 2;

                        EmptyRoads.RemoveAt(0);

                    }
                    else
                    {
                        dist += Road.GetComponent<MeshRenderer>().bounds.size.z / 2;

                        SpawnedTiles[5] = Instantiate(Road, new Vector3(0, 0, dist), Quaternion.identity);

                        dist += Road.GetComponent<MeshRenderer>().bounds.size.z / 2;
                    }
                    
                }
                
            }
            else
            {

                if (EmptyRoads.Count() >= 2)
                {
                    dist += EmptyRoads[0].GetComponent<MeshRenderer>().bounds.size.z / 2;
                    SpawnedTiles[5] = EmptyRoads[0];
                    EmptyRoads[0].transform.position = new Vector3(0, 0, dist);
                    dist += EmptyRoads[0].GetComponent<MeshRenderer>().bounds.size.z / 2;

                    EmptyRoads.RemoveAt(0);
                }
                else
                {
                    dist += Road.GetComponent<MeshRenderer>().bounds.size.z / 2;

                    SpawnedTiles[5] = Instantiate(Road, new Vector3(0, 0, dist), Quaternion.identity);

                    dist += Road.GetComponent<MeshRenderer>().bounds.size.z / 2;
                }
            }
            



            backbox.transform.position = new Vector3(backbox.transform.position.x, backbox.transform.position.y, SpawnedTiles[0].transform.position.z - 25);
        }
        
    }

}
