using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationManager : MonoBehaviour
{
    public enum OrientationType
    {
        Portrait,
        Landscape,
        AutoRotation
    }

    public OrientationType targetOrientation = OrientationType.Portrait;

    private void Start()
    {
        SetOrientation();
    }

    private void SetOrientation()
    {
        switch (targetOrientation)
        {
            case OrientationType.Portrait:
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case OrientationType.Landscape:
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                break;
            case OrientationType.AutoRotation:
                Screen.orientation = ScreenOrientation.AutoRotation;
                break;
        }
    }

    private void Update()
    {
        if (targetOrientation == OrientationType.AutoRotation && Screen.orientation != ScreenOrientation.AutoRotation)
        {
            SetOrientation();
        }
    }
}


