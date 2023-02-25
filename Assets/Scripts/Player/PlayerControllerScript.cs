using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class PlayerControllerScript : MonoBehaviour
{
    float veloc;
	float velocRot;
	public Camera mainCamera;
	public bool playerInvincible;
	
	public Transform firePosition;
	public float timeBetweenBullets = 1;
	float lastShot;
	
	
	public Image healthBar;
	public float maxHealth = 100;
	public float currentHealth = 0;

	public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20;

	void Start(){
		veloc = 8f;
		velocRot = 160f;
		currentHealth = maxHealth;
		UpdateHealthBar();
		lastShot = timeBetweenBullets;
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
        
		if(Input.GetButtonDown("Fire1"))
		{
			if(lastShot >= timeBetweenBullets)
        	{
				lastShot = 0;
				FrontalShoot();
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

	void UpdateHealthBar()
	{
		healthBar.fillAmount = currentHealth / maxHealth;
	}

	public void TakeDamage(float damageAmount)
    {
		playerInvincible = true;
		Invoke("InvencibilityOff", 3f);		

        //Debug.Log($"Damage Amount: {damageAmount}");
        currentHealth -= damageAmount;
        //Debug.Log($"Health is now: {currentHealth}");
		UpdateHealthBar();
        if(currentHealth <= 0)
        {
			playerInvincible = true;
            Destroy(gameObject);
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

	
	//I
}
