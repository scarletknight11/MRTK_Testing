using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripperDemoManualInput : MonoBehaviour
{
    public GameObject hand;


    void Update()
    {
        float input = Input.GetAxis("BigHandVertical");
        BigHandState moveState = MoveStateForInput(input);
        GripperDemoController controller = hand.GetComponent<GripperDemoController>();
        controller.moveState = moveState;
    }

    BigHandState MoveStateForInput(float input)
    {
        if (input > 0)
        {
            return BigHandState.MovingUp;
        }
        else if (input < 0)
        {
            return BigHandState.MovingDown;
        }
        else
        {
            return BigHandState.Fixed;
        }
    }

    public void Move()
    {
        //float speed = 2.0f;
        //float input = Input.GetAxis("BigHandVertical2") * speed * Time.deltaTime;
        transform.Translate(0, 2.0f, 0);
        Debug.Log("Clicked");
        //BigHandState moveState = MoveStateForInput(input);
        //GripperDemoController controller = hand.GetComponent<GripperDemoController>();
        //controller.moveState = moveState;
    }
}
