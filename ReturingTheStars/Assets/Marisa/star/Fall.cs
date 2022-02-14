using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public float fallSpeed = 4.0f; //how fast the object fall
    [SerializeField]
    GameObject star;
    private bool CanFall;
    public float TimeBeforeFall = 5; // The value before falling for exemple 5

    // delays the method defined in the code
    void Start()
    {
        Invoke("DisplayObject", 2);
        StartCoroutine(ReadyToFall());
    }

    //unhides the gameobject
    public void DisplayObject()
    {
        star.SetActive(true);
    }

    //should move the object down the screen
    void Update()
    {
        if (CanFall == true)
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
