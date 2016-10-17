﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

	public List<UIAnimation> UIList = new List<UIAnimation>();
	public float K = 20;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < transform.GetChild(0).childCount; i++) 
		{
			transform.GetChild(0).GetChild (i).gameObject.AddComponent<UIAnimation>().Init (
				transform.GetChild(0).GetChild(i).position,
				transform.GetChild(0).GetChild(i).position+transform.GetChild(0).GetChild(i).GetChild(0).localPosition,
				K
			);
			UIList.Add (transform.GetChild(0).GetChild (i).GetComponent<UIAnimation> ());
			//transform.GetChild (i).gameObject.SetActive (false);		
		}

	}
	
	// Update is called once per frame
	void Update () {
		//a.ToEndPoint ();
	}
	public void MapToStory()
	{
		foreach (UIAnimation i in UIList) 
		{
			switch (i.gameObject.name)
			{		
			case "Map":
				i.MapBlurPlus ();
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
			case "Map":
				i.MapBlurDec ();
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
			case "Map":
				i.MapBlurPlus ();
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
			case "Black":
				i.StartBlackFadeOut ();
				break;
			
			}
		}
	}
}