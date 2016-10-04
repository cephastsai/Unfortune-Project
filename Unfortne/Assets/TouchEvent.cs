using UnityEngine;
using System.Collections;

public class TouchEvent : MonoBehaviour {

	public delegate void MyTEMD_Object(Transform target);
	public event MyTEMD_Object TEDObjectCL;
	public event MyTEMD_Object TEDObjectCR;
	public event MyTEMD_Object TEDObjectHL;
	public event MyTEMD_Object TEDObjectHR;

	public delegate void MyTEMD_Screen(Vector3 target);
	public event MyTEMD_Screen TEDScreen;

	void Update(){	
		
		/*
		if(Input.touchCount > 0){
			for(int i=0; i<Input.touchCount; i++){				
				if(Input.GetTouch(i).phase == TouchPhase.Began){
					Ray touchray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
					RaycastHit hit;

					//Touch Object
					if(Physics.Raycast(touchray, out hit)){
						if(TEDObject != null){							
							TEDObject(hit.transform);	
						}

					}//Physics.Raycast

					//Touch Screen					
					if(TEDScreen != null){
						TEDScreen(Input.GetTouch(i).position);
					}

				}

			}
		}*/

		
		if(Input.GetMouseButtonDown(0)){
			Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//print("CL");
			//Touch Object
			if(Physics.Raycast(touchray, out hit)){
				if(TEDObjectCL != null){							
					TEDObjectCL(hit.transform);	
				}

			}//Physics.Raycast

			//Touch Screen					
			if(TEDScreen != null){
				TEDScreen(Input.mousePosition);
			}
		}else if(Input.GetMouseButtonDown(1)){
			Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//print("CR");
			//Touch Object
			if(Physics.Raycast(touchray, out hit)){
				if(TEDObjectCR != null){							
					TEDObjectCR(hit.transform);	
				}

			}//Physics.Raycast				
		}

		if(Input.GetMouseButton(0)){
			Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//print("HL");
			//Touch Object
			if(Physics.Raycast(touchray, out hit)){
				if(TEDObjectHL != null){							
					TEDObjectHL(hit.transform);
				}
			}else{
				if(TEDObjectHL != null){							
					TEDObjectHL(null);
				}
			}
		}else if(Input.GetMouseButton(1)){
			Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//print("HR");
			//Touch Object
			if(Physics.Raycast(touchray, out hit)){
				if(TEDObjectHR != null){						
					TEDObjectHR(hit.transform);
				}
			}else{
				if(TEDObjectHR != null){							
					TEDObjectHR(null);	
				}
			}				
		}

		if(Input.GetMouseButtonUp(0)){
			if(TEDObjectHL != null){							
				TEDObjectHL(null);
			}
		}else if(Input.GetMouseButtonUp(1)){
			if(TEDObjectHR != null){						
				TEDObjectHR(null);	
			}
		}

	}//update
		
}
