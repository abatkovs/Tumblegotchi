using UnityEngine;

public class DraggingAround : MonoBehaviour
{
    [SerializeField] private Transform game;
    [SerializeField] private Button button;

    private bool _isDragged;
    private Camera _camera;
    private Vector3 _updatedPosition;

    private void Start()
    {
        _camera = Camera.main;
        button.OnButtonDown += ButtonOnOnButtonDown;
        button.OnButtonReleased += ButtonOnOnButtonReleased;
    }


    private void Update()
    {
        if (_isDragged)
        {
            _updatedPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _updatedPosition.z = 0;
            game.position = _updatedPosition;
        }
    }

    private void ButtonOnOnButtonDown()
    {
        ToggleDragging(true);
    }

    private void ButtonOnOnButtonReleased()
    {
        ToggleDragging(false);
    }
    
    private void ToggleDragging(bool value)
    {
        _isDragged = value;
    }
}
