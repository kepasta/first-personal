using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private float horizontalBound = 50.0f;
    private float verticalBound = 50.0f;
    public float maxHp;
    public float hp;
    public GameObject[] weapons;
    private Coroutine[] weaponCoroutines;
    private int maxWeaponLvl = 6;
    private int currentWeapon;
    public Vector3 movingDir = new(1, 0, 0);
    private int[] weaponLvl;



    // Start is called before the first frame update
    void Start()
    {
        weaponCoroutines = new Coroutine[weapons.Length];
        currentWeapon = 0;
        maxHp = 100;
        hp = maxHp;
        weaponLvl = new int[] { 0, 0 };
        UpgradeWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();

        // ->Game manager 
        if (hp <= 0)
        {
            // Game Over
        }
    }

    // Moves the player based on WASD/arrows input
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 horizontalStep = horizontalInput * speed * Time.deltaTime * Vector3.right;
        Vector3 verticalStep = speed * Time.deltaTime * verticalInput * Vector3.up;
        transform.Translate(horizontalStep);
        transform.Translate(verticalStep);
        if (horizontalStep != Vector3.zero || verticalStep != Vector3.zero)
        {
            movingDir = (horizontalStep + verticalStep).normalized;
        }
    }

    // Prevents the player from leaving the game area
    void ConstrainPlayerPosition()
    {
        // Horizontal position
        if (Mathf.Abs(transform.position.x) >= horizontalBound)
        {
            transform.position = new(Mathf.Sign(transform.position.x) * horizontalBound, transform.position.y, transform.position.z);
        }
        // Vertical position
        if (Mathf.Abs(transform.position.y) >= verticalBound)
        {
            transform.position = new(transform.position.x, Mathf.Sign(transform.position.y) * verticalBound, transform.position.z);
        }
    }

    // Weapon_1 
    IEnumerator SpawnWeaponOne(float fireRate)
    {
        while (true)
        {
            GameObject weapon = weapons[0];
            weapon.transform.rotation = Quaternion.AngleAxis(Vector3.Angle(Vector3.right, movingDir) * Mathf.Sign(movingDir.y), Vector3.forward);
            Instantiate(weapon, gameObject.transform.position, weapon.transform.rotation);
            yield return new WaitForSeconds(weapon.GetComponent<Weapon>().fireRate / fireRate);
            //Debug.Log("pos:" + (gameObject.transform.position + new Vector3(0, 3, 0)).ToString() + "\nweapon: " + weapon.ToString());
        }
    }

    IEnumerator SpawnWeaponTwo(int number)
    {
        while (true)
        {
            for (int i = 0; i < number; i++)
            {
                Instantiate(weapons[1], gameObject.transform.position, weapons[1].transform.rotation);
            }
            yield return new WaitForSeconds(weapons[1].GetComponent<Weapon>().fireRate);
        }
    }

    void UpgradeWeapon()
    {
        if (weaponLvl[currentWeapon] == maxWeaponLvl && currentWeapon < weapons.Length - 1)
        {
            currentWeapon++;
        }
        if (weaponLvl[currentWeapon] < maxWeaponLvl)
        {
            weaponLvl[currentWeapon]++;
            switch (currentWeapon)
            {
                case 0:
                    if (weaponLvl[currentWeapon] > 1)
                    {
                        StopCoroutine(weaponCoroutines[currentWeapon]);
                    }
                    weaponCoroutines[currentWeapon]  = StartCoroutine(SpawnWeaponOne(weaponLvl[currentWeapon]));
                    break;
                case 1:
                    if (weaponLvl[currentWeapon] > 1)
                    {
                        StopCoroutine(weaponCoroutines[currentWeapon]);
                    }
                    weaponCoroutines[currentWeapon] = StartCoroutine(SpawnWeaponTwo(weaponLvl[currentWeapon]));
                    break;
                default:
                    Debug.Log("switch weapon error");
                    break;
            }
        }
    }

    public void UpdateHp (float hpToAdd)
    {
        hp += hpToAdd;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon Powerup"))
        {
            UpgradeWeapon();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Healthpack"))
        {
            UpdateHp(20);
            Destroy(other.gameObject);
        }
    }
}
