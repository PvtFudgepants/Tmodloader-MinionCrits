using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace MinionCrits
{
	public class Config : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;
		public static Config Instance = new Config();

		[Header("Minion Crit Settings")]

		[Label("Crit Type")]
		[Tooltip("What determines the crit chance of summoner and sentry weapons.")]
		[DrawTicks]
		[OptionStrings(new string[] { "Copy Melee Crit Chance", "Copy Ranged Crit Chance", "Copy Magic Crit Chance", "Copy Throwing Crit Chance", "Copy Highest Crit Chance", "Flat 10% chance" })]
		[DefaultValue("Copy Highest Crit Chance")]
		public string SummonCritType;
		public override void OnChanged() {
			switch ( SummonCritType ) {
				case "Copy Melee Crit Chance":
					MinionCrits.SummonCritType = MinionCrits.CritTypes.Melee;
					break;
				case "Copy Ranged Crit Chance":
					MinionCrits.SummonCritType = MinionCrits.CritTypes.Ranged;
					break;
				case "Copy Magic Crit Chance":
					MinionCrits.SummonCritType = MinionCrits.CritTypes.Magic;
					break;
				case "Copy Throwing Crit Chance":
					MinionCrits.SummonCritType = MinionCrits.CritTypes.Throwing;
					break;
				case "Copy Highest Crit Chance":
					MinionCrits.SummonCritType = MinionCrits.CritTypes.Highest;
					break;
				case "Flat 10% chance":
					MinionCrits.SummonCritType = MinionCrits.CritTypes.Flat;
					break;
				default:
					break;
			}
		}


		[Label("Summon Crit Chance Multiplier")]
		[Tooltip("Summon crit chance will be multiplied by this value (with a minimum of 4%)")]
		[Range(0.25f, 10f)]
		[Increment(.25f)]
		//[DrawTicks]
		[DefaultValue(1f)]
		public float CritChanceMultiplier;

		[Label("Summon Crit Damage Multiplier")]
		[Tooltip("Summon crit damage will be multiplied by this value (If you want crits to deal more or less damage)")]
		[Range(1f, 3f)]
		[Increment(.1f)]
		[DefaultValue(2f)]
		public float CritDamageMultiplier;

		[Label("Summon Crits Increase Knockback")]
		[Tooltip("Summon crits will deal increased knockback (Same behavior as every other crit)")]
		[DefaultValue(true)]
		public bool CritKnockbackIncrease;

	}
}
