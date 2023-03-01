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
        enemyCounter = 0;
        if(SceneManagerScript.enemySpawnDuration !=0) enemyTimer = SceneManagerScript.enemySpawnDuration;
        else enemyTimer = 5;
        //enemyText.text = $"Enemies: {enemyCounter}";

        StartCoroutine(SpawnEnemies());
    }

    /*public void DecreaseEnemyCounter()
    {
        if(enemyCounter > 0) 
        {
            enemyCounter--;
            Debug.Log(enemyCounter);
            //enemyText.text = $"Enemies: {enemyCounter}";
        }      
    }*/

    void FixedUpdate()
    {
        enemyText.text = $"Enemies: {enemyCounter}";
    }

    IEnumerator SpawnEnemies()
    {
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);

        yield return new WaitForSeconds(enemyTimer);
        Instantiate(enemyPrefabs[randomEnemy], new Vector3(Random.Range(-10, 10), Random.Range(-10,10), 0), Quaternion.identity);
        enemyCounter++;
        //enemyText.text = $"Enemies: {enemyCounter}";
        StartCoroutine(SpawnEnemies());
    }
}
