using System.Text.RegularExpressions;

Console.WriteLine("Welcome to CLP Parser!");

string input = @"H    1  2 3        E  U    H 1   2   3     H    23    1  +  H   1      2   3 EUH    - 1   2   3

EUH123 + H123     + EUH  6 7 8  H    -   456 EUH  - 390+H123";

var normalCodes = new List<string>();
var compositeCodes = new List<string>();
var currentCompositeCode = string.Empty;

PatternWithTakingPlusSign();
//PatternWithoutTakingPlusSign();

void PatternWithTakingPlusSign()
{
    string pattern = @"(((H\s*?-?\s*?\d\s*?\d\s*?\d)|(E\s*?U\s*?H\s*?-?\s*?\d\s*?\d\s*?\d))\s*\+?)";

    Regex t = new Regex(pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
    MatchCollection allMatches = t.Matches(input);

    foreach (Match match in allMatches)
    {
        string matchValue = Regex.Replace(match.Value, @"\s+", string.Empty);;

        if (matchValue.Contains('+'))
        {
            currentCompositeCode = string.IsNullOrWhiteSpace(currentCompositeCode) ? matchValue : currentCompositeCode + matchValue;
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(currentCompositeCode))
            {
                currentCompositeCode = currentCompositeCode + matchValue;
                compositeCodes.Add(currentCompositeCode);
                currentCompositeCode = string.Empty;
            }
            else
            {
                normalCodes.Add(matchValue);
            }
        }
    }
}

void PatternWithoutTakingPlusSign()
{
    string pattern = @"((H\s*?-?\s*?\d\s*?\d\s*?\d)|(E\s*?U\s*?H\s*?-?\s*?\d\s*?\d\s*?\d))";

    Regex t = new Regex(pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
    MatchCollection allMatches = t.Matches(input);

    foreach (Match match in allMatches)
    {
        string matchValue = match.Value;

        // Hanlde duplicate
        if (normalCodes.Contains(match.Value) || compositeCodes.Contains(matchValue))
        {
            continue;
        }

        var restString = input.Substring(input.IndexOf(matchValue) + matchValue.Length).Trim();
        if (string.IsNullOrWhiteSpace(restString) || (restString.Length > 0 && restString[0] != '+'))
        {
            if (string.IsNullOrWhiteSpace(currentCompositeCode))
            {
                normalCodes.Add(match.Value);
            }
            else
            {
                currentCompositeCode = string.IsNullOrWhiteSpace(currentCompositeCode.Trim()) ? matchValue.Trim() : currentCompositeCode + "+" + matchValue.Trim();
                compositeCodes.Add(currentCompositeCode);
                currentCompositeCode = string.Empty;
            }
        }
        else
        {
            currentCompositeCode = string.IsNullOrWhiteSpace(currentCompositeCode.Trim()) ? matchValue.Trim() : currentCompositeCode + "+" + matchValue.Trim();
        }
    }
}

Console.ReadLine();