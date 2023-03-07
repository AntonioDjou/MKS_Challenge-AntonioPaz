using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    //public static event Action<EnemyScript> OnEnemyKilled;
    
    public float enemyMoveSpeed = 4;
    public float maxHealth = 30;
    float currentHealth = 0;
    
    Rigidbody2D enemyRigidBody;
    Vector2 moveDirection;
    
    [Space(5)]
    public Image enemyHealthBarFG; // The visual Health Bar that will be updated.
    public EnemySpawnerScript enemySpawnerScript;

    Transform target;
    Animator enemyAnimator;

    public Sprite shipDamage1, shipDamage2;
    float lastDelete = 0;

    void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        enemyAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        lastDelete += Time.deltaTime;
        if(!PlayerControllerScript.playerInvincible)
        {
            Vector3 direction = (target.position - transform.position).normalized; // The Player position relative to the enemy position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // The angle the enemy will rotate to chase the player
            enemyRigidBody.rotation = angle + 90; // Adjusts the correct angle for the sprite
            moveDirection = direction;
        }       
    }

    void FixedUpdate()
    {
        if(!PlayerControllerScript.playerInvincible)
        {
            enemyRigidBody.velocity = new Vector2(moveDirection.x, moveDirection.y) * enemyMoveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !PlayerControllerScript.playerInvincible)
        {
            enemyAnimator.SetBool("isDead", true); // Starts the Death animation
            if(lastDelete > 1.5f) // To make sure not to decrease the enemy amount twice
            {
                lastDelete = 0;
                enemySpawnerScript.DecreaseEnemyCounter();
            }
            enemyRigidBody.isKinematic = true; // Stops the enemy from moving after death
            enemyHealthBarFG.fillAmount = 0;

            Invoke("EnemyDestroy", 1f); // Wait 1 second for the explosion animation
        }
        if(collision.gameObject.tag == "Bullet")
        {
            TakeDamage(10);
        }
    }

    void UpdateHealthBar()
	{
        enemyHealthBarFG.fillAmount = currentHealth / maxHealth;
        if(enemyHealthBarFG.fillAmount <= 0.7 && enemyHealthBarFG.fillAmount >= 0.4) GetComponent<SpriteRenderer>().sprite = shipDamage1;
        else if (enemyHealthBarFG.fillAmount <= 0.4) GetComponent<SpriteRenderer>().sprite = shipDamage2;
	}

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();
        if(currentHealth <= 0)
        {
            if(lastDelete > 1f) // To make sure not to decrease the enemy amount twice
            {
                lastDelete = 0;
                enemySpawnerScript.DecreaseEnemyCounter();
            }
            Destroy(gameObject);
            //OnEnemyKilled?.Invoke(this);
        }
    }

    void EnemyDestroy() // Destroys the enemy after colliding with the Player.
    {
        Destroy(this.gameObject);
        TakeDamage(maxHealth);
    }
}
