using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EmailImport.CCMasker
{
    class Program
    {
        static public string REGEX_CC_NUMBER = @"(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})";

        static public bool ExistsCCNumber(string s)
        {
            string ccCheck = Regex.Replace(s, @"[ \-,.]", "");
            Regex ccRegex = new Regex(REGEX_CC_NUMBER);

            return ccRegex.IsMatch(ccCheck);
        }

        static public int CountCCNumbers(string s)
        {
            string ccCheck = Regex.Replace(s, @"[ \-,.]", "");
            Regex ccRegex = new Regex(REGEX_CC_NUMBER);

            return ccRegex.Matches(ccCheck).Count;
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

        static void Main(string[] args)
        {
            bool findOnly = true;
            bool displayFound = false;
            int found = 0;
            int falsePositives = 0;

            // if -m option is given in args, then do masking
            foreach (var arg in args)
            {
                if (arg == "-m")
                {
                    findOnly = false;
                }
                else if (arg == "-d")
                {
                    displayFound = true;
                }
                else if (arg == "-?" || arg == "/?")
                {
                    Console.WriteLine(String.Format("use:"));
                    Console.WriteLine(String.Format("  -m to perform masking"));
                    Console.WriteLine(String.Format("  -d to display potential matches"));
                }
            }

            using (var ctx = new EmailImportDataContext())
            {
                foreach (var email in ctx.Emails)
                {
                    if (ExistsCCNumber(email.Subject))
                    {
                        found++;

                        if (!findOnly)
                        {
                            email.Subject = MaskCCNumbers(email.Subject, '#');

                            if (ExistsCCNumber(email.Subject))
                            {
                                Console.WriteLine("CHECK: '{0}'", email.Subject);

                                falsePositives++;
                            }
                        }
                        else if (displayFound)
                        {
                            Console.WriteLine("Potential: '{0}'", email.Subject);
                        }
                    }
                }

                if (!findOnly)
                    ctx.SubmitChanges();
            }

            Console.WriteLine(String.Format("-------------------------------------------------------------------------"));
            if (!findOnly)
                Console.WriteLine(String.Format("{0} CC #s masked, {1} not masked or false positives (please check)", found, falsePositives));
            else
                Console.WriteLine(String.Format("Found {0} potential CC #s{1}", found, (displayFound) ? "" : " - use -d option to show potentials"));
            Console.WriteLine(String.Format("Please press a key to exit..."));

            Console.ReadKey();
        }
    }
}
