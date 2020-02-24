
public class CreatureBuilderWindow : SubWindowHandler
{
    void OnGUI()
    {
        BaseFunction();
        Camera();
        ShowList();
        BuildHandleItem("Creature", ItemType.eCreature, "BuiltCreatures", ItemBaseParts);
    }
}
