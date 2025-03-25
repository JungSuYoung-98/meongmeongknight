using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public EnemyUI enemyUI;
    public BaseUI baseUI;
    public InventoryUI InventoryuI;

    private void Awake()
    {
        if (null == instance) instance = this;
        UIManager.Instance.NewEnemy();
    }

    public void NewEnemy()
    {
        enemyUI = Enemy.Instance.GetComponentInChildren<EnemyUI>();
    }


    public void StartGame()
    {
        Time.timeScale = 1;
        enemyUI.gameObject.SetActive(true);
    }

    public void StopGame()
    {
        Time.timeScale = 0;
        enemyUI.gameObject.SetActive(false);

    }

}
