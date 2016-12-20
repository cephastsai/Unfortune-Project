using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryInfo : MonoBehaviour {

	public int Kind;

}		

// StoryKind : 1
public class S_Stroy : StoryInfo {

	public string[] _Tag;

	public string _Info;
}

//StoryKind : 2
public class S_GetCards : StoryInfo {

	public string[] _Tag;

	public int _CardID;
}

//StoryKind : 3
public class S_Option :StoryInfo{

	public List<Option> OptionList = new List<Option>();
}

public class Option{
	
	public string _AddTag;
	public string _OptionInfo;
}

//StoryKind : 4
public class S_Fighting : StoryInfo {

	public int[] Cards;
}

//StoryKind : 4
public class S_Shop : StoryInfo {


}	