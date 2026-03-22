using DebugerLog;
using Lang;
using System.Runtime.InteropServices;

namespace FileOperations
{
    /// <summary>
    /// Provides file search and change tracking helpers.
    /// <para>Предоставляет вспомогательные методы поиска файлов и отслеживания изменений.</para>
    /// </summary>
    public class FileWithChanges : IDisposable
    {
        #region Variable

        private readonly object obj = new object();
        private IntPtr bufferPtr = IntPtr.Zero;
        private bool disposed;

        private static List<FilesDatabase> lstFiles = new List<FilesDatabase>();
        private static List<string> lstFolders = new List<string>();

        #endregion Variable

        #region Property

        public int BUFFER_SIZE = 1024 * 1024 * 50;

        #endregion Property

        #region Basic

        /// <summary>
        /// Finalizes an instance of the class.
        /// </summary>
        ~FileWithChanges()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets a list of files from a directory.
        /// </summary>
        public static List<FilesDatabase> SearchFiles(string path, bool useSubDir = false)
        {
            try
            {
                lstFiles = new List<FilesDatabase>();

                if (!useSubDir)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                    FileInfo[] files = dirInfo.GetFiles();

                    foreach (FileInfo fileInfo in files)
                    {
                        FilesDatabase file = new FilesDatabase
                        {
                            PathFile = fileInfo.FullName,
                            SizeFile = fileInfo.Length,
                            LastTimeChanged = fileInfo.LastWriteTime,
                        };

                        lstFiles.Add(file);
                    }

                    return lstFiles;
                }

                return IterateSortFoldersFiles(path);
            }
            catch (Exception ex)
            {
                WriteSearchError(ex);
                return new List<FilesDatabase>();
            }
        }

        #endregion Basic

        #region Support methods

        /// <summary>
        /// Releases resources.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            lock (obj)
            {
                if (disposed)
                {
                    return;
                }

                if (bufferPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(bufferPtr);
                    bufferPtr = IntPtr.Zero;
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Gets files recursively.
        /// </summary>
        private static List<FilesDatabase> IterateSortFoldersFiles(string dir)
        {
            try
            {
                lstFiles = new List<FilesDatabase>();
                lstFolders = new List<string> { dir };

                return IterateSortFiles(IterateSortFolders(dir));
            }
            catch (Exception ex)
            {
                WriteSearchError(ex);
                lstFiles = new List<FilesDatabase>();
                return lstFiles;
            }
        }

        /// <summary>
        /// Gets all subfolders recursively.
        /// </summary>
        private static List<string> IterateSortFolders(string dir)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                foreach (DirectoryInfo tmpDir in dirInfo.GetDirectories())
                {
                    lstFolders.Add(tmpDir.FullName);
                    IterateSortFolders(tmpDir.FullName);
                }

                return lstFolders;
            }
            catch (Exception ex)
            {
                WriteSearchError(ex);
                lstFolders = new List<string>();
                return lstFolders;
            }
        }

        /// <summary>
        /// Gets files from the specified folder list.
        /// </summary>
        private static List<FilesDatabase> IterateSortFiles(List<string> folders)
        {
            try
            {
                foreach (string dir in folders)
                {
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(dir);
                        FileInfo[] files = dirInfo.GetFiles();

                        foreach (FileInfo fileInfo in files)
                        {
                            FilesDatabase file = new FilesDatabase
                            {
                                PathFile = fileInfo.FullName,
                                SizeFile = fileInfo.Length,
                                LastTimeChanged = fileInfo.LastWriteTime,
                            };

                            lstFiles.Add(file);
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteSearchError(ex);
                    }
                }

                return lstFiles;
            }
            catch (Exception ex)
            {
                WriteSearchError(ex);
                return lstFiles;
            }
        }

        /// <summary>
        /// Writes a search error to the log.
        /// </summary>
        private static void WriteSearchError(Exception ex)
        {
            string errMsg = ex.Message;
            Debuger.Log(
                Locale.IsRussian
                    ? @$"[Ошибка] {errMsg}"
                    : @$"[Error] {errMsg}");
        }

        #endregion Support methods
    }
}
