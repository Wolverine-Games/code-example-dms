using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIData : MonoBehaviour
{
    public Text cashText;
    public Text gemsText;
    public InputField nameInput;
    public Button nameBtn;
    public Button buyCashBtn;
    public Button buyGemsBtn;
	public Text saveButtonText;
	public Text weaponText;
	public Text buyCashBtnText;
	public Text buyGemsBtnText;
	public Dropdown weaponDropDown;
	public Dropdown languageDropDown;

	private void Start()
	{
		if (cashText != null)
		{
			cashText.text = PersistentDataManager.GetCurrency(DataManager.DATA_CURRENCY_TYPE_CASH).ToString();
		}
		if (gemsText != null)
		{
			gemsText.text = PersistentDataManager.GetCurrency(DataManager.DATA_CURRENCY_TYPE_GEMS).ToString();
		}
		if (nameInput != null)
		{
			nameInput.text = PersistentDataManager.GetName();
		}
		if (weaponDropDown != null)
		{
			weaponDropDown.ClearOptions();
			//            weaponDropDown.AddListener(delegate { SelectWeapon(weaponDropDown.itemText); });
			List<WeaponData> curWeaponList = DataManager.Instance.GetWeapons();
			List<string> weaponNames = new List<string>();
			foreach (WeaponData weapon in curWeaponList)
			{
				weaponNames.Add(weapon.displayName);
			}
			weaponDropDown.AddOptions(weaponNames);
		}
		if (languageDropDown != null)
		{
			languageDropDown.ClearOptions();
			List<string> langNames = new List<string>();
			langNames.Add(DataManager.LOC_KEY_HEADER_LANGUAGE_ENGLISH);
			langNames.Add(DataManager.LOC_KEY_HEADER_LANGUAGE_FRENCH);
			langNames.Add(DataManager.LOC_KEY_HEADER_LANGUAGE_GERMAN);
			languageDropDown.AddOptions(langNames);
			languageDropDown.value = 0;
		}
	}
	public void ChangeName()
    {
        if (nameInput != null)
        {
            PersistentDataManager.UpdatePlayerData(nameInput.text);
        }
    }
    public void BuyCash(int purchaseAmount)
    {
        PersistentDataManager.ChangeCurrency(DataManager.DATA_CURRENCY_TYPE_CASH, purchaseAmount);
        cashText.text = PersistentDataManager.GetCurrency(DataManager.DATA_CURRENCY_TYPE_CASH).ToString();
    }
    public void BuyGems(int purchaseAmount)
    {
        PersistentDataManager.ChangeCurrency(DataManager.DATA_CURRENCY_TYPE_GEMS, purchaseAmount);
        gemsText.text = PersistentDataManager.GetCurrency(DataManager.DATA_CURRENCY_TYPE_GEMS).ToString();
    }

    public void SelectWeapon(Dropdown weaponDD)
    {
        string weaponSelectedDName = "";
        if (weaponDD != null && weaponDD.options != null && weaponDD.options.Count > weaponDD.value)
        {
            weaponSelectedDName = weaponDD.options[weaponDD.value].text;
            DataManager.Instance.SelectWeapon(weaponSelectedDName);
            Debug.Log("Selected Weapon: " + weaponSelectedDName);

            //TODO: need to look up the weapon's id by the name and assign to the player record
        }
    }
	public void SelectLanguage(Dropdown languageDD)
	{
		string languageSelected = "";
		if (languageDD != null && languageDD.options != null && languageDD.options.Count > languageDD.value)
		{
			languageSelected = languageDD.options[languageDD.value].text;
			DataManager.currentLanguage = languageSelected;
			if (saveButtonText != null)
			{
				saveButtonText.text = DataManager.GetTranslation(DataManager.LOC_KEY_BUTTON_SAVE_NAME, languageSelected);
			}
			if (buyCashBtnText != null)
			{
				buyCashBtnText.text = DataManager.GetTranslation(DataManager.LOC_KEY_BUTTON_BUY_CASH_5, languageSelected);
			}
			if (buyGemsBtnText != null)
			{
				buyGemsBtnText.text = DataManager.GetTranslation(DataManager.LOC_KEY_BUTTON_BUY_GEMS_1, languageSelected);
			}
			if (weaponText != null)
			{
				weaponText.text = DataManager.GetTranslation(DataManager.LOC_KEY_LABEL_CURRENT_WEAPON, languageSelected);
			}

			Debug.Log("Selected Language: " + languageSelected);
		}
	}
}
