using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class PlayerControllerScript : MonoBehaviour
{
    float veloc;
	//float velocRot;
	public Camera mainCamera;
	public bool playerInvincible;
	
	public Transform firePosition;
	public float timeBetweenBullets = 1;
	float lastShot;
	[SerializeField] float lastLateralShot;
	
	
	public Image healthBar;
	public float maxHealth = 100;
	public float currentHealth = 0;

	public GameObject bulletPrefab;
	public Transform firePoint;
    public float bulletSpeed = 20;

	public Transform firePointL1;
	public Transform firePointL2;
	public Transform firePointL3;

	public Transform firePointR1;
	public Transform firePointR2;
	public Transform firePointR3;

	void Start(){
		veloc = 8f;
		//velocRot = 160f;
		currentHealth = maxHealth;
		UpdateHealthBar();
		lastShot = timeBetweenBullets;
		lastLateralShot = timeBetweenBullets;
	}

	void Update () 
	{
		float x = Input.GetAxis("Vertical");
		//float y = Input.GetAxis("Horizontal");

		float desloc = Mathf.Clamp(x, 0f, 1f) * veloc * Time.deltaTime;
		//float deslocV = y * velocRot * Time.deltaTime;
		transform.Translate(0, desloc, 0);
		//transform.Rotate(0, 0, -1*deslocV);*/

		LookAtMouse();
		
		lastShot += Time.deltaTime;
		lastLateralShot += Time.deltaTime;
        
		if(Input.GetButtonDown("Fire1"))
		{
			if(lastShot >= timeBetweenBullets)
        	{
				lastShot = 0;
				FrontalShoot();
			}
		}

		if(Input.GetKeyDown(KeyCode.Z))
		{
			
			if(lastLateralShot >= timeBetweenBullets)
        	{
				lastLateralShot = 0;
				LeftShoot();
			}
		}

		if(Input.GetKeyDown(KeyCode.X))
		{
			
			if(lastLateralShot >= timeBetweenBullets)
        	{
				lastLateralShot = 0;
				RightShoot();
			}
		}
	}

	void FrontalShoot()
	{
		/*GameObject bullet = ObjectPoolerScript.objPoolerScript.GetPooledObject(); // Call a instance of the ObjectPoolerScript Class then call the function to create the ObjectPooling
		if(bullet == null) 
		{
			return;
		}
		bullet.transform.position = firePosition.position; // Tells on which point the bullet should be created.
		bullet.transform.rotation = firePosition.rotation; // Gets the correct rotation for the bullet.
		bullet.SetActive(true);
		*/
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();	
		bulletRigidBody.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
		
	}

	void LeftShoot()
	{
		GameObject bulletL1 = Instantiate(bulletPrefab, firePointL1.position, firePointL1.rotation);
		Rigidbody2D bulletRigidBodyL1 = bulletL1.GetComponent<Rigidbody2D>();	
		bulletRigidBodyL1.AddForce(firePointL1.up * bulletSpeed, ForceMode2D.Impulse);

		GameObject bulletL2 = Instantiate(bulletPrefab, firePointL2.position, firePointL2.rotation);
		Rigidbody2D bulletRigidBodyL2 = bulletL2.GetComponent<Rigidbody2D>();	
		bulletRigidBodyL2.AddForce(firePointL2.up * bulletSpeed, ForceMode2D.Impulse);

		GameObject bulletL3 = Instantiate(bulletPrefab, firePointL3.position, firePointL3.rotation);
		Rigidbody2D bulletRigidBodyL3 = bulletL3.GetComponent<Rigidbody2D>();	
		bulletRigidBodyL3.AddForce(firePointL3.up * bulletSpeed, ForceMode2D.Impulse);				
	}

	void RightShoot()
	{
		GameObject bulletR1 = Instantiate(bulletPrefab, firePointR1.position, firePointR1.rotation);
		Rigidbody2D bulletRigidBodyR1 = bulletR1.GetComponent<Rigidbody2D>();	
		bulletRigidBodyR1.AddForce(firePointR1.up * bulletSpeed, ForceMode2D.Impulse);

		GameObject bulletR2 = Instantiate(bulletPrefab, firePointR2.position, firePointR2.rotation);
		Rigidbody2D bulletRigidBodyR2 = bulletR2.GetComponent<Rigidbody2D>();	
		bulletRigidBodyR2.AddForce(firePointR2.up * bulletSpeed, ForceMode2D.Impulse);

		GameObject bulletR3 = Instantiate(bulletPrefab, firePointR3.position, firePointR3.rotation);
		Rigidbody2D bulletRigidBodyR3 = bulletR3.GetComponent<Rigidbody2D>();	
		bulletRigidBodyR3.AddForce(firePointR3.up * bulletSpeed, ForceMode2D.Impulse);				
	}

	void UpdateHealthBar()
	{
		healthBar.fillAmount = currentHealth / maxHealth;
	}

	public void TakeDamage(float damageAmount)
    {
		playerInvincible = true;
		Invoke("InvencibilityOff", 3f);
		
        currentHealth -= damageAmount;
		UpdateHealthBar();
        if(currentHealth <= 0)
        {
			playerInvincible = true;
            //Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    void InvencibilityOff()
    {
        playerInvincible = false;
    }

	void LookAtMouse()
	{
		Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		transform.up = mousePos - new Vector2(transform.position.x, transform.position.y);
	}

	void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(15);
        }
		if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Self damage!");
        }
    }


	
	//I
}
