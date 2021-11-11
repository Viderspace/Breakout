using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingBlock : MonoBehaviour
{






    private void OnCollisionEnter2D()
    {
        Destroy(this.gameObject);
    }
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
