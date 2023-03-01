using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public static event Action<EnemyScript> OnEnemyKilled;
    
    public float maxHealth = 30;
    float currentHealth = 0;
    public Image enemyHealthBarFG; // The visual Health Bar that will be updated.
    
    Rigidbody2D enemyRigidBody;
    Vector2 moveDirection;
    public float enemyMoveSpeed = 5;
    
    Transform target;
    public PlayerControllerScript playerScript;
    public EnemySpawnerScript enemySpawnerScript;

    bool updateCounter = true;

    void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(target) // Checks if there's a target
        {
            Vector3 direction = (target.position - transform.position).normalized; // The Player position relative to the enemy position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // The angle the enemy will rotate to chase the player
            enemyRigidBody.rotation = angle + 90;
            moveDirection = direction;
        }       
    }

    void FixedUpdate()
    {
        if(/*target && */!playerScript.playerInvincible)
        {
            enemyRigidBody.velocity = new Vector2(moveDirection.x, moveDirection.y) * enemyMoveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !playerScript.playerInvincible)
        {
            playerScript.TakeDamage(10);
        }
        if(collision.gameObject.tag == "Bullet")
        {
            TakeDamage(10);
        }
    }

    void UpdateHealthBar()
	{
        enemyHealthBarFG.fillAmount = currentHealth / maxHealth;
	}

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();
        if(currentHealth <= 0)
        {
            if(updateCounter && EnemySpawnerScript.enemyCounter > 0) EnemySpawnerScript.enemyCounter--;
            Debug.Log("Here");
            updateCounter = false;
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);

            //EnemySpawnerScript.enemyAmount--;
            //EnemySpawnerScript.enemyCounter.text = $"Enemies: {EnemySpawnerScript.enemyAmount}";
            
        }
        updateCounter = true;
    }
}
