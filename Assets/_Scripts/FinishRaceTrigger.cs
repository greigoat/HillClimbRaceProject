using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishRaceTrigger : MonoBehaviour
{
    public UnityEvent OnFinishRace;
    public bool isEndOfDemo;

    void OnTriggerExit2D(Collider2D collider)
    { 
        if (collider.CompareTag("Player"))
        {
            if (GameplayManager.Instance.IsPlayerCrashed)
                return;

            GameplayManager.Instance.Pause();

            if(!isEndOfDemo)
                UIManager.Instance.SetIntermediateMenu(UIPlaceholder.Instance.FinishRaceMenu);

            OnFinishRace.Invoke();
        }
    }
}
