using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Leap;

internal class WorldManager : MonoBehaviour {
	Controller controller;
	public Text handModeDisplay;
	public GameObject camera;

	private string handMode;
	private string previousHandMode;
	private int previousExtendedCount;

	private object previousHandPositionX;
	private object previousHandPositionY;

	// Use this for initialization

	void Start () {
		controller = new Controller();
		previousExtendedCount = 0;
		handModeDisplay.text = "";
	}

	void Update () {
		Frame frame = controller.Frame();
		Hand hand = frame.Hands[0];

		if (frame.Hands.Count == 0) {
			handModeDisplay.text = "Place your hand into the scene";
			return;
		}

		HandModeCalculator (hand);

		if (previousHandMode != handMode) {
			handModeDisplay.text = "You are " + handMode;
			Debug.Log ("Hand Mode is " + handMode);
			previousHandMode = handMode;
		}

		if (handMode == "camera") {
			cameraController(hand);
		}


	}

	void HandModeCalculator (Hand hand){
		FingerList fingers = hand.Fingers;
		FingerList extendedFingers = fingers.Extended ();

		FingerList indexFinger = fingers.FingerType(Finger.FingerType.TYPE_INDEX);
		FingerList thumb = fingers.FingerType(Finger.FingerType.TYPE_THUMB);
		FingerList pinkyFinger = fingers.FingerType(Finger.FingerType.TYPE_PINKY);

		bool isPointing = extendedFingers.Count == 1 &&
			extendedFingers [0].Equals (indexFinger [0]);

		bool isCamera = extendedFingers.Count == 2 &&
			extendedFingers [0].Equals (thumb[0]) &&
				extendedFingers [1].Equals (pinkyFinger[0]);

		if (extendedFingers.Count != previousExtendedCount) {
			Debug.Log ("Extended Fingers Count: " + extendedFingers.Count);
			previousExtendedCount = extendedFingers.Count;
		}

		if (isPointing) {
			handMode = "pointing";
			return;
		}
		if(isCamera){
			handMode = "camera";
			return;
		}
			handMode = "grabbing";
	}

	void cameraController(Hand hand){
		var moveX = 1;
		var moveY = 1;
		Vector handPosition = hand.PalmPosition;
		camera.transform.Translate(moveX,moveY, 0);
	}
}
