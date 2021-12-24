using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBattleHud : MonoBehaviour
{
    public TMP_Text nameText, actionPointsText;

    public void SetHud(Unit unit)
    {
        nameText.text = unit.unitName;
        actionPointsText.text = unit.playerActionPoints.ToString();
    } 
}
