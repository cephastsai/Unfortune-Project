using UnityEngine;
using System.Collections;

public class TableSlot : MonoBehaviour {

	//slot
	public GameObject Slot; // name : Slot
	public int SlotNum = 0;
	private float Width = 2.04f;
	public float lastSlotPos = 0;

	//Timer
	private float Timer = 0;
	private bool TimerFlag = true;

	//Mat
	public Material SlotMat; // name : Stencil Draw In Mask Mat

	void Start(){
		init();
	}

	void Update(){
		
		if(SlotNum < Table.Ins.ActionNumber+Table.Ins.TableList.Count){
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


	public void init(){
		CreateSlot();
	}

	void CreateSlot(){
		GameObject slot = Instantiate(Slot);
		slot.transform.SetParent(Table.Ins.transform);
		slot.transform.localPosition = new Vector3( SlotNum*Width, 2.1f, 0);
		slot.GetComponent<SpriteRenderer>().material = SlotMat;
		slot.AddComponent<GameObjectMoving>().SetTergetPostion(new Vector3( SlotNum*Width, 0, 0), 0.1f);
		lastSlotPos = SlotNum*Width;	
		SlotNum++;
	}

}
