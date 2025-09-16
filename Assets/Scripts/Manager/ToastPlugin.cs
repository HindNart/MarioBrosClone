using UnityEngine;

public class ToastPlugin : MonoBehaviour
{
    AndroidJavaObject toastObject;

    public void ShowToast(string message)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                var context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                using (var toast = new AndroidJavaObject("android.widget.Toast", context))
                {
                    toast.CallStatic<AndroidJavaObject>("makeText", context, message, 0)
                         .Call("show");
                }
            }
        }
    }
}
