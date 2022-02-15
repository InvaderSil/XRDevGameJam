using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] private List<StarGroup> Stars = new List<StarGroup>();

    // Start is called before the first frame update
    void Start()
    {
        InitStarGroup();
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var item in Stars)
        {
            var localStar = item.StarObject;

            if (localStar == null)
            {
                Debug.Log("localStar is null");
                continue;
            }

            
        }

        ProcessTimeOutStars();
    }

    private void ProcessTimeOutStars()
    {
        foreach(var item in Stars)
        {
            if(item.StarObject.IsTimerCompleted())
            {
                item.StarObject.DestorySelf();
                item.SpawnStar(); // TODO If can't get all 7 then add a delay on this
            }
        }
    }

    public void InitStarGroup()
    {
        foreach(var item in Stars)
        {
            item.SpawnStar();
        }
    }

    private void SpawnStar(StarSpawner spawner)
    {
        spawner.SpawnStar();
    }
}

[System.Serializable]
public class StarGroup
{
    public Star StarObject { get; set; }
    public StarSpawner Spawner;

    public void SpawnStar()
    {

        StarObject = Spawner.SpawnStar();
        
    }

    
}
