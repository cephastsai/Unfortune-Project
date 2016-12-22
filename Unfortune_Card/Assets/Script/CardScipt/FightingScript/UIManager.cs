using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

	public List<UIAnimation> UIList = new List<UIAnimation>();
	private float K = 20;

	public GameObject TableUI;
	public GameObject ETableUI;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < transform.childCount; i++) 
		{
		transform.GetChild (i).gameObject.AddComponent<UIAnimation>().Init (
				transform.GetChild(i).localPosition,GetTargetPosition(transform.GetChild (i)),K);
			UIList.Add (transform.GetChild (i).GetComponent<UIAnimation> ());
		}

		TableUI.AddComponent<UIAnimation> ().Init (TableUI.transform.localPosition,new Vector3(0.2f,-1.66f,53),K);
		UIList.Add (TableUI.GetComponent<UIAnimation>());

		ETableUI.AddComponent<UIAnimation> ().Init (ETableUI.transform.localPosition,new Vector3(-0.1f,2.38f,53),K);
		UIList.Add (ETableUI.GetComponent<UIAnimation>());

	}
	public Vector3 GetTargetPosition(Transform _Target)
	{
		switch (_Target.gameObject.name) {
		case "CircleB":
			return new Vector3 (8.07f, 5.12f, 53);
			break;
		case "BGB":
			return new Vector3 (-0.2f, 5.91f, 53);
			break;
		case "BGA":
			return new Vector3 (0.07f, -2.83f, 53);
			break;
		case "EnemyStatus":
			return new Vector3 (6.77f, 1.6f, 53);
			break;
		case "MyStatus":
			return new Vector3 (-6.65f, 1.6f, 53);
			break;
		case "TurnEnd":
			return new Vector3 (0.01f, 1.6f, 53);
			break;
		default:
			return new Vector3 (0, 0, 0);
			break;
		}
	}

	public void BattleBegin()
	{
		foreach (UIAnimation i in UIList) 
		{
			switch (i.gameObject.name)
			{
			case "CircleB":
				i.BattleBegin ();
				break;
			case "BGA":
				i.ToChildPoint ();
				break;
			case "BGB":
				i.ToChildPoint ();
				break;
			case "TableUI":
				i.ToChildPoint ();
				break;
			case "ETableUI":
				i.ToChildPoint ();
				break;
			case "TurnEnd":
				i.StartFadeIn ();
				break;

			}
		}
	}

	public void BattleOver()
	{
		foreach (UIAnimation i in UIList) 
		{
			switch (i.gameObject.name)
			{
			case "CircleB":
				i.CircleBRemove ();
				break;
			case "BGA":
				i.ToSelfPoint ();
				break;
			case "BGB":
				i.ToSelfPoint ();
				break;
			case "TableUI":
				i.ToSelfPoint ();
				break;
			case "ETableUI":
				i.ToSelfPoint ();
				break;
			case "TurnEnd":
				i.StartFadeOut ();
				break;
			case "MyStatus":
				i.ToSelfPoint ();
				break;
			case "EnemyStatus":
				i.ToSelfPoint ();
				break;

			}
		}
	}


	public void CirleBGetOut()
	{
		foreach (UIAnimation i in UIList) 
		{
			switch (i.gameObject.name)
			{
			case "CircleB":
				i.CircleBRemove ();
				break;
			}
		}
	}

	public void MapToStory()
	{
		foreach (UIAnimation i in UIList) 
		{
			switch (i.gameObject.name)
			{
			case "BlurMask":
				i.MaskBlurPlus ();
				break;
			case "BG":
				i.StartBGFadeIn ();
				break;
			case "LOG":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			default:
				i.transform.position = i.ChildPosition;
				break;
			}
		}
	}
	public void StoryToFight()
	{
		foreach(UIAnimation i in UIList)
		{
			switch (i.gameObject.name)
			{
			case "bottom":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "TOP":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "DAY":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "OPTION":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "CARDFRAME":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "Status":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "LOG":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			}
		}		
	}
	public void FightToMap()
	{
		foreach(UIAnimation i in UIList)
		{
			switch (i.gameObject.name)
			{
			case "bottom":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case "TOP":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case "DAY":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case "OPTION":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case "CARDFRAME":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case "Status":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case "BlurMask":
				i.MaskBlurDec ();
				break;
			case "BG":
				i.StartBGFadeOut ();
				break;
			}
		}		
	}
	public void MapToFight()
	{
		foreach(UIAnimation i in UIList)
		{
			switch (i.gameObject.name)
			{
			case "bottom":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "TOP":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "DAY":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "OPTION":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "CARDFRAME":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "Status":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "BlurMask":
				i.MaskBlurPlus ();
				break;
			case "BG":
				i.StartBGFadeIn ();
				break;
			}
		}		
	}
	public void FightToMemory()
	{
		foreach (UIAnimation i in UIList) 
		{
			switch (i.gameObject.name) 
			{

			case "Black":
				i.StartBlackFadeIn ();
				break;
			case "CARDFRAME":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case "Status":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			
			}
		}
	}
	public void MemoryBack()
	{
		foreach (UIAnimation i in UIList) 
		{
			switch (i.gameObject.name) 
			{
			case "CARDFRAME":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "Status":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "Black":
				i.StartBlackFadeOut ();
				break;
			
			}
		}
	}
	public void StoryToChose()
	{
		foreach (UIAnimation i in UIList) 
		{
			switch (i.gameObject.name) 
			{
			case"LOG":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case"ChoseCard":
				i.StartFadeIn ();
				break;
			case "bottom":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "TOP":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "DAY":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "OPTION":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			case "Status":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			}
		}
	}
	public void ChoseToFight()
	{
		foreach (UIAnimation i in UIList) 
		{
			switch (i.gameObject.name) 
			{
			case "ChoseCard":
				i.StartFadeOut ();
				break;
			case "CARDFRAME":
				i.ToSelfPoint ();
				i.StartFadeIn ();
				break;
			}
		}
	}
	public void FightToChose()
	{
		foreach (UIAnimation i in UIList) 
		{
			switch (i.gameObject.name) 
			{
			case "ChoseCard":
				i.StartFadeIn ();
				break;
			case "CARDFRAME":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			}
		}
	}
	public void ChoseToMap()
	{
		foreach (UIAnimation i in UIList) 
		{
			switch (i.gameObject.name) 
			{
			case "ChoseCard":
				i.StartFadeOut ();
				break;
			case "bottom":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case "TOP":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case "DAY":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case "OPTION":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case "Status":
				i.ToChildPoint ();
				i.StartFadeOut ();
				break;
			case "BlurMask":
				i.MaskBlurDec ();
				break;
			case "BG":
				i.StartBGFadeOut ();
				break;
			}
		}
	}
}
