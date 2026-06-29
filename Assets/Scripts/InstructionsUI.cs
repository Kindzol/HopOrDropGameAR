using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionsUI : MonoBehaviour
{
    public GameObject instructionsPanel;
    public Button continueButton;

    void Start()
    {
        instructionsPanel.SetActive(false);
        continueButton.onClick.AddListener(OnContinueClicked);
    }

    public void Show()
    {
        instructionsPanel.SetActive(true);
    }

    void OnContinueClicked()
    {
        instructionsPanel.SetActive(false);
    }
}