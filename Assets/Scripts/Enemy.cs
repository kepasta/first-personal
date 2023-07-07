using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float damage;
    public float reward;
    private GameManager gameManager;
    private IEnumerator dealingDamage;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        dealingDamage = DealDamage();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            gameManager.UpdateGold(reward);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile") && other.gameObject.GetComponent<Weapon>().hitCount > 0)
        {
            TakeDamage(other.gameObject.GetComponent<Weapon>().damage);
            other.gameObject.GetComponent<Weapon>().Hit();
            //Debug.Log("Enemy Trigger " + other.gameObject.GetComponent<Weapon>().damage);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(dealingDamage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            StopCoroutine(dealingDamage);
        } 
    }


    void TakeDamage (float damage)
    {
        hp -= damage;
    }

    IEnumerator DealDamage ()
    {
        while (true)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().UpdateHp(-damage);
            Debug.Log("hp: " + GameObject.FindWithTag("Player").GetComponent<PlayerController>().hp);
            yield return new WaitForSeconds(1);
        }
    }
}
