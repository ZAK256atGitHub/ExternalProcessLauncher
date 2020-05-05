# External Process Launcher
**English:** This program demonstrates how to start external processes from a C# Windows Forms application. It can run any executable file, for example CMD commands like DIR (using CMD.EXE /C), command line programs like PING.EXE or complete applications like NOTEPAD.EXE.

**Deutsch:** Dieses Programm demonstriert, wie externe Prozesse von einer C# Windows Forms-Anwendung aus gestartet werden können. Es kann jede beliebige ausführbare Datei ausführen, zum Beispiel CMD-Befehle wie DIR (unter Verwendung von CMD.EXE /C), Befehlszeilenprogramme wie PING.EXE oder komplette Anwendungen wie NOTEPAD.EXE.

topics: C# Windows-Forms Execute Run Start EXE Executable CMD-Command External-Process Background-Worker Command-Line Redirect-Standard-Output Output-Data-Received Thread Runner Launcher Starter

## Contributing
Please refer to each project's style guidelines and guidelines for submitting patches and additions. In general, we follow the "fork-and-pull" Git workflow.

 1. **Fork** the repo on GitHub
 2. **Clone** the project to your own machine
 3. **Commit** changes to your own branch
 4. **Push** your work back up to your fork
 5. Submit a **Pull request** so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

## License
External Process Launcher is licensed under the [MIT license](LICENSE).

## English: Features / problem solutions

### The user interface accepts input during process execution
To avoid blocking the user interface, the processes are executed in a separate thread.
The BackgroundWorker class is used for this.
#### Links
- [BackgroundWorker Class](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.backgroundworker?redirectedfrom=MSDN&view=netframework-4.5.2)

### A process can be aborted
In order for a running process to be terminated, the user interface must be able to accept input.
The running process can be aborted using the **Abort** button.
If the button **Abort** is pressed, the **CancelAsync** method of the BackgroundWorker is called.
This sets the CancellationPending property of the BackgroundWorker.
The Process.WaitForExit(Int32) method is used to wait for the termination of the process to be executed.
It always waits 5 milliseconds, then it checks whether the BackgroundWorker was aborted.
If the BackgroundWorker was aborted, the running process is stopped immediately.
Then the asynchronous read operation on the redirected  StandardOutput stream and StandardError-Stream is aborted.

#### Links
- [BackgroundWorker.CancelAsync Method](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.backgroundworker.cancelasync?view=netframework-4.5.2)
- [Process.WaitForExit(Int32) Method](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexit?view=netframework-4.5.2#System_Diagnostics_Process_WaitForExit_System_Int32_)
- [BackgroundWorker.CancellationPending Property](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.backgroundworker.cancellationpending?view=netframework-4.5.2)
- [Process.Kill Method](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.kill?redirectedfrom=MSDN&view=netframework-4.5.2#System_Diagnostics_Process_Kill)
- [Process.CancelErrorRead Method](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.cancelerrorread?redirectedfrom=MSDN&view=netframework-4.5.2#System_Diagnostics_Process_CancelErrorRead)
- [Process.CancelOutputRead Method](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.canceloutputread?redirectedfrom=MSDN&view=netframework-4.5.2#System_Diagnostics_Process_CancelOutputRead)

### Display of the progress
For progress to be displayed, the user interface must be able to accept input.
Since the progress of a process cannot be determined exactly, a progress bar is used with the style Marquee.

### The output of the process is redirected
RedirectStandardOutput
RedirectStandardError

### The output of the process is displayed in real time
ErrorDataReceived --> EventHandler
OutputDataReceived --> EventHandler
Process.WaitForExit

### The processing time is displayed.
StopWatch --> Running Time

### It always scroll to the last output of the RichTextBox
ScrollToBottom (RichTextBox)

### The RichTextBox does not display a cursor
HideCaret (RichTextBox)
### The RichTextBox text can be displayed in color and bold
Color FontStyle (RichTextBox)

### The execution of a CMD command is possible
To execute a CMD command the CMD.exe with the parameter /C must be used.
For example 'CMD.EXE /C dir'

## Deutsch: Merkmale / Problemlösungen

### Die Benutzerschnittstelle akzeptiert Eingaben während der Prozessausführung
Um eine Blockierung der Benutzerschnittstelle zu vermeiden, werden die Prozesse in einem separaten Thread ausgeführt.
Dazu wird die Klasse BackgroundWorker verwendet.

#### Verweise
- [BackgroundWorker Klasse](https://docs.microsoft.com/de-de/dotnet/api/system.componentmodel.backgroundworker?redirectedfrom=MSDN&view=netframework-4.5.2)

### Ein Prozess kann abgebrochen werden
Damit ein laufender Prozess beendet werden kann, muss die Benutzeroberfläche in der Lage sein, Eingaben zu akzeptieren.
Der laufende Prozess kann mit der Schaltfläche **Abort** abgebrochen werden.
Wird auf die Schaltfläche **Abort** gedrückt, dann wird die **CancelAsync** Methode des BackgroundWorker aufgerufen.
Dadurch wird die CancellationPending-Eigenschaft des BackgroundWorker festgelegt.
Die Methode Process.WaitForExit(Int32) wird verwendet, um auf die Beendigung des auszuführenden Prozesses zu warten.
Es wird immer 5 Millisekunden gewartet, dann wird geprüft, ob der BackgroundWorker abgebrochen wurde.
Wenn der BackgroundWorker abgebrochen wurde, wird der laufende Prozess sofort gestoppt.
Dann wird der asynchrone Lesevorgang für den umgeleiteten StandardOutput-Stream und StandardError-Stream abgebrochen.
#### Verweise
- [BackgroundWorker.CancelAsync Methode](https://docs.microsoft.com/de-de/dotnet/api/system.componentmodel.backgroundworker.cancelasync?view=netframework-4.5.2)
- [Process.WaitForExit(Int32) Methode](https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.process.waitforexit?view=netframework-4.5.2#System_Diagnostics_Process_WaitForExit_System_Int32_)
- [BackgroundWorker.CancellationPending Eigenschaft](https://docs.microsoft.com/de-de/dotnet/api/system.componentmodel.backgroundworker.cancellationpending?view=netframework-4.5.2)
- [Process.Kill Methode](https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.process.kill?redirectedfrom=MSDN&view=netframework-4.5.2#System_Diagnostics_Process_Kill)
- [Process.CancelErrorRead Methode](https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.process.cancelerrorread?redirectedfrom=MSDN&view=netframework-4.5.2#System_Diagnostics_Process_CancelErrorRead)
- [Process.CancelOutputRead Methode](https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.process.canceloutputread?redirectedfrom=MSDN&view=netframework-4.5.2#System_Diagnostics_Process_CancelOutputRead)

### Anzeige des Fortschritts
Damit der Fortschritt angezeigt werden kann, muss die Benutzeroberfläche in der Lage sein, Eingaben zu akzeptieren.
Da der Fortschritt eines Prozesses nicht genau bestimmt werden kann, wird ein Fortschrittsbalken mit dem Style Marquee verwendet.

### Die Ausgabe des Prozesses wird umgeleitet

### Die Ausgabe des Prozesses wird in Echtzeit angezeigt

### Die Bearbeitungszeit wird ausgegeben

### Es wird immer bis zur letzten Ausgabe der RichTextBox gescrollt

### Das RichTextBox zeigt keinen Cursor an

### Der RichTextBox-Text kann farbig und fett dargestellt werden

### Das Ausführen eines CMD-Befehls ist möglich
Um einen CMD-Befehl auszuführen, muss die CMD.exe mit dem Parameter /C verwendet werden.
Zum Beispiel 'CMD.EXE /C dir'



-----------------------------------------------------------------------------
Invoke ->  delegate -> output
-----------------------------------------------------------------------------


