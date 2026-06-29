using UnityEngine;
using UnityEngine.UI;

public class SkinSelectorUI : MonoBehaviour
{
    [Header("Skin button prefab")]
    public GameObject skinButtonPrefab;

    [Header("Container for skin buttons")]
    public Transform skinButtonsContainer;

    private Button[] buttons;
    private Image[] buttonImages;

    void Start()
    {
        if (SkinManager.Instance == null) return;

        Color[] colors = SkinManager.Instance.availableColors;
        buttons = new Button[colors.Length];
        buttonImages = new Image[colors.Length];

        for (int i = 0; i < colors.Length; i++)
        {
            int index = i;
            GameObject btn = Instantiate(skinButtonPrefab, skinButtonsContainer);

            buttonImages[i] = btn.GetComponent<Image>();
            if (buttonImages[i] != null)
                buttonImages[i].color = colors[i];

            buttons[i] = btn.GetComponent<Button>();
            if (buttons[i] != null)
                buttons[i].onClick.AddListener(() => OnSkinSelected(index));
        }

        HighlightSelected(SkinManager.Instance.selectedColorIndex);
    }

    void OnSkinSelected(int index)
    {
        SkinManager.Instance.SelectColor(index);
        HighlightSelected(index);
    }

    void HighlightSelected(int index)
    {
        for (int i = 0; i < buttonImages.Length; i++)
        {
            if (buttons[i] != null)
            {
                var outline = buttons[i].GetComponent<Outline>();
                if (outline == null && i == index)
                    outline = buttons[i].gameObject.AddComponent<Outline>();

                if (outline != null)
                {
                    outline.enabled = (i == index);
                    outline.effectColor = Color.white;
                    outline.effectDistance = new Vector2(4, 4);
                }
            }
        }
    }
}