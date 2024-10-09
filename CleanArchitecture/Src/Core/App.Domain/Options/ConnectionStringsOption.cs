namespace App.Domain.Options
{
	public class ConnectionStringsOption
	{
		public const string Key = "ConnectionStrings";
		public string SqlServer { get; set; } = default!;
	}
}
