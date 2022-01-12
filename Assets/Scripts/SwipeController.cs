using UnityEngine;
using UnityEngine.Events;

public class SwipeController : MonoBehaviour
{
    [HideInInspector] public bool tap, swipeLeft, swipeRight, isDragging;
    public UnityEvent swipeLeftEvent;
    public UnityEvent swipeRightEvent;
    private Vector2 _startTouch, _swipeDelta;
    private float _x, _y;

    private void Update()
    {
        tap = swipeLeft = swipeRight = false;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            _startTouch = Input.mousePosition;
            isDragging = true;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }
#else
        if (Input.touches.Length!= 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                _startTouch = Input.touches[0].position;
                isDragging = true;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }
#endif
        if (isDragging)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            }
#else
            if(Input.touches.Length != 0)
            {
                _swipeDelta = Input.touches[0].position - _startTouch;
            }
#endif
        }

        if (_swipeDelta.magnitude > 150)
        {
            _x = _swipeDelta.x;
            _y = _swipeDelta.y;

            if(Mathf.Abs(_x) > Mathf.Abs(_y))
            {
                if(_x < 0)
                {
                    swipeLeft = true;
                    swipeLeftEvent.Invoke();
                }
                else
                {
                    swipeRight = true;
                    swipeRightEvent.Invoke();
                }
            }

            Reset();
        }
    }

    private void Reset()
    {
        isDragging = false;
        _startTouch = _swipeDelta = Vector2.zero;
    }
}
