using System.Text.RegularExpressions;

Console.WriteLine("Welcome to CLP Parser!");

string input = @"H    1  2 3        E  U    H 1   2   3     H    23    1  +  H   1      2   3 EUH    - 1   2   3

EUH123 + H123     + EUH  6 7 8  H523";

string pattern1 = @"((H\s*?\d\s*?\d\s*?\d)|(E\s*?U\s*?H\s*?-?\s*?\d\s*?\d\s*?\d))?\+?((H\s*?\d\s*?\d\s*?\d)|(E\s*?U\s*?H\s*?-?\s*?\d\s*?\d\s*?\d))";

string pattern2 = @"((H\s*?\d\s*?\d\s*?\d)|(E\s*?U\s*?H\s*?-?\s*?\d\s*?\d\s*?\d))";

Regex t = new Regex(pattern2, RegexOptions.Multiline | RegexOptions.IgnoreCase);
MatchCollection allMatches = t.Matches(input);

var normalCodes = new List<string>();
var compositeCodes = new List<string>();

var currentCompositeCode = string.Empty;

foreach (Match match in allMatches)
{
    var restString = input.Substring(input.IndexOf(match.Value) + match.Value.Length).Trim();
    if (string.IsNullOrWhiteSpace(restString) || (restString.Length > 0 && restString[0] != '+'))
    {
        if (string.IsNullOrWhiteSpace(currentCompositeCode))
        {
            normalCodes.Add(match.Value);
        }
        else
        {
            currentCompositeCode = string.IsNullOrWhiteSpace(currentCompositeCode.Trim()) ? match.Value.Trim() : currentCompositeCode + "+" + match.Value.Trim();
            compositeCodes.Add(currentCompositeCode);
            currentCompositeCode = string.Empty;
        }
    }
    else
    {
        currentCompositeCode = string.IsNullOrWhiteSpace(currentCompositeCode.Trim()) ? match.Value.Trim() : currentCompositeCode + "+" + match.Value.Trim();
    }
}
Console.ReadLine();