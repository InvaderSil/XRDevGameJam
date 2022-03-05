using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{

    [SerializeField] private Transform m_spawnLocation;
    [SerializeField] private GameObject m_starPrefab;

    public Star SpawnStar()
    {
        var go = Instantiate(m_starPrefab, m_spawnLocation.position, Quaternion.identity);
        return go.GetComponentInChildren<Star>();

    }
}
