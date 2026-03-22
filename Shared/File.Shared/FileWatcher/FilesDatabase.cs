namespace FileOperations
{
    /// <summary>
    /// Represents file information stored in the watcher database.
    /// <para>Представляет сведения о файле, хранящиеся в базе наблюдателя.</para>
    /// </summary>
    public class FilesDatabase
    {
        #region Property

        public string NameFile { get; set; }

        public string PathFile { get; set; }

        public DateTime LastTimeChanged { get; set; }

        public long SizeFile { get; set; }

        public int NumberLines { get; set; }

        public bool Parsed { get; set; }

        public int Status { get; set; }

        public string StatusString { get; set; }

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FilesDatabase()
        {
            NameFile = string.Empty;
            PathFile = string.Empty;
            StatusString = string.Empty;
            LastTimeChanged = DateTime.MinValue;
        }

        /// <summary>
        /// Loads rows from the database file.
        /// </summary>
        public static List<FilesDatabase> LoadDB(string path)
        {
            List<FilesDatabase> rows = new List<FilesDatabase>();

            using StreamReader streamReader = new StreamReader(path);
            while (!streamReader.EndOfStream)
            {
                string? line = streamReader.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                string[] parameters = line.Split("|", StringSplitOptions.None);
                if (parameters.Length >= 6)
                {
                    rows.Add(ParseRow(parameters));
                }
            }

            return rows;
        }

        /// <summary>
        /// Gets file paths from the specified rows.
        /// </summary>
        public static List<string> ListPathFiles(List<FilesDatabase> list)
        {
            List<string> result = new List<string>();
            foreach (FilesDatabase file in list)
            {
                result.Add(file.PathFile);
            }

            return result;
        }

        /// <summary>
        /// Adds a row to the database file.
        /// </summary>
        public static void AddRow(string path, FilesDatabase row)
        {
            using StreamWriter streamWriter = File.AppendText(Path.Combine(path));
            streamWriter.WriteLine(ToDbLine(row));
        }

        /// <summary>
        /// Deletes a row from the database file.
        /// </summary>
        public static void DeleteRow(string path, string pathFile)
        {
            string tempFile = path.Replace(".db", ".tmp");

            using StreamReader streamReader = new StreamReader(path);
            using StreamWriter streamWriter = new StreamWriter(tempFile);

            string? line;
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] parameters = line.Split("|", StringSplitOptions.None);
                if (parameters.Length < 6)
                {
                    continue;
                }

                FilesDatabase row = ParseRow(parameters);
                if (row.PathFile != pathFile)
                {
                    streamWriter.WriteLine(ToDbLine(row));
                }
            }

            File.Delete(path);
            File.Move(tempFile, path);
        }

        /// <summary>
        /// Selects a row by file path.
        /// </summary>
        public static FilesDatabase? SelectRow(string path, string pathFile)
        {
            using StreamReader streamReader = new StreamReader(path);
            while (!streamReader.EndOfStream)
            {
                string? line = streamReader.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                string[] parameters = line.Split("|", StringSplitOptions.None);
                if (parameters.Length < 6)
                {
                    continue;
                }

                FilesDatabase dbLine = ParseRow(parameters);
                if (dbLine.PathFile == pathFile)
                {
                    return dbLine;
                }
            }

            return null;
        }

        /// <summary>
        /// Updates a row in the database file.
        /// </summary>
        public static void UpdateRow(string path, FilesDatabase rowUpdate)
        {
            string tempFile = path.Replace(".db", ".tmp");

            using StreamReader streamReader = new StreamReader(path);
            using StreamWriter streamWriter = new StreamWriter(tempFile);

            string? line;
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] parameters = line.Split("|", StringSplitOptions.None);
                if (parameters.Length < 6)
                {
                    continue;
                }

                FilesDatabase row = ParseRow(parameters);
                streamWriter.WriteLine(row.PathFile != rowUpdate.PathFile ? ToDbLine(row) : ToDbLine(rowUpdate));
            }

            File.Delete(path);
            File.Move(tempFile, path);
        }

        /// <summary>
        /// Creates file database entry from the specified file path.
        /// </summary>
        public static FilesDatabase AddFilePath(string pathFile)
        {
            return new FilesDatabase
            {
                PathFile = pathFile,
                NameFile = Path.GetFileName(pathFile),
            };
        }

        #endregion Basic

        #region Support methods

        /// <summary>
        /// Parses a database row.
        /// </summary>
        private static FilesDatabase ParseRow(string[] parameters)
        {
            FilesDatabase row = new FilesDatabase();

            try { row.PathFile = parameters[0]; } catch { row.PathFile = string.Empty; }
            try { row.LastTimeChanged = DateTime.Parse(parameters[1]); } catch { row.LastTimeChanged = DateTime.MinValue; }
            try { row.SizeFile = Convert.ToInt32(parameters[2]); } catch { row.SizeFile = 0; }
            try { row.NumberLines = Convert.ToInt32(parameters[3]); } catch { row.NumberLines = 0; }
            try { row.Parsed = Convert.ToBoolean(parameters[4]); } catch { row.Parsed = false; }
            try { row.Status = Convert.ToInt32(parameters[5]); } catch { row.Status = 3; }

            row.NameFile = Path.GetFileName(row.PathFile);
            return row;
        }

        /// <summary>
        /// Converts a row to database line.
        /// </summary>
        private static string ToDbLine(FilesDatabase row)
        {
            return $@"{row.PathFile}|{row.LastTimeChanged}|{row.SizeFile}|{row.NumberLines}|{row.Parsed}|{row.Status}";
        }

        #endregion Support methods
    }
}
