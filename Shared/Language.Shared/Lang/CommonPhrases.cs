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
 * Summary  : The common phrases for the entire software package
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2015
 * Modified : 2022
 */

#pragma warning disable 1591

namespace Lang
{
    /// <summary>
    /// The common phrases for the entire software package.
    /// <para>Общие фразы для всего программного комплекса.</para>
    /// </summary>
    public static class CommonPhrases
    {
        #region Property

        // Scada.Application
        public static string ProductName { get; private set; } = string.Empty;
        public static string ServerAppName { get; private set; } = string.Empty;
        public static string CommAppName { get; private set; } = string.Empty;
        public static string WebAppName { get; private set; } = string.Empty;
        public static string WebsiteUrl { get; private set; } = string.Empty;
        public static string UnhandledException { get; private set; } = string.Empty;
        public static string ExecutionImpossible { get; private set; } = string.Empty;
        public static string StartLogic { get; private set; } = string.Empty;
        public static string LogicAlreadyStarted { get; private set; } = string.Empty;
        public static string StartLogicError { get; private set; } = string.Empty;
        public static string LogicStopped { get; private set; } = string.Empty;
        public static string UnableToStopLogic { get; private set; } = string.Empty;
        public static string StopLogicError { get; private set; } = string.Empty;
        public static string LogicLoopError { get; private set; } = string.Empty;
        public static string ThreadFatalError { get; private set; } = string.Empty;
        public static string WriteInfoError { get; private set; } = string.Empty;
        public static string ConnectionNotFound { get; private set; } = string.Empty;
        public static string DatabaseNotSupported { get; private set; } = string.Empty;
        public static string OperationNotSupported { get; private set; } = string.Empty;
        public static string CommandSent { get; private set; } = string.Empty;
        public static string SendCommandError { get; private set; } = string.Empty;
        public static string AgentDisabled { get; private set; } = string.Empty;

        // Scada.ConfigDatabase
        public static string UndefinedTable { get; private set; } = string.Empty;
        public static string ArchiveTable { get; private set; } = string.Empty;
        public static string ArchiveKindTable { get; private set; } = string.Empty;
        public static string CnlTable { get; private set; } = string.Empty;
        public static string CnlStatusTable { get; private set; } = string.Empty;
        public static string CnlTypeTable { get; private set; } = string.Empty;
        public static string CommLineTable { get; private set; } = string.Empty;
        public static string DataTypeTable { get; private set; } = string.Empty;
        public static string DeviceTable { get; private set; } = string.Empty;
        public static string DevTypeTable { get; private set; } = string.Empty;
        public static string FormatTable { get; private set; } = string.Empty;
        public static string LimTable { get; private set; } = string.Empty;
        public static string ObjTable { get; private set; } = string.Empty;
        public static string ObjRightTable { get; private set; } = string.Empty;
        public static string QuantityTable { get; private set; } = string.Empty;
        public static string RoleTable { get; private set; } = string.Empty;
        public static string RoleRefTable { get; private set; } = string.Empty;
        public static string ScriptTable { get; private set; } = string.Empty;
        public static string UnitTable { get; private set; } = string.Empty;
        public static string UserTable { get; private set; } = string.Empty;
        public static string ViewTable { get; private set; } = string.Empty;
        public static string ViewTypeTable { get; private set; } = string.Empty;
        public static string IndexNotFound { get; private set; } = string.Empty;
        public static string EntityCaption { get; private set; } = string.Empty;

        // Scada.Files
        public static string FileNotFound { get; private set; } = string.Empty;
        public static string NamedFileNotFound { get; private set; } = string.Empty;
        public static string DirectoryNotExists { get; private set; } = string.Empty;
        public static string PathNotSupported { get; private set; } = string.Empty;
        public static string InvalidFileFormat { get; private set; } = string.Empty;
        public static string LoadConfigError { get; private set; } = string.Empty;
        public static string SaveConfigError { get; private set; } = string.Empty;
        public static string SaveConfigConfirm { get; private set; } = string.Empty;
        public static string LoadViewError { get; private set; } = string.Empty;
        public static string SaveViewError { get; private set; } = string.Empty;

        // Scada.Format
        public static string IntegerRequired { get; private set; } = string.Empty;
        public static string IntegerInRangeRequired { get; private set; } = string.Empty;
        public static string RealRequired { get; private set; } = string.Empty;
        public static string NonemptyRequired { get; private set; } = string.Empty;
        public static string ValidUrlRequired { get; private set; } = string.Empty;
        public static string ValidRangeRequired { get; private set; } = string.Empty;
        public static string DateTimeRequired { get; private set; } = string.Empty;
        public static string NotNumber { get; private set; } = string.Empty;
        public static string NotHexadecimal { get; private set; } = string.Empty;
        public static string InvalidParamVal { get; private set; } = string.Empty;
        public static string InvalidSecretKey { get; private set; } = string.Empty;

        // Scada.Forms
        public static string InfoCaption { get; private set; } = string.Empty;
        public static string QuestionCaption { get; private set; } = string.Empty;
        public static string ErrorCaption { get; private set; } = string.Empty;
        public static string WarningCaption { get; private set; } = string.Empty;
        public static string NoData { get; private set; } = string.Empty;
        public static string EmptyData { get; private set; } = string.Empty;
        public static string CorrectErrors { get; private set; } = string.Empty;
        public static string HiddenPassword { get; private set; } = string.Empty;
        public static string NewConnection { get; private set; } = string.Empty;
        public static string UnnamedConnection { get; private set; } = string.Empty;
        public static string XmlFileFilter { get; private set; } = string.Empty;

        // Scada.ComponentModel
        public static string TrueValue { get; private set; } = string.Empty;
        public static string FalseValue { get; private set; } = string.Empty;
        public static string EmptyValue { get; private set; } = string.Empty;
        public static string CollectionValue { get; private set; } = string.Empty;

        // Scada.CnlDataFormatter
        public static string UndefinedSign { get; private set; } = string.Empty;
        public static string CommandDescrPrefix { get; private set; } = string.Empty;
        public static string StatusFormat { get; private set; } = string.Empty;
        public static string CriticalSeverity { get; private set; } = string.Empty;
        public static string MajorSeverity { get; private set; } = string.Empty;
        public static string MinorSeverity { get; private set; } = string.Empty;
        public static string InfoSeverity { get; private set; } = string.Empty;
        public static string UnknownUser { get; private set; } = string.Empty;

        // Scada.Forms.BitItemCollection
        public static string EventEnabled { get; private set; } = string.Empty;
        public static string EventBeep { get; private set; } = string.Empty;
        public static string DataChangeEvent { get; private set; } = string.Empty;
        public static string ValueChangeEvent { get; private set; } = string.Empty;
        public static string StatusChangeEvent { get; private set; } = string.Empty;
        public static string CnlUndefinedEvent { get; private set; } = string.Empty;
        public static string CommandEvent { get; private set; } = string.Empty;

        #endregion Property

        #region Basic

        static CommonPhrases()
        {
            ProductName = "Rapid SCADA";

            if (Locale.IsRussian)
            {
                UnhandledException = "Необработанное исключение";
                ExecutionImpossible = "Нормальная работа невозможна";
                NamedFileNotFound = "Файл {0} не найден.";
            }
            else
            {
                UnhandledException = "Unhandled exception";
                ExecutionImpossible = "Normal execution is impossible";
                NamedFileNotFound = "File {0} not found.";
            }
        }

        /// <summary>
        /// Initializes the common phrases from locale dictionaries.
        /// </summary>
        public static void Init()
        {
            LocaleDict dict = Locale.GetDictionary("Scada.Application");
            ProductName = dict[nameof(ProductName)];
            ServerAppName = dict[nameof(ServerAppName)];
            CommAppName = dict[nameof(CommAppName)];
            WebAppName = dict[nameof(WebAppName)];
            WebsiteUrl = dict[nameof(WebsiteUrl)];
            UnhandledException = dict[nameof(UnhandledException)];
            ExecutionImpossible = dict[nameof(ExecutionImpossible)];
            StartLogic = dict[nameof(StartLogic)];
            LogicAlreadyStarted = dict[nameof(LogicAlreadyStarted)];
            StartLogicError = dict[nameof(StartLogicError)];
            LogicStopped = dict[nameof(LogicStopped)];
            UnableToStopLogic = dict[nameof(UnableToStopLogic)];
            StopLogicError = dict[nameof(StopLogicError)];
            LogicLoopError = dict[nameof(LogicLoopError)];
            ThreadFatalError = dict[nameof(ThreadFatalError)];
            WriteInfoError = dict[nameof(WriteInfoError)];
            ConnectionNotFound = dict[nameof(ConnectionNotFound)];
            DatabaseNotSupported = dict[nameof(DatabaseNotSupported)];
            OperationNotSupported = dict[nameof(OperationNotSupported)];
            CommandSent = dict[nameof(CommandSent)];
            SendCommandError = dict[nameof(SendCommandError)];
            AgentDisabled = dict[nameof(AgentDisabled)];

            dict = Locale.GetDictionary("Scada.ConfigDatabase");
            UndefinedTable = dict[nameof(UndefinedTable)];
            ArchiveTable = dict[nameof(ArchiveTable)];
            ArchiveKindTable = dict[nameof(ArchiveKindTable)];
            CnlTable = dict[nameof(CnlTable)];
            CnlStatusTable = dict[nameof(CnlStatusTable)];
            CnlTypeTable = dict[nameof(CnlTypeTable)];
            CommLineTable = dict[nameof(CommLineTable)];
            DataTypeTable = dict[nameof(DataTypeTable)];
            DeviceTable = dict[nameof(DeviceTable)];
            DevTypeTable = dict[nameof(DevTypeTable)];
            FormatTable = dict[nameof(FormatTable)];
            LimTable = dict[nameof(LimTable)];
            ObjTable = dict[nameof(ObjTable)];
            ObjRightTable = dict[nameof(ObjRightTable)];
            QuantityTable = dict[nameof(QuantityTable)];
            RoleTable = dict[nameof(RoleTable)];
            RoleRefTable = dict[nameof(RoleRefTable)];
            ScriptTable = dict[nameof(ScriptTable)];
            UnitTable = dict[nameof(UnitTable)];
            UserTable = dict[nameof(UserTable)];
            ViewTable = dict[nameof(ViewTable)];
            ViewTypeTable = dict[nameof(ViewTypeTable)];
            IndexNotFound = dict[nameof(IndexNotFound)];
            EntityCaption = dict[nameof(EntityCaption)];

            dict = Locale.GetDictionary("Scada.Files");
            FileNotFound = dict[nameof(FileNotFound)];
            NamedFileNotFound = dict[nameof(NamedFileNotFound)];
            DirectoryNotExists = dict[nameof(DirectoryNotExists)];
            PathNotSupported = dict[nameof(PathNotSupported)];
            InvalidFileFormat = dict[nameof(InvalidFileFormat)];
            LoadConfigError = dict[nameof(LoadConfigError)];
            SaveConfigError = dict[nameof(SaveConfigError)];
            SaveConfigConfirm = dict[nameof(SaveConfigConfirm)];
            LoadViewError = dict[nameof(LoadViewError)];
            SaveViewError = dict[nameof(SaveViewError)];

            dict = Locale.GetDictionary("Scada.Format");
            IntegerRequired = dict[nameof(IntegerRequired)];
            IntegerInRangeRequired = dict[nameof(IntegerInRangeRequired)];
            RealRequired = dict[nameof(RealRequired)];
            NonemptyRequired = dict[nameof(NonemptyRequired)];
            ValidUrlRequired = dict[nameof(ValidUrlRequired)];
            ValidRangeRequired = dict[nameof(ValidRangeRequired)];
            DateTimeRequired = dict[nameof(DateTimeRequired)];
            NotNumber = dict[nameof(NotNumber)];
            NotHexadecimal = dict[nameof(NotHexadecimal)];
            InvalidParamVal = dict[nameof(InvalidParamVal)];
            InvalidSecretKey = dict[nameof(InvalidSecretKey)];

            dict = Locale.GetDictionary("Scada.Forms");
            InfoCaption = dict[nameof(InfoCaption)];
            QuestionCaption = dict[nameof(QuestionCaption)];
            ErrorCaption = dict[nameof(ErrorCaption)];
            WarningCaption = dict[nameof(WarningCaption)];
            NoData = dict[nameof(NoData)];
            EmptyData = dict[nameof(EmptyData)];
            CorrectErrors = dict[nameof(CorrectErrors)];
            HiddenPassword = dict[nameof(HiddenPassword)];
            NewConnection = dict[nameof(NewConnection)];
            UnnamedConnection = dict[nameof(UnnamedConnection)];
            XmlFileFilter = dict[nameof(XmlFileFilter)];

            dict = Locale.GetDictionary("Scada.ComponentModel");
            TrueValue = dict[nameof(TrueValue)];
            FalseValue = dict[nameof(FalseValue)];
            EmptyValue = dict[nameof(EmptyValue)];
            CollectionValue = dict[nameof(CollectionValue)];

            dict = Locale.GetDictionary("Scada.CnlDataFormatter");
            UndefinedSign = dict[nameof(UndefinedSign)];
            CommandDescrPrefix = dict[nameof(CommandDescrPrefix)];
            StatusFormat = dict[nameof(StatusFormat)];
            CriticalSeverity = dict[nameof(CriticalSeverity)];
            MajorSeverity = dict[nameof(MajorSeverity)];
            MinorSeverity = dict[nameof(MinorSeverity)];
            InfoSeverity = dict[nameof(InfoSeverity)];
            UnknownUser = dict[nameof(UnknownUser)];

            dict = Locale.GetDictionary("Scada.Forms.BitItemCollection");
            EventEnabled = dict[nameof(EventEnabled)];
            EventBeep = dict[nameof(EventBeep)];
            DataChangeEvent = dict[nameof(DataChangeEvent)];
            ValueChangeEvent = dict[nameof(ValueChangeEvent)];
            StatusChangeEvent = dict[nameof(StatusChangeEvent)];
            CnlUndefinedEvent = dict[nameof(CnlUndefinedEvent)];
            CommandEvent = dict[nameof(CommandEvent)];
        }

        #endregion Basic
    }
}
