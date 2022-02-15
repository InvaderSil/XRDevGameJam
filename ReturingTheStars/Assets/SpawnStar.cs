using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStar : MonoBehaviour
{
    [SerializeField] private Transform m_spawnLocation;
    [SerializeField] private GameObject m_starPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }
      

    public GameObject GenerateStar()
    {
        return Instantiate(m_starPrefab, m_spawnLocation.position, Quaternion.identity);
        
    }
}
