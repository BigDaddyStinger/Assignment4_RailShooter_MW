using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Color _hurt = hurtDisplay.color;

        _hurt.a = hurtDisplayAlpha;

        hurtDisplay.color = _hurt;

        if (hurtDisplayAlpha > 0f)
        {
            hurtDisplayAlpha -= Time.deltaTime;
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
