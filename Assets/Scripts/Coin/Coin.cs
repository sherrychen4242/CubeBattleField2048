using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [Header("Spin")]
    [SerializeField] float amplitude;
    [SerializeField] float verticalSpeed;
    [SerializeField] float rotationSpeed;

    [Space]
    [SerializeField] string playerTag;
    #endregion

    #region PRIVATE VARIABLES
    private Vector3 startPosition;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Spin();
    }

    void Spin()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * verticalSpeed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            if (other.gameObject.GetComponent<Player>() != null)
            {
                SoundManager.Instance.PlaySound(SoundManager.SoundEffects.PickUpCoinSound);
                other.gameObject.GetComponent<Player>().PickUpCoin();
                Destroy(gameObject);
            }
            else if (other.gameObject.GetComponentInParent<Player>() != null)
            {
                SoundManager.Instance.PlaySound(SoundManager.SoundEffects.PickUpCoinSound);
                other.gameObject.GetComponentInParent<Player>().PickUpCoin();
                Destroy(gameObject);
            }
        }
    }
}
