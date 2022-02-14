using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStar : MonoBehaviour
{
    [SerializeField] private Transform m_spawnLocation;
    [SerializeField] private float m_timeToRespawn;
    [SerializeField] private GameObject m_starPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }
      

    public void OnStartStarDeath()
    {
        Instantiate(m_starPrefab, m_spawnLocation.position, Quaternion.identity);
        Debug.Log("Destroying " + gameObject.name);
        Destroy(gameObject, m_timeToRespawn);
    }
}
