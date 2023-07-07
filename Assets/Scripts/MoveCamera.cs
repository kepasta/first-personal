using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Vector3 offset = new(0, 0, -16);
    public GameObject player;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (gameManager.isGameActive && player.activeInHierarchy)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
