using UnityEngine;
using System.Collections;
using System;

public class StartBurn : MonoBehaviour {
	Renderer rend;

	private int RanMat;

	private float SliceNum;

	public bool startburn = false;

	public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());

	public GameObject CardManager;
	void Start()
	{
		CardManager = GameObject.Find ("CardManager");
		rend = GetComponent<Renderer> ();
	}
	void Update()
	{

		if (startburn==true) 
		{
			SliceNum += 0.4f * Time.deltaTime;
			this.GetComponent<Renderer> ().material.SetFloat ("_SliceAmount", SliceNum);
			//Burning
		}
		if(SliceNum>1)
		{
			startburn = false;
			SliceNum = 0;
			//Go back when it finish
		}

	}
	public void GetMat()
	{
		RanMat = ran.Next (1,4);
		this.GetComponent<Renderer> ().material =GameManager.Instance.Cardmanager.BurnMaterial[RanMat];
		//Random Material
		Instantiate(Resources.Load("FireSpark"),this.transform.position, Quaternion.identity);
		//Creat FireSpark
		startburn = true;
		//Burn!!!
	}
}
