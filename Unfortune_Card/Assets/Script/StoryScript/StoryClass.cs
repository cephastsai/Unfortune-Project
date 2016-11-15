using UnityEngine;
using System.Collections;

public class Story : MonoBehaviour {

	public Vector3 _Psition;

	public int StoryKind;

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