using UnityEngine;
using UnityEngine.Serialization;
public class OrientationController : MonoBehaviour {
    public enum Orientation {
        Any,
        Portrait,
        PortraitFixed,
        Landscape,
        LandscapeFixed
    }

    [FormerlySerializedAs("ScreenOrientation")]
    public Orientation screenOrientation;

    private void Awake() {
        switch (screenOrientation)
        {
            case Orientation.Any:
                Screen.orientation = ScreenOrientation.AutoRotation;

                Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
                Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
                break;

            case Orientation.Portrait:
                // Force screen to orient right, then switch to Auto
                Screen.orientation = ScreenOrientation.Portrait;
                Screen.orientation = ScreenOrientation.AutoRotation;

                Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
                Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = false;
                break;

            case Orientation.PortraitFixed:
                Screen.orientation = ScreenOrientation.Portrait;
                break;

            case Orientation.Landscape:
                // Force screen to orient right, then switch to Auto
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                Screen.orientation = ScreenOrientation.AutoRotation;

                Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
                Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
                break;

            case Orientation.LandscapeFixed:
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                break;
        }

        Destroy(gameObject);
    }
}