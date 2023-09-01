using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// script reference by - Zee Vasilyev - https://www.youtube.com/watch?v=H2qMxWcO9dg&list=PLFY3TFPG0dkomvWyb2fcPeqDSSvIO0NYb&index=13
// script edited by Oliver lancashire
// sid 1901981
[RequireComponent(typeof(Dropdown))]
public class AchievementDropdownController : MonoBehaviour {
    // UI
    private Dropdown m_dropdown;
    private Dropdown Dropdown {
        get {
            if (m_dropdown == null)
            {
                m_dropdown = GetComponent<Dropdown>();
            }
            return m_dropdown;
        }
    }
    // action
    public Action<AchievementID> onValueChanged;

    private void Start()
    {
        UpdateOptions(); // update options
        Dropdown.onValueChanged.AddListener(HandleDropdownValueChanged); // update value
    }

    [ContextMenu("UpdateOptions()")]
    // function option update
    public void UpdateOptions()
    {
        Dropdown.options.Clear();
        var ids = Enum.GetValues(typeof(AchievementID));
        foreach (AchievementID id in ids)
        {
            Dropdown.options.Add(new Dropdown.OptionData(id.ToString()));
        }
        Dropdown.RefreshShownValue();
    }
    // chnage values
    private void HandleDropdownValueChanged(int value)
    {
        if (onValueChanged != null)
        {
            onValueChanged((AchievementID)value);
        }
    }

}
