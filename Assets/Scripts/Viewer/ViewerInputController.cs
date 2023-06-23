using UnityEngine;
using UnityEngine.UI;
namespace Viewer {
    public class ViewerInputController : MonoBehaviour {
        private const float MinSwipeDistance = 50f;

        [SerializeField] private Image image;

        [Range(0, 1)] public float rotationSpeed = 0.1f;

        [Range(0.001f, 0.01f)] public float zoomSpeed = 0.001f;

        [Range(0.001f, 0.1f)] public float dragSpeed = 0.003f;

        private RectTransform _imageRectTransform;
        private Vector2 _imageStartDelta;
        private Vector3 _imageStartPosition;
        private bool _isRotating;
        private float _prevTouchDeltaMag;

        private float _startingDistance;
        private Vector3 _startingScale;
        private float _touchDeltaMag;
        private Touch _touchOne;
        private Vector2 _touchOnePrevPos;
        private Vector2 _touchStartPos;
        private Vector2 _touchStartPosition;

        private Touch _touchZero;
        private Vector2 _touchZeroPrevPos;


        private void Start() {
            _imageRectTransform = image.GetComponent<RectTransform>();
        }

        private void Update() {
            GoBack();

            ZoomAndRotate();
            Drag();
        }

        private static void GoBack() {
#if UNITY_ANDROID
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneTransition.SwitchToScene("Gallery");
            }
#endif

#if UNITY_IOS
        if (Input.touchCount = 1)
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

        private void ZoomAndRotate() {
            if (Input.touchCount == 2)
            {
                _touchZero = Input.GetTouch(0);
                _touchOne = Input.GetTouch(1);

                switch (_touchZero.phase)
                {
                    // Проверяем, что оба пальца начали касание
                    case TouchPhase.Began when _touchOne.phase == TouchPhase.Began:
                        _isRotating = true;
                        _touchZeroPrevPos = _touchZero.position - _touchZero.deltaPosition;
                        _touchOnePrevPos = _touchOne.position - _touchOne.deltaPosition;
                        _prevTouchDeltaMag = (_touchZeroPrevPos - _touchOnePrevPos).magnitude;
                        break;
                    // Поворачиваем и масштабируем объект, если оба пальца двигаются
                    case TouchPhase.Moved when _touchOne.phase == TouchPhase.Moved && _isRotating:
                    {
                        // Поворот
                        Vector2 currentTouchDelta = _touchZero.position - _touchOne.position;
                        Vector2 previousTouchDelta = _touchZeroPrevPos - _touchOnePrevPos;
                        float angleDelta = Vector2.Angle(previousTouchDelta, currentTouchDelta);

                        Vector3 cross = Vector3.Cross(previousTouchDelta, currentTouchDelta);
                        if (cross.z > 0)
                            angleDelta *= -1;

                        image.transform.Rotate(0f, 0f, angleDelta * -rotationSpeed);

                        // Зум
                        _touchDeltaMag = (_touchZero.position - _touchOne.position).magnitude;
                        float deltaMagnitudeDiff = _prevTouchDeltaMag - _touchDeltaMag;
                        float scaleMultiplier = Mathf.Clamp(deltaMagnitudeDiff * -zoomSpeed, -1f, 1f);

                        Vector3 newScale = image.transform.localScale + new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);
                        if (newScale.x < 0.1)
                        {
                            image.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        }
                        else if (newScale.x > 5)
                        {
                            image.transform.localScale = new Vector3(5f, 5f, 5f);
                        }
                        else
                        {
                            image.transform.localScale = newScale;
                        }


                        _prevTouchDeltaMag = _touchDeltaMag;
                        _touchZeroPrevPos = _touchZero.position - _touchZero.deltaPosition;
                        _touchOnePrevPos = _touchOne.position - _touchOne.deltaPosition;
                        break;
                    }
                }

            }
            else
            {
                // Сбрасываем флаг поворота, если количество касаний меньше двух
                _isRotating = false;
            }
        }

        private void Drag() {

            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _touchStartPosition = touch.position;
                        _imageStartPosition = _imageRectTransform.position;
                        _imageStartDelta = _imageRectTransform.sizeDelta;
                        break;

                    case TouchPhase.Moved:
                        Vector2 touchDelta = (touch.position - _touchStartPosition) * dragSpeed;
                        Vector3 imagePosition = _imageStartPosition + new Vector3(touchDelta.x, touchDelta.y, 0f);
                        Vector2 imageDelta = _imageStartDelta - new Vector2(touchDelta.x, touchDelta.y);

                        _imageRectTransform.position = imagePosition;
                        _imageRectTransform.sizeDelta = imageDelta;
                        break;
                }
            }
        }
    }
}