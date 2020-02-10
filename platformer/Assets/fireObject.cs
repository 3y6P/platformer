using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireObject : MonoBehaviour {

	public float speedfireObject;
	Transform playerTransform;
	Animator animator;
	CircleCollider2D circleCollider;
	Vector2 playerTarget;


	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		playerTarget = new Vector2(playerTransform.position.x, playerTransform.position.y);
		animator = GetComponent<Animator>();
		circleCollider = GetComponent<CircleCollider2D>();
	}
	
	void Update () {
		transform.position = Vector2.MoveTowards(transform.position, playerTarget, speedfireObject * Time.deltaTime);
		if(transform.position.x == playerTarget.x && transform.position.y == playerTarget.y){
			StartCoroutine(destroyObject());
		}
		else
		{
			animator.SetInteger("boom", 1);
		}
	}

	void OnTriggerEnter2D(){
		StartCoroutine(destroyObject());
	}

	void OnCollisionExit2D()
	{
		StartCoroutine(destroyObject());
	}

	IEnumerator destroyObject()
	{
		Destroy(circleCollider, 0f);
		animator.SetInteger("boom", 2);
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject, 0f);
	}
}
