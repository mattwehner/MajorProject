using UnityEngine;
using System.Collections;
using Leap;

public class cameraController : MonoBehaviour {
	Controller controller;
	private string handMode;
	public int left;
	public int height;
	public int multiplyFactor;

	// Use this for initialization
	void Start () {
		controller = new Controller ();
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame();
		Hand hand = frame.Hands[0];

		InteractionBox interactionBox = frame.InteractionBox;
		Vector handPosition = interactionBox.NormalizePoint(hand.PalmPosition);

		FingerList fingers = hand.Fingers;
		FingerList extendedFingers = fingers.Extended ();
		FingerList thumb = fingers.FingerType(Finger.FingerType.TYPE_THUMB);
		FingerList pinkyFinger = fingers.FingerType(Finger.FingerType.TYPE_PINKY);
		bool isCamera = extendedFingers.Count == 2 &&
			extendedFingers [0].Equals (thumb[0]) &&
				extendedFingers [1].Equals (pinkyFinger[0]);
		
		if (isCamera) {
			Debug.Log("camera position: " + handPosition);
		}
	}
}
