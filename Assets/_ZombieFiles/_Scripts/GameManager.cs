using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerHealth = 5;



    [Header("Set Up UI Screens")]

    public GameObject hurtFlash;

    public Image hurtDisplay;

    public GameObject gameOverPanel;

    public float hurtDisplayAlpha = 0f;
    public float hurtDisplayTimer = 0.5f;

    [Header("Heart Display")]
    public int numberOfHearts = 5;
    public Image[] _hearts;
    public Sprite _heartSprite;

    [Header("Ammo Setup")]
    public int shellCount;
    public int bulletCount;
    public TMP_Text shellCountText;
    public TMP_Text bulletCountText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        // Ammo Display
        bulletCountText.text = bulletCount.ToString();
        shellCountText.text = shellCount.ToString();

        Color _hurt = hurtDisplay.color;

        _hurt.a = hurtDisplayAlpha;

        hurtDisplay.color = _hurt;

        if (hurtDisplayAlpha > 0f)
        {
            hurtDisplayAlpha -= Time.deltaTime;
        }

        // Set up Heart Display

        for (int i = 0; i < numberOfHearts; i++)
        {
            if (i < playerHealth)
            {
                _hearts[i].sprite = _heartSprite;
                _hearts[i].color = new Color(1, 1, 1, 1);
            }
            else
            {
                _hearts[i].sprite = null;
                _hearts[i].color = new Color(1, 1, 1, 0);
            }
            
            if (i < numberOfHearts)
            {
                _hearts[i].enabled = true;
            }
            else
            {
                _hearts[i].enabled = false;
            }

            if(playerHealth > numberOfHearts)
            {
                playerHealth = numberOfHearts;
            }
        }
    }

    public void HurtPlayer()
    {
        playerHealth--;

        if (playerHealth > 0)
        {
            hurtDisplayAlpha = hurtDisplayTimer;
            StartCoroutine(HurtState());
        }
        else
        {
            PlayerDead();
        }
    }

    IEnumerator HurtState()
    {
        //Color _flash = hurtFlash.color;

        hurtFlash.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        hurtFlash.SetActive(false);
    }


    void PlayerDead()
    {
        Time.timeScale = 0f;

        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }
}
