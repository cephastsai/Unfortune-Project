using UnityEngine;
using System.Collections;

public class TableUIManager : MonoBehaviour {

	private bool isHold = false;
	private float startPositionDis;
	private float startTime;

	//move
	private float startTablePosition;
	private float speed;

	void Start(){
		GameManager.Instance.TE.TEDObjectHL += DropTableUI;
	}

	void Update(){
		if(!isHold){			
			Table.Ins.transform.localPosition += new Vector3(speed*Time.deltaTime, 0, 0);

			if(speed<1 && speed >-1){
				speed = 0;
			}else{
				if(speed >0){
					speed--;
				}else{
					speed++;
				}
			}

			if(Table.Ins.transform.localPosition.x < -5.7f){
				float resistance = 10f -(5.7f +Table.Ins.transform.localPosition.x)*10f;
				Table.Ins.transform.localPosition += new Vector3(resistance*Time.deltaTime, 0, 0);
			}

			if(Table.Ins.transform.localPosition.x /*+ 
				GetComponent<TableSlot>().lastSlotPos*/
				> 5.7f){
				float resistance = 10f + (Table.Ins.transform.localPosition.x -5.7f)*10f;
				Table.Ins.transform.localPosition -= new Vector3(resistance*Time.deltaTime, 0, 0);
			}

		}



	}


	public void DropTableUI(Transform target){

		if(target == transform){			
			Vector3 tempVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if(!isHold){
				startPositionDis = tempVec.x - Table.Ins.transform.localPosition.x;
				startTime = Time.time;
				startTablePosition = Table.Ins.transform.localPosition.x;

				isHold = true;
			}

			Table.Ins.transform.localPosition =new Vector3( tempVec.x -startPositionDis, 0, 0);
			


		}else if(target == null){
			if(isHold){
				speed = (Table.Ins.transform.localPosition.x - startTablePosition)/(Time.time - startTime);
				isHold = false;
			}

		}

	}

}
