using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPHandler : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public GameObject spawnObjectOnDeath;

    public Slider hpSlider;

    public Canvas gameOverCanvas;


    int hp = 5; //Is set by the character data scriptable object
    int maxHP = 5;

    //Flashing on hit
    int _IsHitPropertyID = 0;
    bool isFlashingColor = false;
    WaitForSeconds waitTimeUntilRestoreColor = new WaitForSeconds(0.1f);

    bool isPlayer = false;

    SFXHandler sfxHandler;

    private void Awake()
    {
        hp = GetComponent<CharacterDataHandler>().characterData.HP;

        maxHP = hp;

        isPlayer = CompareTag("Player");

        if (isPlayer)
            hpSlider.maxValue = maxHP;

        sfxHandler = GetComponent<SFXHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _IsHitPropertyID = Shader.PropertyToID("_IsHit");
    }

    public int GetCurrentHP()
    {
        return hp;
    }

    public void OnUpgrade(float newMaxHP)
    {
        //Upgrade HP and restore
        maxHP = (int)newMaxHP;
        hp = maxHP;

        hpSlider.maxValue = maxHP;
        hpSlider.value = hp;

    }

    public void OnHit()
    {
        //CGUtils.DebugLog($"Game object {gameObject.name} was hit");

        if(sfxHandler !=null)
            sfxHandler.PlayPlayerHitSFX();

        hp--;

        if(isPlayer)
        {
            hpSlider.value = hp;
           
        }

        if(hp <= 0)
        {
            gameObject.SetActive(false);

            if(spawnObjectOnDeath !=null)
                Instantiate(spawnObjectOnDeath, transform.position, Quaternion.identity);

            if (isPlayer && !gameOverCanvas.gameObject.activeInHierarchy)
                gameOverCanvas.gameObject.SetActive(true);


            return;
        }

        if (!isFlashingColor)
            StartCoroutine(FlashColorCO());
    }

    IEnumerator FlashColorCO()
    {
        isFlashingColor = true;

        spriteRenderer.material.SetFloat(_IsHitPropertyID, 1.0f);

        yield return waitTimeUntilRestoreColor;

        spriteRenderer.material.SetFloat(_IsHitPropertyID, 0.0f);

        isFlashingColor = false;
    }
}
