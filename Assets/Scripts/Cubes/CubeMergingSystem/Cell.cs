using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public int SideLength { get; set; }
    public Vector3 FirstUnitCellCoordinate { get; set; }
    public List<List<Vector3>> CoordinateList { get; private set; }
    [SerializeField] GameObject pointIndicatorForDebugging;

    #endregion

    #region PRIVATE VARIABLES
    private Color pointIndicatorColor;
    public List<GameObject> pointIndicatorsCollection = new List<GameObject>();

    #endregion

    private void Awake()
    {
        pointIndicatorColor = Random.ColorHSV();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateCell(int sideLength, Vector3 firstUnitCellCoordinate)
    {
        SideLength = sideLength;
        FirstUnitCellCoordinate = firstUnitCellCoordinate;
        InitializeCell();
    }

    public void InitializeCell()
    {
        if (SideLength == 0 || FirstUnitCellCoordinate == null)
        {
            Debug.LogError("Initialize cell failed. No side length or unit cell coordinate defined");
            return;
        }

        CoordinateList = new List<List<Vector3>>();
        for (int row = 0; row < SideLength; row++)
        {
            CoordinateList.Add(new List<Vector3>());
            for (int col = 0; col < SideLength; col++)
            {
                if (row == 0 && col == 0)
                {
                    Vector3 firstCellCenterPoint = new Vector3(FirstUnitCellCoordinate.x + 0.5f,
                        FirstUnitCellCoordinate.y,
                        FirstUnitCellCoordinate.z - 0.5f);
                    CoordinateList[0].Add(firstCellCenterPoint);
                }
                else if (row > 0 && col == 0)
                {
                    Vector3 previousPoint = CoordinateList[row - 1][0];
                    Vector3 centerPoint = new Vector3(previousPoint.x, previousPoint.y, previousPoint.z - 1f);
                    CoordinateList[row].Add(centerPoint);
                }
                else
                {
                    Vector3 previousPoint = CoordinateList[row][col - 1];
                    Vector3 centerPoint = new Vector3(previousPoint.x + 1f, previousPoint.y, previousPoint.z);
                    CoordinateList[row].Add(centerPoint);
                }
            }
        }
    }

    public void DisplayCell()
    {
        if (CoordinateList.Count == 0)
        {
            Debug.LogError("Coordinate list not initialized!");
            return;
        }

        for (int row = 0; row < SideLength; row++)
        {
            for (int col = 0; col < SideLength; col++)
            {
                GameObject indicator = Instantiate(pointIndicatorForDebugging, CoordinateList[row][col], Quaternion.identity);
                indicator.GetComponent<Renderer>().material.color = pointIndicatorColor;
                pointIndicatorsCollection.Add(indicator);
            }
        }
    }

    public void ErasePointIndicators()
    {
        if (pointIndicatorsCollection.Count == 0)
        {
            Debug.LogError("Point Indicator Collection is null!");
            return;
        }

        foreach (GameObject point in pointIndicatorsCollection)
        {
            Destroy(point);
        }
    }
}
