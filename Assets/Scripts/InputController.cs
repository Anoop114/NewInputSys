using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputController : Singelton<InputController>
{
    #region Events

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    #endregion
    
    private Swipe swipeInputs;
    private Camera camera;
    private void Awake() => swipeInputs = new Swipe();

    private void OnEnable() => swipeInputs.Enable();

    private void OnDisable() => swipeInputs.Disable();

    private void Start()
    {
        camera = Camera.main;
        swipeInputs.Touch.TouchDetection.started += StartTouchAction;
        swipeInputs.Touch.TouchDetection.canceled += EndTouchAction;
    }

    private void EndTouchAction(InputAction.CallbackContext ctx)
    {
        OnEndTouch?.Invoke(PrimaryTouchPosition, (float)ctx.time);
    }

    private void StartTouchAction(InputAction.CallbackContext ctx)
    {
        OnStartTouch?.Invoke(PrimaryTouchPosition, (float)ctx.startTime);
    }

    private Vector2 PrimaryTouchPosition => Utilits.ScreenPointToWorldPoint(swipeInputs.Touch.SwipeGesture.ReadValue<Vector2>(), camera);
}
