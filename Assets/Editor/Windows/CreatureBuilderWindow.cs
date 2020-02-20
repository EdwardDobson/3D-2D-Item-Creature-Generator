
public class CreatureBuilderWindow : SubWindowHandler
{
    void OnGUI()
    {
        BaseFunction();
        ShowList("CreatureParts");
        ViewItem();
        BuildHandleItem("Creature", ItemType.eCreature, "BuiltCreatures", ItemBaseParts);
    }
}
