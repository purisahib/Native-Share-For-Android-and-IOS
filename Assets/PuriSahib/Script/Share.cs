using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuriSahib.NativeShareAndroid_IOS;
using UnityEngine.UI;

namespace PuriSahib.ShareExample
{    
    public class Share : MonoBehaviour
    {
        [Header("Input Field"), Tooltip("Input Field it for contain text and link...")]
        [SerializeField] private InputField header_IF;
        [SerializeField] private InputField subject_IF;
        [SerializeField] private InputField message_IF;

        [Header("Send Button"), Tooltip("Send The Message sor Add Send Btn...")]
        [SerializeField] private Button send_Btn;

        [Header("External Script"), Tooltip("You Use The External script that is Share data script...")]
        [SerializeField] private ShareData share;
        void Start()
        {
            share = gameObject.GetComponent<ShareData>();
            send_Btn.onClick.AddListener(Send_Btn_Fun);
        }

        private void Send_Btn_Fun()
        {
            share.Share(header_IF.text, subject_IF.text, message_IF.text);
        }
    }
}


