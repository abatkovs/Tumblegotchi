using _MyAssets.Scripts.Jelly;
using UnityEngine;

public class SelectTamagotchi : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ShellData shellData;
    [SerializeField] private JellyStats jelly; //Reference to scene jelly
    [SerializeField] private JellyEvolutionData evolutionData; //Evolution path to take

    [Space(25)]
    [SerializeField] private SpriteRenderer shellSprite;
    [SerializeField] private SpriteRenderer screenSprite;

    [Space()]
    [SerializeField] private GameObject selectionScene;
    [SerializeField] private GameObject gameScene;


    private void Start()
    {
        button.OnButtonClicked += Button_OnButtonClicked;
    }

    private void Button_OnButtonClicked()
    {
        shellSprite.sprite = shellData.ShellSprite;
        screenSprite.color = shellData.ScreenColor;

        SetJellyData();
        SetSceneStatus();
    }

    private void SetSceneStatus()
    {
        selectionScene.SetActive(false);
        gameScene.SetActive(true);
    }

    private void SetJellyData()
    {
        jelly.ChangeEvolutionData(evolutionData);
    }
}
