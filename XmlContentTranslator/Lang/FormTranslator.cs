using Lang;
using System.Collections;

namespace XmlContentTranslator
{
    /// <summary>
    /// Translates Windows forms and controls.
    /// <para>Переводит формы и элементы управления Windows.</para>
    /// </summary>
    public static class FormTranslator
    {
        #region Variable

        private static readonly FormTranslatorOptions DefaultOptions = new FormTranslatorOptions();
        private static readonly FormTranslatorOptions DefaultOptionsForControls =
            new FormTranslatorOptions { SkipUserControls = false };

        #endregion Variable

        #region Basic

        /// <summary>
        /// Recursively translates controls.
        /// <para>Рекурсивно переводит элементы управления.</para>
        /// </summary>
        private static void Translate(ICollection controls, Dictionary<string, ControlPhrases> controlDict, FormTranslatorOptions options)
        {
            if (controls == null)
            {
                return;
            }

            foreach (object elem in controls)
            {
                ControlPhrases? controlPhrases;

                if (elem is Control control)
                {
                    if (options.SkipUserControls && elem is UserControl)
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(control.Name) && controlDict.TryGetValue(control.Name, out controlPhrases))
                    {
                        if (controlPhrases.Text != null)
                        {
                            control.Text = controlPhrases.Text;
                        }

                        if (controlPhrases.ToolTip != null && options.ToolTip != null)
                        {
                            options.ToolTip.SetToolTip(control, controlPhrases.ToolTip);
                        }

                        if (controlPhrases.Items != null)
                        {
                            int itemCnt = controlPhrases.Items.Count;
                            if (elem is ComboBox comboBox)
                            {
                                for (int i = 0, cnt = Math.Min(comboBox.Items.Count, itemCnt); i < cnt; i++)
                                {
                                    string? itemText = controlPhrases.Items[i];
                                    if (itemText != null)
                                    {
                                        comboBox.Items[i] = itemText;
                                    }
                                }
                            }
                            else if (elem is ListBox listBox)
                            {
                                for (int i = 0, cnt = Math.Min(listBox.Items.Count, itemCnt); i < cnt; i++)
                                {
                                    string? itemText = controlPhrases.Items[i];
                                    if (itemText != null)
                                    {
                                        listBox.Items[i] = itemText;
                                    }
                                }
                            }
                            else if (elem is ListView listView)
                            {
                                for (int i = 0, cnt = Math.Min(listView.Items.Count, itemCnt); i < cnt; i++)
                                {
                                    string? itemText = controlPhrases.Items[i];
                                    if (itemText != null)
                                    {
                                        listView.Items[i].Text = itemText;
                                    }
                                }
                            }
                        }
                    }

                    if (elem is MenuStrip menuStrip)
                    {
                        Translate(menuStrip.Items, controlDict, options);
                    }
                    else if (elem is ToolStrip toolStrip)
                    {
                        Translate(toolStrip.Items, controlDict, options);
                    }
                    else if (elem is DataGridView dataGridView)
                    {
                        Translate(dataGridView.Columns, controlDict, options);
                    }
                    else if (elem is ListView listView)
                    {
                        Translate(listView.Columns, controlDict, options);
                        Translate(listView.Groups, controlDict, options);
                    }

                    if (control.HasChildren)
                    {
                        Translate(control.Controls, controlDict, options);
                    }
                }
                else
                {
                    if (elem is ToolStripItem toolStripItem)
                    {
                        if (!string.IsNullOrEmpty(toolStripItem.Name) && controlDict.TryGetValue(toolStripItem.Name, out controlPhrases))
                        {
                            if (controlPhrases.Text != null)
                            {
                                toolStripItem.Text = controlPhrases.Text;
                            }

                            if (controlPhrases.ToolTip != null)
                            {
                                toolStripItem.ToolTipText = controlPhrases.ToolTip;
                            }
                        }

                        if (elem is ToolStripDropDownItem dropDownItem && dropDownItem.HasDropDownItems)
                        {
                            Translate(dropDownItem.DropDownItems, controlDict, options);
                        }
                    }
                    else if (elem is DataGridViewColumn column && !string.IsNullOrEmpty(column.Name))
                    {
                        if (controlDict.TryGetValue(column.Name, out controlPhrases) && controlPhrases.Text != null)
                        {
                            column.HeaderText = controlPhrases.Text;
                        }
                    }
                    else if (elem is ColumnHeader columnHeader && !string.IsNullOrEmpty(columnHeader.Name))
                    {
                        if (controlDict.TryGetValue(columnHeader.Name, out controlPhrases) && controlPhrases.Text != null)
                        {
                            columnHeader.Text = controlPhrases.Text;
                        }
                    }
                    else if (elem is ListViewGroup listViewGroup && !string.IsNullOrEmpty(listViewGroup.Name))
                    {
                        if (controlDict.TryGetValue(listViewGroup.Name, out controlPhrases) && controlPhrases.Text != null)
                        {
                            listViewGroup.Header = controlPhrases.Text;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Translates a form using the specified dictionary.
        /// <para>Переводит форму по указанному словарю.</para>
        /// </summary>
        public static void Translate(Form form, string? dictName, FormTranslatorOptions? options = null)
        {
            if (form != null && !string.IsNullOrWhiteSpace(dictName) &&
                Locale.Dictionaries.TryGetValue(dictName, out LocaleDict? localeDict))
            {
                options ??= DefaultOptions;
                Dictionary<string, ControlPhrases> controlDict = ControlPhrases.GetControlDict(localeDict);

                if (controlDict.TryGetValue("this", out ControlPhrases? controlPhrases) && controlPhrases.Text != null)
                {
                    form.Text = controlPhrases.Title;
                }

                Translate(form.Controls, controlDict, options);

                if (options.ContextMenus != null)
                {
                    Translate(options.ContextMenus, controlDict, options);
                }
            }
        }

        /// <summary>
        /// Translates a control using the specified dictionary.
        /// <para>Переводит элемент управления по указанному словарю.</para>
        /// </summary>
        public static void Translate(Control control, string? dictName, FormTranslatorOptions? options = null)
        {
            if (control != null && !string.IsNullOrWhiteSpace(dictName) &&
                Locale.Dictionaries.TryGetValue(dictName, out LocaleDict? localeDict))
            {
                options ??= DefaultOptionsForControls;
                Dictionary<string, ControlPhrases> controlDict = ControlPhrases.GetControlDict(localeDict);
                Translate(new Control[] { control }, controlDict, options);

                if (options.ContextMenus != null)
                {
                    Translate(options.ContextMenus, controlDict, options);
                }
            }
        }

        #endregion Basic
    }
}
