using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeyTrigger : MonoBehaviour
{
   public Animator doorAnim;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameVariables.keyCount == 7)
        {
            Debug.Log("Opening Door");
            doorAnim.ResetTrigger("close");
            doorAnim.SetTrigger("open");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Closing Door");
            doorAnim.ResetTrigger("open");
            doorAnim.SetTrigger("close");
        }
    }
}