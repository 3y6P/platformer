using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleEnemy : MonoBehaviour {

	public GameObject tochkaA; //Точка А
	public GameObject tochkaB; //Точка B
	bool izAvB = false; //Состояние пути врага в пути

	Transform enemyTransform; //Transform врага
	public float enemySpeed = 2f; //Скорость врага

	Animator enemyAnimator;
	SpriteRenderer enemySpriteRenderer;
	void Start () {
		enemyTransform = gameObject.GetComponent<Transform>();
		enemyAnimator = gameObject.GetComponent<Animator>();
		enemySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	void Update () {

		if (izAvB != true){
			if (enemyTransform.position.x == tochkaA.transform.position.x)
			{
				izAvB = true;
			}
			enemyAnimator.SetInteger("walk",1);
			enemySpriteRenderer.flipX = false;
			enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, tochkaA.transform.position, enemySpeed * Time.deltaTime);
		}
		else{
			if (enemyTransform.position.x == tochkaB.transform.position.x){
				izAvB = false;
			}
			enemyAnimator.SetInteger("walk", 2);
			enemySpriteRenderer.flipX = true;
			enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, tochkaB.transform.position, enemySpeed * Time.deltaTime);
		}
	}
}
