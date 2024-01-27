using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class keyItem : MonoBehaviour

{
    public AudioSource chime;
    [SerializeField]
    private TMPro.TextMeshProUGUI keyCounterDisplay;

    void Start()
    {
       GameVariables.keyCount = 0;
    }

 
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameVariables.keyCount ++;
            Destroy(gameObject);
            chime.Play();
        }
    }

    void Update()
    {
        keyCounterDisplay.text = "Panties: " + GameVariables.keyCount ;
    }

}
