using UnityEngine;
using System.Collections;
using Leap;

public class cameraController : MonoBehaviour {
	Controller controller;
	private string handMode;
	public float cameraSpeed;

	// Use this for initialization
	void Start () {
		controller = new Controller ();
		cameraSpeed = 0.03f;
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame();
		Hand hand = frame.Hands[0];

		InteractionBox interactionBox = frame.InteractionBox;
		Vector handPosition = interactionBox.NormalizePoint(hand.StabilizedPalmPosition);

		FingerList fingers = hand.Fingers;
		FingerList extendedFingers = fingers.Extended ();
		FingerList thumb = fingers.FingerType(Finger.FingerType.TYPE_THUMB);
		FingerList pinkyFinger = fingers.FingerType(Finger.FingerType.TYPE_PINKY);
		bool isCamera = extendedFingers.Count == 2 &&
			extendedFingers [0].Equals (thumb[0]) &&
				extendedFingers [1].Equals (pinkyFinger[0]);
		
		if (isCamera) {
			var cameraPosition = transform.position;
			var moveX = (handPosition.x <= 0.5)? -cameraSpeed : cameraSpeed;
			var moveY = (handPosition.y <= 0.75)? -cameraSpeed : cameraSpeed;



			transform.Translate(moveX,moveY, 0);

			//x: 0, 0.5, 1
			//y: is 0.5, 0.75, 1
			Debug.Log("camera position X: " + handPosition.x + " camera position y: " + handPosition.y);
			//Debug.Log("camera position: " + handPosition);
		}
	}
}