using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawnerScript : MonoBehaviour
{
    public TextMeshProUGUI enemyText;
    public static int enemyCounter;

    [Space(10)]
    public GameObject[] enemyPrefabs;
    float enemyTimer = 3;

    void Start()
    {
        EnemySpawnerScript.enemyCounter = 0;
        if(SceneManagerScript.enemySpawnDuration !=0) enemyTimer = SceneManagerScript.enemySpawnDuration;
        else enemyTimer = 5;
        
        //enemyText.text = $"Enemies: {EnemySpawnerScript.enemyCounter.ToString()}";

        StartCoroutine(SpawnEnemies());
    }

    public void DecreaseEnemyCounter()
    {
        //Debug.Log("EnemyDecrease Script!");
        //if(enemyCounter > 0) 
        //{
        Debug.Log($"Enemy counter before reducing: {EnemySpawnerScript.enemyCounter.ToString()}");
        EnemySpawnerScript.enemyCounter--;
        Debug.Log($"EnemyReduced to: {EnemySpawnerScript.enemyCounter.ToString()}");
        //enemyText.text = EnemySpawnerScript.enemyCounter.ToString();
        //}      
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
}
