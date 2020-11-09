using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {

	public Vector3 COM = new Vector3 (0, 0, 0);

	public WheelCollider wfl;
	public WheelCollider wfr;
	public WheelCollider wrl;
	public WheelCollider wrr;

	public GameObject fl;
	public GameObject fr;
	public GameObject rl;
	public GameObject rr;

	public float topspeed = 250f;
	public float maxTorque=200f;
	public float maxSteerAngle=45f;
	public float currentspeed;
	public float maxBrakeTorque=2200f;
	public float decelerationSpeed=100f;

	private float Forward;
	private float Turn;
	private float Brake;
	private bool BrakeAllowed;



	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.centerOfMass = COM;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Forward = Input.GetAxis ("Vertical");
		Turn = Input.GetAxis ("Horizontal");
		Brake = Input.GetAxis ("Jump");

		wfl.steerAngle = maxSteerAngle * Turn;
		wfr.steerAngle = maxSteerAngle * Turn;

		currentspeed = 2 * 22 / 7 * wrl.radius * wrl.rpm * 60 / 1000;





		wrl.motorTorque = maxTorque * Forward;
		wrr.motorTorque = maxTorque * Forward;

		DecelerationSpeed ();
	}

	void Update()
	{
		Quaternion flq;//rotation of the wheel
		Vector3 flv;//position of wheel collider
		wfl.GetWorldPose (out flv, out flq);//get wheel collider position and rotation
		fl.transform.position=flv;
		fl.transform.rotation = flq;

		Quaternion frq;
		Vector3 frv;//position of wheel collider
		wfr.GetWorldPose (out frv, out frq);//get wheel collider position and rotation
		fr.transform.position=frv;
		fr.transform.rotation = frq;

		Quaternion rlq;
		Vector3 rlv;//position of wheel collider
		wrl.GetWorldPose (out rlv, out rlq);//get wheel collider position and rotation
		rl.transform.position=rlv;
		rl.transform.rotation = rlq;

		Quaternion rrq;
		Vector3 rrv;//position of wheel collider
		wrr.GetWorldPose (out rrv, out rrq);//get wheel collider position and rotation
		rr.transform.position=rrv;
		rr.transform.rotation = rrq;

		if (Input.GetKeyDown (KeyCode.Space)) {
			BrakeAllowed = true;
		} else {
			BrakeAllowed = false;
		}

		if (BrakeAllowed) {
			wrl.brakeTorque = maxBrakeTorque * Brake;
			wrr.brakeTorque = maxBrakeTorque * Brake;
			wfl.brakeTorque = maxBrakeTorque * Brake;
			wfr.brakeTorque = maxBrakeTorque * Brake;
			wrl.motorTorque = 0f;
			wrr.motorTorque = 0f;

		} else if (!BrakeAllowed && Input.GetButton ("Vertical") == true&&currentspeed < topspeed) {
			wrl.motorTorque = maxTorque * Forward;
			wrr.motorTorque = maxTorque * Forward;
			wrl.brakeTorque = 0f;
			wrr.brakeTorque = 0f;
			wfl.brakeTorque = 0f;
			wfr.brakeTorque = 0f;
		}
	}

	void DecelerationSpeed (){
		if (!BrakeAllowed && Input.GetButton ("Vertical") == false) {
			wrl.brakeTorque = decelerationSpeed;
			wrr.brakeTorque = decelerationSpeed;
			wfl.brakeTorque = decelerationSpeed;
			wfr.brakeTorque = decelerationSpeed;
			wrl.motorTorque = 0f;
			wrr.motorTorque = 0f;

		}
	}
}
