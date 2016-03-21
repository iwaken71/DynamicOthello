using UnityEngine;
using System.Collections;

public class JumpSc : MonoBehaviour {
	GameObject player;


	// Use this for initialization
	void Start () {
		player = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Jump(){
		player.GetComponent<Rigidbody> ().velocity = new Vector3 (0,50,0);
	}
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Zabuton") {
			Debug.Log ("zab");
			Jump ();
		}
	}
}
