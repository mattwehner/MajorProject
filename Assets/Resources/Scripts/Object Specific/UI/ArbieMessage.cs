using UnityEngine;
using System.Collections;

public class ArbieMessage : MonoBehaviour
{
    private GameObject _camera;

    void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        transform.LookAt(_camera.transform);
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }
}
