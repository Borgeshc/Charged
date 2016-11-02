﻿using UnityEngine;
using System.Collections;

public class TabSwitcher : MonoBehaviour {

    TabWrangler tabWrangler;


    void Start()
    {
        tabWrangler = GetComponentInParent<TabWrangler>();
    }

    public void ActivateTab(int index)
    {
        foreach (GameObject tab in tabWrangler.tabs)
        {
            tab.SetActive(false);
        }

        tabWrangler.tabs[index].SetActive(true);
    }

    public void ActivateTab(string name)
    {
        foreach (GameObject tab in tabWrangler.tabs)
        {
            if (tab.name == name)
                tab.SetActive(true);
            else
                tab.SetActive(false);
        }
    }
}
