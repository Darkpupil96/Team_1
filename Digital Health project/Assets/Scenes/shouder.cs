using UnityEngine;
using UnityEngine.UI;

public class shouder : MonoBehaviour
{
    // Start is called before the first frame update
public CanvasGroup popupWindow; // Drag the Canvas Group component of the PopupWindow here in the Inspector

    public void OnMouseDown()
    {
        ShowPopup();
    }

    private void ShowPopup()
    {
        popupWindow.alpha = 1f; // Make the popup visible
        popupWindow.interactable = true;
        popupWindow.blocksRaycasts = true;
    }
}
