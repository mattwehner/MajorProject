using UnityEngine;
using System.Collections;
using Leap;

public class SwipeTest : MonoBehaviour
{
    private Controller _controller;
	// Use this for initialization
	void Start ()
	{
	    _controller = new Controller();
        _controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    GestureList gestureList = _controller.Frame().Gestures();
	        Gesture gesture = gestureList[0];
	        if (gesture.Type == Gesture.GestureType.TYPESCREENTAP)
	        {
	            ScreenTapGesture screenTap = new ScreenTapGesture(gesture);
                Debug.Log("Screen Tap");
	        }
	}
}
