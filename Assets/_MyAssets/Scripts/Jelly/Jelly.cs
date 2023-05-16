using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    [SerializeField] private GameObject bgJelly;

    public void ActivateBGJelly()
    {
        bgJelly.gameObject.SetActive(true);
    }
}
