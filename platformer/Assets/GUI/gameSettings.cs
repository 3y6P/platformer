using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameSettings : MonoBehaviour {

	public Slider volumeSlider;
	public Text volumeValueText;

	void Start () {
		
	}
	
	
	void Update () {
		float value = volumeSlider.value * 100;
		volumeValueText.text = "Громкость: " + Math.Round(value, 0).ToString() + "%";
		AudioListener.volume = volumeSlider.value;
	}
}
