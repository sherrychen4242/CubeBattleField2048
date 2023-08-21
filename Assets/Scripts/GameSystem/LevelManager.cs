using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [HideInInspector]
    public static LevelManager levelManagerInstance { get; private set; }
    public LevelGenerator levelGeneratorInstance { get; private set; }
    public EnemySpawner enemySpawnerInstance { get; private set; }
    public NavMeshBaker navMeshBaker { get; private set; }

    [Header("Level Related")]
    public int currentLevel = 1;
    public int enemyNumberForCurrentLevel = 2;

    [Header("Player Related")]
    public int playerCurrentNumber = 2;

    [Header("Wasted")]
    [SerializeField] GameObject wastedCanvas;

    [Header("ReadyStart")]
    [SerializeField] GameObject readyStartCanvas;

    [Header("Portal")]
    [SerializeField] GameObject PortalPrefab;

    #endregion

    #region PRIVATE VARIABLES
    private Vector3[] oneBlockVertices = new Vector3[2] { new Vector3(-30f, 0f, -30f), new Vector3(30f, 0f, 30f) };
    private Vector3[] fourBlockVertices = new Vector3[2] { new Vector3(-60f, 0f, -60f), new Vector3(60f, 0f, 60f) };
    private Vector3[] nineBlockVertices = new Vector3[2] { new Vector3(-90f, 0f, -90f), new Vector3(90f, 0f, 90f) };
    private Vector3 oneBlockPortalPos = new Vector3(0f, 0f, 20f);
    private Vector3 fourBlockPortalPos = new Vector3(0f, 0f, 50f);
    private Vector3 nineBlockPortalPos = new Vector3(0f, 0f, 80f);


    public bool portalFlag = false;
    public bool saveDataFlag = false;
    #endregion

    private void Awake()
    {
        //DontDestroyOnLoad(this);
        // Singleton class
        if (levelManagerInstance != null && levelManagerInstance != this)
        {
            Debug.Log("More than one level manager!");
            Destroy(this);
            return;
        }
        
        levelManagerInstance = this;
        levelGeneratorInstance = GetComponent<LevelGenerator>();
        enemySpawnerInstance = GetComponent<EnemySpawner>();
        navMeshBaker = GetComponent<NavMeshBaker>();

        //wastedCanvas.SetActive(false);

        // Generate the first level
        levelGeneratorInstance.GenerateGroundPrefabs(enemyNumberForCurrentLevel);
        // Build Nav Mesh
        //navMeshBaker.BuildNavMesh();
        // Spawn Enemies
        enemySpawnerInstance.SpawnEnemies(enemyNumberForCurrentLevel, oneBlockVertices[0], oneBlockVertices[1]);

    }

    public void Initialize()
    {
        currentLevel = 1;
        enemyNumberForCurrentLevel = 2;
        playerCurrentNumber = 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerCurrentNumber = FindObjectOfType<Player>().currentNumber;

        // Check whether player dies
        CubeMove[] cubeMoves = FindObjectsOfType<CubeMove>();
        bool allOutside = true;
        foreach (CubeMove cm in cubeMoves)
        {
            if (cm.onGround)
            {
                allOutside = false;
                break;
            }
        }

        if (playerCurrentNumber <= 0 || allOutside)
        {
            //Time.timeScale = 0.1f;
            // Save coins earned
            if (!saveDataFlag)
            {
                int coinsObtained = FindObjectOfType<Player>().highestNumber;
                DataPersistenceManager.instance.gameData.totalCoins += coinsObtained;
                DataPersistenceManager.instance.SaveGame();
                saveDataFlag = true;
            }
            
            FindObjectOfType<CameraFollow>().enabled = false;
            wastedCanvas.SetActive(true);
            wastedCanvas.GetComponent<WastedCanvas>().Appear();
            if (wastedCanvas.GetComponent<WastedCanvas>().appearFinished)
            {
                SceneManager.LoadScene(1);
            }
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            if (!portalFlag)
            {
                if (enemyNumberForCurrentLevel < 64)
                {
                    Instantiate(PortalPrefab, oneBlockPortalPos, Quaternion.identity);
                }
                else if (enemyNumberForCurrentLevel < 1024)
                {
                    Instantiate(PortalPrefab, fourBlockPortalPos, Quaternion.identity);
                }
                else
                {
                    Instantiate(PortalPrefab, nineBlockPortalPos, Quaternion.identity);
                }

                portalFlag = true;
            }
            
        }
    }

    public void GenerateNextLevel()
    {
        // Level up
        currentLevel++;
        if (enemyNumberForCurrentLevel < 16)
        {
            enemyNumberForCurrentLevel *= 2;
        }
        else
        {
            enemyNumberForCurrentLevel = enemyNumberForCurrentLevel * 2 - 2;
        }

        // Clean the current scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //navMeshBaker.RemoveNavMesh();
        foreach (GameObject ground in GameObject.FindGameObjectsWithTag("Ground"))
        {
            Destroy(ground);
        }
        foreach (GameObject coin in GameObject.FindGameObjectsWithTag("Coin"))
        {
            Destroy(coin);
        }
        Destroy(GameObject.FindGameObjectWithTag("Portal"));

        // Place the player
        GameObject.FindGameObjectWithTag("Player").transform.position = Vector3.zero;


        // Generate next Level level
        levelGeneratorInstance.GenerateGroundPrefabs(enemyNumberForCurrentLevel);
        // Build Nav Mesh
        //navMeshBaker.BuildNavMesh();
        // Spawn Enemies
        if (enemyNumberForCurrentLevel < 64)
        {
            enemySpawnerInstance.SpawnEnemies(enemyNumberForCurrentLevel, oneBlockVertices[0], oneBlockVertices[1]);
        }
        else if (enemyNumberForCurrentLevel < 1024)
        {
            enemySpawnerInstance.SpawnEnemies(enemyNumberForCurrentLevel, fourBlockVertices[0], fourBlockVertices[1]);
        }
        else
        {
            enemySpawnerInstance.SpawnEnemies(enemyNumberForCurrentLevel, nineBlockVertices[0], nineBlockVertices[1]);
        }

        portalFlag = false;

        StartCoroutine(SlowDown());

        StartCoroutine(readyStartCanvas.GetComponent<ReadyStartCanvas>().CountDown());
    }

    IEnumerator SlowDown()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 1f;
    }
}
