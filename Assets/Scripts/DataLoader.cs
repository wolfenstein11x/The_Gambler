using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour
{

    [SerializeField] Text playerMoneyText;

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerMoneyText();
    }

    private void LoadPlayerMoneyText()
    {
        playerMoneyText.text = PlayerData.playerTotalMoney.ToString();
    }

    private void LoadPlayerCoordinates()
    {

    }
}
