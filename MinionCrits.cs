using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace MinionCrits
{
	class MinionCrits : Mod
	{
		public static Mod Instance;
		public static CritTypes SummonCritType = CritTypes.Highest;
		public enum CritTypes {Melee, Ranged, Magic, Throwing, Highest, Flat}
		public override void Load()
		{
			Instance = this;
			//Config.Load();
		}

	}
}
