using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBestScoreText : MonoBehaviour {
    public Text text;
    public PlayerScore score;

    public void GetScore()
    {
        var str = PlayerPrefs.GetString(UIPlaceholder.Instance.currentLevelName);
        var score = JsonUtility.FromJson(str, typeof(PlayerScore)) as PlayerScore;

        if(score != null)
        {
            text.text = score.levelName + ": " + score.time;
        }
        else
        {
            text.text = "";
        }   
    }

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
