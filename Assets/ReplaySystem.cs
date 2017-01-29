using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour {

	private const int bufferFrames = 100;
	private MyKeyFrame[] keyFrames = new MyKeyFrame [bufferFrames];
	private Rigidbody rigi;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		rigi = GetComponent<Rigidbody> ();
		gameManager = FindObjectOfType<GameManager> ();
	}
	// Update is called once per frame
	void Update () {
		if (!gameManager.recording) {
			PlayBack ();
		}else {
			Record ();
		}
	}

	public void PlayBack(){
		rigi.isKinematic = true;
		int frame = Time.frameCount % bufferFrames;
		print ("reading:" + frame);
		transform.position = keyFrames [frame].pos;
		transform.rotation = keyFrames [frame].rot;

	}


	void Record ()
	{
		rigi.isKinematic = false;
		int frame = Time.frameCount % bufferFrames;
		float time = Time.time;
		print ("writing:" + frame);
		keyFrames [frame] = new MyKeyFrame (time, transform.position, transform.rotation);
	}
	

}

public struct MyKeyFrame {

	public float time;
	public Vector3 pos;
	public Quaternion rot;

	public MyKeyFrame (float time, Vector3 pos, Quaternion rot){
		this.time = time;
		this.pos = pos;
		this.rot = rot;
	}



}