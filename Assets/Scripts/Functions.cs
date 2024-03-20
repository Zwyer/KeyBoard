using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class Functions : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    // Start is called before the first frame update
    private string header = "f,";
    private StringBuilder sb;
    private int id;
    private int CapsdownCount;

    private bool isDown;
    
    void Start()
    {
        id = idChange(gameObject.name);
        sb = new StringBuilder();
        isDown = false;
    }

    int idChange(string name)
    {
        int _id = -1;
        if (name == "CapsLock")
        {
            _id = 0;
        }else if (name == "LShift")
        {
            _id = 6;
        }else if (name == "Tab")
        {
            _id = 4;
        }else if (name == "RShift")
        {
            _id = 14;
        }
        return _id;
    }
    // Update is called once per frame
    void Update()
    {
           
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        sb.Clear();
        sb.Append(header);
        char t = (char)('a' + id);
        sb.Append(t + ",0");
        TCP.toSendMsg = sb;
        TCP.clickSend = true;
        isDown = true;
        if (id == 6 || id == 14)//shift
        {
            CentralContorller.ShiftDown = true;
            CentralContorller.isCaps = !CentralContorller.isCaps;
        }else if (id == 0)//capslock
        {
            CapsdownCount++;
            CentralContorller.isCaps = (CapsdownCount & 1) == 1;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        sb.Clear();
        sb.Append(header);
        char t = (char)('a' + id);
        sb.Append(t + ",1");
        TCP.toSendMsg = sb;
        TCP.clickSend = true;
        isDown = false;
        if (id == 6 || id == 14)//shift
        {
            CentralContorller.ShiftDown = false;
            CentralContorller.isCaps = !CentralContorller.isCaps;
        }
    }
    
}
