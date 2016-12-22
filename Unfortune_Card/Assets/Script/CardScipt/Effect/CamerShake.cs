using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerShake : MonoBehaviour {
	private Vector2 ShakePos;
	private float ShakeTimer;
	private float ShakeAmount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ShakeTimer >= 0) 
		{
			ShakePos = Random.insideUnitCircle * ShakeAmount;

			transform.position = new Vector3 (transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);

			ShakeTimer -= Time.deltaTime;
		}
		if (ShakeTimer <= 0) 
		{
			transform.localPosition = new Vector3 (0, 0, -53);
		} 
		//CameraShake
	}
	public void CameraShake(float shakepower,float shakeDur)
	{
		ShakeAmount = shakepower;
		ShakeTimer = shakeDur;
	}
}
