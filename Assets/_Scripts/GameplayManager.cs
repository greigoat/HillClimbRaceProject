using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }
    public float respawnTime;

    public string currentLevelName;
    public float timer { get; private set; }
    public bool IsPlayerCrashed;
    public bool IsRaceLevel { get; set; }
    public bool IsTimerActive { get; set; }
    public bool IsGamePaused { get; set; }
    public bool IsRaceStarted { get; set; }
    public bool IsPlayerSpawned { get; set; }

    public CarController playerVehicle { get; private set; }
    private Transform playerSpawn;

    public UnityEvent OnRaceLoaded;

    public void RespawnPlayerVehicle()
    {
        Instance.playerVehicle.transform.position = Instance.playerVehicle.startPos;
        Instance.playerVehicle.Reset();
    }

    public void SpawnPlayerVehicle()
    {
        if (IsPlayerSpawned)
            return;

        var prefab = Resources.Load("Player") as GameObject;
        playerVehicle = Instantiate(prefab, playerSpawn.position, playerSpawn.rotation).GetComponent<CarController>();
        IsPlayerSpawned = true;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void StartRace()
    {
        if (IsRaceStarted)
            return;

        timer = 0.0F;
        IsTimerActive = true;
        PlayerInput.Instance.enabled = true;

        IsRaceStarted = true;
    }

    public void RestartRace()
    {
        timer = 0.0F;
        RespawnPlayerVehicle();
        IsTimerActive = true;
        IsPlayerCrashed = false;
        PlayerInput.Instance.enabled = true;
    }

    public void LoadLevel(string levelName)
    {
        if (levelName == "MainMenu")
        {
            // Make sure managers are destroyed
            Destroy(transform.root.gameObject);
        }

        SceneManager.LoadScene(levelName);
    }

    public void Pause()
    {
        if (!IsRaceStarted)
            return;

        IsGamePaused = !IsGamePaused;

        if (IsGamePaused)
        {
            Time.timeScale = 0.0F;
        }
        else
        {
            Time.timeScale = 1.0F;
            UIManager.Instance.DeactivateActiveMenu();
        }

        UIManager.Instance.timerText.gameObject.SetActive(!IsGamePaused);
        IsTimerActive = !IsGamePaused;
    }

    public static IEnumerator DelayAction(System.Action action, float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        action();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject.transform.root);
        Instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Use this for initialization
    void Start()
    {
        
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if (Instance.IsRaceLevel)
        {
            Instance.playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
            Instance.OnRaceLoaded.Invoke();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTimerActive)
        {
            timer += Time.deltaTime;
            UIManager.DrawTimer(timer, IsTimerActive);
        }
    }
}
