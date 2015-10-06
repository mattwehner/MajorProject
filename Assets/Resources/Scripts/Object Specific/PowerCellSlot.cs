using UnityEngine;
using System.Collections;
using Assets.Scripts.Object_Specific;

public class PowerCellSlot : MonoBehaviour
{
    public bool _isCell;

    void Awake()
    {
        _isCell = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "cell")
        {
            _isCell = true;
            PoweredOn = true;
            print(name + "was turned on");
            return;
        }
        _isCell = false;
    }

    void OnTriggerStay()
    {
        PoweredOn = _isCell;
    }

    void OnTriggerExit()
    {
        if (_isCell)
        {
            PoweredOn = false;
            print(name + "was turned off");
        }
    }

    public bool PoweredOn { get; set; }
}
