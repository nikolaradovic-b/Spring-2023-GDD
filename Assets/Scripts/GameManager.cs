using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int enemiesLeft;

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
            Debug.Log("Won!");
        }
    }
}
