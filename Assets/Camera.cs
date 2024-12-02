using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    // SMOOTH FOLLOW
    private readonly Vector3 _offset = new Vector3(0, 0, -10);
    private readonly float _smoothTime = 0.25f;
    private Vector3 _velocity;
    
    // DRAG
    public Vector3 origin;
    public Vector3 difference;
    public UnityEngine.Camera mainCamera;
    private bool _isDragging;
    
    [SerializeField] private Transform target;
    
    private Vector3 GetMousePosition => mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    
    public void Awake()
    {
        mainCamera = UnityEngine.Camera.main;
    }

    void LateUpdate()
    {
        Vector3 targetPos = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos + _offset, ref _velocity, _smoothTime);
        
        if (!_isDragging) return;
        difference = GetMousePosition - transform.position;
        transform.position = origin - difference;
    }
    
    public void OnDrag(InputAction.CallbackContext context)
    {
        if (context.started) origin = GetMousePosition;
        {
            _isDragging = context.started || context.performed;
        }
    }
}