using UnityEngine;
using System.Collections;

public class StoneController : MonoBehaviour {

	public bool black;
	bool reverse;
	Animator anim;

	float timer;
	Vector3 pos;


	void Awake(){
		black = true;
		reverse = false;
		anim = GetComponent<Animator> ();
		timer = 0;
		pos = transform.position;
	}
	
	void Start () {

	}

	void Update () {

		if (reverse) {
			timer += Time.deltaTime;

			if(!anim.GetBool("motion")){
				timer = 0;
				reverse = false;
			}
			if(timer <= (1.0f/6)){
				transform.position += new Vector3(0,3.0f*Time.deltaTime,0);
			}else{
				transform.position += new Vector3(0,-2.0f*Time.deltaTime,0);
			}

		}
		if (anim.GetInteger ("color") == 1) {
			black = true;
		}else if (anim.GetInteger ("color") == 2) {
			black = false;
		}

	
	}
	void LateUpdate(){
		if (!reverse) {
			if(black){
				transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
			}else{
				transform.rotation = Quaternion.Euler(new Vector3(180,0,0));
			}
			transform.position = pos;
		}
		transform.position = new Vector3(pos.x,transform.position.y,pos.z);
	}


	public void Reverse(){
		if (!reverse) {
			timer = 0.0f;
			reverse = true;
			black = !black;
			anim.SetBool("motion",true);
			anim.SetTrigger("reverse");
		}
	}
	public void SetColor(int color){
		if (color == 1) {
			anim.SetInteger ("color", 1);
			black = true;
		} else if (color == 2) {
			anim.SetInteger ("color", 2);
			black = false;
		}
	}
	void OnAnimationMove(){
		
	}
}
