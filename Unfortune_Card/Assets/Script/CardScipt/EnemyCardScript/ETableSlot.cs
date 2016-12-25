using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ETableSlot : MonoBehaviour {

	//slot
	public List<GameObject> SlotList = new List<GameObject>();
	public GameObject Slot; // name : Slot
	public int SlotNum = 0;
	private float Width = -1.58f;
	public float lastSlotPos = 0;

	//Timer
	private float Timer = 0;
	private bool TimerFlag = true;

	//Mat
	public Material SlotMat; // name : Stencil Draw In Mask Mat


	void Update(){

		if(GameManager.GameMainSection == GameManager.GameSection.Cards){
			if(SlotNum < ETable.Ins.ActionNumber+ETable.Ins.TableList.Count){
				if(TimerFlag){
					Timer = Time.time;
					TimerFlag = false;
				}else{
					if(Time.time - Timer > 0.2f){

						CreateSlot();

						TimerFlag = true;
					}
				}
			}
		}

	}


	public void init(){
		CreateSlot();
	}

	void CreateSlot(){		
		GameObject slot = Instantiate(Slot);
		SlotList.Add(slot);
		slot.transform.SetParent(ETable.Ins.transform);
		slot.transform.localPosition = new Vector3( SlotNum*Width, 3f, 0);
		slot.GetComponent<SpriteRenderer>().material = SlotMat;
		slot.AddComponent<GameObjectMoving>().SetTergetPostion(new Vector3( SlotNum*Width, 0, 0), 0.1f);
		lastSlotPos = SlotNum*Width;	
		SlotNum++;
	}

	public void InitSlot(){
		foreach(GameObject i in SlotList){			
			i.AddComponent<MovingAndDestroy>().SetTergetPostion(new Vector3(i.transform.localPosition.x, 3f, i.transform.localPosition.z), 0.1f);
		}

		SlotList.Clear();
		SlotNum = 0;
	}
}
