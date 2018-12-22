using System;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Kingmaker;
using Kingmaker.Blueprints.Items;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Newtonsoft.Json.Converters;

namespace CraftMagicItems {

    public class CraftingProjectData {
        // Not serialized
        public UnitEntityData Crafter;
        
        [JsonProperty]
        public int Progress;

        [JsonProperty]
        public int TargetCost;

        [JsonProperty]
        public int CasterLevel;

        [JsonProperty]
        public BlueprintAbility[] Prerequisites;

        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        public CrafterPrerequisiteType[] CrafterPrerequisites;
        
        [JsonProperty]
        public bool AnyPrerequisite;
        
        [JsonProperty]
        public BlueprintItem ItemBlueprint; // TODO remove eventually
        
        [JsonProperty]
        public ItemEntity ResultItem;

        [JsonProperty]
        public string ItemType;

        [JsonProperty]
        public string LastMessage;

        [JsonProperty]
        public ItemEntity UpgradeItem;

        public CraftingProjectData(UnitEntityData crafter, int targetCost, int casterLevel, ItemEntity resultItem, string itemType,
                BlueprintAbility[] prerequisites = null, bool anyPrerequisite = false, ItemEntity upgradeItem = null,
                CrafterPrerequisiteType[] crafterPrerequisites = null) {
            Crafter = crafter;
            TargetCost = targetCost;
            CasterLevel = casterLevel;
            ResultItem = resultItem;
            ItemType = itemType;
            Prerequisites = prerequisites;
            AnyPrerequisite = anyPrerequisite;
            Progress = 0;
            UpgradeItem = upgradeItem;
            CrafterPrerequisites = crafterPrerequisites;
        }

        public void AddMessage(string message) {
            LastMessage = message;
        }
    }

    [AllowedOn(typeof(BlueprintBuff))]
    public class CraftingTimerComponent : BuffLogic {
        [JsonProperty]
        public List<CraftingProjectData> CraftingProjects;

        [JsonProperty]
        public TimeSpan LastUpdated;

        public CraftingTimerComponent() {
            CraftingProjects = new List<CraftingProjectData>();
            LastUpdated = Game.Instance.Player.GameTime;
        }

        public void AddProject(CraftingProjectData project) {
            if (!CraftingProjects.Any()) {
                LastUpdated = Game.Instance.Player.GameTime;
            }
            CraftingProjects.Add(project);
        }
    }


}
