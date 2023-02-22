using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    /*public float moveSpeed = 6f;
    void Update()
    {
        Vector3 position = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(position * moveSpeed * Time.deltaTime);
    }*/

    float veloc;
	float velocRot;
	private float posicaoAcima;
	private float posicaoLado;
	private float alturaDoSprite;
	private Vector3 vetorCamera;

	void Start(){
		veloc = 8f;
		velocRot = 160f;
		alturaDoSprite = GetComponent<SpriteRenderer>().bounds.extents.y;
		vetorCamera = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
		posicaoAcima = vetorCamera.y;
		posicaoLado  = vetorCamera.x;
	}

	// Atualiza a posicao do GameObject a cada FPS.
	void Update () {
		float x = Input.GetAxis("Vertical");
		float y = Input.GetAxis("Horizontal");

		float desloc = Mathf.Clamp(x, 0f, 1f) * veloc * Time.deltaTime;
		float deslocV = y * velocRot * Time.deltaTime;
		transform.Translate(0, desloc, 0);
		transform.Rotate(0, 0, -1*deslocV);

		//Limita movimentacao no limite superior da tela
		if (transform.position.y + alturaDoSprite > posicaoAcima){
			float posY = posicaoAcima - alturaDoSprite;
			transform.position = new Vector3(transform.position.x, posY, 0);
		} 
		//Limita movimentacao no limite inferior da tela
		else if (transform.position.y - alturaDoSprite < -posicaoAcima){
			float posY = -posicaoAcima + alturaDoSprite;
			transform.position = new Vector3(transform.position.x, posY, 0);
		} 

		//Limita movimentacao no limite lateral DIREITA da tela
		if (transform.position.x + alturaDoSprite > posicaoLado){
			float posX = posicaoLado - alturaDoSprite;
			transform.position = new Vector3(posX, transform.position.y, 0);
		} 
		//Limita movimentacao no limite lateral ESQUERDO da tela
		else if (transform.position.x - alturaDoSprite < -posicaoLado){
			float posX = -posicaoLado + alturaDoSprite;
			transform.position = new  Vector3(posX, transform.position.y, 0);
		} 
	}
}
