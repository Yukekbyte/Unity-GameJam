using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class typewritter: MonoBehaviour
{

    Text txt;
    string story;
    public bool PlayOnAwake = true;
    public float Delay;

    void Awake()
    {
        txt = GetComponent<Text>();
        if (PlayOnAwake)
        {
            ChangeText(txt.text, Delay);
        }

    }

    //Update text and start typewriter effect
    public void ChangeText(string _text, float _delay = 0f)
    {
        GameObject.FindObjectOfType<AudioManager>().Stop("Theme");
        GameObject.FindObjectOfType<AudioManager>().Play("Theme");
        StopCoroutine(PlayText()); //stop Coroutime if exist
        story = _text;
        txt.text = ""; //clean text

        Invoke("Start_PlayText", _delay); //Invoke effect
        
    }

    void Start_PlayText()
    {
        StartCoroutine(PlayText());
    }

    IEnumerator PlayText()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("Typewritter");
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        GameObject.FindObjectOfType<AudioManager>().Stop("Typewritter");
    }

}