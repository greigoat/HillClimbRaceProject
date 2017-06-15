using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoofTopTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 8)
        {
            // Set player as crashed
            GameplayManager.Instance.IsPlayerCrashed = true;

            StartCoroutine(
                GameplayManager.DelayAction(() =>
                {
                    GameplayManager.Instance.RestartRace();
                }, 
                GameplayManager.Instance.respawnTime));
        }
    }

    void Update()
    {
        // Display crash message is game is not paused and player is crashed
        if (!GameplayManager.Instance.IsGamePaused)
        {
            UIPlaceholder.Instance.CarCrashText.gameObject.SetActive(GameplayManager.Instance.IsPlayerCrashed);
        }
        else
        {
            UIPlaceholder.Instance.CarCrashText.gameObject.SetActive(false);
        }
    }
}
