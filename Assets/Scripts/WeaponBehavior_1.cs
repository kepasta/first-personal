using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior_1 : MonoBehaviour
{
    private float speed = 30;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    public void MoveForward()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
