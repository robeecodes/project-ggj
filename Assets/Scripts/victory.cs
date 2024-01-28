using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class victory : MonoBehaviour
{
    public AudioSource winSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("WIN");
            winSound.Play();
            SceneManager.GetSceneByName("Victory");
            SceneManager.GetSceneByName("Victory");

        }
    }
}
