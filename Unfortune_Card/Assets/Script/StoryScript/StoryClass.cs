﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Story : MonoBehaviour {

	public Vector3 _Psition;

	public int StoryKind;

	public int[] MapNodeID = new int[10];

	public List<string> StoryInfo = new List<string>();

	public string StoryInfoSort;

}

//Story Infomation
public class NTree{
	
}

public class StoryInfo{
	public string Info;

}


// StoryKind : 1
public class S_Stroy : Story {

	public string _Info;
}


//StoryKind : 2
public class S_Fighting : Story {


}


//StoryKind : 3
public class S_GetCards : Story {

	public string _Info;
}


//StoryKind : 4
public class S_Shop : Story {


}


public class S_Fighting_E : S_Fighting {

	public string _Info;
}