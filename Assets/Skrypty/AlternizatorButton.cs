using UnityEngine;
using UnityEngine.UI;

public class AlternizatorButton : MonoBehaviour
{
    public string elementName;
    public Color selectedColor = Color.green;
    private Color normalColor;
    private Image buttonImage;
    private bool isSelected = false;
    private AlternizatorLogic logic;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        normalColor = buttonImage.color;
        logic = FindObjectOfType<AlternizatorLogic>();
    }

    public void OnButtonClick()
    {
        isSelected = !isSelected; // Prze³¹czamy stan

        if (isSelected)
        {
            buttonImage.color = selectedColor;
            logic.AddElement(elementName);
        }
        else
        {
            buttonImage.color = normalColor;
            logic.RemoveElement(elementName);
        }
    }

    public void ResetButton()
    {
        isSelected = false; // To pozwala na ponowne klikniêcie od zera
        if (buttonImage != null) buttonImage.color = normalColor;
    }
}
