using AnnulusGames.LucidTools.Inspector;
using UnityEngine;

public class TestPositions : MonoBehaviour
{
    [Button]
    private void GetPositions()
    {
        Debug.Log($"POS: {transform.position}");
        Debug.Log($"LocalPOS: {transform.localPosition}");
    }
}
