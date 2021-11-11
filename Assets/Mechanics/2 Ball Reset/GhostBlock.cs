using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlock : MonoBehaviour
{
    private void OnTriggerEnter2D()
    {
        gameObject.SetActive(false);
    }
}