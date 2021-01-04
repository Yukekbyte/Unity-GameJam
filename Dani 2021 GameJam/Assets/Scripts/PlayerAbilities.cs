using UnityEngine;
using TMPro;

public class PlayerAbilities : MonoBehaviour
{
    public int souls = 0;
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public GameObject abilityInfo;
    public TextMeshPro abilityInfoTitle;
    public TextMeshPro abilityInfoExplanation;
    float displayAbilityDuration = 2f;
    float displayAbilityTimer;

    void Awake()
    {
        abilityInfo = GameObject.Find("AbilityInfo");
        abilityInfo.SetActive(false);
        playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerMovement.wallJumpEnabled = false;
        playerMovement.dashEnabled = false;
        DisplayAbilityInfo( "hahahaha", "loooooooooooooooool");
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
            {
                playerMovement.wallJumpEnabled = true;
                string title = "";
                string explanation = "";
                DisplayAbilityInfo(title, explanation);
                break;
            }
            case 3:
            {
                playerMovement.dashEnabled = true;
                string title = "";
                string explanation = "";
                DisplayAbilityInfo(title, explanation);
                break;
            }
            case 5:
            {
                AttackDamageUp();
                string title = "";
                string explanation = "";
                DisplayAbilityInfo(title, explanation);
                break;
            }
            case 7:
            {
                AttackSpeedUp();
                string title = "";
                string explanation = "";
                DisplayAbilityInfo(title, explanation);
                break;
            }
            case 10:
            {
                AttackRangeUp();
                string title = "";
                string explanation = "";
                DisplayAbilityInfo(title, explanation);
                break;
            }
        }

    }

    void DisplayAbilityInfo(string title, string explanation)
    {
        displayAbilityTimer = displayAbilityDuration;
        abilityInfo.SetActive(true);
        abilityInfoTitle.text = title;
        abilityInfoExplanation.text = explanation;
    }

    void AttackSpeedUp()
    {
        playerAttack.attackRate *= 2;
    }

    void AttackDamageUp()
    {
        playerAttack.attackDamage++;
    }
    public void AttackRangeUp()
    {
        // attackRange vergroten
    }
}
