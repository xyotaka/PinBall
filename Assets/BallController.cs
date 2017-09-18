using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour {

	//ボールが見える可能性のあるz軸の最大値
	private float visiblePosZ = -6.5f;

	//ゲームオーバを表示するテキスト
	private GameObject gameoverText;

	//やり直しをするかどうかを選べるボックス
	private Rect dialogueBox = new Rect ((Screen.width - 200) / 2, (Screen.height - 300) / 2, 200, 200);

	//ボックスの表示に関わるブール
	private bool show = false;
	private bool oneTime = true;

	//Scorekeeperクラス
	private Scorekeeper SK;

	// Use this for initialization
	void Start () {
		//シーン中のGameOverTextオブジェクトを取得
		this.gameoverText = GameObject.Find("GameOverText");
		SK = gameObject.AddComponent (typeof(Scorekeeper)) as Scorekeeper;
	}
	
	// Update is called once per frame
	void Update () {
		//ボールが画面外に出た場合
		if (this.transform.position.z < this.visiblePosZ){
			Open ();
		}
	}
		
	void OnGUI(){
		if (show && oneTime)
			dialogueBox = GUI.Window (0, dialogueBox, DialogueWindow, "Game Over");
	}

	//やり直すあ終わりにするかを選べるボックスの設定
	void DialogueWindow(int windowID){
		float y = 20;
		GUI.Label (new Rect (5, y, dialogueBox.width, 20), "Restart?");

		if(GUI.Button(new Rect(5,y+40,dialogueBox.width-10,40),"Yes")){
			show = false;
			SceneManager.LoadScene ("GameScene");
		}

		if(GUI.Button(new Rect(5,y+100,dialogueBox.width-10,40),"No")){
			show = false;
			oneTime = false;
			//GameoverTextにゲームオーバを表示
			this.gameoverText.GetComponent<Text>().text = "Game Over";
			}
	}

	//ボックスを表示
	public void Open(){
		show = true;
	}

	void OnCollisionEnter(Collision other){

		//得点する点数
		int pointsGained = 0;

		//衝突するものによって点数が違う
		if (other.gameObject.tag == "SmallStarTag") {
			pointsGained = 5;
		} else if (other.gameObject.tag == "LargeStarTag") {
			pointsGained = 25;
		} else if (other.gameObject.tag == "SmallCloudTag") {
			pointsGained = 50;
		} else if (other.gameObject.tag == "LargeCloudTag") {
			pointsGained = 40;
		}

		//Scorekeeperを呼び出して点数を更新する
		SK.UpdateScore (pointsGained);
	}
}
