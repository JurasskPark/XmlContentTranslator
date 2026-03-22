namespace DebugerLog
{
    /// <summary>
    /// Transfers debug messages to subscribers.
    /// <para>Передает сообщения отладчика подписчикам.</para>
    /// </summary>
    internal class DebugerReturn
    {
        #region Variable

        /// <summary>
        /// Delegate for debug message handlers.
        /// </summary>
        public delegate void DebugData(string msg);

        /// <summary>
        /// Callback for receiving debug messages.
        /// </summary>
        public static DebugData? OnDebug = null;

        #endregion Variable

        #region Basic

        /// <summary>
        /// Sends the message to subscribers.
        /// </summary>
        internal void DebugerLog(string text)
        {
            OnDebug?.Invoke(text);
        }

        /// <summary>
        /// Sends the log message.
        /// </summary>
        public void Log(string text)
        {
            DebugerLog(text);
        }

        #endregion Basic
    }
}
