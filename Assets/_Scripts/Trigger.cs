using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent OnEnterTrigger, OnExitTrigger, OnTriggerRemain;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        OnEnterTrigger.Invoke();
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        OnExitTrigger.Invoke();
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        OnTriggerRemain.Invoke();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnterTrigger.Invoke();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        OnTriggerRemain.Invoke();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        OnExitTrigger.Invoke();
    }
}
