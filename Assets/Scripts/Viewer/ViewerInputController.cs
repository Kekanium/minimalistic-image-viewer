using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class ViewerInputController : MonoBehaviour {
    private Vector2 _touchStartPos;
    private const float MinSwipeDistance = 50f;
    
    private float _startingDistance;
    private Vector3 _startingScale;

   [SerializeField]private Image image;

    private void Update() {
        GoBack();
        Zoom();
    }

    private void GoBack() {
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

    private void Zoom() {
        if (Input.touchCount == 2)
        {
            // Получаем данные о двух касаниях
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Проверяем, было ли только что начато движение (начальное положение пальцев)
            if (touchOne.phase == TouchPhase.Began)
            {
                _startingDistance = Vector2.Distance(touchZero.position, touchOne.position);
                _startingScale = image.transform.localScale;
            }
            else if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved)
            {
                // Вычисляем текущее расстояние между пальцами
                float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                // Вычисляем разницу между начальным и текущим расстоянием
                float scaleFactor = currentDistance / _startingDistance;

                // Применяем масштабирование к объекту
                image.transform.localScale = _startingScale * scaleFactor;
                
            }
        }
    }
}