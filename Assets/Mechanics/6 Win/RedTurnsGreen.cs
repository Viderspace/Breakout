using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTurnsGreen : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer = default;
    
    //since all blocks inherit this class, every block that deactivates calls ReduceOne()
    //in order to update how many blocks are still active overall.
    private static int _counter = 9;
    public void ReduceOne()
    {
        _counter -= 1;
        Debug.Log(_counter);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (_counter <= 0) { _spriteRenderer.color = Color.green; }
    }
}
