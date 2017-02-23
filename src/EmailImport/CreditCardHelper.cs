using System;
using System.Text;
using System.Text.RegularExpressions;

namespace EmailImport
{
    class CreditCardHelper
    {
        static public string REGEX_CC_NUMBER = @"(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})";

        static public bool ExistsCCNumber(string s)
        {
            string ccCheck = Regex.Replace(s, @"[ \-,.]", "");
            Regex ccRegex = new Regex(REGEX_CC_NUMBER);

            return ccRegex.IsMatch(ccCheck);
        }

        static public string MaskCCNumbers(string s, char maskChar)
        {
            Regex ccRegex = new Regex(REGEX_CC_NUMBER);

            StringBuilder ss = new StringBuilder(s);
            int ssIndex = 0;

            // ccCheck is what we will use to search for CC #s - it's all the digits within the string of interest
            string ccCheck = Regex.Replace(s, @"[ \-,.]", "");
            int ccCheckIndex = 0;

            Match match;

            // process every match that was found
            do
            {
                var prevCheckIndex = ccCheckIndex;

                match = Regex.Match(ccCheck.Substring(ccCheckIndex), REGEX_CC_NUMBER);

                if (match.Success)
                {
                    bool wasMasked = false;
                    int masked = 0;

                    // skip over any characters in ccCheck that don't fall within the match, designated by match.Index and match.Length
                    for (; ccCheckIndex < ccCheck.Length && ccCheckIndex < match.Index + prevCheckIndex; ccCheckIndex++)
                    {
                        // find this character in the actual string of interest and skip it, as it is not part of the CC match

                        char c = ccCheck[ccCheckIndex];
                        int indexOf = ss.ToString().IndexOf(c, ssIndex);

                        if (indexOf >= 0)
                        {
                            ssIndex = indexOf;
                        }
                    }

                    // loop over each character in ccCheck that falls within match
                    for (; ccCheckIndex < ccCheck.Length && masked < match.Length - 4; ccCheckIndex++)
                    {
                        // find this character in the actual string of interest and mask it, as it is part of the CC match

                        char c = ccCheck[ccCheckIndex];
                        int indexOf = ss.ToString().IndexOf(c, ssIndex);

                        if (indexOf >= 0)
                        {
                            ss[indexOf] = maskChar;
                            ssIndex = indexOf;
                            wasMasked = true;
                            masked++;
                        }
                    }

                    // update check index to go to end of match
                    if (wasMasked)
                    {
                        for (int i = 0; i < 4 && ccCheckIndex < ccCheck.Length; i++, ccCheckIndex++)
                        {
                            // find this character in the actual string of interest and skip it

                            char c = ccCheck[ccCheckIndex];
                            int indexOf = ss.ToString().IndexOf(c, ssIndex);

                            if (indexOf >= 0)
                            {
                                ssIndex = indexOf;
                            }
                        }
                    }
                }
            } while (match.Success);

            return ss.ToString();
        }
    }
}
