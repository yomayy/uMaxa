namespace Shop.Domain.Infrastructure
{
	public static class DecimalExtension
	{
		public static string GetValueString(this decimal value) =>  // 1100.50 => 1,100.50 => $ 1,100.50
			$"{value.ToString("N2")} $";
	}
}
