using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public int ChapterNum = 1;

    public GameObject[] Spawntarget;
    public List<List<int>> SpawnData = new List<List<int>>();

    Vector3 PlayerStartPosion = new Vector3(6f, 0, -52f); // 시작 좌표  

    private void Awake()
    {
        if (null == instance) instance = this;
        Init();
        Spawn();
    }
    private void Init() // SpawnData[StageNum].Add(SpawntargetKeyValue)
    {
        SpawnData.Add(new List<int>()); // 스테이지 수 만큼 리스트를 만들어줘야함.
        SpawnData[0].Add(0); // 스테이지에 챕터 추가.
        SpawnData[0].Add(4);
        SpawnData[0].Add(5);
        SpawnData.Add(new List<int>());
        SpawnData[1].Add(1);
        SpawnData[1].Add(2);
        SpawnData[1].Add(3);
        SpawnData.Add(new List<int>());
        SpawnData[2].Add(6);
        SpawnData[2].Add(7);
        SpawnData[2].Add(8);
    }

    private void OnCollisionEnter(Collision collision) // 다음챕터 자동 이동 트리거
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.Instance.transform.position = PlayerStartPosion;
            Player.Instance.curHp = Player.Instance.maxHp;
            if (ChapterNum == 3)
            {
                StageNum++;
                ChapterNum = 0;
            }
            ChapterNum++;
            Spawn();
        }
    }
    public void Spawn() // 적군 생성
    {
        Spawntarget[SpawnData[StageNum - 1][ChapterNum - 1]].SetActive(true);
    }

}
