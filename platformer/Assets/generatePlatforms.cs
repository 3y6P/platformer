using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generatePlatforms : MonoBehaviour {

	public GameObject genObj; //Объект платформы
	private Transform transformObj; //Transform платформы


	void Start () {
		float sclaVal = 0.5f;
		Vector3 scaleObj = new Vector3(sclaVal, sclaVal, sclaVal);
		transformObj = genObj.GetComponent<Transform>(); //Управление компонентом Transform
		transformObj.localScale = scaleObj;

		float distanceHstart = -2.4f;
		float distanceHend = 2.4f;
		float distanceH = 1.2f;

		float distanceVstart = 0f;
		float distanceVend = 12f;
		float distanceV = 3f;

		int countH = 0;
		int countV = 0;

		for (float Vpos = distanceVstart; Vpos <= distanceVend; Vpos += distanceV)
		{
			for (float Hpos = distanceHstart; Hpos <= distanceHend; Hpos += distanceH)
			{
				transformObj.position = new Vector2(Hpos, Vpos);
				Instantiate(genObj, transformObj.position, Quaternion.identity);
				countH += 1;
			}
			countH = 0;
			countV += 1;
		}
	}

		

	private int checkCreat()
	{
		//Создание объекта для генерации чисел
		System.Random rnd = new System.Random();

		//Получить случайное число (в диапазоне от 0 до 10)
		int value = rnd.Next(0, 5);
		return value;
	}

	private int checkRnd(int i, int j)
	{
		if(i != -1)
		{
			while (i == j)
			{
				j = checkCreat();
			}
		}
		Debug.Log(j);
		return j;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
