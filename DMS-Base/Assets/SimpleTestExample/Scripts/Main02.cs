using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main02 : MonoBehaviour
{
    public List<BaseData> myData;
    public BasicObject[] myObjects;
    public PlayerObject playerObj;
    public List<IActiveObject> activeList = new List<IActiveObject>();
    public GameObject playerPrefab;
    public GameObject[] enemyPrefabs;

    public Action<Vector3> myAction;
    // Start is called before the first frame update
    void Start()
    {
        PlayerObject playerObj = new PlayerObject();
        playerObj.id = 1;
        playerObj.displayName = "Robot";
        playerObj.position = Vector3.zero;
        activeList.Add(playerObj);

        EnemyObject enemyMelee = new EnemyObject();
        enemyMelee.id = 0;
        enemyMelee.displayName = "MeleeBot";
        enemyMelee.position = new Vector3(-5f, 0f, 0f);
        activeList.Add(enemyMelee);

        EnemyObject enemyRanged = new EnemyObject();
        enemyRanged.id = 1;
        enemyRanged.displayName = "RangedBot";
        enemyRanged.position = new Vector3(0f, -10f, 0f);
        activeList.Add(enemyRanged);

        CreateFromData();

        UIManager.Instance.SetupListeners(activeList);

        //myAction = new Action<Vector3>(playerObj.MoveTo);
        //myAction += new Action<Vector3>(enemyMelee.MoveTo);
        //myAction += new Action<Vector3>(enemyRanged.MoveTo);

        //myAction(new Vector3(0f, 5f, 3f));


    }

    void CreateFromData()
    {
        bool isMelee = true;
        foreach (IActiveObject curObj in activeList)
        {
            GameObject curPrefab = null;
            if (curObj is PlayerObject)
            {
                curPrefab = playerPrefab;
            }
            else if (curObj is EnemyObject)
            {
                if (isMelee)
                {
                    curPrefab = enemyPrefabs[0];
                }
                else
                {
                    curPrefab = enemyPrefabs[1];
                }
                isMelee = !isMelee;
                 
            }
            if (curPrefab != null)
            {
                GameObject curGO = Instantiate<GameObject>(curPrefab, curObj.position, Quaternion.identity);
                curGO.name = curObj.displayName;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            foreach(IActiveObject obj in activeList)
            {
                obj.MoveTo(new Vector3(-1f, 5f, 6f));
            }
        }
    }
}
