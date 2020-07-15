using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITestData : MonoBehaviour
{
	public Text nameText;
	public Text cashText;
    public Text gemsText;
    public InputField nameInput;
	public Button nameBtn;
	public Button ResetBtn;
	public Button buyCashBtn;
    public Button buyGemsBtn;
	public Text saveButtonText;
	public Text weaponText;
	public Text enemyText;
	public Text buyCashBtnText;
	public Text buyGemsBtnText;
	public Dropdown weaponDropDown;
	public Dropdown enemyDropDown;
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
		string curWeaponId = PersistentDataManager.GetCurrentWeaponId();
		LoadDropDown<WeaponData>(weaponDropDown, curWeaponId);
		string curEnemyId = PersistentDataManager.GetFavoredEnemyId();
		LoadDropDown<EnemyData>(enemyDropDown, curEnemyId);
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

	private void LoadDropDown<T>(Dropdown curDropdown, string currentDataId) where T : BaseStaticData
	{
		if (curDropdown != null)
		{
			curDropdown.ClearOptions();
			List<T> curList = StaticDataManager.GetDataList<T>();
			List<string> dNames = new List<string>();
			int index = 0;
			int selIndex = 0;
			foreach (T item in curList)
			{
				dNames.Add(item.displayName);
				if (currentDataId != "" && currentDataId == item.id)
				{
					selIndex = index;
				}
				index++;
			}
			curDropdown.AddOptions(dNames);
			curDropdown.value = selIndex;
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
			Debug.Log("Selected Weapon: " + weaponSelectedDName);
			DataManager.Instance.SelectWeapon(weaponSelectedDName);
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
			if (nameText != null)
			{
				nameText.text = DataManager.GetTranslation(DataManager.LOC_KEY_LABEL_PLAYER_NAME, languageSelected);
			}
			if (weaponText != null)
			{
				weaponText.text = DataManager.GetTranslation(DataManager.LOC_KEY_LABEL_CURRENT_WEAPON, languageSelected);
			}
			if (enemyText != null)
			{
				enemyText.text = DataManager.GetTranslation(DataManager.LOC_KEY_LABEL_FAVORED_ENEMY, languageSelected);
			}

			Debug.Log("Selected Language: " + languageSelected);
		}
	}

	public void ResetData()
	{

	}

	public void SelectEnemyType(Dropdown typeDD)
	{
		string typeSelectedDName = "";
		if (typeDD != null && typeDD.options != null && typeDD.options.Count > typeDD.value)
		{
			typeSelectedDName = typeDD.options[typeDD.value].text;
			DataManager.Instance.SelectEnemyType(typeSelectedDName);
			Debug.Log("Selected Enemy Type: " + typeSelectedDName);

		}
	}
}
