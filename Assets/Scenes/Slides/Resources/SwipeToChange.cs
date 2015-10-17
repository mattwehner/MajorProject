using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using Leap;

public class SwipeToChange : MonoBehaviour
{
    private int ForwardScene;
    private int BackwardScene;
    public int WaitFor;

    private Controller _controller;
    private bool swiped;

    void Awake()
    {
        _controller = new Controller();
        _controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
        swiped = true;
        ForwardScene = (Application.loadedLevel) + 1;
        BackwardScene = (Application.loadedLevel) - 1;
        WaitFor = (WaitFor == 0) ? 10 : WaitFor;
    }

    void Start()
    {
            StartCoroutine(WaitForSceneStart());
    }

    void Update()
    {
        Gesture gesture = _controller.Frame().Gestures()[0];
        if (gesture.Type == Gesture.GestureType.TYPESWIPE && !swiped)
        {
            var sd = new SwipeGesture(gesture).Direction;
            bool swipedLeft = (sd.x < -0.3f);
            bool swipedRight = (sd.x > 0.3f);

            if (swipedRight)
            {
                Application.LoadLevel(ForwardScene);
            }
            else if (swipedLeft)
            {
                Application.LoadLevel(BackwardScene);
            }
        }
    }

    IEnumerator WaitForSceneStart()
    {
        yield return new WaitForSeconds(WaitFor);
        swiped = false;
    }
}
