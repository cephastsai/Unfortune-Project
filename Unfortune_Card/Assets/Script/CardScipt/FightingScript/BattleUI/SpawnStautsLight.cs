using UnityEngine;
using System.Collections;

public class SpawnStautsLight : MonoBehaviour {

	public int Stauts = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Stauts==1)
		{
			Stauts = 0;
			Instantiate(Resources.Load("StatusLight/LeftLight1"),new Vector3(-6.2f,0.1f,-16), Quaternion.identity);
		}

		if(Stauts==2)
		{
			Stauts = 0;
			Instantiate(Resources.Load("StatusLight/LeftLight2"),new Vector3(-5f,0.1f,-16), Quaternion.identity);
		}

		if(Stauts==3)
		{
			Stauts = 0;
			Instantiate(Resources.Load("StatusLight/RightLight1"),new Vector3(6.2f,0.14f,-16), Quaternion.identity);
		}

		if(Stauts==4)
		{
			Stauts = 0;
			Instantiate(Resources.Load("StatusLight/RightLight2"),new Vector3(5f,0.1f,-16), Quaternion.identity);
		}

	
	}

}
