using System;
using System.Linq;
using Ravenfield.Mutator.Configuration;

namespace Lua.Wrapper
{
	// Token: 0x02000976 RID: 2422
	[Wrapper(typeof(MutatorEntry))]
	[Name("Mutator")]
	public static class WMutatorEntry
	{
		// Token: 0x06003DA6 RID: 15782 RVA: 0x00029B83 File Offset: 0x00027D83
		[Getter]
		public static string GetName(MutatorEntry self)
		{
			return self.name;
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x00029B8B File Offset: 0x00027D8B
		[Getter]
		public static string GetDescription(MutatorEntry self)
		{
			return self.description;
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x00029B93 File Offset: 0x00027D93
		[Ignore]
		private static bool MatchID(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x0012ECFC File Offset: 0x0012CEFC
		public static float GetConfigurationInt(MutatorEntry self, string id)
		{
			float result;
			try
			{
				result = (float)self.configuration.integers.First((IntegerConfigurationField field) => WMutatorEntry.MatchID(field.id, id)).value;
			}
			catch (Exception)
			{
				ScriptConsole.instance.LogError("No configuration found with id " + id);
				result = 0f;
			}
			return result;
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x0012ED70 File Offset: 0x0012CF70
		public static float GetConfigurationFloat(MutatorEntry self, string id)
		{
			float result;
			try
			{
				result = self.configuration.floats.First((FloatConfigurationField field) => WMutatorEntry.MatchID(field.id, id)).value;
			}
			catch (Exception)
			{
				ScriptConsole.instance.LogError("No configuration found with id " + id);
				result = 0f;
			}
			return result;
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x0012EDE4 File Offset: 0x0012CFE4
		public static string GetConfigurationString(MutatorEntry self, string id)
		{
			string result;
			try
			{
				result = self.configuration.strings.First((StringConfigurationField field) => WMutatorEntry.MatchID(field.id, id)).value;
			}
			catch (Exception)
			{
				ScriptConsole.instance.LogError("No configuration found with id " + id);
				result = "";
			}
			return result;
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x0012EE58 File Offset: 0x0012D058
		public static bool GetConfigurationBool(MutatorEntry self, string id)
		{
			bool result;
			try
			{
				result = self.configuration.bools.First((BoolConfigurationField field) => WMutatorEntry.MatchID(field.id, id)).value;
			}
			catch (Exception)
			{
				ScriptConsole.instance.LogError("No configuration found with id " + id);
				result = false;
			}
			return result;
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x0012EEC8 File Offset: 0x0012D0C8
		public static float GetConfigurationRange(MutatorEntry self, string id)
		{
			float result;
			try
			{
				result = self.configuration.ranges.First((RangeConfigurationField field) => WMutatorEntry.MatchID(field.id, id)).value.value;
			}
			catch (Exception)
			{
				ScriptConsole.instance.LogError("No configuration found with id " + id);
				result = 0f;
			}
			return result;
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x0012EF40 File Offset: 0x0012D140
		public static int GetConfigurationDropdown(MutatorEntry self, string id)
		{
			int result;
			try
			{
				result = self.configuration.dropdowns.First((DropdownConfigurationField field) => WMutatorEntry.MatchID(field.id, id)).value.index;
			}
			catch (Exception)
			{
				ScriptConsole.instance.LogError("No configuration found with id " + id);
				result = 0;
			}
			return result;
		}
	}
}
