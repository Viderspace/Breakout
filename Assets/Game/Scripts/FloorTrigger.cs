using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<BallBehaviour>().Respawn();
        FindObjectOfType<LivesManager>().ReduceLife();
    }
}
