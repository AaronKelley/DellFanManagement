;this is a comment

#SingleInstance Force
SetWorkingDir %A_ScriptDir%

;get admin rights
if not A_IsAdmin
	Run *RunAs "%A_ScriptFullPath%" ; (A_AhkPath is usually optional if the script has the .ahk extension.) You would typically check  first.

;set array for profiles
ProfileArray := ["Optimized", "Cool", "Quiet", "UltraPerformance"]

;radio options with Optimized as default
Gui, Add, Radio, Checked x22 y19 w220 h30 vProfile, Optimized
Gui, Add, Radio, x22 y49 w220 h30 , Cool
Gui, Add, Radio, x22 y79 w220 h30 , Quiet
Gui, Add, Radio, x22 y109 w220 h30 , Ultra Performance
Gui, Add, Button, Default x82 y149 w110 h20 , OK
Gui, Show, , Dell Thermal Profile
Return

;do nothing if escape or close button are pressed
GuiEscape:
GuiClose:
ExitApp


ButtonOK:
Gui, Submit  
SelectedProfile := ProfileArray[Profile]
;Decomment the line below for a feedback message box
;MsgBox You entered: %SelectedProfile%
Run, "C:\tools\fan\DellSetThermalSetting-2.1\DellSetThermalSetting.exe" %SelectedProfile%, , Hide
ExitApp