using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorekeeper : MonoBehaviour {

	//点数
	private int points = 0;

	//点数を表示するオブジェクト
	private GameObject theScore;

	// Use this for initialization
	void Start () {
		this.theScore = GameObject.Find ("Score");
		this.theScore.GetComponent<Text>().text = ("Score: " + points);
	}

	//点数を更新する
	public void UpdateScore(int addedPoints){
		points += addedPoints;
		this.theScore.GetComponent<Text>().text = ("Score: " + points);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
