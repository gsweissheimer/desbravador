public static class StringService
{
    public static string LimitByWords(string str, int limit)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        string[] words = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (words.Length <= limit)
        {
            return str;
        }

        return string.Join(" ", words, 0, limit) + "...";
    }
}