using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectGameScipt : MonoBehaviour {

	Animator objectAnimator;
	BoxCollider2D objectBoxCollider2D;
	string objectTag;

	void Start () {
		objectAnimator = GetComponent<Animator>();
		objectBoxCollider2D = GetComponent<BoxCollider2D>();
		objectTag = gameObject.tag;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(objectTag == "deadObject"){
			objectAnimator.SetInteger("bottle", 2);
			StartCoroutine(destroyObject(3));
		}

		if (objectTag == "heathObject"){
			StartCoroutine(destroyObject(0));
		}

		if (objectTag == "coinsObject")
		{
			StartCoroutine(destroyObject(0));
		}

		if (objectTag == "fireObject")
		{
			StartCoroutine(destroyObject(1));
		}

		if (objectTag == "buubble")
		{
			StartCoroutine(destroyObject(0));
		}

		if (objectTag == "cookieObj")
		{
			StartCoroutine(destroyObject(0));
		}
		
	}
	IEnumerator destroyObject(float seconds)
	{
		Destroy(objectBoxCollider2D, 0f);
		yield return new WaitForSeconds(seconds);
		Destroy(gameObject, 0f);
	}
}
