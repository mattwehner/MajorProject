using UnityEngine;
using System.Collections;
using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using Assets.Scripts;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour { 
    internal IUiOwner Owner;

    public RawImage Tab;
    public Texture2D TabNormal;
    public Texture2D TabHover;

    void DestroySelf()
    {
        gameObject.GetComponent<Animator>().Play("Panel_Big_Out");
        Destroy(gameObject);
    }

    void Start()
    {
    }

    void OnTriggerEnter2D()
    {
        Tab.texture = TabHover;
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<ICursor>().IsGrabbing)
        {
            HandMotionController.Instance.SetCollider(false, null);
            UIController.Instance.CursorModeOn(false);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D()
    {
        Tab.texture = TabNormal;
    }
}
