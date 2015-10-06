using System;
using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using Assets.Resources.Scripts.Storage;
using Assets.Scripts;
using Assets.Scripts.Object_Specific;
using UnityEngine;

public class Terminal : MonoBehaviour, IInteractable, IPowered, IUiOwner
{
    public bool PowerOnOveride;
    public GameObject PoweredBy;
    public int TerminalType;
    public string TerminalUI;

    public GameObject InteractionBounds { get; set; }
    public bool IsActive { get; set; }
    public bool PoweredOn { get; set; }

    private GameObject _uiPanel;
    private IPowerer _iPowerer;
    private MeshRenderer _material;
    private Material _powerOn;
    private Material _powerOff;

    void Awake()
    {
        _material = GetComponent<MeshRenderer>();
        PoweredBy = (PoweredBy)
            ? PoweredBy
            : gameObject;
        _iPowerer = PoweredBy.GetComponent<IPowerer>();

        InteractionBounds = transform.FindChild("InteractiveBox").gameObject;
        InteractionBounds.SetActive(false);

        switch (TerminalType)
        {
            case 1:
                _powerOff = MaterialReferences.Instance.TermainalSmallOff;
                _powerOn = MaterialReferences.Instance.TermainalSmallOn;
                break;
            case 2:
                _powerOff = MaterialReferences.Instance.TerminalMediumOff;
                _powerOn = MaterialReferences.Instance.TerminalMediumOn;
                break;
            case 3:
                _powerOff = MaterialReferences.Instance.TerminalLargeOff;
                _powerOn = MaterialReferences.Instance.TerminalLargeOn;
                break;
        }
    }

    void Update()
    {
        PoweredOn = (_iPowerer == null)
                ? PowerOnOveride
                : _iPowerer.PowerOn;

        _material.material = (PoweredOn) ? _powerOn : _powerOff;
    }

    public void OnTriggerStay(Collider collider)
    {
        if (collider.name.StartsWith("bone"))
        {
            HandMotionController.Instance.SetCollider(true, gameObject);
            InteractionBounds.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.name.StartsWith("bone"))
        {
            HandMotionController.Instance.SetCollider(false, null);
        }
        InteractionBounds.SetActive(false);
    }

    public void Activate()
    {
        if (!_uiPanel)
        {
            _uiPanel = Instantiate(UnityEngine.Resources.Load("Prefabs/UI/" + TerminalUI)) as GameObject; ;
            _uiPanel.transform.SetParent(UIController.Instance.gameObject.transform, false);
            _uiPanel.transform.SetAsFirstSibling();
            _uiPanel.GetComponent<UIPanel>().Owner = gameObject.GetComponent<IUiOwner>();
            InteractionBounds.SetActive(false);
        }
    }

    public void OnUiButtonPress(string pressed)
    {
        throw new NotImplementedException();
    }
}
