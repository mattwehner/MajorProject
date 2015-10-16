using System;
using System.Collections;
using Assets.Scripts.Object_Specific;
using UnityEngine;
using UnityEngine.Events;

public class PowerCellSlot : MonoBehaviour, IPowerer
{
    [Serializable]
    public class PowerOnAction : UnityEvent { }
    public PowerOnAction OnActivation;

    public GameObject Light;
    public bool _isCell;
    public bool PowerOn { get; set; }
    private IPowered _powered;

    void Awake()
    {
        _isCell = false;
    }

    void Update()
    {
        Light.SetActive(PowerOn);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Powercell")
        {
            _isCell = true;
            _powered = collider.GetComponent<IPowered>();
            StartCoroutine(DelayPowerOn());
        }
        
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Powercell")
        {
            PowerOn = _isCell;
            _powered.PoweredOn = PowerOn;
        }
    }

    void OnTriggerExit()
    {
        if (_isCell)
        {
            PowerOn = false;
            _powered.PoweredOn = PowerOn;
        }
    }

    IEnumerator DelayPowerOn()
    {
        yield return new WaitForSeconds(2);
        if (PowerOn)
        {
            OnActivation.Invoke();
        }
    }
}
