using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var MusicObject = GameObject.FindGameObjectWithTag("Music");
        Debug.Log("MusicObject found" + MusicObject);
            //.GetComponent<Music>().PlayMusic();

    }
}
