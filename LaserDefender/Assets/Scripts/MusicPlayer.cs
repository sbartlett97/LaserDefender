using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance;
    // Use this for initialization
    public AudioClip StartMenu;
    public AudioClip game;
    public AudioClip EndMenu;
    private AudioSource music;


	void Awake () {

		if (instance != null && instance != this){
			Destroy (gameObject);
		}else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
        music = GetComponent<AudioSource>();
	}
	void Start() {


	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnLevelWasLoaded(int level)
    {
        Debug.Log("Music Player Loaded New Level");
        music.Pause();
        if(level == 0)
        {
            music.clip = StartMenu;
        }else if(level == 1)
        {
            music.clip = game;
        }else
        {
            music.clip = EndMenu;
        }
        music.Play();
    }
}
