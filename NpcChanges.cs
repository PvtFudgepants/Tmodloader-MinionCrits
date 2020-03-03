using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MinionCrits
{
	class NpcChanges : GlobalNPC
	{
		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (!crit) {
				bool fromsummon = false;
				int BaseCritValue = 0;
				if (projectile.minion) fromsummon = true;
				else {
					for (int i = 0; i < 1000; i++) {
						Projectile CheckProjectile = Main.projectile[i];
						if (CheckProjectile.active && (CheckProjectile.minion || CheckProjectile.sentry) && CheckProjectile.type + 1 == projectile.type) {
							fromsummon = true;
							foreach(Item item in Main.player[projectile.owner].inventory) {
								if (item.shoot == CheckProjectile.type) {
									BaseCritValue = item.crit;
									break;
								}
							}
							break;
						}
					}
				}
				if (!fromsummon) return;
				switch (MinionCrits.SummonCritType) {
					case MinionCrits.CritTypes.Melee:
						BaseCritValue += Main.player[projectile.owner].meleeCrit;
						break;
					case MinionCrits.CritTypes.Ranged:
						BaseCritValue += Main.player[projectile.owner].rangedCrit;
						break;
					case MinionCrits.CritTypes.Magic:
						BaseCritValue += Main.player[projectile.owner].magicCrit;
						break;
					case MinionCrits.CritTypes.Throwing:
						BaseCritValue += Main.player[projectile.owner].thrownCrit;
						break;
					case MinionCrits.CritTypes.Highest:
						BaseCritValue += Math.Max(Math.Max(Main.player[projectile.owner].meleeCrit, Main.player[projectile.owner].rangedCrit), Math.Max(Main.player[projectile.owner].magicCrit, Main.player[projectile.owner].thrownCrit));
						break;
					case MinionCrits.CritTypes.Flat:
						BaseCritValue = 10;
						break;
					default:
						BaseCritValue = 4;
						break;
				}
				int SummonCritChance = (int)Math.Round(BaseCritValue * Config.Instance.CritChanceMultiplier);
				if (SummonCritChance < 4) SummonCritChance = 4;
				if ( Main.rand.Next(100) < SummonCritChance ) {
					crit = true;
					damage = (int)((damage/2)* Config.Instance.CritDamageMultiplier);
					if ( Config.Instance.CritKnockbackIncrease ) {
						knockback *= 1.4f;
					}
					return;
				}
			}
		}
	}
}
