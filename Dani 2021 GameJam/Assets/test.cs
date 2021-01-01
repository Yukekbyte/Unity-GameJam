using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform t;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hey");
        t = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
