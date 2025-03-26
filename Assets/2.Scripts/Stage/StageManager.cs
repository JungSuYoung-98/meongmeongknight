
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
    public int ChapterNum = 1;

    public GameObject[] Spawntarget;
    public List<List<int>> SpawnData = new List<List<int>>();

    Vector3 PlayerStartPosion = new Vector3(6f, 0, -52f); // ���� ��ǥ  

    private void Awake()
    {
        if (null == instance) instance = this;
        Init();
        Spawn();
        
    }

    private void Start()
    {
        UIManager.Instance.enemyUI.EnemyUpdate();
    }

    private void Init() // SpawnData[StageNum].Add(SpawntargetKeyValue)
    {
        SpawnData.Add(new List<int>()); // �������� �� ��ŭ ����Ʈ�� ����������.
        SpawnData[0].Add(0); // ���������� é�Ϳ� ���� �ε�����ȣ �߰�.
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

    private void OnCollisionEnter(Collision collision) // ����é�� �ڵ� �̵� Ʈ����
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
            UIManager.Instance.NewEnemy();
            UIManager.Instance.enemyUI.EnemyUpdate();
            UIManager.Instance.baseUI.BaseUIUpdate();
        }
    }
    public void Spawn() // ���� Ȱ��ȭ
    {   
        Spawntarget[SpawnData[StageNum - 1][ChapterNum - 1]].SetActive(true);
    }

    public void SetStage(int stage)
    {
        StageNum = stage;
    }

    public void SetChapter(int Chapter)
    {
        ChapterNum = Chapter;
        if (Enemy.Instance != null) Enemy.Instance.SetActiveEnd();

        Player.Instance.transform.position = PlayerStartPosion;
        Player.Instance.curHp = Player.Instance.maxHp;

        Spawn();
        UIManager.Instance.enemyUI.gameObject.SetActive(true);
        UIManager.Instance.NewEnemy();
        UIManager.Instance.enemyUI.EnemyUpdate();
        UIManager.Instance.baseUI.BaseUIUpdate();
        UIManager.Instance.enemyUI.gameObject.SetActive(false);
    }

}
