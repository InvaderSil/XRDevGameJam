using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public float fallSpeed = 4.0f; //how fast the object fall
    [SerializeField]
    GameObject _object;
    private bool CanFall;
    public float TimeBeforeFall = 5; // The value before falling for exemple 5

    // delays the method defined in the code
    void Start()
    {
        Invoke("DisplayObject", 2);
        StartCoroutine(ReadyToFall());
    }

    //unhides the object gameobject
    public void DisplayObject()
    {
        //_object.SetActive(true);
    }

    //should move the object down the screen
    void Update()
    {
        if (CanFall)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
        }
    }

    IEnumerator ReadyToFall()
    {
        CanFall = false;
        yield return new WaitForSeconds(TimeBeforeFall);

        CanFall = true;
    }
}
