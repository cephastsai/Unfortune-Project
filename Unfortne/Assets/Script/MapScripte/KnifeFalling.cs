using UnityEngine;
using System.Collections;

public class KnifeFalling : MonoBehaviour {

	public Vector3 FallingPosition;
	public Vector3 LocationNamePosition;
	public float Distance;
	public bool Stop = false;
	public GameObject LNPO;

	// Use this for initialization
	void Start () {
		FallingPosition = new Vector2 (transform.localPosition.x, transform.localPosition.y);
		transform.localPosition  = new Vector2(transform.localPosition.x, transform.localPosition.y+9f);
		LocationNamePosition = new Vector3 (FallingPosition.x+1.25f, FallingPosition.y,FallingPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
		Distance = Vector2.Distance (transform.localPosition, FallingPosition);
		transform.localPosition = Vector2.MoveTowards (transform.localPosition , FallingPosition , 0.4f);
		if (Distance <= 0.1f&&Stop==false) 
		{
			LNPO = (GameObject)Instantiate(Resources.Load("Prifabs/LocalNameBG"),LocationNamePosition, Quaternion.identity);
			GameManager.Instance.Mapmanager.ShowText(GetComponent<MapStoryPiont>().SPName);
			Stop = true;
		}
	}

	public void DestoryOption(){
		Destroy(LNPO);
		Destroy(gameObject);
	}

}
