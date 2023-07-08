using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> enemies;
    public GameObject weaponPowerup;
    public GameObject healthPack;
    public TextMeshProUGUI goldText;
    public GameObject startScreen;
    private float gold;
    private float spawnRate = 10;
    [SerializeField] int waveSize;
    private int[] enemyWaveLimits = new int[] { 30, 50, 70, 75 };
    private float xSpawnRange = 8;
    private float ySpawnRange = 6;
    private float gameTime = 0;
    public bool isGameActive = false;
    public bool isGamePaused = false;
    // Start is called before the first frame update
    public void StartGame()
    {
        startScreen.SetActive(false);
        isGameActive = true;
        goldText.enabled = true;
        player.SetActive(true);
        waveSize = 5;
        gold = 0;
        UpdateGold(0);
        InvokeRepeating(nameof(SpawnWave), 3f, 15f);
        InvokeRepeating(nameof(SpawnPowerup), 10f, 20f);
        InvokeRepeating(nameof(SpawnHealthpack), 30f, 60f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive && !isGamePaused)
        {
            gameTime += Time.deltaTime;
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < waveSize; i++)
        {
            int enemyIndex;
            float xSpawnPos = Random.Range(-30f, 30f);
            float ySpawnPos = Random.Range(0, ySpawnRange);
            int ySide = Random.Range(0, 2);

            if (xSpawnPos < -30 + xSpawnRange || xSpawnPos > 30 - xSpawnRange)
            {
                ySpawnPos = Random.Range(-17f, 17f);
            } else if (ySide == 0)
            {
                ySpawnPos -= 17;
            } else
            {
                ySpawnPos = 17 - ySpawnPos;
            }
            
            Vector3 spawnPos = new(xSpawnPos, ySpawnPos, player.transform.position.z);
            if (i < enemyWaveLimits[0])
            {
                enemyIndex = 0;
            }
            else if (i < enemyWaveLimits[1])
            {
                enemyIndex = 1;
            }
            else if (i < enemyWaveLimits[2])
            {
                enemyIndex = 2;
            }
            else
            {
                enemyIndex = 3;
            }
            Instantiate(enemies[enemyIndex], spawnPos, enemies[enemyIndex].transform.rotation);
        }
        waveSize += 2;
    }

    void SpawnPowerup()
    {
        float xSpawnPos = Random.Range(-15f, 15f);
        float ySpawnPos = Random.Range(-8f, 8f);
        Vector3 spawnPos = new(xSpawnPos, ySpawnPos, player.transform.position.z);
        Instantiate(weaponPowerup, spawnPos, weaponPowerup.transform.rotation);
    }

    void SpawnHealthpack()
    {
        float xSpawnPos = Random.Range(-15f, 15f);
        float ySpawnPos = Random.Range(-8f, 8f);
        Vector3 spawnPos = new(xSpawnPos, ySpawnPos, player.transform.position.z);
        Instantiate(healthPack, spawnPos, healthPack.transform.rotation);
    }

    public void UpdateGold(float reward)
    {
        gold += reward;
        goldText.text = "Gold: " + Mathf.FloorToInt(gold);
    }


}
