using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackEnemy : MonoBehaviour {

	/// <summary>
	/// Раздел для объектов
	/// </summary>
	public GameObject tochkaA; // Точка А
	public GameObject tochkaB; // Точка B
	public GameObject fireObject;
	private Transform enemyTransform; // Transform 
	private GameObject player; // Объект игрока
	private GameObject scope; //Объект сведения
	private GameObject crossHair; //Объект прицела
	private GameObject startFire; // Объект, стартовой позиции снаряда
	private SpriteRenderer scopeSpriteRenderer; //scope.SpriteRenderer - отображение сведения
	private SpriteRenderer crossHairSpriteRenderer; //crossHair.SpriteRenderer - отображение прицела
	/// <summary>
	/// Раздел для значений
	/// </summary>
	public float enemySpeed = 2f; // Скорость врага
	private bool izAvB = false; // Состояние пути врага в пути
	private float scopeFocusSpeed = 5; // Скорость фокусировки сведения
	private float crossHairFocusSpeed = 4; // Скорость фокусировки прицела
	private float distance = 10; // Дистанция обнаружения игрока
	private float timeBtwShots;
	public float startTimeBtwShots;

	void Start(){
		//Поиск объекта
		player = GameObject.FindGameObjectWithTag("Player");
		scope = GameObject.FindGameObjectWithTag("scope");
		crossHair = GameObject.FindGameObjectWithTag("crosshair");
		startFire = GameObject.FindGameObjectWithTag("fireStart");
		//Поиск компонента объекта
		enemyTransform = gameObject.GetComponent<Transform>();
		scopeSpriteRenderer = scope.GetComponent<SpriteRenderer>();
		crossHairSpriteRenderer = crossHair.GetComponent<SpriteRenderer>();
		//Установка значений
		timeBtwShots = startTimeBtwShots;
	}

	void Update(){
		//Движение противника
		if (izAvB != true)
		{
			if (enemyTransform.position.x == tochkaA.transform.position.x)
			{
				izAvB = true;
			}
			enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, tochkaA.transform.position, enemySpeed * Time.deltaTime);
		}
		else
		{
			if (enemyTransform.position.x == tochkaB.transform.position.x)
			{
				izAvB = false;
			}
			enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, tochkaB.transform.position, enemySpeed * Time.deltaTime);
		}

		//Когда игрок вошел в установленную дистанцию обнаружения
		if (Vector2.Distance(transform.position, player.transform.position) < distance){
			scopeSpriteRenderer.enabled = true;
			crossHairSpriteRenderer.enabled = true;

			if (timeBtwShots <= 0)
			{
				Instantiate(fireObject, startFire.transform.position, Quaternion.identity);
				timeBtwShots = startTimeBtwShots;
			}
			else
			{
				timeBtwShots -= Time.deltaTime;
			}
		}
		else{
			scopeSpriteRenderer.enabled = false;
			crossHairSpriteRenderer.enabled = false;
		}

		scope.transform.position = Vector2.MoveTowards(scope.transform.position, player.transform.position, scopeFocusSpeed * Time.deltaTime);
		crossHair.transform.position = Vector2.MoveTowards(crossHair.transform.position, player.transform.position, crossHairFocusSpeed * Time.deltaTime);	
	}
}
