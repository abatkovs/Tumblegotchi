using UnityEngine;

public class JellyBG : MonoBehaviour
{
    public enum BGJellyState
    {
        Idle,
        Walking,
    }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Transform moveToTarget;
    
    [SerializeField] private Vector2 edgeOffset;
    [SerializeField] private Vector2 newMoveToLocation;

    [SerializeField] private float walkInterval = 5f;
    [SerializeField] private float currentWalkInterval;

    private BGJellyState _state;
    private JellyBGAnimator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<JellyBGAnimator>();
        currentWalkInterval = walkInterval;
    }

    private void Update()
    {
        MoveJelly();
        CountdownForNewWalkPosition();
    }

    private void CountdownForNewWalkPosition()
    {
        if(_state == BGJellyState.Walking) return;
        currentWalkInterval -= Time.deltaTime;
        if (currentWalkInterval < 0)
        {
            SetRandomMovePosition();
            currentWalkInterval = walkInterval;
        }
    }

    private void MoveJelly()
    {
        if(_state != BGJellyState.Walking) return;
        var step =  moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, moveToTarget.position, step);
        
        if (Vector3.Distance(transform.position, moveToTarget.position) < 0.001f)
        {
            _state = BGJellyState.Idle;
            _animator.PlayIdleAnim();
        }
    }

    //TODO: Make sure he moves considerable amount
    private void SetRandomMovePosition()
    {
        _state = BGJellyState.Walking;
        _animator.PlayWalkAnim();
        moveToTarget.localPosition = new Vector2(Random.Range(-edgeOffset.x, edgeOffset.x), transform.localPosition.y);
        FlipSpriteToMoveDirection();
    }

    private void FlipSpriteToMoveDirection()
    {
        _spriteRenderer.flipX = transform.position.x > moveToTarget.position.x;
    }
}
