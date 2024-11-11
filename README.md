## Description

*RobocopyBackup* is a Windows service for orchestrating file backups via `robocopy` utility. The GUI aims to provide user-friendly interface for the backup tasks management without the need to understand the `robocopy` utility itself. The service runs with *Local System* privileges, allowing it to work independently on the GUI user or on any interactive session. The service manages scheduling, logging and retention of the backups.

The goal of *RobocopyBackup* is not to be robust, secure and enterprise-ready, but to be small, simple and lower the difficulty of creating and scheduling `robocopy` commands.




## Acknowledgments
This project is totally traslated into VB .NET, the original ones is written in C#
