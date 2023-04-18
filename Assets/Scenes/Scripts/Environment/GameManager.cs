using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject shopMenu = null;

    private int enemiesLeft;
    public bool hasKey = false;
    private bool shopOn = false;
    public int minigunAmmo = 50;
    public int shotgunAmmo = 15;
    public int rocketAmmo = 5;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            shopOn = !shopOn;
            shopMenu.SetActive(shopOn);

            if (shopOn)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void DefeatBoss()
    {
        SceneManager.LoadScene(0);
    }

    public void RegisterEnemy()
    {
        enemiesLeft += 1;
    }

    public void DestroyEnemy()
    {
        enemiesLeft -= 1;
        if (enemiesLeft == 0)
        {
            // Player has won!
            //Debug.Log("Won!");
        }
    }

    public void RecieveKey()
    {
        hasKey = true;
        //Debug.Log("+1 Key");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int currIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((currIndex + 1) % 3);
    }
}
