using UnityEngine;
using TMPro;

public class PlayerAbilities : MonoBehaviour
{
    public int souls = 0;
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public GameManager gameManager;
    public GameObject abilityInfo;
    public TextMeshProUGUI abilityInfoTitle;
    public TextMeshProUGUI abilityInfoExplanation;
    int abilityInfoDuration = 100;
    float abilityInfoTimer;
    bool showingAbility;
    public static int soulsBeforeActiveLevel;
    int previousLoopSouls;
    
    void Awake()
    {
        abilityInfo = GameObject.Find("AbilityInfo");
        abilityInfo.SetActive(false);
        playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        TextMeshProUGUI[] abilityInfoText = abilityInfo.GetComponentsInChildren<TextMeshProUGUI>();
        abilityInfoTitle = abilityInfoText[0];
        abilityInfoExplanation = abilityInfoText[1];
        gameManager = GameObject.FindObjectOfType<GameManager>();
        souls = PlayerAbilities.soulsBeforeActiveLevel;

        DisplayAbilityInfo("", "");
        abilityInfoTimer = -2;
        showingAbility = false;
        abilityInfo.SetActive(false);
    }
    public void AddSoul()
    {
        souls++;
    }

    void Update()
    {
        // ***DO NOT TOUCH*** (unless needed)
        if (Input.GetKeyDown(KeyCode.E)) //als je e indrukt zullen alle enemies op het scherm in een array worden geplaatst
        {
            GameObject.FindObjectOfType<AudioManager>().Play("Consume");
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
        if (souls != previousLoopSouls)
        {
            switch(souls)
            {
                case 1:
                {
                    playerMovement.wallJumpEnabled = true;
                    string title = "WallJump Ability";
                    string explanation = "Walljump ability is now unlocked. Jump while sliding down a wall to walljump. ";
                    DisplayAbilityInfo(title, explanation);
                    break;
                }
                case 3:
                {
                    playerMovement.dashEnabled = true;
                    string title = "Dash Ability";
                    string explanation = "Dash ability is now unlocked. Press shift while moving to dash.";
                    DisplayAbilityInfo(title, explanation);
                    break;
                }
                case 5:
                {
                    AttackDamageUp();
                    string title = "Attack Damage";
                    string explanation = "Attack damage is now increased.";
                    DisplayAbilityInfo(title, explanation);
                    break;
                }
                case 7:
                {
                    AttackSpeedUp();
                    string title = "Attack Speed";
                    string explanation = "Attack speed is now increased.";
                    DisplayAbilityInfo(title, explanation);
                    break;
                }
                case 10:
                {
                    AttackRangeUp();
                    string title = "Attack Range";
                    string explanation = "Attack range is now increased.";
                    DisplayAbilityInfo(title, explanation);
                    break;
                }
            }
        }
        previousLoopSouls = souls;
        if (abilityInfoTimer >= -1)
        {
            abilityInfoTimer -= 1;
        }

        if (Input.anyKey && abilityInfoTimer < 0 && showingAbility)
        {
            gameManager.ResumeGame();
            abilityInfo.SetActive(false);
            showingAbility = false;
        }
    }

    void DisplayAbilityInfo(string title, string explanation)
    {   if (soulsBeforeActiveLevel < souls)
        {
            gameManager.PauseGame();
            abilityInfoTimer = abilityInfoDuration;
            abilityInfo.SetActive(true);
            showingAbility = true;
            abilityInfoTitle.text = title;
            abilityInfoExplanation.text = explanation;
        }
    }

    void AttackSpeedUp()
    {
        playerAttack.attackRate *= 2;
    }

    void AttackDamageUp()
    {
        playerAttack.attackDamage += 1;
    }
    public void AttackRangeUp()
    {
        playerAttack.attackRange *= 2;
    }

    void PlayAbilitySound()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("Ability Unlocked");
    }
}
