using UnityEngine;

public class scr_death : MonoBehaviour
{
    private int crntlevel = 0;

    public void Death(int x)
    {
        switch (x)
        {
            case 2:
                //level 3
                this.GetComponent<Animator>().Play("jumpscare_1");
                break;
            case 3:
                //level 3
                crntlevel = 3;
                this.GetComponent<Animator>().Play("jumpscare_3");
                GameObject.FindFirstObjectByType<scr_levelthree>().ResetLevel();
                break;
        }
    }

    public void OnJumpscareFinish()
    {
        switch (crntlevel)
        {
            case 3:
                //Set player at spawnpoint already bro
                GameObject.Find("obj_player").transform.position = GameObject.FindFirstObjectByType<scr_levelthree>().spawn.transform.position;
                break;
        }
    }
}
