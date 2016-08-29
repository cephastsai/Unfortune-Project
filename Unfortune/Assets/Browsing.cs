using UnityEngine;
using System.Collections;

public class Browsing : MonoBehaviour {

	public TouchEvent TE;
	public GameObject BigCard;
	public GameObject Maincamera;
	public Vector3 PreviewPoint;
	// Use this for initialization
	public void init () {
		Maincamera = GameObject.Find ("Main Camera");
		Camera camera = Maincamera.GetComponent<Camera>();	

		TE = GameObject.Find ("TouchManager").GetComponent<TouchEvent> ();

		if (TE != null) 
		{						
			TE.TEDObjectCR +=CreatBigOne;
			TE.TEDObjectHR +=preview;
		}
	}
			
	public void CreatBigOne(Transform j)
	{		
		if (j == transform.GetChild(0)) 			
		{			
			BigCard = Instantiate(gameObject);
			Destroy (BigCard.GetComponent<Browsing> ());	
			BigCard.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			PreviewPoint = new Vector3 (BigCard.transform.position.x-20, BigCard.transform.position.y+45, 0);
			BigCard.transform.localScale = new Vector3 (3, 3, 1);
			BigCard.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			BigCard.transform.position = PreviewPoint;

		}
	}

	public void preview(Transform i)
	{
		if (i == this.transform) {
			if(BigCard != null)
			{

				PreviewPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				PreviewPoint = new Vector3 (PreviewPoint.x-20, PreviewPoint.y+45, 0);
				BigCard.transform.position = PreviewPoint;
			}

		}if (i == null) 
		{			
			if (BigCard != null) 
			{
				Destroy (BigCard);
			}
		}

	}

	void OnDestroy()
	{
		TE.TEDObjectHR -=preview;
	}
}
