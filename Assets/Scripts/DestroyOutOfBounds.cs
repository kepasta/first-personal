using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private GameObject mainCamera;
    private float xBoundary = 30;
    private float yBoundary = 17;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float cameraY = mainCamera.transform.position.y;
        float cameraX = mainCamera.transform.position.x;
        if (Mathf.Abs(xPos - cameraX) > xBoundary || Mathf.Abs(yPos - cameraY) > yBoundary)
        {
            Destroy(gameObject);
        }
    }
}
