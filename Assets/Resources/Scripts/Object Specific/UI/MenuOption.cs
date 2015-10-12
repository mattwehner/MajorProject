using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour, IUiButton
{
    private RawImage image;

    void Awake()
    {
        image =
            GetComponent<RawImage>();
    }

    public void OnTriggerEnter2D()
    {
        image.color = Color.cyan;
        transform.localPosition = new Vector2((transform.localPosition.x + 35f), transform.localPosition.y);
    }

    public void OnTriggerExit2D()
    {
        image.color = new Color32(219, 165, 16, 255);
        transform.localPosition = new Vector2((transform.localPosition.x - 35f), transform.localPosition.y);
    }

    public void ButtonAction()
    {
        transform.localPosition = new Vector2((transform.localPosition.x - 35f), transform.localPosition.y);
        image.color = new Color32(219, 165, 16, 255);
        UIController.Instance.CursorModeOn(false);
    }
}
