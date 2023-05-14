using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BerryBush : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float growthTickInterval = 1f;
    [SerializeField] private List<BerryBushGrowthStages> growthStages;
    [SerializeField] private BerryBushGrowthStages currentGrowthStage;

    [SerializeField] private GameObject drone;

    [SerializeField] private int age;
    [SerializeField] private bool hasSeed;
    [SerializeField] private bool isDroneVisible;


    private void Start()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        StartGrowing();
        if(!isDroneVisible) drone.SetActive(false);
    }

    private void StartGrowing()
    {
        if(!hasSeed) return;
        StartCoroutine(GrowthTick());
    }

    private IEnumerator GrowthTick()
    {
        yield return new WaitForSeconds(growthTickInterval);
        BerryAgeTick();
    }

    private void BerryAgeTick()
    {
        age++;
        UpdateSpriteAndGrowthStage();
        StartGrowing();
    }

    private void UpdateSpriteAndGrowthStage()
    {
        foreach (var growthStage in growthStages)
        {
            if (age < currentGrowthStage.AgeThreshold) continue;
            if (age >= growthStage.AgeThreshold)
            {
                spriteRenderer.sprite = growthStage.Sprite;
                currentGrowthStage = growthStage;
            }
        }
    }
}