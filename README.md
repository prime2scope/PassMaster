# PassMaster
Simple password manager GUI written in C# (with Basic Encryption), intended as an example.

**Master Password:**  admin

>***Warning!*** 
>This program is meant as an **example** and should not be used as anything else - without appropriate modification. (see **Current Issues** below)

### Screenshot
![](https://i.imgur.com/aBiK2GX.png)


## Features
1. **Instant Search** as you type (case sensitive)
     - Searches **Website**, and **Usernames**
     - only searches **Password** when "Show Passwords" is checked
2. **Cell Entry Modification** with confirmation
     - Select the cell you want to edit, and click **"Edit Cell"** from the **Options** menu at the bottom
     - Changes **must** be confirmed by pressing enter (user will be prompted if they attempt to leave the cell without pressing enter)
     - Cells do not require any specific format, but ***must not be blank***
3. **Add New Entries**, or **Delete** old ones
     - Select **"Add New+"** from the **options** menu, a new row will be added
     - Changes **must** be confirmed by pressing enter (user will be prompted if they attempt to leave the each cell without pressing enter)
     - Cells do not require any specific format, but ***must not be blank***
4. **Hover Password** and **Show all passwords**
     - hover the mouse pointer over a password to *peak* at it for a short time, or show all passwords if needed
5. **Save/Unsaved Confirmation**
     - No changes will be **saved** unless **save** is pressed, exit with **unsaved** changes prompts a confirmation

## Current Issues:
- The Master Password cannot be changed, its default is "admin".
- If the encrypted file containing the master password is deleted, it cannot be recreated without modifying the code and recompiling
- Instant Search ***may*** not perform well with large volumes of password entries (untested)
