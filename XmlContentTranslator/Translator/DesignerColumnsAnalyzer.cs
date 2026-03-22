using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace XmlContentTranslator.Translator
{
    /// <summary>
    /// Result of designer column analysis.
    /// <para>Результат анализа столбцов designer-файла.</para>
    /// </summary>
    public sealed class DesignerColumnsAnalysis
    {
        #region Variable

        public Dictionary<string, List<string>> ListViewColumnVariables { get; } =
            new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        public Dictionary<string, List<string>> DataGridViewColumnVariables { get; } =
            new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        public Dictionary<string, List<string>> ListViewHeaderTexts { get; } =
            new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        public Dictionary<string, List<string>> DataGridViewHeaderTexts { get; } =
            new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        #endregion Variable
    }

    /// <summary>
    /// Analyzer of ListView and DataGridView columns in designer code.
    /// <para>Анализатор столбцов ListView и DataGridView в designer-коде.</para>
    /// </summary>
    public static class DesignerColumnsAnalyzer
    {
        #region Variable

        private const string OptionalThisPattern = @"(?:this\.)?";
        private const string ColumnHeaderArrayPattern = @"(?:System\.Windows\.Forms\.)?ColumnHeader\[\]";
        private const string DataGridViewColumnArrayPattern = @"(?:System\.Windows\.Forms\.)?DataGridViewColumn\[\]";

        #endregion Variable

        #region Basic

        /// <summary>
        /// Analyzes designer content.
        /// <para>Анализирует содержимое designer-файла.</para>
        /// </summary>
        public static DesignerColumnsAnalysis Analyze(string content)
        {
            DesignerColumnsAnalysis analysis = new DesignerColumnsAnalysis();
            ParseListViewColumns(content, analysis);
            ParseDataGridViewColumns(content, analysis);
            return analysis;
        }

        /// <summary>
        /// Parses ListView columns.
        /// <para>Разбирает столбцы ListView.</para>
        /// </summary>
        private static void ParseListViewColumns(string content, DesignerColumnsAnalysis analysis)
        {
            Dictionary<string, string> headerTextByVar = Regex.Matches(
                    content,
                    $@"{OptionalThisPattern}(?<column>\w+)\.Text\s*=\s*""(?<text>[^""]*)"";",
                    RegexOptions.Multiline)
                .Cast<Match>()
                .ToDictionary(
                    m => m.Groups["column"].Value,
                    m => m.Groups["text"].Value,
                    StringComparer.OrdinalIgnoreCase);

            MatchCollection addRangeMatches = Regex.Matches(
                content,
                $@"{OptionalThisPattern}(?<control>\w+)\.Columns\.AddRange\(new {ColumnHeaderArrayPattern}\s*\{{(?<columns>.*?)\}}\);",
                RegexOptions.Singleline);

            foreach (Match match in addRangeMatches)
            {
                string control = match.Groups["control"].Value;
                List<string> columnVars = Regex.Matches(match.Groups["columns"].Value, $@"{OptionalThisPattern}(?<column>\w+)")
                    .Cast<Match>()
                    .Select(m => m.Groups["column"].Value)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList();

                if (columnVars.Count == 0)
                {
                    continue;
                }

                analysis.ListViewColumnVariables[control] = columnVars;

                List<string> headers = new List<string>();
                foreach (string columnVar in columnVars)
                {
                    if (headerTextByVar.TryGetValue(columnVar, out string? header))
                    {
                        headers.Add(header);
                    }
                }

                if (headers.Count > 0)
                {
                    analysis.ListViewHeaderTexts[control] = headers;
                }
            }

            MatchCollection inlineAddMatches = Regex.Matches(
                content,
                $@"{OptionalThisPattern}(?<control>\w+)\.Columns\.Add\(""(?<text>[^""]+)"",",
                RegexOptions.Multiline);

            foreach (Match match in inlineAddMatches)
            {
                string control = match.Groups["control"].Value;
                string text = match.Groups["text"].Value;

                if (!analysis.ListViewHeaderTexts.ContainsKey(control))
                {
                    analysis.ListViewHeaderTexts[control] = new List<string>();
                }

                analysis.ListViewHeaderTexts[control].Add(text);
            }
        }

        /// <summary>
        /// Parses DataGridView columns.
        /// <para>Разбирает столбцы DataGridView.</para>
        /// </summary>
        private static void ParseDataGridViewColumns(string content, DesignerColumnsAnalysis analysis)
        {
            Dictionary<string, string> headerTextByVar = Regex.Matches(
                    content,
                    $@"{OptionalThisPattern}(?<column>\w+)\.HeaderText\s*=\s*""(?<text>[^""]*)"";",
                    RegexOptions.Multiline)
                .Cast<Match>()
                .ToDictionary(
                    m => m.Groups["column"].Value,
                    m => m.Groups["text"].Value,
                    StringComparer.OrdinalIgnoreCase);

            MatchCollection addRangeMatches = Regex.Matches(
                content,
                $@"{OptionalThisPattern}(?<control>\w+)\.Columns\.AddRange\(new {DataGridViewColumnArrayPattern}\s*\{{(?<columns>.*?)\}}\);",
                RegexOptions.Singleline);

            foreach (Match match in addRangeMatches)
            {
                string control = match.Groups["control"].Value;
                List<string> columnVars = Regex.Matches(match.Groups["columns"].Value, $@"{OptionalThisPattern}(?<column>\w+)")
                    .Cast<Match>()
                    .Select(m => m.Groups["column"].Value)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList();

                if (columnVars.Count == 0)
                {
                    continue;
                }

                analysis.DataGridViewColumnVariables[control] = columnVars;

                List<string> headers = new List<string>();
                foreach (string columnVar in columnVars)
                {
                    if (headerTextByVar.TryGetValue(columnVar, out string? header))
                    {
                        headers.Add(header);
                    }
                }

                if (headers.Count > 0)
                {
                    analysis.DataGridViewHeaderTexts[control] = headers;
                }
            }

            MatchCollection indexHeaderAssignments = Regex.Matches(
                content,
                $@"{OptionalThisPattern}(?<control>\w+)\.Columns\[(?<index>\d+)\]\.HeaderText\s*=\s*""(?<header>[^""]+)"";",
                RegexOptions.Multiline);

            foreach (Match match in indexHeaderAssignments)
            {
                string control = match.Groups["control"].Value;
                string header = match.Groups["header"].Value;

                if (!analysis.DataGridViewHeaderTexts.ContainsKey(control))
                {
                    analysis.DataGridViewHeaderTexts[control] = new List<string>();
                }

                analysis.DataGridViewHeaderTexts[control].Add(header);
            }
        }

        #endregion Basic
    }
}
