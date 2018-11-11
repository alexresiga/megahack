using GoogleARCore.Examples.HelloAR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour
{
    private List<string> phones;

    private HelloARController controller;
    public Dropdown dropdown;

    public void Dropdown_IndexChanged(int index)
    {
        Debug.Log("DROPDOWN");
        controller.AddToPhoneList(phones[index-1]);
    }

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<HelloARController>();
        PopulateList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PopulateList()
    {
        phones = new List<string>() { "Apple IPhone 6", "Apple IPhone 7", "Apple IPhone X", "ASUS ZenFone", "Huawei P Smart", "Huawei P20 Lite", "LG G6", "Samsung Galaxy J4", "Samsung Galaxy S9" };
        dropdown.AddOptions(phones);
    }
}
