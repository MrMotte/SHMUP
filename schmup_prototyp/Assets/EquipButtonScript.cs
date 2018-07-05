using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipButtonScript : MonoBehaviour
{


    public int WeaponID;
	public int WeaponArrayID;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClicked()
    {

        var colors = GetComponent<Button>().colors;
        colors.normalColor = Color.red;
		colors.highlightedColor = Color.red;
        GetComponent<Button>().colors = colors;

        VariableSaver.WeaponIDTemp = WeaponID;
        VariableSaver.StoredButton = this.GetComponent<Button>();
        VariableSaver.StoredButtonText = this.GetComponentInChildren<Text>();

    }

    public void ShipWeaponButtonClicked()
    {
        GetComponentInChildren<Text>().text = VariableSaver.StoredButtonText.text;
        var colors = VariableSaver.StoredButton.colors;
        colors.normalColor = Color.white;
		colors.highlightedColor = Color.white;
        VariableSaver.StoredButton.colors = colors;

		VariableSaver.WeaponIDs[WeaponArrayID] = VariableSaver.WeaponIDTemp;
        print(VariableSaver.WeaponIDs[WeaponArrayID]);
    }
}
