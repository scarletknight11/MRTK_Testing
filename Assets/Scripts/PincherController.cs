﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GripState { Fixed = 0, Opening = -1, Closing = 1 };

public class PincherController : MonoBehaviour
{
    public GameObject fingerA;
    public GameObject fingerB;

    PincherFingerController fingerAController;
    PincherFingerController fingerBController;

    // Grip - the extent to which the pincher is closed. 0: fully open, 1: fully closed.
    public float grip;
    public float gripSpeed = 3.0f;
    public GripState gripState = GripState.Fixed;
    public GripState gripState2 = GripState.Closing;
    public GripState gripState3 = GripState.Opening;



    void Start()
    {
        fingerAController = fingerA.GetComponent<PincherFingerController>();
        fingerBController = fingerB.GetComponent<PincherFingerController>();
    }

    void FixedUpdate()
    {
        UpdateGrip();
        UpdateFingersForGrip();
    }


    // READ
    public float CurrentGrip()
    {
        // TODO - we can't really assume the fingers agree, need to think about that
        float meanGrip = (fingerAController.CurrentGrip() + fingerBController.CurrentGrip()) / 2.0f;
        return meanGrip;
    }


    public Vector3 CurrentGraspCenter()
    {
        /* Gets the point directly between the middle of the pincher fingers,
         * in the global coordinate system.      
         */
        Vector3 localCenterPoint = (fingerAController.GetOpenPosition() + fingerBController.GetOpenPosition()) / 2.0f;
        Vector3 globalCenterPoint = transform.TransformPoint(localCenterPoint);
        return globalCenterPoint;
    }


    // CONTROL

    public void ResetGripToOpen()
    {
        grip = 0.0f;
        fingerAController.ForceOpen(transform);
        fingerBController.ForceOpen(transform);
        gripState = GripState.Fixed;
    }

    // GRIP HELPERS
    void UpdateGrip()
    {
        if (gripState != GripState.Fixed)
        {
            float gripChange = (float)gripState * gripSpeed * Time.fixedDeltaTime;
            float gripGoal = CurrentGrip() + gripChange;
            grip = Mathf.Clamp01(gripGoal);
        }
    }

    public void UpdateGripping()
    {
        if (gripState2 == GripState.Closing)
        {
            float gripChange = (float)gripState2 * gripSpeed * Time.fixedDeltaTime;
            float gripGoal = CurrentGrip() + gripChange;
            grip = Mathf.Clamp01(gripGoal);
        }
    }

    public void UpdateGripping2()
    {
        if (gripState3 == GripState.Opening)
        {
            gripState3 = GripState.Opening;
            float gripChange = (float)gripState3 * gripSpeed * Time.fixedDeltaTime;
            float gripGoal = CurrentGrip() + gripChange;
            grip = Mathf.Clamp01(gripGoal);
        }
    }

    void UpdateFingersForGrip()
    {
        fingerAController.UpdateGrip(grip);
        fingerBController.UpdateGrip(grip);
    }
}
