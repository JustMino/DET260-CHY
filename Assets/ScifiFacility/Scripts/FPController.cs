using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController : MonoBehaviour {

	public float speed = 6f;
	public float mouseSensitivity =5f;
	public float jumpSpeed = 10f;

	private float rotationLeftRight;
	private float verticalRotation;
	private float forwardspeed;
	private float sideSpeed;
	private float verticalVelocity;
	private Vector3 speedCombined;
	private CharacterController cc;

	bool keypadactive = false;
	bool inkeypadrange = false;
	bool lookedatkeypad = false;

	private Camera cam;

	GameObject canvas;
	public GameObject keypadUI;

	// Use this for initialization
	void Start () {
		cam = GetComponentInChildren<Camera> ();
		cc = GetComponent<CharacterController> ();
		canvas = GameObject.Find("Canvas");
		keypadUI = GameObject.Find("KeypadGUI");
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		keypadUI.SetActive(false);
	}

	// Update is called once per frame
	void Update () {

		Checkhit();
		OpenKeypad();

		rotationLeftRight = Input.GetAxis ("Mouse X") * mouseSensitivity;
		if (!keypadactive) transform.Rotate (0, rotationLeftRight,0);

		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -60f, 60f);
		if (!keypadactive) cam.transform.localRotation = Quaternion.Euler (verticalRotation, 0,0);

		forwardspeed = Input.GetAxis ("Vertical") * speed;
		sideSpeed = Input.GetAxis ("Horizontal") * speed;

		if (Input.GetKey(KeyCode.LeftShift)) {
			forwardspeed *= 2f;
		}

		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		if (cc.isGrounded && Input.GetButtonDown ("Jump")) {
			verticalVelocity = jumpSpeed;
		}

		speedCombined = new Vector3 (sideSpeed, verticalVelocity, forwardspeed);

		speedCombined = transform.rotation * speedCombined;

		if (!keypadactive) cc.Move (speedCombined * Time.deltaTime);
	}

	void OpenKeypad()
	{
		if (lookedatkeypad && Input.GetKey(KeyCode.E))
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			keypadUI.SetActive(true);
			keypadactive = true;
		}
	}

	public void CloseKeypad()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		keypadUI.SetActive(false);
		keypadactive = false;
	}

	void Checkhit()
	{
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit))
		{
			if (hit.transform.tag.Equals("Keypad1"))
			{
				canvas.BroadcastMessage("UpdateText", 6);
				lookedatkeypad = true;
			}
			else
			{
				if (lookedatkeypad)
				{
					lookedatkeypad = false;
					canvas.BroadcastMessage("UpdateText", 4);
				}
			}
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag.Equals("Keypad"))
		{
			inkeypadrange = true;
		}
		if (c.tag.Equals("End"))
		{
			canvas.BroadcastMessage("GameComplete");
		}
	}

	void OnTriggerExit(Collider c)
	{
		if (c.tag.Equals("Keypad"))
		{
			inkeypadrange = false;
		}
	}
}
