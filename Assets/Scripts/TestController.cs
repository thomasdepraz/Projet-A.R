using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestController : MonoBehaviour
{



    public Camera FirstPersonCamera;

    /// <summary>
    /// A prefab to place when a raycast from a user touch hits a vertical plane.
    /// </summary>
    public GameObject GameObjectVerticalPlanePrefab;

    /// <summary>
    /// A prefab to place when a raycast from a user touch hits a horizontal plane.
    /// </summary>
    public GameObject GameObjectHorizontalPlanePrefab;


    /// <summary>
    /// The rotation in degrees need to apply to prefab when it is placed.
    /// </summary>
    private const float k_PrefabRotation = 180.0f;

    /// <summary>
    /// True if the app is in the process of quitting due to an ARCore connection error,
    /// otherwise false.
    /// </summary>
    private bool m_IsQuitting = false;

    private List<DetectedPlane> NewPlanes = new List<DetectedPlane>();

    public GameObject ground;
    public ARCoreSessionConfig config;
    private bool groundCreated = false;
    public GameObject virtualWorldRoot;
    private Anchor virtualWorldAnchor; 
    private TrackableHit hitResult;
    private TrackableHitFlags filter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _UpdateApplicationLifecycle();

        FindGround();
        if(groundCreated)
        {
            StartCoroutine(CreateBatteries());
        }
        
    }
    
    public void FindGround()
    {
        Session.GetTrackables<DetectedPlane>(NewPlanes, TrackableQueryFilter.All);
        if(!groundCreated)
        {
            virtualWorldAnchor =  NewPlanes[0].CreateAnchor(NewPlanes[0].CenterPose);
            virtualWorldRoot.transform.SetParent(virtualWorldAnchor.transform);
            groundCreated = true;
        }
        else
        {
            return;
        }  
    }

    IEnumerator CreateBatteries()
    {
         TrackableHit hitResult;
         TrackableHitFlags filter = TrackableHitFlags.PlaneWithinBounds;
        if(Frame.Raycast(FirstPersonCamera.transform.position, FirstPersonCamera.transform.forward, out hitResult,100f, filter))
        {
            if(Random.Range(0f, 1f) > 0.3 && hitResult.Trackable is DetectedPlane)
            {
                GameObject battery;
                if (hitResult.Trackable is DetectedPlane)
                {
                    DetectedPlane detectedPlane = hitResult.Trackable as DetectedPlane;
                    if (detectedPlane.PlaneType == DetectedPlaneType.Vertical)
                    {
                        battery = GameObjectVerticalPlanePrefab;
                    }
                    else
                    {
                        battery = GameObjectHorizontalPlanePrefab;
                    }
                }
                else
                {
                    battery = GameObjectHorizontalPlanePrefab;
                }

                // Instantiate prefab at the hit pose.
                var gameObject = Instantiate(battery, hitResult.Pose.position, hitResult.Pose.rotation);

                // Compensate for the hitPose rotation facing away from the raycast (i.e.
                // camera).
                gameObject.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);

                // Create an anchor to allow ARCore to track the hitpoint as understanding of
                // the physical world evolves.
                var anchor = hitResult.Trackable.CreateAnchor(hitResult.Pose);

                // Make game object a child of the anchor.
                gameObject.transform.parent = anchor.transform;
            }
        }
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(CreateBatteries());
    }
    #region premade_methods
    private void _UpdateApplicationLifecycle()
    {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Only allow the screen to sleep when not tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        if (m_IsQuitting)
        {
            return;
        }

        // Quit if ARCore was unable to connect and give Unity some time for the toast to
        // appear.
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            _ShowAndroidToastMessage("Camera permission is needed to run this application.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
        else if (Session.Status.IsError())
        {
            _ShowAndroidToastMessage(
                "ARCore encountered a problem connecting.  Please start the app again.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
    }

    private void _DoQuit()
    {
        Application.Quit();
    }
    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity =
            unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject =
                    toastClass.CallStatic<AndroidJavaObject>(
                        "makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
    #endregion 
}
