using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    //Simple script to move the pipe along
    [SerializeField] private float speed = 5f;

    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0f, 0f), Space.World);
    }
}
