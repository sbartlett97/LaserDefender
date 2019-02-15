using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
        Text mytext = GetComponent<Text>();
        mytext.text = ScoreKeeper.Score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
