using UnityEngine;
using System.Collections;

public class QuickTimeAreaProjector : MonoBehaviour {
	
	public Transform triggerToFollow; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	transform.position = new Vector3 (triggerToFollow.position.x, triggerToFollow.position.y + 5, triggerToFollow.position.z);
	}
}
