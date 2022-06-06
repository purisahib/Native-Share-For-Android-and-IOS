using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace PuriSahib.NativeShareAndroid_IOS
{
    public class ShareData : MonoBehaviour
    {
        private bool isFocus = false;
        public void Share(string header, string subject, string message)
        {
#if UNITY_ANDROID
            StartCoroutine(ShareTextInAnroid(header, subject, message));
#elif UNITY_IOS
            CallSocialShare(subject, message);
#else
            Debug.Log("No sharing set up for this platform.");
#endif
        }
        public void ShareAdvansad(string header, string subject, string iosUrl, string message)
        {
#if UNITY_IOS
            CallSocialShareAdvanced(header, subject, iosUrl, message);
#else
            Debug.Log("No sharing set up for this platform.");
#endif
        }


        // Share Option
        void OnApplicationFocus(bool focus)
        {
            isFocus = focus;
        }
        private IEnumerator ShareTextInAnroid(string header, string subject, string message)
        {

        if (!Application.isEditor)
        {
#if UNITY_ANDROID
            //Create intent for action send
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //put text and subject extra
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), message);

            //call createChooser method of activity class
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, header);
            currentActivity.Call("startActivity", chooser);
#endif
        }

        yield return new WaitUntil(() => isFocus);

        }
#if UNITY_IOS
    public struct ConfigStruct
    {
	    public string title;
	    public string message;
    }

    [DllImport ("__Internal")] private static extern void showAlertMessage(ref ConfigStruct conf);
	
    public struct SocialSharingStruct
    {
	    public string text;
	    public string url;
	    public string image;
	    public string subject;
    }
	
    [DllImport ("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);
	
    public static void CallSocialShare(string title, string message)
    {
	    ConfigStruct conf = new ConfigStruct();
	    conf.title  = title;
	    conf.message = message;
	    showAlertMessage(ref conf);
    }

    public static void CallSocialShareAdvanced(string defaultTxt, string subject, string url, string img)
    {
	    SocialSharingStruct conf = new SocialSharingStruct();
	    conf.text = defaultTxt; 
	    conf.url = url;
	    conf.image = img;
	    conf.subject = subject;
		
	    showSocialSharing(ref conf);
    }
#endif
    }
}



