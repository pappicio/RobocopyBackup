Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Xml.Serialization

Namespace RobocopyBackup
    Public Module Config
        '''  Public Property LangRoot As String
        Public Property Language As String = "it"
        Public Property LogRetention As UShort = 30
        Public Property debug As Boolean = False
        Public Property LogRoot As String
        Public Property Tasks As Dictionary(Of String, Task) = New Dictionary(Of String, Task)()
        Private ReadOnly _configFile As String
        Private _xmlSerializer As XmlSerializer = New XmlSerializer(GetType(SerializedConfig))

        Sub New()
            Dim exeDir As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            ''' Dim progData As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
            Dim progData As String = (exeDir)
            _configFile = Path.Combine(progData, "config.xml")
            '''  LangRoot = Path.Combine(exeDir, "lang")
            LogRoot = Path.Combine(progData, "logs")
            Directory.CreateDirectory(LogRoot)
        End Sub

        Sub Load()


            If File.Exists(_configFile) Then
                serviceLogger.LogConfigLoad()
                Using reader As StreamReader = New StreamReader(_configFile)
                    Dim serializedConfig As SerializedConfig = CType(_xmlSerializer.Deserialize(reader), SerializedConfig)
                    Language = serializedConfig.Language
                    LogRetention = serializedConfig.LogRetention
                    debug = serializedConfig.debug
                    Tasks = serializedConfig.Tasks.ToDictionary(Function(task) task.Guid, Function(task) task)
                End Using
            Else
                Config.debug = True
                serviceLogger.LogConfignotLoad()

            End If
            If Config.debug Then
                serviceLogger.LogConfig()
            End If

        End Sub

        Sub Save()
            Dim serializedConfig As SerializedConfig = New SerializedConfig() With {
                .Language = Language,
                .LogRetention = LogRetention,
                .debug = debug,
                .Tasks = Tasks.Values.ToArray()
            }

            Using writer As StreamWriter = New StreamWriter(_configFile)
                _xmlSerializer.Serialize(writer, serializedConfig)
            End Using
        End Sub

        <Serializable>
        Public Structure SerializedConfig
            Public Language As String
            Public debug As Boolean
            Public LogRetention As UShort
            Public Tasks As Task()
        End Structure
    End Module
End Namespace

