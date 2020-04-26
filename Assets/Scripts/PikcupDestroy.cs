using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikcupDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }
    }

    
}
