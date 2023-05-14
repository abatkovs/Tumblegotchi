using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ValueToSprite : MonoBehaviour
{
    [SerializeField] private List<Sprite> numbersSprites;

    [SerializeField] private SpriteRenderer number1Sprite;
    [SerializeField] private SpriteRenderer number2Sprite;

    private readonly int _minValue = 0;
    private readonly int _maxValue = 99;

    /// <summary>
    /// Changes number value to sprite image.
    /// </summary>
    /// <param name="sr"></param>
    /// <param name="value"> 0 - 9</param>
    private void ChangeValueToSpriteNumber(SpriteRenderer sr, int value)
    {
        sr.sprite = numbersSprites[value];
    }

    public void SetSpriteNumbers(int value)
    {
        var n = Mathf.Clamp(value, _minValue, _maxValue);
        var n1 = value / 10;
        var n2 = value % 10;
        ChangeValueToSpriteNumber(number1Sprite, n1);
        ChangeValueToSpriteNumber(number2Sprite, n2);
    }

}
