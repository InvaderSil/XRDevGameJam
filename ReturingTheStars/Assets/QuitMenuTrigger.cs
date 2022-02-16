using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenuTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Quitting the game");
            Application.Quit();
        }
    }
}
