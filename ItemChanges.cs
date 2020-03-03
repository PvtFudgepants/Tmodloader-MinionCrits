using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace MinionCrits
{
	class ItemChanges : GlobalItem
	{
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (item.summon) {
				int count = 0;
				bool founddmg = false;
				foreach (TooltipLine tooltip in tooltips) {
					count++;
					if(tooltip.Name == "Damage") {
						founddmg = true;
						break;
					}
				}
				if (founddmg) {
					int BaseCritValue;
					switch (MinionCrits.SummonCritType) {
						case MinionCrits.CritTypes.Melee:
							BaseCritValue = Main.player[item.owner].meleeCrit;
							break;
						case MinionCrits.CritTypes.Ranged:
							BaseCritValue = Main.player[item.owner].rangedCrit;
							break;
						case MinionCrits.CritTypes.Magic:
							BaseCritValue = Main.player[item.owner].magicCrit;
							break;
						case MinionCrits.CritTypes.Throwing:
							BaseCritValue = Main.player[item.owner].thrownCrit;
							break;
						case MinionCrits.CritTypes.Highest:
							BaseCritValue = Math.Max(Math.Max(Main.player[item.owner].meleeCrit, Main.player[item.owner].rangedCrit), Math.Max(Main.player[item.owner].magicCrit, Main.player[item.owner].thrownCrit));
							break;
						case MinionCrits.CritTypes.Flat:
							BaseCritValue = 10;
							break;
						default:
							BaseCritValue = 4;
							break;
					}
					int SummonCritChance = item.crit + (int)Math.Round(BaseCritValue * Config.Instance.CritChanceMultiplier);
					if ( SummonCritChance < 4 ) SummonCritChance = 4;
					if ( SummonCritChance > 100 ) SummonCritChance = 100;
					string SummonCritString = SummonCritChance + "% critical strike chance";
					tooltips.Insert(count, new TooltipLine(MinionCrits.Instance, "CritChance", SummonCritString));
				}
			}
		}
	}
}
