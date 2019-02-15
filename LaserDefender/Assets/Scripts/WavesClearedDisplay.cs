using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WavesClearedDisplay : MonoBehaviour {
    public ScoreKeeper scoreKeeper;
    public Text waves;
	// Use this for initialization
	void Start () {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        waves = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        waves.text = "Waves Cleared: " + scoreKeeper.Waves.ToString();
	}
}
