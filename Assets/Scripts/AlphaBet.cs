using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AlphaBet : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    // Start is called before the first frame update
    public Text Alpha;
    private char key;
    
    private StringBuilder toSendMsg;
    void Start()
    {
        toSendMsg = new StringBuilder();
        key = (char)(gameObject.name[0] - 'A' + 'a');
        Alpha = transform.Find("Alpha").GetComponent<Text>();
        Alpha.text = key.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CentralContorller.isCaps)
        {
            key = gameObject.name[0];
            Alpha.text = key.ToString();
        }
        else
        {
            key = (char)(gameObject.name[0] - 'A' + 'a');
            Alpha.text = key.ToString();
        }
    }

    public virtual void OnPointerDown (PointerEventData eventData)
    {
        toSendMsg.Clear();
        toSendMsg.Append("a,");
        toSendMsg.Append(key+",0");//0 = 按下，1 = 松开
        TCP.toSendMsg.Clear();
        TCP.toSendMsg.Append(toSendMsg);
        TCP.clickSend = true;
    }

    public virtual void OnPointerUp (PointerEventData eventData)
    {
        toSendMsg.Clear();
        toSendMsg.Append("a,");
        toSendMsg.Append(key+",1");//0 = 按下，1 = 松开
        TCP.toSendMsg.Clear();
        TCP.toSendMsg.Append(toSendMsg);
        TCP.clickSend = true;
    }
}
