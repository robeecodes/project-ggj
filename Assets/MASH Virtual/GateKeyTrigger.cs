using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeyTrigger : MonoBehaviour
{
   public AudioSource gateSound;
   public Animator doorAnim;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameVariables.keyCount == 0)
        {
            //Debug.Log("Opening Door");
            gateSound.Play();
            doorAnim.ResetTrigger("close");
            doorAnim.SetTrigger("open");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Closing Door");
            gateSound.Play();
            doorAnim.ResetTrigger("open");
            doorAnim.SetTrigger("close");
        }
    }
}
