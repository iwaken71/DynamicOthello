using UnityEngine;
using System.Collections;

public class Camera2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Shot ();
		}
	
	}

	void Shot(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000)) {
			if(hit.collider.gameObject.tag == "image"){
				Animator anim = hit.collider.gameObject.GetComponent <Animator>();
				anim.SetTrigger("test2");
			}
		}
	}
}
