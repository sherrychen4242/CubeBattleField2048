using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [Header("Cube Related")]
    public List<int> numCubesForEach = new List<int>();
    public List<int> numCubesForEachPreFrame = new List<int>();
    public int numCubesChanged;
    [SerializeField] LayerMask playerCubeLayer;
    private List<float> cubeHalfLengthList = new List<float> { 3f, 2.5f, 2f, 1.5f, 1f, 0.5f };
    [SerializeField] GameObject cubeAppearEffect;
    [SerializeField] GameObject cubeDisappearEffect;
    public bool obstacleEncountered;

    [Header("Cube Lists")]
    public List<List<GameObject>> cubeList = new List<List<GameObject>>();

    [Header("Cell Related")]
    [SerializeField] GameObject cellPrefab;
    [SerializeField] GameObject cellPointIndicatorPrefab;
    public float cubeGapWidth;
    public int largestCubeSideLength;

    [Header("Cube Prefabs")]
    [SerializeField] GameObject cube2Prefab;
    [SerializeField] GameObject cube4Prefab;
    [SerializeField] GameObject cube8Prefab;
    [SerializeField] GameObject cube16Prefab;
    [SerializeField] GameObject cube32Prefab;
    [SerializeField] GameObject cube64Prefab;
    public List<GameObject> cubePrefabList = new List<GameObject>();



    #endregion

    #region PRIVATE VARIABLES
    private List<int> cubeNumList = new List<int> {64, 32, 16, 8, 4, 2};
    private List<string> cubeNameList = new List<string> { "Cube64", "Cube32", "Cube16", "Cube8", "Cube4", "Cube2" };
    public int totalNumCubes;
    public int numCubesPreFrame;

    private List<GameObject> cube64List = new List<GameObject>();
    private List<GameObject> cube32List = new List<GameObject>();
    private List<GameObject> cube16List = new List<GameObject>();
    private List<GameObject> cube8List = new List<GameObject>();
    private List<GameObject> cube4List = new List<GameObject>();
    private List<GameObject> cube2List = new List<GameObject>();

    public bool flag;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        flag = false;

        /*GameObject cell = Instantiate(cellPrefab, transform.position, Quaternion.identity, transform);
        cell.GetComponent<Cell>().CreateCell(10, Vector3.zero);
        cell.GetComponent<Cell>().DisplayCell();*/

        numCubesForEachPreFrame = new List<int> { 0, 0, 0, 0, 0, 0};
        cubeList.Add(cube64List);
        cubeList.Add(cube32List);
        cubeList.Add(cube16List);
        cubeList.Add(cube8List);
        cubeList.Add(cube4List);
        cubeList.Add(cube2List);

        cubePrefabList.Add(cube64Prefab);
        cubePrefabList.Add(cube32Prefab);
        cubePrefabList.Add(cube16Prefab);
        cubePrefabList.Add(cube8Prefab);
        cubePrefabList.Add(cube4Prefab);
        cubePrefabList.Add(cube2Prefab);

        numCubesPreFrame = 0;

        obstacleEncountered = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check whether encounter obstacle
        foreach (GameObject cube in GameObject.FindGameObjectsWithTag("PlayerCube"))
        {
            if (!cube.GetComponent<CubeMove>().obstacleEncountered)
            {
                obstacleEncountered = false;
            }
            else
            {
                obstacleEncountered = true;
                break;
            }
        }

        int currentHealth = FindObjectOfType<Player>().currentNumber;
        CalculateNumberOfCubes(currentHealth);
        if (ListMatch(numCubesForEach, numCubesForEachPreFrame))
        {
            return;
        }
        else
        {
            
            List<Vector3> posList = CalculateCubePositions();

            for(int i = 0; i < posList.Count; i++)
            {
                Vector3 pos = posList[i];
                //print("pos" + pos.ToString());
                Vector3 _pos = new Vector3(pos.x + largestCubeSideLength / 2f,
                    0f,
                    pos.z - largestCubeSideLength / 2f);
                posList[i] = _pos;
                //Instantiate(cellPointIndicatorPrefab, _pos, Quaternion.identity);
            }


            for (int j = 0; j < 2; j++)
            {
                // loop first time to remove cubes
                if (j == 0)
                {
                    for (int i = 0; i < numCubesForEach.Count; i++)
                    {
                        if (i > 0)
                        {
                            // When the cubes are not cube 64
                            if (numCubesForEach[i] < numCubesForEachPreFrame[i])
                            {
                                // When cubes are deleted
                                if (cubeList[i].Count > 0)
                                {
                                    for (int b = 0; b < cubeList[i].Count; b++)
                                    {
                                        if (cubeList[i][b] != null)
                                        {
                                            GameObject disappearEffect = Instantiate(cubeDisappearEffect, cubeList[i][b].transform.position, Quaternion.identity);
                                            disappearEffect.transform.localScale = new Vector3(cubeList[i][b].transform.localScale.x,
                                                cubeList[i][b].transform.localScale.y,
                                                cubeList[i][b].transform.localScale.z);
                                            DestroyImmediate(cubeList[i][b]);
                                            cubeList[i].RemoveAt(b);
                                        }
                                        
                                    }
                                    
                                }
                                
                            }
                        }
                        else
                        {
                            // When the cubes are cube64
                            if (numCubesForEach[0] < numCubesForEachPreFrame[0])
                            {
                                // When cubes are deleted
                                if (cubeList[0].Count > 0)
                                {
                                    for (int t = 0; t < cubeList[0].Count; t++)
                                    {
                                        if (cubeList[0][t] != null)
                                        {
                                            Vector3 instantiatePos = cubeList[0][t].transform.position;
                                            GameObject disappearEffect = Instantiate(cubeDisappearEffect, instantiatePos, Quaternion.identity);
                                            disappearEffect.transform.localScale = new Vector3(cubeList[0][t].transform.localScale.x,
                                                cubeList[0][t].transform.localScale.y,
                                                cubeList[0][t].transform.localScale.z);
                                            DestroyImmediate(cubeList[0][t]);
                                            cubeList[0].RemoveAt(t);
                                            break;
                                        }
                                    }
                                    
                                }
                                
                            }
                        }
                
                    }
                }
                else if (j == 1)
                {
                    for (int i = 0; i < numCubesForEach.Count; i++)
                    {
                        if (i > 0)
                        {
                            // When the cubes are not cube 64
                            if (numCubesForEach[i] > numCubesForEachPreFrame[i])
                            {
                                // When cubes are added
                                Vector3 placementPosition = FindPlacementPosition(posList);
                                //print("placement pos" + placementPosition.ToString());
                                placementPosition.y = cubeHalfLengthList[i];
                                //placementPosition += transform.position;
                                GameObject cube = Instantiate(cubePrefabList[i], placementPosition, Quaternion.identity, transform);
                                cubeList[i].Add(cube);
                                GameObject appearEffect = Instantiate(cubeAppearEffect, placementPosition, Quaternion.identity);
                                appearEffect.transform.localScale = new Vector3(cube.transform.localScale.x,
                                    cube.transform.localScale.y,
                                    cube.transform.localScale.z);
                            }
                        }
                        else
                        {
                            // When the cubes are cube64
                            if (numCubesForEach[0] > numCubesForEachPreFrame[0])
                            {
                                // When cubes are added
                                Vector3 placementPosition = FindPlacementPosition(posList);
                                placementPosition.y = cubeHalfLengthList[0];
                                //placementPosition += transform.position;
                                GameObject cube = Instantiate(cubePrefabList[0], placementPosition, Quaternion.identity, transform);
                                cubeList[0].Add(cube);
                                GameObject appearEffect = Instantiate(cubeAppearEffect, placementPosition, Quaternion.identity);
                                appearEffect.transform.localScale = new Vector3(cube.transform.localScale.x,
                                    cube.transform.localScale.y,
                                    cube.transform.localScale.z);
                            }
                        }
                    }
                }
            }
            
            /*foreach(Vector3 pos in posList)
            {
                GameObject _cell = Instantiate(cellPrefab, transform.position, Quaternion.identity, transform);
                _cell.GetComponent<Cell>().CreateCell(largestCubeSideLength, pos);
                _cell.GetComponent<Cell>().DisplayCell();
            }*/
            
        }
        
        numCubesForEachPreFrame = numCubesForEach;

        //
        GameObject[] playerCubes = GameObject.FindGameObjectsWithTag("PlayerCube");
        int numCubesCurrentFrame = playerCubes.Length;
        
        if (numCubesCurrentFrame == numCubesPreFrame)
        {
            return;
        }

        if (playerCubes.Length == 2)
        {
            GameObject cube1 = playerCubes[0];
            GameObject cube2 = playerCubes[1];
            int largestCubeSize = (int)Mathf.Max(cube1.transform.localScale.x, cube2.transform.localScale.x);
            Vector3 pos1 = new Vector3(transform.position.x - cubeGapWidth / 2f - largestCubeSize,
                0f,
                transform.position.z);
            Vector3 pos2 = new Vector3(transform.position.x + cubeGapWidth / 2f + largestCubeSize,
                0f,
                transform.position.z);
            
            pos1 = new Vector3(pos1.x, cube1.transform.position.y, pos1.z);
            pos2 = new Vector3(pos2.x, cube2.transform.position.y, pos2.z);

            cube1.GetComponent<CubeMove>().destinationVector = pos1 - transform.position;
            cube2.GetComponent<CubeMove>().destinationVector = pos2 - transform.position;
        }
        /*else if (playerCubes.Length == 3)
        {
            GameObject cube1 = playerCubes[0];
            GameObject cube2 = playerCubes[1];
            GameObject cube3 = playerCubes[2];
            int largestCubeSize = (int)Mathf.Max(cube1.transform.localScale.x, cube2.transform.localScale.x, cube3.transform.localScale.x);
            Vector3 pos1 = new Vector3(transform.position.x - 1.5f * cubeGapWidth - 1.5f * largestCubeSize,
                0f,
                transform.position.z);
            Vector3 pos2 = transform.position;
            Vector3 pos3 = new Vector3(transform.position.x + 1.5f * cubeGapWidth + 1.5f * largestCubeSize,
                0f,
                transform.position.z);

            cube1.transform.position = new Vector3(pos1.x, cube1.transform.position.y, pos1.z);
            cube2.transform.position = new Vector3(pos2.x, cube2.transform.position.y, pos2.z);
            cube3.transform.position = new Vector3(pos3.x, cube3.transform.position.y, pos3.z);
        }*/
        else if (playerCubes.Length >= 3)
        {
            // Sort the cubes based on their size
            List<Tuple<float, GameObject>> sortedCubes = new List<Tuple<float, GameObject>>();
            Vector3 centerPoint = Vector3.zero;
            foreach (GameObject cube in playerCubes)
            {
                float size = cube.transform.localScale.x;
                sortedCubes.Add(new Tuple<float, GameObject>(size, cube));

                centerPoint += cube.transform.position;
            }
            centerPoint = centerPoint / numCubesCurrentFrame;

            sortedCubes.Sort((x, y) => y.Item1.CompareTo(x.Item1));
            //sortedCubes.Reverse();
            int largestCubeSize = (int) sortedCubes[0].Item1;
            //print("largestCubeSize: " + largestCubeSize.ToString());
            List<GameObject> tempList = new List<GameObject>();
            foreach (Tuple<float, GameObject> tuple in sortedCubes)
            {
                tempList.Add(tuple.Item2);
            }

            List<Vector3> positionList = CalculateCubePositionsFixedUpdate(playerCubes.Length, largestCubeSize, centerPoint);
            for (int i = 0; i < positionList.Count; i++)
            {
                Vector3 pos = positionList[i];
                Vector3 _pos = new Vector3(pos.x + largestCubeSize / 2f,
                    0f,
                    pos.z - largestCubeSize / 2f);
                positionList[i] = _pos;
                //Instantiate(cellPointIndicatorPrefab, _pos, Quaternion.identity);
            }
            for (int i = 0; i < tempList.Count; i++)
            {
                GameObject cube = tempList[i];
                float minDistance = Mathf.Infinity;
                Vector3 finalPos = Vector3.zero;
                for (int j = 0; j < positionList.Count; j++)
                {
                    float distance = Vector3.Distance(cube.transform.position, positionList[i]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        finalPos = new Vector3(positionList[i].x, cube.transform.position.y, positionList[i].z);
                    }
                }
                cube.GetComponent<CubeMove>().destinationVector = finalPos - transform.position;
                //cube.transform.position = finalPos;
                //tempList[i].transform.position = new Vector3(positionList[i].x, tempList[i].transform.position.y, positionList[i].z);
            }

        }

        numCubesPreFrame = numCubesCurrentFrame;

    }

    private void FixedUpdate()
    {
        
    }

    private void CalculateNumberOfCubes(int number)
    {
        int currentNumber = number;
        numCubesForEach = new List<int>();
        for (int i = 0; i < cubeNumList.Count; i++)
        {
            if (currentNumber % cubeNumList[i] == 0)
            {
                numCubesForEach.Add((int)(currentNumber / cubeNumList[i]));
            }
            else
            {
                numCubesForEach.Add(Mathf.FloorToInt(currentNumber / cubeNumList[i]));
            }
            currentNumber -= (int)numCubesForEach[i] * cubeNumList[i];
        }
        totalNumCubes = 0;
        foreach (int n in numCubesForEach)
        {
            totalNumCubes += n;
        }
    }

    private List<Vector3> CalculateCubePositions()
    {
        if (totalNumCubes == 0)
        {
            Debug.LogError("No cubes!");
            return null;
        }

        List<Vector3> posList = new List<Vector3>();

        //int largestCubeSideLength = 0;
        for (int i = 0; i < numCubesForEach.Count; i++)
        {
            if (numCubesForEach[i] > 0)
            {
                largestCubeSideLength = 6 - i;
                break;
            }
        }
        
        int gridSideLength = 0;
        if (totalNumCubes >= 3)
        {
            if (Mathf.Sqrt(totalNumCubes) % 1 == 0 )
            {
                gridSideLength = (int)Mathf.Sqrt(totalNumCubes);
            }
            else
            {
                gridSideLength = Mathf.FloorToInt(Mathf.Sqrt(totalNumCubes)) + 1;
            }

            int numOfGaps = gridSideLength - 1;

            List<List<Vector3>> tempPosList = new List<List<Vector3>>();
            for (int row = 0; row < gridSideLength; row++)
            {
                tempPosList.Add(new List<Vector3>());
                for (int col = 0; col < gridSideLength; col++)
                {
                    if (row == 0 && col == 0)
                    {
                        Vector3 firstPos = new Vector3(transform.position.x - (numOfGaps / 2f) * cubeGapWidth - gridSideLength * largestCubeSideLength / 2f,
                            0f,
                            transform.position.z + (numOfGaps / 2f) * cubeGapWidth + gridSideLength * largestCubeSideLength / 2f);
                        tempPosList[0].Add(firstPos);
                    }
                    else if (row > 0 && col == 0)
                    {
                        Vector3 prePoint = tempPosList[row - 1][0];
                        Vector3 pos = new Vector3(prePoint.x, 0f, prePoint.z - cubeGapWidth - largestCubeSideLength);
                        tempPosList[row].Add(pos);
                    }
                    else
                    {
                        Vector3 prePoint = tempPosList[row][col - 1];
                        Vector3 pos = new Vector3(prePoint.x + cubeGapWidth + largestCubeSideLength, 0f, prePoint.z);
                        tempPosList[row].Add(pos);
                    }
                }
            }

            for (int row = 0; row < gridSideLength; row++)
            {
                for (int col = 0; col < gridSideLength; col++)
                {
                    posList.Add(tempPosList[row][col]);
                }
            }
        }
        else if (totalNumCubes == 1)
        {
            Vector3 pos = new Vector3(transform.position.x - largestCubeSideLength / 2f,
                0f,
                transform.position.z + largestCubeSideLength / 2f);
            posList.Add(pos);
        }
        else if (totalNumCubes == 2)
        {
            Vector3 pos1 = new Vector3(transform.position.x - cubeGapWidth / 2f - largestCubeSideLength,
                0f,
                transform.position.z + largestCubeSideLength / 2f);
            Vector3 pos2 = new Vector3(transform.position.x + cubeGapWidth / 2f + largestCubeSideLength,
                0f,
                transform.position.z + largestCubeSideLength / 2f);
            posList.Add(pos1);
            posList.Add(pos2);
        }
        /*else if (totalNumCubes == 3)
        {
            Vector3 pos1 = new Vector3(transform.position.x - 1.5f * cubeGapWidth - 1.5f * largestCubeSideLength,
                0f,
                transform.position.z + largestCubeSideLength / 2f);
            Vector3 pos2 = transform.position;
            Vector3 pos3 = new Vector3(transform.position.x + 1.5f * cubeGapWidth + 1.5f * largestCubeSideLength,
                0f,
                transform.position.z + largestCubeSideLength / 2f);
            
            posList.Add(pos2);
            posList.Add(pos1);
            posList.Add(pos3);
        }*/

        // Sort posList based on their distance from the center
        List<Tuple<float, Vector3>> tempListWithDistance = new List<Tuple<float, Vector3>>();
        foreach (Vector3 pos in posList)
        {
            float distance = Vector3.Distance(pos, transform.position);
            tempListWithDistance.Add(new Tuple<float, Vector3>(distance, pos));
        }
        tempListWithDistance.Sort((x, y) => y.Item1.CompareTo(x.Item1));
        tempListWithDistance.Reverse();
        posList = new List<Vector3>();
        foreach (Tuple<float, Vector3> tuple in tempListWithDistance)
        {
            posList.Add(tuple.Item2);
        }
        return posList;
    }

    private List<Vector3> CalculateCubePositionsFixedUpdate(int totalNumCubes, int largestCubeSideLength, Vector3 centerPoint)
    {
        if (totalNumCubes == 0)
        {
            Debug.LogError("No cubes!");
            return null;
        }

        List<Vector3> posList = new List<Vector3>();

        int gridSideLength = 0;
        if (totalNumCubes >= 3)
        {
            if (Mathf.Sqrt(totalNumCubes) % 1 == 0)
            {
                gridSideLength = (int)Mathf.Sqrt(totalNumCubes);
            }
            else
            {
                gridSideLength = Mathf.FloorToInt(Mathf.Sqrt(totalNumCubes)) + 1;
            }

            int numOfGaps = gridSideLength - 1;

            List<List<Vector3>> tempPosList = new List<List<Vector3>>();
            for (int row = 0; row < gridSideLength; row++)
            {
                tempPosList.Add(new List<Vector3>());
                for (int col = 0; col < gridSideLength; col++)
                {
                    if (row == 0 && col == 0)
                    {
                        Vector3 firstPos = new Vector3(transform.position.x - (numOfGaps / 2f) * cubeGapWidth - gridSideLength * largestCubeSideLength / 2f,
                            0f,
                            transform.position.z + (numOfGaps / 2f) * cubeGapWidth + gridSideLength * largestCubeSideLength / 2f);
                        tempPosList[0].Add(firstPos);
                    }
                    else if (row > 0 && col == 0)
                    {
                        Vector3 prePoint = tempPosList[row - 1][0];
                        Vector3 pos = new Vector3(prePoint.x, 0f, prePoint.z - cubeGapWidth - largestCubeSideLength);
                        tempPosList[row].Add(pos);
                    }
                    else
                    {
                        Vector3 prePoint = tempPosList[row][col - 1];
                        Vector3 pos = new Vector3(prePoint.x + cubeGapWidth + largestCubeSideLength, 0f, prePoint.z);
                        tempPosList[row].Add(pos);
                    }
                }
            }

            for (int row = 0; row < gridSideLength; row++)
            {
                for (int col = 0; col < gridSideLength; col++)
                {
                    posList.Add(tempPosList[row][col]);
                }
            }
        }
        else if (totalNumCubes == 1)
        {
            Vector3 pos = new Vector3(transform.position.x - largestCubeSideLength / 2f,
                0f,
                transform.position.z + largestCubeSideLength / 2f);
            posList.Add(pos);
        }
        else if (totalNumCubes == 2)
        {
            Vector3 pos1 = new Vector3(transform.position.x - cubeGapWidth / 2f - largestCubeSideLength,
                0f,
                transform.position.z + largestCubeSideLength / 2f);
            Vector3 pos2 = new Vector3(transform.position.x + cubeGapWidth / 2f + largestCubeSideLength,
                0f,
                transform.position.z + largestCubeSideLength / 2f);
            posList.Add(pos1);
            posList.Add(pos2);
        }
        /*else if (totalNumCubes == 3)
        {
            Vector3 pos1 = new Vector3(transform.position.x - 1.5f * cubeGapWidth - 1.5f * largestCubeSideLength,
                0f,
                transform.position.z + largestCubeSideLength / 2f);
            Vector3 pos2 = transform.position;
            Vector3 pos3 = new Vector3(transform.position.x + 1.5f * cubeGapWidth + 1.5f * largestCubeSideLength,
                0f,
                transform.position.z + largestCubeSideLength / 2f);

            posList.Add(pos2);
            posList.Add(pos1);
            posList.Add(pos3);
        }*/

        // Sort posList based on their distance from the center
        List<Tuple<float, Vector3>> tempListWithDistance = new List<Tuple<float, Vector3>>();
        foreach (Vector3 pos in posList)
        {
            float distance = Vector3.Distance(pos, centerPoint);
            tempListWithDistance.Add(new Tuple<float, Vector3>(distance, pos));
        }
        tempListWithDistance.Sort((x, y) => y.Item1.CompareTo(x.Item1));
        tempListWithDistance.Reverse();
        posList = new List<Vector3>();
        foreach (Tuple<float, Vector3> tuple in tempListWithDistance)
        {
            posList.Add(tuple.Item2);
        }
        return posList;
    }

    private bool ListMatch(List<int> list1, List<int> list2)
    {
        bool match = true;
        if (list1.Count != list2.Count)
        {
            return false;
        }
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
            {
                match = false;
                break;
            }
        }
        return match;
    }

    private Vector3 FindPlacementPosition(List<Vector3> posList)
    {
        Vector3 position = Vector3.zero;
        for (int i = 0; i < posList.Count; i++)
        {
            Vector3 centerPoint = posList[i];
            /*Vector3 centerPoint = new Vector3(verticePoint.x + largestCubeSideLength / 2f,
                0f, verticePoint.z - largestCubeSideLength / 2f);*/
            Collider[] colliders = Physics.OverlapSphere(centerPoint, largestCubeSideLength / 2f, playerCubeLayer);
            if (colliders.Length > 0)
            {
                continue;
            }
            else
            {
                position = centerPoint;
            }
        }
        if ((position - Vector3.zero).magnitude < 0.1f)
        {
            GameObject[] playerCubes = GameObject.FindGameObjectsWithTag("PlayerCube");
            float leftMostX = Mathf.Infinity;
            foreach (GameObject cube in playerCubes)
            {
                if (cube.transform.position.x < leftMostX)
                {
                    leftMostX = cube.transform.position.x;
                    position = new Vector3(cube.transform.position.x - cubeGapWidth - largestCubeSideLength,
                        0f,
                        cube.transform.position.z);
                }
            }
        }

        return position;
    }
}
