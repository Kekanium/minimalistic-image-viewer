using UnityEngine;
using UnityEngine.UI;


public class ViewerInputController : MonoBehaviour {
    private Vector2 _touchStartPos;
    private const float MinSwipeDistance = 50f;

    private void Update() {
        GoBack();
    }

    private void GoBack() {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneTransition.SwitchToScene("Gallery");
        }
#endif

#if UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began: _touchStartPos = touch.position;
                    break;
                case TouchPhase.Ended:
                {
                    Vector2 swipeDelta = touch.position - _touchStartPos;

                    if (swipeDelta.magnitude > MinSwipeDistance)
                    {
                        if (swipeDelta.x > 0)
                        {
                            SceneTransition.SwitchToScene("Gallery");
                        }
                    }
                    break;
                }
            }
        }
#endif
    }
}