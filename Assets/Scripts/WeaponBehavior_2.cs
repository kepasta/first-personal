using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior_2 : MonoBehaviour
{
    private Rigidbody projectileRb;
    private float force = 10.0f;
    private float torqueForce = 20.0f;
    private float throwRange = 1;
    // Start is called before the first frame update
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
        float yDir = 1;
        float xDir = Random.Range(-throwRange, throwRange);
        Vector3 torqueDir = Vector3.forward * -xDir;
        Vector3 throwDir = new(xDir, yDir, 0);
        projectileRb.AddForce(throwDir.normalized * force, ForceMode.Impulse);
        projectileRb.AddTorque(torqueDir * torqueForce, ForceMode.Impulse);
        //Invoke(nameof(Throw), 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
