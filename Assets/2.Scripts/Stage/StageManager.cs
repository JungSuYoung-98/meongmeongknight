using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;

    public static StageManager Instance 
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


    public int StageNum = 1;
    Vector3 PlayerStartPosion = new Vector3(6f,0,-52f); // Ω√¿€ ¡¬«•

    void Awake()
    {
        if (null == instance) instance = this;
    }

    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player.Instance.transform.position = PlayerStartPosion;
            Player.Instance.curHp = Player.Instance.maxHp;
            StageNum++;
        }
    }
}
