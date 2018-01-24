# PassMaster
Simple password manager GUI written in C# (with Basic Encryption), intended as an example.

**Master Password:**  admin

>***Warning!*** 
>This program is meant as an **example** and should not be used without appropriate modification. (see **Current Issues**)

### Screenshot
![](https://i.imgur.com/aBiK2GX.png)


## Features


## Current Issues:
- The Master Password cannot be changed, its default is "admin".
- If the encrypted file containing the master password is deleted, it cannot be recreated without modifying the code and recompiling
- Instant Search ***may*** not perform well with large volumes of password entries (untested)
