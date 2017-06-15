using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InputButton
{
    public string button;
    public UnityEvent OnPressed;
    public UnityEvent OnReleased;

    public void Read()
    {
        if (Input.GetButtonDown(button))
            OnPressed.Invoke();

        if (Input.GetButtonUp(button))
            OnReleased.Invoke();
    }
}

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    public InputButton[] buttons;
    public Vector2 touchOrigin = -Vector2.one;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        enabled = false;
    }


    void Update()
    {
        foreach (var button in buttons)
            button.Read();
    }

    void Read()
    {
        //var motorTorque = Input.GetAxis("Horizontal");
        //var torque = Input.GetAxis("Pitch");
        //GameplayManager.Instance.playerVehicle.AddMotorTorque(motorTorque);
        //GameplayManager.Instance.playerVehicle.AddTorque(torque);

#if (UNITY_STANDALONE || UNITY_WEBPLAYER)
        var motorTorque = Input.GetAxis("Horizontal");
        var torque = Input.GetAxis("Pitch");
        GameplayManager.Instance.playerVehicle.AddMotorTorque(motorTorque);
        GameplayManager.Instance.playerVehicle.AddTorque(torque);
#else
        int horizontal =0;
        int vertical =0;

        if(Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if(touch.phase == TouchPhase.Began)
            {
                touchOrigin = touch.position;
            }
            else if(touch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = touch.position;
                Vector2 diff = touchEnd - touchOrigin;
                touchOrigin.x = -1;

                // Horizontal
                if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
                    horizontal = diff.x > 0 ? 1 : -1;
                else
                    vertical = diff.y > 0 ? 1 : -1;
            }
        }

        if (horizontal != 0 || vertical != 0)
        {
            print("touch happened");
        }

#endif
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Read();
    }


}
