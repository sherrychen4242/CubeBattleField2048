using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int[] enemyNumberList = new int[6] { 2, 4, 8, 16, 32, 64 };
    private float[] enemyCubeYPosList = new float[6] { 0.5f, 1f, 1.5f, 2f, 2.5f, 3f };
    [SerializeField] GameObject[] EnemyCubePrefabs;

    public void SpawnEnemies(int enemyNumberForCurrentLevel, Vector3 bottomLeftPos, Vector3 upperRightPos)
    {
        int remainingNumber = enemyNumberForCurrentLevel;

        float smallestX = bottomLeftPos.x;
        float largestX = upperRightPos.x;
        float smallestZ = bottomLeftPos.z;
        float largestZ = upperRightPos.z;

        List<Vector3> existingPosList = new List<Vector3>();
        existingPosList.Add(Vector3.zero);
        while (remainingNumber > 0)
        {
            int ranIndex = Random.Range(0, enemyNumberList.Length);
            int enemyNumber = enemyNumberList[ranIndex];
            float enemyCubeYPos = enemyCubeYPosList[ranIndex];
            while (enemyNumber > remainingNumber && remainingNumber > 0)
            {
                ranIndex = Random.Range(0, enemyNumberList.Length);
                enemyNumber = enemyNumberList[ranIndex];
                enemyCubeYPos = enemyCubeYPosList[ranIndex];
            }

            float ranX = Random.Range(smallestX, largestX);
            float ranZ = Random.Range(smallestZ, largestZ);
            Vector3 newPos = new Vector3(ranX, enemyCubeYPos, ranZ);

            bool positionOverlap = false;
            if (existingPosList.Count > 0)
            {
                foreach(Vector3 pos in existingPosList)
                {
                    float distance = Vector3.Distance(newPos, pos);
                    if (distance < 4f)
                    {
                        positionOverlap = true;
                        break;
                    }
                }
            }
            while (positionOverlap)
            {
                ranX = Random.Range(smallestX, largestX);
                ranZ = Random.Range(smallestZ, largestZ);
                newPos = new Vector3(ranX, enemyCubeYPos, ranZ);

                positionOverlap = false;
                if (existingPosList.Count > 0)
                {
                    foreach (Vector3 pos in existingPosList)
                    {
                        float distance = Vector3.Distance(newPos, pos);
                        if (distance < 4f)
                        {
                            positionOverlap = true;
                            break;
                        }
                    }
                }
            }
            existingPosList.Add(newPos);
            Instantiate(EnemyCubePrefabs[ranIndex], newPos, Quaternion.identity);
            remainingNumber -= enemyNumber;
            
        }
    }
}
