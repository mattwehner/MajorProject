using UnityEngine;
using System.Collections;
using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.TutorialSpecific;

public class RespawnObject : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Arbie")
        {
            if (Application.loadedLevelName == "Tutorial")
            {
                Destroy(collider.gameObject);
                var arbie = Instantiate(UnityEngine.Resources.Load("Prefabs/Arbie/Arbie")) as GameObject;
                arbie.name = "Arbie";
                var phase = TutorialController.Instance.CurrentPhase;
                switch (phase)
                {
                    case 1:
                        arbie.transform.position = new Vector3(38.98f, -2.44f, 2.23f);
                        return;
                    case 2:
                        arbie.transform.position = new Vector3(47.45f, -2.44f, 2.23f);
                        return;
                    case 3:
                        arbie.transform.position = new Vector3(60, -11.45f, 1.939f);
                        return;
                }
            }
            GameController.Instance.SpawnArbie();
        }
        if (collider.tag == "Powercell")
        {
            Destroy(collider.gameObject);
            var powercell = Instantiate(UnityEngine.Resources.Load("Prefabs/LevelPieces/Powercell")) as GameObject;
            powercell.name = "Powercell";
powercell.transform.position = new Vector3(69.764f, -1.396047f, 1.446029f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            RespawnObjects();
        }
    }

    private void RespawnObjects()
    {
        Destroy(GameObject.FindGameObjectWithTag("Powercell"));
        var powercell = Instantiate(UnityEngine.Resources.Load("Prefabs/LevelPieces/Powercell")) as GameObject;
        powercell.name = "Powercell";
        powercell.transform.position = new Vector3(69.764f, -1.396047f, 1.446029f);

        Destroy(GameObject.FindGameObjectWithTag("Arbie"));
        var arbie = Instantiate(UnityEngine.Resources.Load("Prefabs/Arbie/Arbie")) as GameObject;
        arbie.name = "Arbie";
        var phase = TutorialController.Instance.CurrentPhase;
        switch (phase)
        {
            case 1:
                arbie.transform.position = new Vector3(38.98f, -2.44f, 2.23f);
                return;
            case 2:
                arbie.transform.position = new Vector3(47.45f, -2.44f, 2.23f);
                return;
            case 3:
                arbie.transform.position = new Vector3(60, -2.44f, 2.23f);
                return;
        }
    }
}
