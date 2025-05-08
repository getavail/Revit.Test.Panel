# Revit WPF Drag and Drop Test Panel

This test add-in demonstrates a potential **drag and drop deadlock issue** in **Revit on Windows 11** when using a **WPF Dockable Panel**. It is intended for diagnostic and reproduction purposes and helps evaluate whether the freezing behavior stems from a compatibility issue between the Revit API and WPF drag-and-drop handling.

## 🔍 Overview

The add-in creates a Dockable Panel that lists Revit Family files from a selected directory. These families can be dragged and dropped into the active project, prompting placement. During repeated use, especially with families saved in older Revit versions (e.g., 2023 families used in Revit 2025), the application begins to freeze intermittently and eventually deadlocks.

## 🛠️ Installation

1. The .addin files supplied assume that the **Revit.Test.Panel.2024.dll** and **Revit.Test.Panel.2025.dll** files exist in the Revit Addins directory (%ProgramData%\Autodesk\Revit\Addins\):

2. Copy the `.addin` manifest files:

Revit.Test.Panel.2024.addin → %ProgramData%\Autodesk\Revit\Addins\2024\
Revit.Test.Panel.2025.addin → %ProgramData%\Autodesk\Revit\Addins\2025\

3. The **Revit.Test.Panel.csproj** file is currently only targeting one version of Revit. To switch to a different version of Revit, comment/uncomment out the relevant `<PropertyGroup>` and `<ItemGroup>` sections.

---

## ▶️ Usage & Reproduction Steps

1. Launch Revit 2024 or 2025 and create a new Project file.
2. If the Panel is not already visible, go to the **Add-Ins** tab and click the **"Revit Test"** button to open it.
3. In the panel, click **"Select Folder"** and choose a folder containing Revit Family (`.rfa`) files.  
⚠️ *Use families saved in an older Revit version (e.g., 2023 content in Revit 2025) so that it upgrades the files.*
4. Drag and drop a Family from the list into the active view.
5. Place the prompted Family Type into the active view.
6. Repeat steps 4–5, occasionally using Revit tools (like drawing a Wall) in between.
7. Continue testing. You may start to notice:
- Slowdown or freezing during drag, click, or place operations.
- A persistent blue spinning cursor when interacting with the canvas.
8. Once freezing starts, click the **"Revit Test"** toolbar button again to trigger a complete application hang.

🛑 When frozen, Revit must be force-closed via Task Manager.

---

## 🧪 Notes

- The issue occurs **intermittently**, but reliably after several drag/drop + placement operations.
- Only appears to affect **Windows 11**, not reproducible on Windows 10.
- The Revit 2024 Version was built targeting .NET Framework 4.8.
- The Revit 2025 Version was built targeting .NET 8.

---

## 📬 Contact / Feedback

This project is part of an effort to report and investigate the problem in collaboration with the Autodesk Developer Network.  
For inquiries or to share your results with us, please open an Issue or contact us directly.

