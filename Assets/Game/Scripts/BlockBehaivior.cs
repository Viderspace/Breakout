using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaivior : GameManager
{
    
    private void OnCollisionEnter2D()
    {
        gameObject.SetActive(false);
        ReduceBlock();
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
