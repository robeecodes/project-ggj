using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenGate : MonoBehaviour
{
    public Animation anim;
    public AudioSource slideDoor;
    [SerializeField]
    private TMPro.TextMeshProUGUI gateText;

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        GameVariables.doorWorks = 1;
        gateText.enabled = false;

    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "shadow" && GameVariables.keyCount == 10 && GameVariables.doorWorks == 1)
        {
            Debug.Log("open gate");
            gateText.enabled = false;
            anim.Play();
            GameVariables.doorWorks = 0;
            slideDoor.Play();
            //Destroy(gameObject);

            //make bkx xollider false so that it doesn't play anim again?
        }
        else
        {
            Debug.Log("No enough key!!");
            gateText.enabled = true;
            Invoke("disappear", 3f);

        }
    }

    void disappear()
    {
        gateText.enabled = false;
    }
}
