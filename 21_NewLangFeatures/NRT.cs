namespace _21_NewLangFeatures;

public class NRT
{
    //"привет" - "тевирп"
    //Fail Fast, NRT (Nullable Reference Types) (C# 8.0+)
    //Fody.NullGuard - гарантия null-safety в рантайме
    public static string? ReverseOrNull(string text) //Kotlin, Dart 2.0 (null-safety)
    {
        if (text is null)
        {
            return null!;
        }
        //Если s null, то будет эксепшн NullReferenceException
        var charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
