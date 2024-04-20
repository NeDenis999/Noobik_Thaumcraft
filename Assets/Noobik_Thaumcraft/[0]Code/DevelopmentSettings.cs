using System.Collections;
using Noobik_Thaumcraft;
using UnityEngine;

public class DevelopmentSettings : MonoBehaviour
{
    public bool _isMaximumSkillLeveling;
    public SpeedCell SpeedCell;
    public MaxBackpackSizeCell MaxBackpackSizeCell;
    public MiningSpeedCell MiningSpeedCell;
    
    private IEnumerator Start()
    {
        yield return null;
        
        if (_isMaximumSkillLeveling)
        {
            SpeedCell.FullBuy();
            MaxBackpackSizeCell.FullBuy();
            MiningSpeedCell.FullBuy();
        }
    }
}