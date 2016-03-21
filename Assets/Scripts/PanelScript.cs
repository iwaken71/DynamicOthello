using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour {

	Animator anim;
	int state;
	RectTransform trans;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		state = 0;
		trans = this.GetComponent<RectTransform>();
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void Info(){
		anim.SetTrigger ("Test");
	}
}
