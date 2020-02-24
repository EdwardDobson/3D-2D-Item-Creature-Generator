
public class CreatureBuilderWindow : SubWindowHandler
{
    void OnGUI()
    {
        BaseFunction();
        ShowList();
        ViewItem();
        BuildHandleItem("Creature", ItemType.eCreature, "BuiltCreatures", ItemBaseParts);
    }
}
