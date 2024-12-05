using Game.SceneFlow;
using Game.UI;

namespace TrainVisuals.Code.Formulas
{
    public static class NameSystemExtensions
    {
        // public static AccessTools.StructFieldRef<NameSystem.Name, NameSystem.NameType> fieldRefNameType = HarmonyLib.AccessTools.StructFieldRefAccess<NameSystem.Name, NameSystem.NameType>("m_NameType");
        // public static AccessTools.StructFieldRef<NameSystem.Name, string> fieldRefNameId = HarmonyLib.AccessTools.StructFieldRefAccess<NameSystem.Name, string>("m_NameID");
        // public static AccessTools.StructFieldRef<NameSystem.Name, string[]> fieldRefNameArgs = HarmonyLib.AccessTools.StructFieldRefAccess<NameSystem.Name, string[]>("m_NameArgs");
        // public static NameSystem.NameType GetNameType(this NameSystem.Name name) => fieldRefNameType(ref name);
        // public static string GetNameID(this NameSystem.Name name) => fieldRefNameId(ref name);
        // public static string[] GetNameArgs(this NameSystem.Name name) => fieldRefNameArgs(ref name);
        //
        // internal static ValuableName ToValueableName(this NameSystem.Name name) => new(name);
        //
        // public static string Translate(this NameSystem.Name name)
        // {
        //     var type = name.GetNameType();
        //     switch (type)
        //     {
        //         default:
        //         case NameSystem.NameType.Custom:
        //             return name.GetNameID();
        //         case NameSystem.NameType.Localized:
        //             return GameManager.instance.localizationManager.activeDictionary.TryGetValue(name.GetNameID(), out var value) ? value : name.GetNameID();
        //         case NameSystem.NameType.Formatted:
        //             var activeDictionary = GameManager.instance.localizationManager.activeDictionary;
        //             var format = activeDictionary.TryGetValue(name.GetNameID(), out var value2) ? value2 : name.GetNameID();
        //             var args = name.GetNameArgs();
        //             for (var i = 0; i < args.Length; i += 2)
        //             {
        //                 format = format.Replace($"{{{args[i]}}}", activeDictionary.TryGetValue(args[i + 1], out value2) ? value2 : args[i + 1]);
        //             }
        //             return format;
        //     }
        // }
        //
        // public class ValuableName
        // {
        //     public readonly string __Type;
        //     public readonly string name;
        //     public readonly string nameId;
        //     public readonly string[] nameArgs;
        //
        //     internal ValuableName(NameSystem.Name name)
        //     {
        //         var type = name.GetNameType();
        //         switch (type)
        //         {
        //             default:
        //             case NameSystem.NameType.Custom:
        //                 __Type = "names.CustomName";
        //                 this.name = name.GetNameID();
        //                 nameId = null;
        //                 nameArgs = null;
        //                 break;
        //             case NameSystem.NameType.Localized:
        //                 __Type = "names.LocalizedName";
        //                 this.name = null;
        //                 nameId = name.GetNameID();
        //                 nameArgs = null;
        //                 break;
        //             case NameSystem.NameType.Formatted:
        //                 __Type = "names.FormattedName";
        //                 this.name = null;
        //                 nameId = name.GetNameID();
        //                 nameArgs = name.GetNameArgs();
        //                 break;
        //         }
        //     }
        // }
    }
}