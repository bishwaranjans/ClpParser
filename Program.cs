using System.Text.RegularExpressions;

Console.WriteLine("Welcome to CLP Parser!");

string input = @"H    1  2 3        E  U    H 1   2   3     H    23    1  +  H   1      2   3 EUH    - 1   2   3

EUH123 + H123     + EUH  6 7 8  H123";

string pattern1 = @"((H\s*?\d\s*?\d\s*?\d)|(E\s*?U\s*?H\s*?-?\s*?\d\s*?\d\s*?\d))?\+?((H\s*?\d\s*?\d\s*?\d)|(E\s*?U\s*?H\s*?-?\s*?\d\s*?\d\s*?\d))";

string pattern2 = @"((H\s*?\d\s*?\d\s*?\d)|(E\s*?U\s*?H\s*?-?\s*?\d\s*?\d\s*?\d))";

Regex t = new Regex(pattern2, RegexOptions.Multiline | RegexOptions.IgnoreCase);
var allMatches = t.Matches(input);

Console.ReadLine();