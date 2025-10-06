using System;
using UnityEngine;

public class ZombieBite : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<playerHealth>(out var pHealth))
            {
                pHealth.takeDamage(1);
                Debug.Log("Player health: " + pHealth.health);
            }
        }
    }
}
