
public class CreatureBuilderWindow : SubWindowHandler
{
    void OnGUI()
    {
        BuilderHandle();
        BuildHandleItem("Creature", ItemType.eCreature, "BuiltCreatures", ItemBaseParts);
    }
}
