using UnityEngine;
using TMPro;

public class EnemyConsume : MonoBehaviour
{
    public GameObject canvas;
    public GameObject can;

    void Awake()
    {
        canvas = this.gameObject.transform.GetChild(0).gameObject;
    }
    // Update is called once per frame
    void Update()
    {
    
    }
    
    void OnTriggerEnter2D()
    {
        canvas.SetActive(true);
        Debug.Log("Consume me");
    }
    void OnTriggerExit2D()
    {
        canvas.SetActive(false);
        Debug.Log("nevermind");
    }
}
