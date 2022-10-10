﻿using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace NPCKillCounter;

public class NPCKillCounterGlobalInfoDisplay : GlobalInfoDisplay
{
    public override void ModifyDisplayValue(InfoDisplay currentDisplay, ref string displayValue)
    {
        if (currentDisplay is not TallyCounterInfoDisplay)
        {
            return;
        }

        var npc = new NPC(); // 重新设置NPC,防止NPC死亡后无法继续显示
        npc.SetDefaults(NPCKillCounterPlayer.Hit);

        if (npc.type == NPCID.None) // NPC的名字不能为None
        {
            displayValue = Language.GetTextValue("GameUI.NoKillCount");
            return;
        }

        var entity = new NPCDefinition(npc.type);
        var name = Main.keyState.IsKeyDown(Main.FavoriteKey) switch
        {
            true => (Main.GameUpdateCount % 120 > 60) switch
            {
                true => entity.Name,
                false => entity.Mod
            },
            false => npc.GivenOrTypeName
        };
        var playerCount = NPCKillCounterPlayer.Count[entity.ToString()];
        var systemCount = NPCKillCounterSystem.Count[entity.ToString()];
        displayValue = $"{name}: {playerCount}/{systemCount}";
    }
}using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace NPCKillCounter;

public class NPCKillCounterGlobalInfoDisplay : GlobalInfoDisplay
{
    public override void ModifyDisplayValue(InfoDisplay currentDisplay, ref string displayValue)
    {
        if (currentDisplay is not TallyCounterInfoDisplay)
        {
            return;
        }

        var npc = new NPC(); // 重新设置NPC,防止NPC死亡后无法继续显示
        npc.SetDefaults(NPCKillCounterPlayer.Hit);

        if (npc.type == NPCID.None) // NPC的名字不能为None
        {
            displayValue = Language.GetTextValue("GameUI.NoKillCount");
            return;
        }

        var entity = new NPCDefinition(npc.netID);
        var name = Main.keyState.IsKeyDown(Main.FavoriteKey) switch
        {
            true => (Main.GameUpdateCount % 120 > 60) switch
            {
                true => entity.Name,
                false => entity.Mod
            },
            false => npc.GivenOrTypeName
        };
		
		var playerCount = Main.LocalPlayer.GetModPlayer<NPCKillCounterPlayer>().Count[entity.ToString()];
  //      var playerCount = NPCKillCounterPlayer.Count[entity.ToString()];
        var systemCount = NPCKillCounterSystem.Count[entity.ToString()];
        displayValue = $"{name}: {playerCount}/{systemCount}";
    }
}
