using UnityEngine;

public class EnemyConsume : MonoBehaviour
{
    public GameObject enemyTextCanvas;
    private bool onPlayer;

    void Awake()
    {
        enemyTextCanvas = GameObject.Find("EnemyTextCanvas");
    }

    // ***DO NOT TOUCH*** Warning: fragile code ahead
    void OnTriggerStay2D()
    {
        enemyTextCanvas.transform.position = transform.position;
        onPlayer = true;
    }
    void OnTriggerExit2D()
    {
        Invoke("Exit",0.3f);
    }
    void Exit()
    {
        enemyTextCanvas.transform.position = new Vector3(100,100,0);
        onPlayer = false;
    }

    public void Destroy()
    {   
        if(onPlayer)
        {
            Destroy(gameObject);
            enemyTextCanvas.transform.position = new Vector3(100,100,0);
            GameObject.Find("Player").GetComponent<PlayerAbilities>().AddSoul();
        }
    }
}
