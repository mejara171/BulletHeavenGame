using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatsHandler : MonoBehaviour
{
    public CanvasGroup levelUpCanvasGroup;
    public Slider xpSlider;
    public Text textUpgradeOption2;
    public Text textUpgradeOption3;

    int level = 1;

    int totalXP = 0;

    int xpCollectedSinceLevelUp = 0;
    int xpRequiredToReachNextLevel = 5;

    string upgradeOption2 = "";
    string upgradeOption3 = "";


    Dictionary<string, float> statsDictonary = new Dictionary<string, float>();

    enum Stats { maxHP, fireDelay, movementSpeed};

    SFXHandler sfxHandler;

    private void Awake()
    {
        sfxHandler = GetComponent<SFXHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setup default stats
        statsDictonary.Add(Stats.maxHP.ToString(), GetComponent<HPHandler>().GetCurrentHP());
        statsDictonary.Add(Stats.fireDelay.ToString(), 1);
        statsDictonary.Add(Stats.movementSpeed.ToString(), 3);

        xpSlider.value = xpCollectedSinceLevelUp;
        xpSlider.maxValue = xpRequiredToReachNextLevel-1;
    }


    void LevelUpCompleted()
    {
        xpCollectedSinceLevelUp = 0;
        xpRequiredToReachNextLevel = xpRequiredToReachNextLevel + 5;
        level++;

        xpSlider.value = xpCollectedSinceLevelUp;
        xpSlider.maxValue = xpRequiredToReachNextLevel-1;

        levelUpCanvasGroup.alpha = 0;
        levelUpCanvasGroup.interactable = false;

        Time.timeScale = 1;
    }

    public void OnCollectXP()
    {
        sfxHandler.PlayCollectXPSFX();

        xpSlider.value = xpCollectedSinceLevelUp;

        totalXP++;
        xpCollectedSinceLevelUp++;

        if (xpCollectedSinceLevelUp == xpRequiredToReachNextLevel)
        {
            sfxHandler.PlayLevelUP();

            Time.timeScale = 0;

            levelUpCanvasGroup.alpha = 1;
            levelUpCanvasGroup.interactable = true;

            //Create a list of potential upgrades
            List<string> potentialUpgrades = new List<string>();

            foreach (KeyValuePair<string, float> pair in statsDictonary)
            {
                potentialUpgrades.Add(pair.Key);
            }

            //Pick a random stat and make sure they are unique
            int randomIndex = UnityEngine.Random.Range(0, potentialUpgrades.Count-1);
            upgradeOption2 = potentialUpgrades[randomIndex];
            potentialUpgrades.RemoveAt(randomIndex);

            upgradeOption3 = potentialUpgrades[randomIndex];

            textUpgradeOption2.text = ConvertStatNameToUIFriendly(upgradeOption2);
            textUpgradeOption3.text = ConvertStatNameToUIFriendly(upgradeOption3);
        }
    }

    string ConvertStatNameToUIFriendly(string statname)
    {
        //Conver the string to a stat
        Stats stat = Enum.Parse<Stats>(statname);

        switch (stat)
        {
            case Stats.maxHP:
                return "More HP & Heal";

            case Stats.fireDelay:
                return "Faster fire rate";

            case Stats.movementSpeed:
                return "Faster movement";
        }

        return "";
    }

    void HandleUpgrade(string statstring)
    {
        float upgradeAmount = 0;

        //Conver the string to a stat
        Stats stat = Enum.Parse<Stats>(statstring);

        switch (stat)
        {
            case Stats.maxHP:
                upgradeAmount = 1;
                break;

            case Stats.fireDelay:
                upgradeAmount = -0.1f;
                break;

            case Stats.movementSpeed:
                upgradeAmount = 0.1f;
                break;
        }

        statsDictonary[stat.ToString()] += upgradeAmount;

        //Apply the upgrade
        switch (stat)
        {
            case Stats.maxHP:
                GetComponent<HPHandler>().OnUpgrade(statsDictonary[stat.ToString()]);
                break;

            case Stats.fireDelay:
                GetComponent<WeaponHandler>().OnUpgrade(statsDictonary[stat.ToString()]);
                break;

            case Stats.movementSpeed:
                GetComponent<CharacterMovementHandler>().OnUpgrade(statsDictonary[stat.ToString()]);
                break;
        }

        CGUtils.DebugLog($"Stat {stat.ToString()} upgraded is now {statsDictonary[stat.ToString()]}");
    }

    public void OnRemoveGraves()
    {
        GameObject[] gravesGameObjects = GameObject.FindGameObjectsWithTag("Grave");

        //Remove a random grave if there are any
        if(gravesGameObjects.Length > 0)
            Destroy(gravesGameObjects[UnityEngine.Random.Range(0, gravesGameObjects.Length)]);

        LevelUpCompleted();
    }

    public void OnOption2()
    {
        HandleUpgrade(upgradeOption2);
        LevelUpCompleted();
    }

    public void OnOption3()
    {
        HandleUpgrade(upgradeOption3);
        LevelUpCompleted();
    }

    
}
