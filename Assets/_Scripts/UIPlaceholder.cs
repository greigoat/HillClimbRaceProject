using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScore
{
    public string levelName;
    public float time;
}

public class UIPlaceholder : MonoBehaviour
{
    public string currentLevelName;
    public static UIPlaceholder Instance { get; private set; }

    public RectTransform FinishRaceMenu;
    public RectTransform CarCrashText;

    void Start()
    {
        Instance = this;
    }

    public void NextRace_OnClick(string raceName)
    {
        GameplayManager.Instance.LoadLevel(raceName);
        PlayerScore ps = new PlayerScore();
        ps.levelName = currentLevelName;
        ps.time = GameplayManager.Instance.timer;

        var pss = JsonUtility.ToJson(ps);
        PlayerPrefs.SetString(currentLevelName, pss);

        GameplayManager.Instance.Pause();
        GameplayManager.Instance.IsRaceLevel = true;
        GameplayManager.Instance.IsPlayerSpawned = false;
        GameplayManager.Instance.IsRaceStarted = false;
        GameplayManager.Instance.IsPlayerCrashed = false;
    }
}
