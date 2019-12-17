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
    /// A prefab to place when a raycast from a user touch hits a feature point.
    /// </summary>
    public GameObject GameObjectPointPrefab;

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
    private PhysicsRaycaster raycast; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _UpdateApplicationLifecycle();

        //FindGround();

    }
    
    public void FindGround()
    {
        Session.GetTrackables<DetectedPlane>(NewPlanes, TrackableQueryFilter.All);
        if(NewPlanes.Count > 0 && !groundCreated)
        {
            virtualWorldAnchor =  NewPlanes[0].CreateAnchor(NewPlanes[0].CenterPose);
            //config.PlaneFindingMode = DetectedPlaneFindingMode.Disabled;
            virtualWorldRoot.transform.SetParent(virtualWorldAnchor.transform);
            Instantiate(ground, virtualWorldRoot.transform.position, Quaternion.identity); ;
            groundCreated = true;
        }
        else
        {
            return;
        }  
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
