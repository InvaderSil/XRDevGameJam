using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{

    [SerializeField] private Transform m_spawnLocation;
    [SerializeField] private GameObject m_starPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Star SpawnStar()
    {
        var go = Instantiate(m_starPrefab, m_spawnLocation.position, Quaternion.identity);
        return go.GetComponentInChildren<Star>();

    }
}
