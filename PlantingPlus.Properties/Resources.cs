using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace PlantingPlus.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					resourceMan = new ResourceManager("PlantingPlus.Properties.Resources", typeof(Resources).Assembly);
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static byte[] blueberryBushPieceIcon => (byte[])ResourceManager.GetObject("blueberryBushPieceIcon", resourceCulture);

		internal static byte[] cloudberryBushPieceIcon => (byte[])ResourceManager.GetObject("cloudberryBushPieceIcon", resourceCulture);

		internal static byte[] raspberryBushPieceIcon => (byte[])ResourceManager.GetObject("raspberryBushPieceIcon", resourceCulture);

		internal Resources()
		{
		}
	}
}
