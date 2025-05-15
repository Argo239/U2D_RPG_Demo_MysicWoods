using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboListData", menuName = "Combo Settings/Combo List Data")]
public class ComboListData : ScriptableObject {
    [Header("Combo Step List/ 連招段列表")]
    public List<ComboStepData> comboSteps = new List<ComboStepData>();  
}