using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpdateDebugInfo : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private JellyBG bgJelly;
    [SerializeField] private JellyStats jellyStats;

    private Label _content;
    private void Start()
    {
        _content = uiDocument.rootVisualElement.Q<Label>("debug-info");
    }

    private void Update()
    {
        var values = $"Jelly:\n State: {jellyStats.CurrentJellyState}\n Hunger: {jellyStats.GetCurrentHunger()}\n Mood: {jellyStats.GetMoodLevel()}\n Sleepy: {jellyStats.GetSleepyLevel()}\n Love: {jellyStats.GetCurrentLove()}\n\n\n BG Jelly:\n PlayInterval: {bgJelly.GetCurrentPlayInterval()}\n WalkIn: {bgJelly.GetCurrentWalkInterval()}\n NextAnim: {bgJelly.GetJellyState()}";
        _content.text = values;
    }
}
