using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour {

	public GameObject panel;
	public GameObject panel1;
	public Text textObj;

	/// <summary>
	/// Раздел для физики игрока и всего, что сней связано
	/// </summary>
	Rigidbody2D playerRigidbody2D; //Rigidbody2D игрока
	float horizontalRotation; //Акселерометр или горизонтальная ориентация устройства
	float sensitivity = 10f;

	/// <summary>
	/// Раздел для здоровья и очков 
	/// </summary>
	public int playerHeath = 3; //Здоровье игрока
	public int playerCoins = 0; //Монеты игрока
	public Text playerCoinsText; //Отображение монет игрока

	/// <summary>
	/// magnitnoePole
	/// </summary>
	GameObject magnitnoePoleObj;
	Transform magnitnoePoleObjTransform;
	SpriteRenderer magnitnoePoleObjSpriteRenderer;
	GameObject magnitnoePoleObjClose;
	Image magnitnoePoleImageClose;
	GameObject magPoleAnimation;
	Image magnitnoePoleImageAnim;
	public float sTimeMagPol = 0;
	public float eTimeMagPol = 1;
	public float magnitnoePoleVal = 0;

	//Cookie
	GameObject cookieObj;
	Transform cookieObjTransform;
	SpriteRenderer cookieSpriteRender;
	public float cookieVal = 1f;
	public float sCookie = 0f;
	public float eCookie = 1f;
	GameObject cookieObjClose;
	Image cookieCloseObjImage;
	GameObject cookieBtnObjAnim;
	Image cookieObjAnim;


	//Nitro
	GameObject energyBtnObjClose; //Кнопка нитро
	Image energyImageClose; //Крестик для кнопки нитро
	GameObject molniyaBtnObjAnimMini;
	Image btnAnimMini;
	GameObject molniyaBtnObjAnim;
	Image btnObjAnim;
	public float nitro = 0.4f;
	float NitroValue = 10f;

	/// <summary>
	/// Раздел для анимации игрока
	/// </summary>
	Animator playerAnimator;
	bool move = true;

	GameObject finAnim;
	SpriteRenderer finAnimSpriteRender;

	void Start () {

		finAnim = GameObject.FindGameObjectWithTag("finAn");
		finAnimSpriteRender = finAnim.GetComponent<SpriteRenderer>();

		cookieObj = GameObject.FindGameObjectWithTag("circleFire");
		cookieObjTransform = cookieObj.GetComponent<Transform>();
		cookieSpriteRender = cookieObj.GetComponent<SpriteRenderer>();
		cookieObjClose = GameObject.FindGameObjectWithTag("close2");
		cookieCloseObjImage = cookieObjClose.GetComponent<Image>();
		cookieBtnObjAnim = GameObject.FindGameObjectWithTag("cookieAnimation");
		cookieObjAnim = cookieBtnObjAnim.GetComponent<Image>();

		//MagnitnoePole
		magnitnoePoleObj = GameObject.FindGameObjectWithTag("magnitnoePole");
		magnitnoePoleObjTransform = magnitnoePoleObj.GetComponent<Transform>();
		magnitnoePoleObjSpriteRenderer = magnitnoePoleObj.GetComponent<SpriteRenderer>();
		
		molniyaBtnObjAnim = GameObject.FindGameObjectWithTag("nitroAnimation");
		btnObjAnim = molniyaBtnObjAnim.GetComponent<Image>();

		molniyaBtnObjAnimMini = GameObject.FindGameObjectWithTag("nitroAnimationMini");
		btnAnimMini = molniyaBtnObjAnimMini.GetComponent<Image>();

		energyBtnObjClose = GameObject.FindGameObjectWithTag("close0");
		energyImageClose = energyBtnObjClose.GetComponent<Image>();

		magnitnoePoleObjClose = GameObject.FindGameObjectWithTag("close1");
		magnitnoePoleImageClose = magnitnoePoleObjClose.GetComponent<Image>();

		magPoleAnimation = GameObject.FindGameObjectWithTag("buubbleAnimation");
		magnitnoePoleImageAnim = magPoleAnimation.GetComponent<Image>();

		//Физика
		playerRigidbody2D = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();
	}
	
	void Update () {
		if (Application.platform == RuntimePlatform.Android){
			horizontalRotation = Input.acceleration.x;
		}else{
			horizontalRotation = Input.GetAxis("Horizontal");
		}

		if(move == true){
			playerRigidbody2D.velocity = new Vector2(horizontalRotation * sensitivity, playerRigidbody2D.velocity.y);
		}

		//playerCoinsText.text = playerCoins.ToString();

		cookieObjTransform.position = Vector2.MoveTowards(cookieObjTransform.position, gameObject.transform.position, 10 * Time.deltaTime);
		magnitnoePoleObjTransform.position = Vector2.MoveTowards(magnitnoePoleObjTransform.position, gameObject.transform.position, 10 * Time.deltaTime);

		if (playerHeath == 0)
		{
			deadPlayer();
		}

		textObj.text = "x " + playerHeath.ToString();
	}

	/// <summary>
	/// Соприкосновение игрока с другим предмета
	/// </summary>
	/// <param name="other"> другой объект </param>
	void OnCollisionEnter2D(Collision2D other){
		//Платформа
		if(other.gameObject.tag == "platform") {
			//Физика
			playerRigidbody2D.velocity = Vector2.zero;
			playerRigidbody2D.AddForce(transform.up * 10, ForceMode2D.Impulse);

			//Анимация
			playerAnimator.SetInteger("penis", 1);
		}

		//Объект, отмнимающий жизни
		if (other.gameObject.tag == "deadObject"){
			if (magnitnoePoleObjSpriteRenderer.enabled != true)
			{
				//Здоровье
				playerHeath--;
				//Если здоровье закончилось
				if (playerHeath <= 0)
				{
					deadPlayer();
				}
			}
		}

		if (other.gameObject.tag == "simpleEnemy")
		{
			if (magnitnoePoleObjSpriteRenderer.enabled != true)
			{
				deadPlayer();
			}
		}

		//Объект, добавляющий здоровье
		if (other.gameObject.tag == "heathObject"){
			playerHeath++;
		}

		//Объект, добавляющий монеты
		if (other.gameObject.tag == "coinsObject")
		{
			playerCoins++;
		}

		if(other.gameObject.tag == "buubble")
		{
			magnitnoePoleVal = 1;
		}

		if (other.gameObject.tag == "cookieObj")
		{
			cookieVal = 1;
		}

	}

	/// <summary>
	/// Покидание соприкосновения игрока с другим объектом
	/// </summary>
	/// <param name="other"> другой объект </param>
	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "platform")
		{
			//Анимация
			playerAnimator.SetInteger("penis", 2);
			if (NitroValue > 0)
			{
				if(NitroValue != 10f)
				{
					NitroValue = NitroValue - nitro;
				}
			}
			else
			{
				NitroValue = 0;
				energyImageClose.enabled = true;
				flaseNitro();
			}
		}
	}

	void deadPlayer()
	{
		playerHeath = 0;

		//Анимация
		playerAnimator.SetInteger("penis", 3);
		move = false;

		//Физика
		playerRigidbody2D.velocity = Vector2.zero;
		playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;

		panel.SetActive(true);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "fireObject")
		{
			if(magnitnoePoleObjSpriteRenderer.enabled != true)
			{
				//Здоровье
				playerHeath--;
				//Если здоровье закончилось
				if (playerHeath <= 0)
				{
					deadPlayer();
				}
			}
		}

		if (other.gameObject.tag == "finish")
		{
			finishGame();
		}

		if (other.gameObject.tag == "lava")
		{
			playerHeath = 0;
		}
	}

	public void trueNitro()
	{
		if (NitroValue > 0)
		{
			if (playerRigidbody2D.gravityScale > 0.1)
			{
				energyImageClose.enabled = false;
				btnObjAnim.enabled = true;
				btnAnimMini.enabled = true;
				playerRigidbody2D.gravityScale = playerRigidbody2D.gravityScale - nitro;
				NitroValue = NitroValue - nitro;
			}
		}
		else
		{
			NitroValue = 0;
			energyImageClose.enabled = true;
			btnObjAnim.enabled = false;
			btnAnimMini.enabled = false;
		}
	}

	public void flaseNitro()
	{
		btnObjAnim.enabled = false;
		btnAnimMini.enabled = false;
		playerRigidbody2D.gravityScale = 1;
	}

	public void activeMagnitnoePole()
	{
		if (magnitnoePoleVal!=0)
		{
			magnitnoePoleImageAnim.enabled = true;
			magnitnoePoleObjSpriteRenderer.enabled = true;
		}
	}

	public void deactivedMagnitnoePole()
	{
		magnitnoePoleObjSpriteRenderer.enabled = false;
		magnitnoePoleImageAnim.enabled = false;
	}

	void FixedUpdate()
	{

		if (cookieVal != 0)
		{
			cookieCloseObjImage.enabled = false;
			if (cookieSpriteRender.enabled == true)
			{
				sCookie += 0.1f * Time.deltaTime;
				if (sCookie >= eCookie)
				{
					deactivedCookie();
					sCookie = 0f;
					cookieVal = 0;
				}
			}
		}
		else
		{
			cookieCloseObjImage.enabled = true;
		}

		if (magnitnoePoleVal != 0)
		{
			magnitnoePoleImageClose.enabled = false;
			if (magnitnoePoleObjSpriteRenderer.enabled == true)
			{
				sTimeMagPol += 0.1f * Time.deltaTime;
				if (sTimeMagPol >= eTimeMagPol)
				{
					deactivedMagnitnoePole();
					sTimeMagPol = 0f;
					magnitnoePoleVal = 0;
				}
			}
			else
			{

			}
		}
		else
		{
			magnitnoePoleImageClose.enabled = true;
		}

	}

	public void activeCookie()
	{
		if(cookieVal != 0)
		{
			cookieObjAnim.enabled = true;
			cookieSpriteRender.enabled = true;
			transform.localScale = new Vector3(3,3,3);
		}
	}

	public void deactivedCookie()
	{
		cookieObjAnim.enabled = false;
		cookieSpriteRender.enabled = false;
		transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
	}

	public void finishGame()
	{
		finAnimSpriteRender.enabled = true;
		StartCoroutine(menuSH());
	}

	IEnumerator menuSH()
	{
		yield return new WaitForSeconds(2);
		panel.SetActive(false);
		panel1.SetActive(true);
	}
}
