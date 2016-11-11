using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {
	
	public float rotationSpeed;
	private Rigidbody rigidBody;

	void Start() 
	{
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.angularVelocity = Random.insideUnitSphere * rotationSpeed;
	}

	void Update () {
		
//		transform.Rotate(Vector3.right, Time.deltaTime);
//		transform.Rotate (Vector3.up, Time.deltaTime, Space.World);
//		transform.eulerAngles = Vector3.down;
	}
}
