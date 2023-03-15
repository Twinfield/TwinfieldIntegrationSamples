namespace ExtendedApiExampleApp;

static class Extensions
{
	public static string ToCommaSeparatedString<T>(this IEnumerable<T> collection)
	{
		return string.Join(", ", collection.Select(x => x.ToString()).ToArray());
	}
}