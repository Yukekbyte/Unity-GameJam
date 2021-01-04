using UnityEngine;
using TMPro;

public class PlayerAbilities : MonoBehaviour
{
    public int souls = 0;
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public GameObject abilityInfo;
    public TextMeshProUGUI abilityInfoTitle;
    public TextMeshProUGUI abilityInfoExplanation;
    public float displayAbilityInfoDuration;
    float displayAbilityInfoTimer;
    bool showingAbilityInfo;
    int previous_souls;

    void Awake()
    {
        abilityInfo = GameObject.Find("AbilityInfo");
        abilityInfo.SetActive(false);
        playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerMovement.wallJumpEnabled = false;
        playerMovement.dashEnabled = false;
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
        if (souls != previous_souls)
        {
            switch(souls)
            {
                case 1:
                {
                    playerMovement.wallJumpEnabled = true;
                    string title = "WallJump ability";
                    string explanation = "Walljump ability is now unlocked. Jump while sliding down a wall to walljump. ";
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

        previous_souls = souls;

        if (showingAbilityInfo)
        {
            displayAbilityInfoTimer -= Time.deltaTime;
        }
        if (displayAbilityInfoTimer < 0)
        {
            showingAbilityInfo = false;
            abilityInfo.SetActive(false);
        }

    }

    void DisplayAbilityInfo(string title, string explanation)
    {
        displayAbilityInfoTimer = displayAbilityInfoDuration;
        abilityInfo.SetActive(true);
        abilityInfoTitle.text = title;
        abilityInfoExplanation.text = explanation;
        showingAbilityInfo = true;
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
