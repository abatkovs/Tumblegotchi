using UnityEngine;
using UnityEngine.Serialization;

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
    
    [FormerlySerializedAs("_animator")] [SerializeField] private JellyBGAnimator animator;

    [SerializeField] private BGJellyState state;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<JellyBGAnimator>();
        currentWalkInterval = walkInterval;
    }

    private void Update()
    {
        MoveJelly();
        CountdownForNewWalkPosition();
    }

    private void CountdownForNewWalkPosition()
    {
        if(state != BGJellyState.Idle) return;
        currentWalkInterval -= Time.deltaTime;
        if (currentWalkInterval < 0)
        {
            SetRandomMovePosition();
            currentWalkInterval = walkInterval;
        }
    }

    private void MoveJelly()
    {
        if(state != BGJellyState.Walking) return;
        var step =  moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, moveToTarget.position, step);
        
        if (Vector3.Distance(transform.position, moveToTarget.position) < 0.001f)
        {
            state = BGJellyState.Idle;
            animator.PlayIdleAnim();
        }
    }

    //TODO: Make sure he moves considerable amount
    private void SetRandomMovePosition()
    {
        state = BGJellyState.Walking;
        animator.PlayWalkAnim();
        moveToTarget.localPosition = new Vector2(Random.Range(-edgeOffset.x, edgeOffset.x), transform.localPosition.y);
        FlipSpriteToMoveDirection();
    }

    private void FlipSpriteToMoveDirection()
    {
        _spriteRenderer.flipX = transform.position.x > moveToTarget.position.x;
    }

    public void ActivateJelly()
    {
        state = BGJellyState.Walking;
        gameObject.SetActive(true);
        transform.localPosition = new Vector2(-0.71f, 0.084f);
        SetRandomMovePosition();
        animator.PlayWalkAnim();
    }
    
    public void DeactivateJelly()
    {
        gameObject.SetActive(false);
    }

    public void SetIdleState()
    {
        state = BGJellyState.Idle;
        animator.PlayIdleAnim();
    }

    public void NudgeJelly()
    {
        Vector2 nudgeOffset = new Vector2(-0.05f,0f);
        transform.localPosition = (Vector2)transform.localPosition + nudgeOffset;
    }

}
