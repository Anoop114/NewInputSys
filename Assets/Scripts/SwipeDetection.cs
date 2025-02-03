using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class SwipeDetection : MonoBehaviour
    {
        
        [SerializeField] private float _minSwipeDistance = 0.2f;
        [SerializeField] private float _maxTime = 1f;
        [SerializeField,Range(0f,1f)] private float _directionThresold = 0.9f;
        
        private Vector2 startPosition;
        private Vector2 endPosition;
        private float startTime;
        private float endTime;
        
        
        private InputController control;

        private void Awake()
        {
            control = InputController.Instance;
        }

        private void OnEnable()
        {
            control.OnStartTouch += SwipeStart;
            control.OnEndTouch += SwipeEnd;
        }

        
        private void OnDisable()
        {
            control.OnStartTouch -= SwipeStart;
            control.OnEndTouch -= SwipeEnd;
        }
        
        
        private void SwipeEnd(Vector2 position, float time)
        {
            endPosition = position;
            endTime = time;
            DetectSwipe();
        }

        

        private void SwipeStart(Vector2 position, float time)
        {
            startPosition = position;
            startTime = time;
        }
        
        private void DetectSwipe()
        {
            if (Vector3.Distance(startPosition, endPosition) >= _minSwipeDistance && (endTime - startTime) <= _maxTime)
            {
                Debug.DrawLine(startPosition, endPosition, Color.green,2f);
                Vector3 direction = endPosition - startPosition;
                Vector2 direction2D = new Vector2(direction.x , direction.y).normalized;
                SwipeDirection(direction2D);
            }
        }

        private void SwipeDirection(Vector2 direction2D)
        {
            if (Vector2.Dot(direction2D, Vector2.up) > _directionThresold)
            {
                Debug.Log("swipe up");
            }

            else if (Vector2.Dot(direction2D, Vector2.down) > _directionThresold)
            {
                Debug.Log("swipe down");
            }

            if (Vector2.Dot(direction2D, Vector2.left) > _directionThresold)
            {
                Debug.Log("swipe left");
            }

            else if (Vector2.Dot(direction2D, Vector2.right) > _directionThresold)
            {
                Debug.Log("swipe right");
            }
        }
    }
}