using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    Vector3 PlayerStartPosion = new Vector3(6f,0,-52f);
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.transform.position = PlayerStartPosion;
            player.curHp = player.maxHp;
        }
    }
}
