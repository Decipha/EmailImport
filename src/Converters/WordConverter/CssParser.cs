using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Aspose.Words
{
    /// <summary>
    /// Object used to parse CSS Files.
    /// This can also be used to minify a CSS file though I
    /// doubt this will pass all the same tests as YUI compressor
    /// or some other tool
    /// </summary>
    public partial class CssParser : List<KeyValuePair<String, List<KeyValuePair<String, String>>>>
    {
        private const String SELECTOR_KEY = "selector";
        private const String NAME_KEY = "name";
        private const String VALUE_KEY = "value";
        private const String REGEX_CSS_GROUPS = @"(?<selector>(?:(?:[^,{]+),?)*?)\{(?:(?<name>[^}:]+):?(?<value>[^};]+);?)*?\}";
        private const String REGEX_CSS_COMMENTS = @"(?<!"")\/\*.+?\*\/(?!"")";

        private Regex rStyles = new Regex(REGEX_CSS_GROUPS, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private string stylesheet = String.Empty;

        private Dictionary<String, Dictionary<String, String>> classes;
        private Dictionary<String, Dictionary<String, String>> elements;

        /// <summary>
        /// Original Style Sheet loaded
        /// </summary>
        public String StyleSheet
        {
            get
            {
                return this.stylesheet;
            }
            set
            {
                //If the style sheet changes we will clean out any dependant data
                this.stylesheet = value;
                this.Clear();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CascadingStyleSheet"/> class.
        /// </summary>
        public CssParser()
        {
            this.StyleSheet = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CascadingStyleSheet"/> class.
        /// </summary>
        /// <param name="CascadingStyleSheet">The cascading style sheet.</param>
        public CssParser(String cascadingStyleSheet)
        {
            this.Read(cascadingStyleSheet);
        }

        /// <summary>
        /// Reads the CSS file.
        /// </summary>
        /// <param name="Path">The path.</param>
        public void ReadCssFile(String path)
        {
            this.StyleSheet = File.ReadAllText(path);
            this.Read(StyleSheet);
        }

        /// <summary>
        /// Reads the specified cascading style sheet.
        /// </summary>
        /// <param name="CascadingStyleSheet">The cascading style sheet.</param>
        public void Read(String cascadingStyleSheet)
        {
            this.StyleSheet = cascadingStyleSheet;

            if (!String.IsNullOrEmpty(cascadingStyleSheet))
            {
                //Remove comments before parsing the CSS. Don't want any comments in the collection.
                MatchCollection MatchList = rStyles.Matches(Regex.Replace(cascadingStyleSheet, REGEX_CSS_COMMENTS, String.Empty));

                foreach (Match item in MatchList)
                {
                    //Check for nulls
                    if (item != null && item.Groups != null && item.Groups[SELECTOR_KEY] != null && item.Groups[SELECTOR_KEY].Captures != null && item.Groups[SELECTOR_KEY].Captures[0] != null && !String.IsNullOrEmpty(item.Groups[SELECTOR_KEY].Value))
                    {
                        String strSelector = item.Groups[SELECTOR_KEY].Captures[0].Value.Trim();
                        var style = new List<KeyValuePair<String, String>>();

                        for (int i = 0; i < item.Groups[NAME_KEY].Captures.Count; i++)
                        {
                            String className = item.Groups[NAME_KEY].Captures[i].Value;
                            String value = item.Groups[VALUE_KEY].Captures[i].Value;

                            //Check for null values in the properies
                            if (!String.IsNullOrEmpty(className) && !String.IsNullOrEmpty(value))
                            {
                                className = TrimWhiteSpace(className);
                                value = TrimWhiteSpace(value);

                                //One more check to be sure we are only pulling valid css values
                                if (!String.IsNullOrEmpty(className) && !String.IsNullOrEmpty(value))
                                {
                                    style.Add(new KeyValuePair<String, String>(className, value));
                                }
                            }
                        }

                        this.Add(new KeyValuePair<String, List<KeyValuePair<String, String>>>(strSelector, style));
                    }
                }
            }
        }

        /// <summary>
        /// Trims whitespaces including non printing 
        /// whitespaces like carriage returns, line feeds,
        /// and form feeds
        /// </summary>
        /// <param name="str">The string to trim</param>
        /// <returns></returns>        
        private String TrimWhiteSpace(String str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return null;

            Char[] whiteSpace = { '\r', '\n', '\f', '\t', '\v' };
            return str.Trim(whiteSpace).Trim();
        }

        /// <summary>
        /// Gets the CSS classes.
        /// </summary>
        public Dictionary<String, Dictionary<String, String>> Classes
        {
            get
            {
                if (classes == null || classes.Count == 0)
                {
                    this.classes = this.Where(cl => cl.Key.StartsWith(".")).ToDictionary(cl => cl.Key.Trim(new Char[] { '.' }), cl => cl.Value.ToDictionary(p => p.Key, p => p.Value));
                }

                return classes;
            }
        }

        /// <summary>
        /// Gets the elements.
        /// </summary>
        public Dictionary<String, Dictionary<String, String>> Elements
        {
            get
            {
                if (elements == null || elements.Count == 0)
                {
                    elements = this.Where(el => !el.Key.StartsWith(".")).ToDictionary(el => el.Key, el => el.Value.ToDictionary(p => p.Key, p => p.Value));
                }
                return elements;
            }
        }

        /// <summary>
        /// Gets all styles in an Immutable collection
        /// </summary>
        public IEnumerable<KeyValuePair<String, List<KeyValuePair<String, String>>>> Styles
        {
            get
            {
                return this.ToArray();
            }
        }

        /// <summary>
        /// Removes all elements from the <see cref="CSSParser"></see>.
        /// </summary>
        new public void Clear()
        {
            base.Clear();
            this.classes = null;
            this.elements = null;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> the CSS that was entered as it is stored internally.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder strb = new StringBuilder(this.StyleSheet.Length);

            foreach (var item in this)
            {
                strb.Append(item.Key).Append("{");

                foreach (var property in item.Value)
                {
                    strb.Append(property.Key).Append(":").Append(property.Value).Append(";");
                }

                strb.Append("}");
            }

            return strb.ToString();
        }
    }
}
