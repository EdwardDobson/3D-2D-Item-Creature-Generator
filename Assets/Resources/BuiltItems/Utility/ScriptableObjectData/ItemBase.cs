
using UnityEngine;
[CreateAssetMenu(fileName = "ItemBase", menuName = "BaseData/ItemBase", order = 2)]
public class ItemBase : ScriptableObjectData
{
    public ScriptableObjectData[] Slots = new ScriptableObjectData[6];
    public float BuffValue;
    public float Speed;
    public void ItemBaseReset()
    {
        Reset();
        Slots = new ScriptableObjectData[5];
        BuffValue = 0;
        Speed = 0;
    }
}
