using UnityEngine;
using System.Collections;
//using System;

public class CamaraShake : MonoBehaviour {

	///public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());
	public float ShakeTimer;
	public float ShakeAmount;
	public GameObject Center;
	public bool once = false;
	public Vector2 ShakePos;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//print (ShakeTimer);
		if (Center.transform.localPosition.y == 0&&once == false) 
		{
			ShakeCa (0.2f, 0.1f);
			once = true;

		}

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
	}
	public void ShakeCa(float shakepower,float shakeDur)
	{
		ShakeAmount = shakepower;
		ShakeTimer = shakeDur;
	}
}

