using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour {
	public Transform Cam;
	public Vector3 CamForward;
	Vector3 move;
	bool jump;

	public bool IsMine;


	// Use this for initialization
	void Start () {
		if(Camera.main != null)
		{
			Cam = Camera.main.transform;
		}
		else
		{
			Debug.LogWarning(
			"Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
			// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
		}
		
	
	}
	
	// Update is called once per frame
	void Update () {
		if (IsMine)
		{
			if (!jump)
			{
				jump = Input.GetMouseButtonDown(0);
			}
		}
	}

	void FixedUpdate()
	{
		if (IsMine)
		{
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			bool crouch = Input.GetKey(KeyCode.C);

			if (Cam != null)
			{
				CamForward = Vector3.Scale(Cam.forward, new Vector3(1, 0, 1)).normalized;
				move = v * CamForward + h * Cam.right;
			}else
			{
				move = v * Vector3.forward + h * Vector3.right;
			}

			transform.Translate(move);
		   
		}
	}
}
