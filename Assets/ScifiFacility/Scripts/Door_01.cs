using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_01 : MonoBehaviour {

//	public GameObject Wing_Right;
//	public GameObject Wing_Left;

	public Animation WingLeft;
	public Animation WingRight;

	bool opened = false;
	bool canaccess = false;

	GameObject canvas;

	InventoryManager im;

	public enum RequiredCard
	{
		Key1,
		Key2,
		Key3,
		Keypad
	}

	public RequiredCard card;

	int required;

	void Start() {
		im = GameObject.Find("GameManager").GetComponent<InventoryManager>();
		canvas = GameObject.Find("Canvas");
	}

	void Update()
	{
		if (!canaccess)
		{
			switch (card)
			{
				case RequiredCard.Key1:
					if (im.GotKey1) canaccess = true;
					required = 1;
					break;
				case RequiredCard.Key2:
					if (im.GotKey2) canaccess = true;
					required = 2;
					break;
				case RequiredCard.Key3:
					if(im.GotKey3) canaccess = true;
					required = 3;
					break;
				case RequiredCard.Keypad:
					if(im.GotKey4) canaccess = true;
					required = 4;
					break;
			}
		}
	}


	void OnTriggerEnter(Collider c) {
		if (c.tag.Equals("GameController"))
		{
			canvas.BroadcastMessage("UpdateText", 5);
		}
	}

	void OnTriggerStay(Collider c)
	{
		if (c.tag.Equals("GameController") && Input.GetKey(KeyCode.E) && !opened) {
			if (canaccess)
			{
				opened = true;
				GetComponent<AudioSource> ().Play ();
				WingLeft ["door_01_wing_left"].speed = 1;
				WingRight ["door_01_wing_right"].speed = 1;
				WingLeft.Play ();
				WingRight.Play ();
			}
			else
			{
				canvas.BroadcastMessage("NoAccess", required);
			}
		}
	}

	void OnTriggerExit(Collider c) {
		canvas.BroadcastMessage("UpdateText", 4);
		if (c.tag.Equals("GameController") && canaccess && opened) {
			opened = false;
			GetComponent<AudioSource> ().Play ();
			WingLeft ["door_01_wing_left"].time = WingLeft ["door_01_wing_left"].length;
			WingRight ["door_01_wing_right"].time = WingRight ["door_01_wing_right"].length;
			WingLeft ["door_01_wing_left"].speed = -1;
			WingRight ["door_01_wing_right"].speed = -1;
			WingLeft.Play ();
			WingRight.Play ();
		}

	}


}
