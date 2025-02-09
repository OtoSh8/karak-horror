using UnityEngine;
using Fungus;
using Unity.Cinemachine;

[RequireComponent(typeof(Fungus.Flowchart))]

public class InteractableFungusCharacter : MonoBehaviour
{
    private string flowchartStartMessage = "Start";
    private Flowchart flow;
    private bool isConversing;


    private void Start()
    {
        /*this.gameObject.layer = LayerMask.NameToLayer("Interactable");*/

        flow = this.GetComponent<Flowchart>();
    }

    private void Update()
    {
        if (isConversing)
        {
            if (!flow.HasExecutingBlocks())
            {
                /*Invoke("EnableMove", 0.5f);*/
                isConversing = false;
            }
        }
    }


    public void Interact()
    {
        flow.SendFungusMessage(flowchartStartMessage);
        isConversing = true;
    }
}
