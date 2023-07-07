using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int hitCount;
    public float damage;
    public int fireRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.CompareTag("Enemy") && hitCount > 0)
        {
            hitCount--;
            Debug.Log("Weapon trigger " + hitCount);
        }*/
    }

    public void Hit()
    {
        hitCount--;
    }
}
