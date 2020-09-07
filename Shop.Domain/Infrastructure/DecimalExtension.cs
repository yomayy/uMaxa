namespace Shop.Domain.Infrastructure
{
	public static class DecimalExtension
	{
		public static string GetValueString(this decimal value) =>
			$"{value.ToString("N2")} $";
	}
}
