using UnityEngine;

public class DraggingAround : MonoBehaviour
{
    [SerializeField] private Transform game;
    [SerializeField] private Button button;
    [Space] 
    [SerializeField] private Vector3 draggingOffset;
    [SerializeField] private float minX = -6.5f, maxX = 8f, minY = -0.5f, maxY = 4.8f;

    private bool _isDragged;
    private Camera _camera;
    private Vector3 _updatedPosition;
    private float _screenWidth;
    private float _screenHeight;

    private void Start()
    {
        _camera = Camera.main;
        button.OnButtonDown += ButtonOnOnButtonDown;
        button.OnButtonReleased += ButtonOnOnButtonReleased;
    }


    private void Update()
    {
        DragGameAround();
    }

    private void ConfineDraggingArea()
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        
    }

    private void DragGameAround()
    {
        if (_isDragged)
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log($"{mousePos.x > minX && mousePos.x < maxX}");
            if (mousePos.x > minX && mousePos.x < maxX)
            {
                _updatedPosition.x = mousePos.x;
                _updatedPosition.x -= draggingOffset.x;
            }
            
            if (mousePos.y > minY && mousePos.y < maxY)
            {
                _updatedPosition.y = mousePos.y;
                _updatedPosition.y -= draggingOffset.y;
            }

            _updatedPosition.z = 0;
            game.position = _updatedPosition;
        }
    }

    private void ButtonOnOnButtonDown()
    {
        var gamePos = game.position;
        var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        draggingOffset = mouseWorldPos - gamePos;
        //Debug.Log($"g: {gamePos} w: {mouseWorldPos} d: {draggingOffset}");
        ToggleDragging(true);
    }

    private void ButtonOnOnButtonReleased()
    {
        ToggleDragging(false);
        Debug.Log(_camera.ScreenToWorldPoint(Input.mousePosition));
    }
    
    private void ToggleDragging(bool value)
    {
        _isDragged = value;
    }
}
