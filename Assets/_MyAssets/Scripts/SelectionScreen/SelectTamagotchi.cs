using UnityEngine;

public class SelectTamagotchi : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        button.OnButtonClicked += Button_OnButtonClicked;
    }

    private void Button_OnButtonClicked()
    {
        throw new System.NotImplementedException();
    }
}
