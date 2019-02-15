using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
    public static int Score;
    private Text ScoreText;
    public int Waves;
    private ExtraFormationManager extraManager;
    private ExtraFormationManager extraManagerTwo;
    private FormationManager manager;
    public bool wavesIncremented = false;
    public AudioClip alarm;
    public bool playAlarm = false;

    void Start()
    {
        Waves = 0;
        Score = 0;
        manager = GameObject.Find("EnemyFormation").GetComponent<FormationManager>();
        extraManager = GameObject.Find("ExtraEnemyFormation").GetComponent<ExtraFormationManager>();
        extraManagerTwo = GameObject.Find("ExtraEnemyFormation2").GetComponent<ExtraFormationManager>();
        ScoreText = GetComponent<Text>();
        extraManager.active = false;
        extraManagerTwo.active = false;
        
    }

    public void incrementScore(int Points)
    {
        Score += Points;
        ScoreText.text = "Current Score: " + Score.ToString();
    }

    public void incrementWaves()
    {
        if (!wavesIncremented)
        {
            Waves += 1;
            if (Waves == 4)
            {
                manager.playAlert = true;
            }
            if (Waves == 9)
            {
                playAlarm = true;
            }
            Debug.Log(Waves);
            if (Waves >= 10 && Waves <= 19)
            {
                if (playAlarm)
                {
                    AudioSource.PlayClipAtPoint(alarm, transform.position, 10f);
                    playAlarm = false;
                }
               
                Debug.Log("extra formation active");
                extraManager.active = true;
                extraManagerTwo.active = false;
            }
            else if (Waves >= 20)
            {
                extraManager.active = true;
                extraManagerTwo.active = true;
            }
            else
            {
                extraManager.active = false;
                extraManagerTwo.active = false;
            }
            wavesIncremented = true;
        }
        
    }

    public static void ResetScore()
    {
        Score = 0;
    }
	
}
