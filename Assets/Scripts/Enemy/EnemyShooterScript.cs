using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShooterScript : MonoBehaviour
{
    public static event Action<EnemyShooterScript> OnEnemyKilled;
    
    public float maxHealth = 30;
    float currentHealth = 0;
    public Image enemyHealthBarFG;
    
    
    Rigidbody2D enemyRigidBody;
    Vector2 moveDirection;
    public float enemyMoveSpeed = 5;
    
    
    public PlayerControllerScript playerScript;
    public EnemySpawnerScript enemySpawnerScript;

    Transform target;
    bool playerInRange = false;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20;

    public float timeBetweenBullets = 1;
	float lastShot;

    bool updateCounter = true;

    void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
    }
    void Start()
    {
        currentHealth = maxHealth;
        lastShot = timeBetweenBullets;
        UpdateHealthBar();
    }

    void Update()
    {
        lastShot += Time.deltaTime;
        if(/*target && */!playerInRange && !playerScript.playerInvincible) // Checks if there's a target
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
        if(/*target && */!playerScript.playerInvincible && playerInRange)
        {
            if(lastShot >= timeBetweenBullets)
        	{
				lastShot = 0;
				EnemyFire();
			}
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
            Debug.Log(updateCounter);
            if(updateCounter && EnemySpawnerScript.enemyCounter > 0) EnemySpawnerScript.enemyCounter--;
            Debug.Log("Enemy Shooter");
            updateCounter = false;
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);

            //EnemySpawnerScript.enemyAmount--;
            //EnemySpawnerScript.enemyCounter.text = $"Enemies: {EnemySpawnerScript.enemyAmount}";
            
        }
        updateCounter = true;
    }
    
    void EnemyFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();	
		bulletRigidBody.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        playerInRange = false;
    }
}
