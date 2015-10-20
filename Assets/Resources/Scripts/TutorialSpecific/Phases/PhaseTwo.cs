using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Scripts.TutorialSpecific;
using UnityEngine.UI;

public class PhaseTwo : MonoBehaviour
{
    public GameObject Player;
    public GameObject Line;
    public Animator Door;
    public RawImage HandAnimation;
    public int ActivatedTerminals;

    public bool AnimateDoor;

    private float _switchTime;
    private TutorialCamera _tutorialCamera;

    void Awake()
    {
        Line.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () {
	    if (Player.transform.localPosition.x > 28f && Player.transform.localPosition.x < 40f)
	    {
	        var position = Player.transform.localPosition.x;
	        var newCap = Mathf.Clamp(1 -(position - 28)/13, 0, 1);
            print(newCap);
	        TutorialCamera.Instance.CameraBounds.w = newCap;
	    }
	    if (ActivatedTerminals == 3)
	    {
	        Door.enabled = true;
            Line.SetActive(true);
	        var saveItems = new List<Transform> {gameObject.transform};
            TutorialController.Instance.NextPhase();
            TutorialController.Instance.ClearPreviousPhase(saveItems);
            TutorialCamera.Instance.CameraBounds.y = 60f;
        }
    }
}
