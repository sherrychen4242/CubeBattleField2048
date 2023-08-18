using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [SerializeField] GameObject[] GrasslandGroundPrefabs;
    #endregion

    #region PRIVATE VARIABLES
    private Vector3 oneBlockPos = Vector3.zero;
    private Vector3[] fourBlocksPos = new Vector3[4] { new Vector3(-30f, 0f, 30f), new Vector3(30f, 0f, 30f), new Vector3(-30f, 0f, -30f), new Vector3(30f, 0f, -30f) };
    private Vector3[] nineBlocksPos = new Vector3[9]
    {
        new Vector3(-60f, 0f, 60f),
        new Vector3(0f, 0f, 60f),
        new Vector3(60f, 0f, 60f),
        new Vector3(-60f, 0f, 0f),
        new Vector3(0f, 0f, 0f),
        new Vector3(60f, 0f, 0f),
        new Vector3(-60f, 0f, -60f),
        new Vector3(0f, 0f, -60f),
        new Vector3(60f, 0f, -60f)
    };

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateGroundPrefabs(int enemyNumberForCurrentLevel)
    {
        if (enemyNumberForCurrentLevel < 64)
        {
            int randNum = Random.Range(0, GrasslandGroundPrefabs.Length);
            Instantiate(GrasslandGroundPrefabs[randNum], oneBlockPos, Quaternion.identity);
        }
        else if (enemyNumberForCurrentLevel < 1024)
        {
            for (int i = 0; i < 4; i++)
            {
                int randNum = Random.Range(0, GrasslandGroundPrefabs.Length);
                Instantiate(GrasslandGroundPrefabs[randNum], fourBlocksPos[i], Quaternion.identity);
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                int randNum = Random.Range(0, GrasslandGroundPrefabs.Length);
                Instantiate(GrasslandGroundPrefabs[randNum], nineBlocksPos[i], Quaternion.identity);
            }
        }
    }
}
