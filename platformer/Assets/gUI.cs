using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gUI : MonoBehaviour {

	Image image;
	Rigidbody2D rigidbody2D;
	public float nitro = 0.4f;
	public Text textNitro;
	public Text textNitroValue;
	float NitroValue = 10f;
	// Use this for initialization
	void Start () {
		GameObject imageObject = GameObject.FindGameObjectWithTag("nitroAnimation");
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		rigidbody2D = player.GetComponent<Rigidbody2D>();
		image = imageObject.GetComponent<Image>();
		flaseNitro();
	}
	
	// Update is called once per frame
	void Update () {
		textNitro.text = rigidbody2D.gravityScale.ToString();
		textNitroValue.text = NitroValue.ToString();
	}

	public void trueNitro()
	{
		
		if(NitroValue > 0)
		{
			image.enabled = true;
			if (rigidbody2D.gravityScale != 0.1)
			{
				rigidbody2D.gravityScale = rigidbody2D.gravityScale - nitro;
				NitroValue = NitroValue - nitro;
			}
		}
		else
		{
			NitroValue = 0;
			image.enabled = false;
		}
	}

	public void flaseNitro()
	{
		image.enabled = false;
		rigidbody2D.gravityScale = 1;
	}
}
