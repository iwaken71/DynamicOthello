using UnityEngine;
using System.Collections;

public class BGMmanager : MonoBehaviour {
	public AudioSource startbgm;
	public AudioSource setbgm;
	public AudioSource[] bgm;
	public AudioSource winbgm,losebgm;
	bool Onsetbgm;
	bool OnBGM;
	int number;
	bool BGMStart;
	bool Onwin,Onlose;
	
	// Use this for initialization
	void Start () {
		//DontDestroyOnLoad (this.gameObject);
		OnBGM = true;
		Onsetbgm = false;
		number = 0;
		BGMStart = false;
		Onwin = false;
		Onlose = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "Start") {
			if(!startbgm.isPlaying){
				startbgm.Play();
				startbgm.volume = 0.25f;
			}
		} else {
			if(startbgm.isPlaying){
				startbgm.volume = 0;
				startbgm.Stop();
			}
		}
		if (Application.loadedLevelName == "Game"||Application.loadedLevelName == "Game2") {
			if(!ScoreScript.GAME){
				if(!Onsetbgm){
					setbgm.Play();
					setbgm.volume = 0.25f;
					Onsetbgm = true;
				}
				bgm[number].volume = 0;
			}else{
				setbgm.Stop();
				if(!BGMStart){
					bgm[number].Play();
					BGMStart = true;
					OnBGM = true;
					bgm[number].volume = 0.25f;
				}
				
				
			}
		}
		
	}
	public void BGMClick(){
		Debug.Log (number);
		if (OnBGM) {
			bgm [number].volume = 0;
			bgm [number].Stop ();
			number++;
			if (number == bgm.Length) {
				number = 0;
			}
			OnBGM = false;
		} else {
			bgm[number].Play();
			bgm [number].volume = 0.25f;
			OnBGM = true;
			
		}
	}
	public void WinBGMPlay(){
		if (!Onwin) {
			winbgm.Play ();
			winbgm.volume = 0.5f;
			Onwin = true;
		}
	}
	public void LoseBGMPlay(){
		if (!losebgm.isPlaying) {
			losebgm.Play ();
			losebgm.volume = 0.5f;
		}
	}
}
