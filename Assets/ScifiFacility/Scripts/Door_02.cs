using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_02 : MonoBehaviour {

//	public GameObject Wing_Right;
//	public GameObject Wing_Left;

InventoryManager im;

public enum RequiredCard
{
	Key1,
	Key2,
	Key3
}

bool canaccess = false;

bool opened = false;

	public Animation Wing;

	public RequiredCard card;
	GameObject canvas;

	int required;

	void Start()
	{
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
			}
		}
	}

	void OnTriggerEnter(Collider c) {
		if(c.tag.Equals("GameController"))
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
				Wing ["door_02_wing"].speed = 1;
				Wing.Play ();
				canvas.BroadcastMessage("UpdateText", 4);
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
			Wing ["door_02_wing"].time = Wing ["door_02_wing"].length;
			Wing ["door_02_wing"].speed = -1;
			Wing.Play ();
		}

	}


}
