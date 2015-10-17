using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using Assets.Scenes.Slides.Resources;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    internal IUiOwner Owner;
    public RawImage Tab;
    public Texture2D TabHover;
    public Texture2D TabNormal;

    private void DestroySelf()
    {
        gameObject.GetComponent<Animator>().Play("Panel_Big_Out");
        Destroy(gameObject);
    }

    private void Start()
    {
    }

    private void OnTriggerEnter2D()
    {
        Tab.texture = TabHover;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<ICursor>().IsGrabbing)
        {
            if (Application.loadedLevelName.Contains("slide"))
            {
                var sg = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectGesture>();
                sg.SetCollider(false, null);
                UIController.Instance.CursorModeOn(false);
                Destroy(gameObject);
                return;
            }
            HandMotionController.Instance.SetCollider(false, null);
            UIController.Instance.CursorModeOn(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D()
    {
        Tab.texture = TabNormal;
    }
}