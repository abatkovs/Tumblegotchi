using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrownJelly : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private Transform moveToTarget;
    
    [SerializeField] private Vector2 edgeOffset;

    private void Start()
    {
        transform.position = GetRandomMovePosition();
    }


    private Vector2 GetRandomMovePosition()
    {
        //state = BGJellyState.Walking;
        //animator.PlayWalkAnim();
        return new Vector2(Random.Range(-edgeOffset.x, edgeOffset.x), transform.localPosition.y);
        //FlipSpriteToMoveDirection();
    }
    
}
