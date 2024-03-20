using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject SetPanel;
    public GameObject BackButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBackButtonClick()
    {
        SetPanel.SetActive(false);
    }

    public void OnOpenSetting()
    {
        SetPanel.SetActive(true);
    }
}
