using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class textCollision : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "shadow")
        {
            Debug.Log("TEXT");
            text.enabled = true;
            Invoke("disappear", 2f);
        }
    }
    
    void disappear()
    {
        text.enabled = false;
    }

}
