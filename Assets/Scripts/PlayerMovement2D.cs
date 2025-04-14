using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 _moveDirection;

    private Rigidbody2D _rb;

    // Событие, которое передает объект угла
    public static event System.Action<GameObject> OnPlayerCollidedWithCorner;
    public static event System.Action OnEnemyCollider;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveDirection = Vector2.right;
    }

    private void Update()
    {
        // Обработка нажатия на экран (ПК и мобильные устройства)
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            ReverseDirection();
        }
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveDirection * (speed * Time.unscaledDeltaTime));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy")) OnEnemyCollider?.Invoke();
        if (!other.CompareTag("Corner")) return;

        var cornerName = other.gameObject.name;
        Debug.Log($"Trigger at: {cornerName}, Current direction: {_moveDirection}");

        // Вызываем событие и передаем объект угла
        OnPlayerCollidedWithCorner?.Invoke(other.gameObject);

        // Логика для изменения направления в зависимости от угла
        switch (cornerName)
        {
            case "Corner_TopLeft":
                if (_moveDirection == Vector2.left)
                    _moveDirection = Vector2.down;       
                else if (_moveDirection == Vector2.up)
                    _moveDirection = Vector2.right;      
                break;

            case "Corner_TopRight":
                if (_moveDirection == Vector2.right)
                    _moveDirection = Vector2.down;      
                else if (_moveDirection == Vector2.up)
                    _moveDirection = Vector2.left;    
                break;

            case "Corner_BottomLeft":
                if (_moveDirection == Vector2.left)
                    _moveDirection = Vector2.up;     
                else if (_moveDirection == Vector2.down)
                    _moveDirection = Vector2.right;      
                break;

            case "Corner_BottomRight":
                if (_moveDirection == Vector2.right)
                    _moveDirection = Vector2.up;      
                else if (_moveDirection == Vector2.down)
                    _moveDirection = Vector2.left;    
                break;
        }

        transform.rotation = Quaternion.LookRotation(Vector3.forward, _moveDirection);
    }

    private void ReverseDirection()
    {
        _moveDirection = -_moveDirection;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, _moveDirection);

        Debug.Log($"ReverseDirection called. New direction: {_moveDirection}");
    }
}
