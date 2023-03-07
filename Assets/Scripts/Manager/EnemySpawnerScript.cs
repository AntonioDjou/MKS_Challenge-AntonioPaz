using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawnerScript : MonoBehaviour
{
    public TextMeshProUGUI enemyText;
    public static int enemyCounter;

    [Space(10)]
    public GameObject[] enemyPrefabs;
    public GameObject[] obstaclePrefabs;
    float enemyTimer = 3;

    void Start()
    {
        EnemySpawnerScript.enemyCounter = 0;
        if(SceneManagerScript.enemySpawnDuration !=0) enemyTimer = SceneManagerScript.enemySpawnDuration;
        else enemyTimer = 5;
        
        //enemyText.text = $"Enemies: {EnemySpawnerScript.enemyCounter.ToString()}";

        StartCoroutine(SpawnEnemies());
        SpawnObstacles();
    }

    public void DecreaseEnemyCounter()
    {
        EnemySpawnerScript.enemyCounter--;   
    }

    void FixedUpdate()
    {
        enemyText.text = $"Enemies: {EnemySpawnerScript.enemyCounter}";
    }

    IEnumerator SpawnEnemies()
    {
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);

        yield return new WaitForSeconds(enemyTimer);
        Instantiate(enemyPrefabs[randomEnemy], new Vector3(Random.Range(-15, 15), Random.Range(-15,15), 0), Quaternion.identity);
        
        EnemySpawnerScript.enemyCounter++;
        //Debug.Log($"Enemy Increased to: {EnemySpawnerScript.enemyCounter.ToString()}");
        
        //enemyText.text = $"Enemies: {EnemySpawnerScript.enemyCounter.ToString()}";
        StartCoroutine(SpawnEnemies());
    }

    void SpawnObstacles()
    {
        int randomObstacle = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[randomObstacle], new Vector3(Random.Range(-15, 12), Random.Range(-12,14), 0), Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        Instantiate(obstaclePrefabs[randomObstacle], new Vector3(Random.Range(-12, 15), Random.Range(-16,14), 0), Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
    }
}
