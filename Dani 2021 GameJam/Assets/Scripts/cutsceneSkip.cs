﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutsceneSkip : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectOfType<AudioManager>().Stop("Theme");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            if (SceneManager.GetActiveScene().name == "CutsceneEnd")
            {
                SceneManager.LoadScene("EndScreen");
                GameObject.FindObjectOfType<AudioManager>().Play("Theme");
                GameObject.FindObjectOfType<AudioManager>().Stop("Typewritter");
            }
            if (SceneManager.GetActiveScene().name == "CutsceneStart")
            {
                SceneManager.LoadScene("level 1");
                GameObject.FindObjectOfType<AudioManager>().Play("Theme");
                GameObject.FindObjectOfType<AudioManager>().Stop("Typewritter");
            }
        }
    }
}
