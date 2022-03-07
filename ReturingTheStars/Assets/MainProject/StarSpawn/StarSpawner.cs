using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{

    [SerializeField] private Transform m_spawnLocation;
    [SerializeField] private GameObject m_starPrefab;
    [SerializeField] private GameObject childOf;


    public Star SpawnStar()
    {
        var go = Instantiate(m_starPrefab, m_spawnLocation.position, Quaternion.identity);
        go.transform.SetParent(childOf.transform);
        return go.GetComponentInChildren<Star>();

    }
}
