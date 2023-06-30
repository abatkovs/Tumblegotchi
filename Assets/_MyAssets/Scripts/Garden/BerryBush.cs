using System;
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
    [SerializeField] private SoundData berryPickSound;
    [Space]
    [SerializeField] private BerryBushGrowthStages growthAfterGathering;
    [SerializeField] private int ageAfterPickingBerries;

    [SerializeField] private GameObject drone;

    [SerializeField] private int age;
    [SerializeField] public bool hasSeed;
    [SerializeField] private bool isDroneVisible;

    [SerializeField] private GameObject animatedBerryTree;

    public event Action OnBerryGather;
    
    private void Start()
    {
        InitBush();
    }

    private void InitBush()
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
                if (currentGrowthStage.IsFullyGrown)
                {
                    ToggleAnimatedBerryTree(true);
                }
            }
        }
    }

    public void ToggleDrone(bool value)
    {
        isDroneVisible = value;
        drone.SetActive(value);
    }

    public void GatherBerries()
    {
        if (currentGrowthStage.BerryYield > 0)
        {
            StopAllCoroutines();
            GameManager.Instance.AddBerries(currentGrowthStage.BerryYield);
            age = ageAfterPickingBerries;
            currentGrowthStage = growthAfterGathering;
            spriteRenderer.sprite = currentGrowthStage.Sprite;
            StartGrowing();
            OnBerryGather?.Invoke();
            ToggleAnimatedBerryTree(false);
            SoundManager.Instance.PlaySound(berryPickSound);
        }
    }

    private void ToggleAnimatedBerryTree(bool active)
    {
        spriteRenderer.enabled = !active;
        animatedBerryTree.SetActive(active);
    }

    public void PlantSeed()
    {
        hasSeed = true;
        StartGrowing();
    }
}