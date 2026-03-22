/*
 * Copyright 2024 Rapid Software LLC
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 
 * Product  : Rapid SCADA
 * Module   : ScadaCommon
 * Summary  : Represents phrases of a control
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2021
 * Modified : 2021
 */

using System;
using System.Collections.Generic;

namespace Lang
{
    /// <summary>
    /// Represents phrases of a control.
    /// <para>Представляет фразы элемента управления.</para>
    /// </summary>
    public class ControlPhrases
    {
        #region Variable

        /// <summary>
        /// Gets or sets a value indicating whether to append the product name to a form title.
        /// </summary>
        public static bool AppendProductName { get; set; }

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets or sets the control text.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the tooltip.
        /// </summary>
        public string? ToolTip { get; set; }

        /// <summary>
        /// Gets the item phrases.
        /// </summary>
        public List<string?>? Items { get; private set; }

        /// <summary>
        /// Gets the phrases by property name, including text and tooltip.
        /// </summary>
        public Dictionary<string, string>? Phrases { get; private set; }

        /// <summary>
        /// Gets the form title.
        /// </summary>
        public string Title
        {
            get
            {
                string title = Text ?? string.Empty;

                if (AppendProductName)
                {
                    string suffix = " - " + CommonPhrases.ProductName;
                    return title.EndsWith(suffix, StringComparison.OrdinalIgnoreCase) ? title : title + suffix;
                }

                return title;
            }
        }

        #endregion Property

        #region Basic

        /// <summary>
        /// Sets the value of the item at the specified index.
        /// </summary>
        /// <param name="index">The item index.</param>
        /// <param name="val">The item value.</param>
        public void SetItem(int index, string val)
        {
            if (index < 0)
            {
                return;
            }

            Items ??= new List<string?>();

            if (index < Items.Count)
            {
                Items[index] = val;
            }
            else
            {
                while (Items.Count < index)
                {
                    Items.Add(null);
                }

                Items.Add(val);
            }
        }

        /// <summary>
        /// Sets the phrase.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="val">The phrase value.</param>
        public void SetPhrase(string propertyName, string val)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            Phrases ??= new Dictionary<string, string>();
            Phrases[propertyName] = val;
        }

        /// <summary>
        /// Gets a dictionary that contains phrases accessed by control names.
        /// </summary>
        /// <param name="dict">The source locale dictionary.</param>
        /// <returns>The control phrases dictionary.</returns>
        public static Dictionary<string, ControlPhrases> GetControlDict(LocaleDict dict)
        {
            Dictionary<string, ControlPhrases> controlDict = new Dictionary<string, ControlPhrases>();

            foreach (KeyValuePair<string, string> pair in dict.Phrases)
            {
                string phraseKey = pair.Key;
                string phraseVal = pair.Value;
                int dotIdx = phraseKey.IndexOf('.');

                if (dotIdx < 0)
                {
                    if (controlDict.ContainsKey(phraseKey))
                    {
                        controlDict[phraseKey].Text = phraseVal;
                    }
                    else
                    {
                        controlDict[phraseKey] = new ControlPhrases { Text = phraseVal };
                    }
                }
                else if (dotIdx > 0 && dotIdx < phraseKey.Length - 1)
                {
                    string controlName = phraseKey.Substring(0, dotIdx);
                    string propName = phraseKey.Substring(dotIdx + 1);
                    bool propAssigned = true;

                    if (!controlDict.TryGetValue(controlName, out ControlPhrases? controlPhrases))
                    {
                        controlPhrases = new ControlPhrases();
                    }

                    if (propName == "Text")
                    {
                        controlPhrases.Text = phraseVal;
                    }
                    else if (propName == "ToolTip")
                    {
                        controlPhrases.ToolTip = phraseVal;
                    }
                    else if (propName.StartsWith("Items[", StringComparison.Ordinal))
                    {
                        int pos = propName.IndexOf(']');

                        if (pos >= 0 && int.TryParse(propName.Substring(6, pos - 6), out int ind))
                        {
                            controlPhrases.SetItem(ind, phraseVal);
                        }
                    }
                    else if (!string.IsNullOrEmpty(propName))
                    {
                        controlPhrases.SetPhrase(propName, phraseVal);
                    }
                    else
                    {
                        propAssigned = false;
                    }

                    if (propAssigned)
                    {
                        controlDict[controlName] = controlPhrases;
                    }
                }
            }

            return controlDict;
        }

        #endregion Basic
    }
}
