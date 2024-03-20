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
    private int downCount;
    
    void Start()
    {
        id = idChange(gameObject.name);
        sb = new StringBuilder();
        
    }

    int idChange(string name)
    {
        int _id = -1;
        if (name == "CapsLock")
        {
            _id = 0;
        }
        return _id;
    }
    // Update is called once per frame
    void Update()
    {
        if (id == 0)
        {
            CentralContorller.isCaps = (downCount & 1) == 1?true:false;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        sb.Clear();
        sb.Append(header);
        switch (id)
        {
            case 0:
                sb.Append("a,0");
                downCount++;
                TCP.toSendMsg = sb;
                TCP.clickSend = true;
                break;
            
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        sb.Clear();
        sb.Append(header);
        switch (id)
        {
            case 0:
                sb.Append("a,1");
                TCP.toSendMsg = sb;
                TCP.clickSend = true;
                break;
            
        }
    }
    
}
