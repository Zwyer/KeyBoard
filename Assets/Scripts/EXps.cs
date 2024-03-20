using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EXps : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    // Start is called before the first frame update
    public Text Upper, Downer;
    private char key;
    private string header;
    private string name;
    private StringBuilder sb;
    
    void Start()
    {
        header = "x,";
        name = gameObject.name;
        key = name[1];
        Upper.text = name[0].ToString();
        Downer.text = name[1].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (CentralContorller.ShiftDown)
        {
            Upper.color = Color.white;
            Downer.color = new Color(0.3f,0.3f,0.3f,0.5f);
        }
        else
        {
            Upper.color = new Color(0.3f,0.3f,0.3f,0.5f);
            Downer.color = Color.white;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        sb.Clear();
        sb.Append(header);
        sb.Append(key + ",0");
        TCP.toSendMsg = sb;
        TCP.clickSend = true;
    }
    
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        sb.Clear();
        sb.Append(header);
        sb.Append(key + ",0");
        TCP.toSendMsg = sb;
        TCP.clickSend = true;
    }
}
