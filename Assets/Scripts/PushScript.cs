using UnityEngine;
using System.Collections;

public class PushScript : MonoBehaviour {

	Animator anim;

	void Start(){
		anim = GetComponent <Animator> ();
	}


	public void Push(){
		if (anim.GetBool ("start")) {
			anim.SetBool("start",false);
		} else {
			anim.SetBool("start",true);
		}
	}
}
