using UnityEngine;
using System.Collections;

public class TopScript : MonoBehaviour {

	public GameObject panel;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = panel.GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Change(){
		ScoreScript.PAUSE = !ScoreScript.PAUSE;
		anim.SetTrigger ("Test");
	}
}
