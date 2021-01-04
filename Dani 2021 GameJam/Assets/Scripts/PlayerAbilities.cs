using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public int souls = 0;
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;

    void Awake()
    {
        playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }
    public void AddSoul()
    {
        souls++;
        print(souls);
    }

    void Update()
    {
        // ***DO NOT TOUCH*** (unless needed)
        if (Input.GetKeyDown(KeyCode.E)) //als je e indrukt zullen alle enemies op het scherm in een array worden geplaatst
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            //Voor elke enemy voert hij Destroy(); uit
            //in het EnemyConsume script checkt de enemy eerst ofdat de speler op hem staat indien ja dan destroyed hij zichzelf 
            foreach(GameObject enemy in enemies) 
            {
                if(enemy.GetComponent<EnemyConsume>() != null)
                    enemy.GetComponent<EnemyConsume>().Destroy();
            }
        }

        //Power Ups
        switch(souls)
        {
            case 1:
                //playerMovement.wallJumpEnabled = true;
                break;
            case 3:
                //playerMovement.dashEnabled = true;
                break;
            case 5:
                playerAttack.AttackDamageUp();
                break;
            case 7:
                playerAttack.AttackSpeedUp();
                break;
            case 10:
                playerAttack.AttackRangeUp();
                break;
        }
    }
}
