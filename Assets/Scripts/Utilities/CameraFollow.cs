using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float origYPos;
    public float origCameraSize;
    // Start is called before the first frame update
    void Start()
    {
        origYPos = transform.position.y;
        origCameraSize = GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int playerNumber = player.GetComponent<Player>().currentNumber;
        
        transform.position = new Vector3(player.transform.position.x, origYPos, player.transform.position.z);
        GetComponent<Camera>().orthographicSize = origCameraSize + playerNumber / 200f;
    }
}
