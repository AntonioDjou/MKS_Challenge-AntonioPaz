using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    /*public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20;
    bool isMoving = false;
    */
    /*public float bulletSpeed = 20;
    private Rigidbody2D bulletRigidBody;

    GameObject player;   
    //Vector3 playerRotation;
  

    void OnEnable()
    {
        if(bulletRigidBody != null)
        {
            bulletRigidBody.velocity = Vector2.up * bulletSpeed;
            //bulletRigidBody.transform.Translate(Vector3.up /* * Time.deltaTime * bulletSpeed);
            //bulletRigidBody.velocity = bulletRigidBody.transform.position * Time.deltaTime * bulletSpeed * 50;
        }
        Invoke("Disable", 3f);
    }

    void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
        //player = GameObject.Find("Player"); 
        //Quaternion playerRotation = player.transform.rotation;
        //bulletRigidBody.MoveRotation(bulletRigidBody.rotation * playerRotation);
        
        bulletRigidBody.velocity = Vector2.up * bulletSpeed;
        //bulletRigidBody.transform.Translate(Vector3.up * Time.deltaTime * bulletSpeed);
        //bulletRigidBody.velocity = bulletRigidBody.transform.position;
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<EnemyScript>(out EnemyScript enemyComponent)) // If the target of the collision has a EnemyScript attached to it
        {
            enemyComponent.TakeDamage(1); // Calls the function to Take "1" out of damage from the enemy
            gameObject.SetActive(false); // Destroys the bullet after collision;
        }        
    }
    
    void OnDisable() // To make sure it won't keep calling the Invoke Method
    {
        CancelInvoke();
    }
    */

    /*void Start()
    {
        isMoving = false;
    }

    public void Shoot()
    {
        isMoving = true;
    }

    void Update()
    {
        if(isMoving)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRigidBody = GetComponent<Rigidbody2D>();	
            bulletRigidBody.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }
    */

    void Start()
    {
        Invoke("Terminate", 3f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
                
    }

    void Terminate()
    {
        Destroy(gameObject);
    }

}
