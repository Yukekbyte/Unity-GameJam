using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutsceneSkip : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            if (SceneManager.GetActiveScene().name == "Cutscene end")
            {
                //loadscene
            }
            if (SceneManager.GetActiveScene().name == "Cutscene start")
            {
                SceneManager.LoadScene("level1");
            }
        }
    }
}
