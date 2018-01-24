/***************************************************************
PassMaster - Password Manager

Date: 11/27/2017

Programmer: Ben Stockwell

Purpose: A Password Manager with basic encryption and instant 
         search as you type

***************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace PassMaster
{
    public partial class PassMasterForm : Form
    {
        #region Global Variables
        // File Names
        String SetupFile = "pm_setup.pm";
        String DataFile = "pm_other.pm";
        String TSetupFile = "pm_setup_temp.pm";
        String TDataFile = "pm_other_temp.pm";
        String MasterPW = "";

        List<PWEntry> pwList = new List<PWEntry>();         // Holds the original list from the file
        List<PWEntry> Temp_pwList = new List<PWEntry>();    // Holds the temporary list, that may contain edits
        List<PWEntry> SearchPWList = new List<PWEntry>();   // Holds the list of Search Results after a search
        List<String> DeletePWList = new List<String>();     // Holds a list of Primary Keys that have been deleted during a search

        bool UsingSrch = false;     // Flag to indicate if the Search Results array is being used in the data grid
        bool ShowPasswords = false; // Flag set when All pass words are shown
        bool valSent = false;       // Flag to prevent validation looping
        bool waitEnter = false;     // Flag to indicate if the user has pressed Enter yet, after editing/adding new
        bool isUnsaved = false;     // Flag to indicate if there are unsaved changes

        int PrevCol = 0; // Used for Highlighting the selected cell

        int NewCellEdit = -1;   // Used to keep track of the new cell Column being edited
        int OldCellEdit = -1;   // Used to keep track of the cell being edited

        int skipRow = -1; // Indicated the row which should not have its value formated, just before the user edits it

        #endregion

        #region Form Initialization Functions
        public PassMasterForm()
        {

            InitializeComponent();

            // Allow key preview
            this.KeyPreview = true;
        }

        private void PassMasterForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Uncomment to encrypt Setup and Data files
                //EncryptFile(TSetupFile, SetupFile);
                //EncryptFile(TDataFile, DataFile);

                //File.Delete(TSetupFile);
                //File.Delete(TDataFile);

                // Handle the master password and initial setup
                InitSetupFile();

                //SetupMain(); // Uncomment this to avoid entering a Password each time

            }
            catch(Exception ex)
            {
                Console.WriteLine("Error - Exception - " + ex);
            }
        }
        #endregion

        #region Setup File Functions for Master Password
        // Create/Read the setup file for the Master PW
        private void InitSetupFile()
        {
            //// Check if the files exists, if not, make it
            //// Only intended for use by programmer, Master PW cannot currently be changed
            //if (!File.Exists(SetupFile))
            //{
            //    CreateSetup();
            //}

            // Read the file for the master pw, kind of redundant since we just made it
            ReadSetup();

        }

        // Used to Create the Setup file with Master PW
        // Master PW cannot be changed currently, this program should not be used without allowing Users to change it
        // if the Master PW is lost because the encrypted file is deleted, then the program will no longer function
        // this function is only intended for Debug purposes by the programmer
        /*
        private void CreateSetup()
        {
            // Write to the file
            using (StreamWriter SW = new StreamWriter(TSetupFile))
            {
                SW.WriteLine("admin");
            }

            // Encrypt the file
            EncryptFile(TSetupFile, SetupFile);
            File.Delete(TSetupFile); // delete temp file
        }
        */

        // Used to read the setup file and get Master PW
        private void ReadSetup()
        {
            // if the Master PW file is mission, then the program should exit after showing an error
            if (!File.Exists(SetupFile))
            {
                // Show error box
                MessageBox.Show("File containing Master PW is missing, cannot continue", "Error");

                // Exit the program
                Application.Exit();

                // ensure this function doesn't continue
                return;
            }

            // Decrypt the file before reading
            //File.Decrypt(SetupFile);
            DecryptFile(SetupFile, TSetupFile);

            // Read from the file
            using (StreamReader SR = new StreamReader(TSetupFile))
            {
                String S = SR.ReadLine();

                // Read the master PW, no loop needed, should be first line
                if (S != null)
                {
                    if (MasterPW == "" && S != "")
                    {
                        MasterPW = S;
                    }
                }// end if

            } // end of using for StreamReader

            // Delete the decrypted file
            File.Delete(TSetupFile);
        }

        #endregion

        #region Initial Password Entry Functions and Setup for Main Form
        // Handles the when the User presses enter while typing in the textbox
        private void MasterPWbox_KeyDown(object sender, KeyEventArgs e)
        {
            // If the enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {
                // If the password is NOT blank, and matches the master PW
                if (MasterPWbox.Text != "" && MasterPWbox.Text == MasterPW)
                {
                    // Call the setup function
                    SetupMain();
                }
                else
                {
                    // Show error box
                    MessageBox.Show("Incorrect Password", "Error");
                }
            }
        }

        // Set up the main GUI area for user interaction
        private void SetupMain()
        {
            // Remove the PW Prompt and Textbox
            MasterPWbox.Dispose();
            PW_Prompt.Dispose();

            // Set the background to Green
            ColorStatusPanel.BackColor = Color.FromArgb(43, 242, 36);

            // If the file exists, read it, otherwise make a new file
            if (File.Exists(DataFile))
                ReadEntryList();
            else
            {
                MessageBox.Show("No Password file found!" + "\n Creating a new, empty file.");

                // Make a new blank file
                NewPWDataFile();
            }

            // Show the main panel
            MainPanel1.Visible = true;

            // Fill the DataGrid with all the entries from the file
            FillDataGrid(pwList);

        }

        // Used to create a new Password file, it will be empty
        private void NewPWDataFile()
        {

            // Write to the file, it will be blank
            using (StreamWriter SW = new StreamWriter(TDataFile))
            {
                SW.WriteLine();
            }

            // Encrypt the file
            EncryptFile(TDataFile, DataFile);
            File.Delete(TDataFile); // delete temp file
        }

        // Get the password entries from the file
        private void ReadEntryList()
        {
            DecryptFile(DataFile, TDataFile);

            // Read from the file
            using (StreamReader SR = new StreamReader(TDataFile))
            {
                String S = SR.ReadLine();

                // Loop through the file reading lines
                while (S != null)
                {
                    String[] eAr = S.Split(new Char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    PWEntry pwe = new PWEntry();
                    
                    // If there are 3 parts to the string
                    if (eAr.Length == 3)
                    {
                        // Get the parts of the PW entry
                        pwe.Site = eAr[0];
                        pwe.User = eAr[1];
                        pwe.Pass = eAr[2];
                    }
                    else
                    {
                        Console.WriteLine(S);
                        // Show Errors 
                        Console.WriteLine("Password File Error, line with wrong number of arguments " + eAr.Count());

                        MessageBox.Show("Password File Error, line with wrong number of arguments " + eAr.Count(), "ERROR with Password File");

                        // Throw an exception for debugging
                        //Exception e = new IndexOutOfRangeException();
                        //throw e;
                    }
                    Console.WriteLine(S);
                    // Add the pw entry to the list
                    pwList.Add(pwe);

                    Temp_pwList.Add(pwe); // add to the temporary as well, used for searching

                    // Get a new line
                    S = SR.ReadLine();

                }// end of while
            } // end of using for StreamReader

            // Delete the decrypted file
            File.Delete(TDataFile);
        }

        // This function formats the Password cells to show * instead of actual characters
        private void PWListGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Get call stack, could be used to make a condition for the replacement maybe
            //StackTrace s = new StackTrace();

            // If the column is the password column, and not blank, and not ShowPasswords
            if (PWListGrid.Columns[e.ColumnIndex].Index == 2 && e.Value != null && !ShowPasswords && e.Value as String != "")
            {
                // If this is not the skip Row
                if (e.RowIndex != skipRow)
                {
                    // Store the value in the Cells Tag
                    //PWListGrid.Rows[e.RowIndex].Tag = e.Value;
                    PWListGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = e.Value;

                    // Show a string of 10 star characters
                    e.Value = new String('*', 10);
                }
            }
        }

        #endregion

        #region Form Event Handlers

            #region Options

                #region Buttons
        // Function for the Options Button
        // Deals with showing/toggling the Option panel
        private void OptionsButton_Click(object sender, EventArgs e)
        {
            // If the Option panel is visible, hide it, else, show it
            if(OptionPanel.Visible == true)
            {
                OptionPanel.Visible = false;
            }
            else
            {
                OptionPanel.Visible = true;
            }

        }

        // Add NEW button
        private void AddButton_Click(object sender, EventArgs e)
        {
            // Set the flag for unsaved changes
            isUnsaved = true;

            // If there isn't already a NEW row, make one and start editing it
            if (NewCellEdit == -1 && OldCellEdit == -1)
            {
                // Change the outline color
                ColorStatusPanel.BackColor = SystemColors.Highlight;

                // if a cell is being edited, end the edit
                if (PWListGrid.IsCurrentCellInEditMode)
                    PWListGrid.EndEdit();

                // Add a new row
                string[] row = { "", "", ""};
                PWListGrid.Rows.Add(row);

                // Set the currently selected Row
                PWListGrid.Rows[PWListGrid.Rows.Count - 1].Selected = true;
                PWListGrid.Rows[PWListGrid.Rows.Count - 1].Tag = Guid.NewGuid().ToString("N");
                //PWListGrid.Rows[PWListGrid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGreen;

                // Set the Cell
                PWListGrid.CurrentCell = PWListGrid.Rows[PWListGrid.Rows.Count - 1].Cells[0];

                // Set to Begin Edit
                PWListGrid.BeginEdit(true);

                // Set the flags
                NewCellEdit = 1;
                waitEnter = true;
            }
            
        }

        // Edit Selected button
        private void EditButton_Click(object sender, EventArgs e)
        {
            // If there are actually Rows to edit
            if (PWListGrid.Rows.Count != 0)
            {
                // Set the flag for unsaved changes
                isUnsaved = true;

                // If there isn't something being edited or added, start editing selected cell
                if (OldCellEdit == -1 && NewCellEdit == -1)
                {
                    // Change the outline color
                    ColorStatusPanel.BackColor = SystemColors.Highlight;

                    // Change the cell to allow editing
                    PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].ReadOnly = false;

                    // Used to change row formatting for Password, so the value is stored and not the place holder characters
                    if (PWListGrid.CurrentCell.ColumnIndex == 2)
                    {
                        // Set the flag to skip this row in formatting, then refresh all rows
                        skipRow = PWListGrid.CurrentCell.RowIndex;
                        PWListGrid.Refresh();
                    }

                    // Set to Begin Edit the cell
                    PWListGrid.BeginEdit(true);

                    OldCellEdit = 1; // set the flag 
                    waitEnter = true;
                }
            
            }
            else
            {
                // Show error
                MessageBox.Show("There are no entries to edit, add something first", "ERROR - Nothing to edit");
            }
            
        }

        // Delete Selected button
        private void DeleteButton_Click(object sender, EventArgs e)
        {

            // If there are rows to delete, and we arent editing or adding new already
            if (PWListGrid.Rows.Count != 0 && OldCellEdit == -1 && NewCellEdit == -1)
            {
                // Set the flag for unsaved changes
                isUnsaved = true;

                // Change the cell to allow editing
                PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].ReadOnly = false;

                // If there is a Row Selected
                if (this.PWListGrid.SelectedRows.Count > 0)
                {
                    // If searching, save the ID of the deleted entry
                    if (UsingSrch)
                        DeletePWList.Add(PWListGrid.SelectedRows[0].Tag as String);

                    // Remove it
                    PWListGrid.Rows.RemoveAt(this.PWListGrid.SelectedRows[0].Index);
                }

                // Update the temp list
                RefreshTempList();
            }
            else
            {
                // Show error box
                MessageBox.Show("There are no entries to delete, add something first", "ERROR - Nothing to delete");
            }
        }

                #endregion // end Buttons

                #region Cell and Row Edit/Leave/Validate Events
        // This function is used in conjunction with others to handle when cells are being edited
        // It also resets the ReadOnly Property for cells after editing is complete
        private void PWListGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Console.WriteLine("Cell END - Coming from " + (NewCellEdit - 1) + "," + (PWListGrid.Rows.Count - 1));
            
            // if the cell count is 3 or more, reset it and make the row Read Only
            if (NewCellEdit > 3)
            {
                // Make the New Row Readonly
                PWListGrid.Rows[PWListGrid.Rows.Count - 1].ReadOnly = true;

                // Update the temp list
                RefreshTempList();

                //// Reset the row skip flag
                //skipRow = -1;
                //
                //PWListGrid.Refresh();

                // Reset the next Edit cell
                NewCellEdit = -1;

                // Set the background to Green
                ColorStatusPanel.BackColor = Color.FromArgb(43, 242, 36);
            }

            // If they are in Edit Mode
            if (OldCellEdit != -1)
            {
                // Change the Row to be read only and set editing flag to false
                PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].ReadOnly = true;

                // Update the temp list
                RefreshTempList();

                // Reset the row skip flag
                skipRow = -1;

                //PWListGrid.Refresh();

                OldCellEdit = -1; // Reset the flag

                // Set the background to Green
                ColorStatusPanel.BackColor = Color.FromArgb(43, 242, 36);

            }

            // If the user presses enter without typing anything
            // it will Cause an endedit event without Validating
            // this handles that case
            if(waitEnter && NewCellEdit != -1)
            {
                // If the cell is blank, error
                if (PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].Cells[PWListGrid.CurrentCell.ColumnIndex].Value as String == "")
                {
                    // Show message box depending on the Column
                    if (e.ColumnIndex == 0)
                        MessageBox.Show("You MUST enter a Website, this cannot be blank!", "Error - No Website Entered");
                    else if (e.ColumnIndex == 1)
                        MessageBox.Show("You MUST enter a Username, this cannot be blank!", "Error - No Username Entered");
                    else if (e.ColumnIndex == 2)
                        MessageBox.Show("You MUST enter a Password, this cannot be blank!", "Error - No Password Entered");

                }

                // Try to set the current cell to begin edit
                try
                {
                    // Set to Begin Edit
                    PWListGrid.BeginEdit(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("----- Set cell fail - Exception" + ex);
            
                }
            
            }

        }

        // This function handles the Cell Validating Event
        // This prevents the User from Leaving the Cell for the new entry if the entry is blank
        // This function sets the next active cell after a user enters more information
        // also shows message box notifications
        private void PWListGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //Console.WriteLine(" CELL VALIDAT - form val = " + e.FormattedValue + " Row = " + e.RowIndex + " Col = " + e.ColumnIndex);

            // If the current cell is Blank, Show a MessageBox and Cancel them from leaving
            if (e.FormattedValue as String == "")
            {
                // Show message box depending on the Column
                if (e.ColumnIndex == 0)
                    MessageBox.Show("You MUST enter a Website, this cannot be blank!", "Error - No Website Entered");
                else if (e.ColumnIndex == 1)
                    MessageBox.Show("You MUST enter a Username, this cannot be blank!", "Error - No Username Entered");
                else if (e.ColumnIndex == 2)
                    MessageBox.Show("You MUST enter a Password, this cannot be blank!", "Error - No Password Entered");

                // stop them from leaving
                e.Cancel = true;
            }
            // If they have not pressed Enter yet
            else if (waitEnter)
            {
                // if they didnt hit enter to trigger this event
                // show error
                MessageBox.Show("Press ENTER to confirm previous changes before continuing", "ERROR - Confirm Changes");

                // stop them from leaving
                e.Cancel = true;
            }
            // If the Cell is NOT blank, and the User has Pressed ENTER to confirm
            else
            {
                // If a New cell has been created, and this function is not the one that triggered the validation event
                // then the user is trying to Escape, so Force the user to fill in the next value
                if (NewCellEdit != -1 && NewCellEdit <= 3 && !valSent && OldCellEdit == -1)
                {
                    // because of the order of events, this is needed
                    // only reset cell if not on the last cell
                    if (NewCellEdit != 3)
                    {
                        // Cancel the event, and set the flag so we know whats re-triggering this event
                        e.Cancel = true;
                        valSent = true;

                        // Check if the current cell is in edit mode, if so end it - not sure if needed
                        if (PWListGrid.IsCurrentCellInEditMode)
                            PWListGrid.EndEdit();

                        // Set the currently selected Row
                        if (!PWListGrid.Rows[PWListGrid.Rows.Count - 1].Selected)
                            PWListGrid.Rows[PWListGrid.Rows.Count - 1].Selected = true;

                        // Try to set the current cell
                        try
                        {
                            // Set the Cell
                            PWListGrid.CurrentCell = PWListGrid.Rows[PWListGrid.Rows.Count - 1].Cells[NewCellEdit];
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("----- Set cell fail - Exception" + ex);

                        }

                        // Set to Begin Edit
                        PWListGrid.BeginEdit(true);

                        waitEnter = true;  // Set the flag to wait for enter press
                    }
                    NewCellEdit++;     // increment to the next column
                }
                // If the event was triggered by the previous call, reset the flag
                else if (valSent)
                {
                    valSent = false; // Reset valSent for the next time
                }
                // If they are EDITING a cell and NOT adding a new cell
                else if (OldCellEdit != -1 && NewCellEdit == -1)
                {
                    // Reset the row skip flag
                    skipRow = -1;
                }

            }

        }

        // Handels Row Edits - Functionality moved to CellEndEdit
        // Currently Unused
        private void PWListGrid_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            // Unused
            /*
            // If they are in Edit Mode
            if (OldCellEdit != -1)
            {
                // Change the Row to be read only and set editing flag to false
                PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].ReadOnly = true;

                // Update the temp list
                RefreshTempList();

                // Reset the row skip flag
                skipRow = -1;

                PWListGrid.Refresh();

                OldCellEdit = -1; // Reset the flag

                // Set the background to Green
                ColorStatusPanel.BackColor = Color.FromArgb(43, 242, 36);
            }
            */
        }

        // This is used to add an event handler to a CELL while it is in Edit mode
        // It also controls the Password Character used for the password Column when editing a cell
        private void PWListGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Add the event handler to the contol
            if (e.Control is DataGridViewTextBoxEditingControl tb)
            {
                tb.KeyPress -= PWListGrid_KeyPress;
                tb.KeyPress += PWListGrid_KeyPress;

                tb.PreviewKeyDown -= PWListGrid_PreviewKeyDown;
                tb.PreviewKeyDown += PWListGrid_PreviewKeyDown;

                // This is used to cast the Editing control (cell) as a textbox to hide the password
                // Only hides the PW if not already showing passwords
                if (PWListGrid.CurrentCell.ColumnIndex == 2 && !ShowPasswords)
                {
                    TextBox textBox = e.Control as TextBox;
                    if (textBox != null)
                    {
                        // Set the Password mask character
                        textBox.PasswordChar = '*';
                    }
                }
                else
                {
                    // Need to reset the control if ShowPasswords is Set
                    TextBox textBox = e.Control as TextBox;
                    if (textBox != null)
                    {
                        // Set the password Mask to the Default Value
                        textBox.PasswordChar = (char)0;
                    }
                }

            }

        }

        // Handles when the user presses buttons in a cell
        // Currently Unused
        private void PWListGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Currently Unused
            //Console.WriteLine("press " + (int)e.KeyChar);

        }

        // Handles when the user presses Enter to confirm a value in the cell
        private void PWListGrid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // If the enter key is pressed, change the flag
            if (e.KeyCode == Keys.Enter)
            {
                //Console.WriteLine("ENTER IN CELL- \n efv= '" + PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].Cells[PWListGrid.CurrentCell.ColumnIndex].EditedFormattedValue as String + "'");
                //Console.WriteLine("ENTER IN CELL efv = '" + PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].Cells[PWListGrid.CurrentCell.ColumnIndex].EditedFormattedValue as String + "'");

                // If the cell is not blank, and we are waiting for Enter
                if (PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].Cells[PWListGrid.CurrentCell.ColumnIndex].EditedFormattedValue as String != "" && waitEnter)
                    waitEnter = false;
                else if (PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].Cells[PWListGrid.CurrentCell.ColumnIndex].EditedFormattedValue as String == "")
                {
                    // For some reason form validating is skipped with the new button when pressing enter in a cell
                    // so only show these if in New Cell Mode
                    //if (NewCellEdit != -1)
                    //{
                    //    //PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].Cells[PWListGrid.CurrentCell.ColumnIndex].Value = "Enter Value!";
                    //    //PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].Cells[PWListGrid.CurrentCell.ColumnIndex].Selected = true;
                    //    // Show message box depending on the Column
                    //    if (PWListGrid.CurrentCell.ColumnIndex == 0)
                    //        MessageBox.Show("You MUST enter a Website, this cannot be blank!", "Error - No Website Entered - Preview");
                    //    else if (PWListGrid.CurrentCell.ColumnIndex == 1)
                    //        MessageBox.Show("You MUST enter a Username, this cannot be blank!", "Error - No Username Entered - Preview");
                    //    else if (PWListGrid.CurrentCell.ColumnIndex == 2)
                    //        MessageBox.Show("You MUST enter a Password, this cannot be blank!", "Error - No Password Entered - Preview");
                    //}
                }
            }
        }

        #endregion // end Cell and Row Edit/Leave/Validate

                #region Cell Hightlighting Events
        // Event for selection change
        // Used to change the Selection Highlight for specific Columns in the Row
        private void PWListGrid_SelectionChanged(object sender, EventArgs e)
        {
            // If there are actually Rows, set the highlight colors
            if (PWListGrid.Rows.Count != 0)
            {
                // Loop through and set all the columns to the default color
                for (int i = 0; i < PWListGrid.ColumnCount; i++)
                {
                    PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].Cells[i].Style.SelectionBackColor = SystemColors.Highlight;
                }

                // Set the current column to the special color
                PWListGrid.Rows[PWListGrid.CurrentCell.RowIndex].Cells[PWListGrid.CurrentCell.ColumnIndex].Style.SelectionBackColor = Color.LightSkyBlue;
            }

        }

        // Handles the click even for the List of PWs
        // Used because changing the selected column didn't activate the SelectionChanged event
        // probably because "Select by entire Row" is set
        private void PWListGrid_Click(object sender, EventArgs e)
        {
            // If there are actually Rows
            if (PWListGrid.Rows.Count != 0)
            {
                // If the previous column does not match the current column, call the selection changed event
                if (PWListGrid.CurrentCell.ColumnIndex != PrevCol)
                {
                    // Save the column
                    PrevCol = PWListGrid.CurrentCell.ColumnIndex;

                    // Call the event
                    PWListGrid_SelectionChanged(this, new EventArgs());

                }
            }
        }
                #endregion // Cell Hightlighting Events

            #endregion // End Options region

            #region Show Password
        // When the show all Passwords Checkbox is Checked
        // Toggle, and message box confirmation
        private void ShowAllPWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // If showpasswords is already set, flip it and refresh
            if (ShowPasswords)
            {
                ShowPasswords = false;
                PWListGrid.Refresh();
            }
            else
            {
                // Set the background to Green
                ColorStatusPanel.BackColor = Color.Red;

                // Show Confirmation box to show all passwords
                var confirmResult = MessageBox.Show("Are you sure you want to show ALL passwords?" +
                                                   "\n\n * You can hover over passwords to peak them" +
                                                   "\n ** If checked, Passwords will also be searched",
                                                    "Confirm Show ALL Passwords",
                                                    MessageBoxButtons.YesNo);
                // If the result was YES, show all Passwords
                if (confirmResult == DialogResult.Yes)
                {
                    ShowPasswords = true;
                    PWListGrid.Refresh();
                }
                else
                {
                    // DeReference this function to change the checkbox
                    ShowAllPWCheckBox.CheckedChanged -= ShowAllPWCheckBox_CheckedChanged;

                    // make the checkbox NOT checked
                    ShowAllPWCheckBox.Checked = false;

                    // ReReference this function so it works next time
                    ShowAllPWCheckBox.CheckedChanged += ShowAllPWCheckBox_CheckedChanged;
                }

                // Set the background to Green
                ColorStatusPanel.BackColor = Color.FromArgb(43, 242, 36);
            }
        }

        // Shows the password in a tool tip when the mouse moves over the PW cell
        private void PWListGrid_MouseMove(object sender, MouseEventArgs e)
        {
            var p = PWListGrid.PointToClient(Cursor.Position);
            var mpos = PWListGrid.HitTest(p.X, p.Y);

            //You can use these values
            //mpos.ColumnX
            //mpos.RowY
            //mpos.ColumnIndex
            //mpos.RowIndex

            // If mouse is over passwords, and passwords are not already showing
            if (mpos.ColumnIndex == 2 && mpos.RowIndex != -1 && !ShowPasswords)
            {
                // Se the tool tip
                PWListGrid.Rows[mpos.RowIndex].Cells[mpos.ColumnIndex].ToolTipText = PWListGrid.Rows[mpos.RowIndex].Cells[mpos.ColumnIndex].Value as String;
            }
            
        }

        // Clear the Tool Tips back to nothing when the mouse leaves the control
        private void PWListGrid_MouseLeave(object sender, EventArgs e)
        {
            // Loop through all the tool tips for PWs
            for (int i = 0; i < PWListGrid.Rows.Count; i++)
            {
                PWListGrid.Rows[i].Cells[2].ToolTipText = "";
            }
        }
            #endregion // End Password region

            #region Search Text Box
        // Handles when users are typing in the search box
        // Automatically calls the search function and updates as you type
        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // Show the clear button in the search box
            if (!ClearButton.Visible)
                ClearButton.Visible = true;

            // Hide the options panel
            if (OptionPanel.Visible)
                OptionPanel.Visible = false;

            // The moment a key is released, do a search and update the list
            FindPW();
        }

        // Used to clear the search and re-hide the clear button
        private void ClearButton_Click(object sender, EventArgs e)
        {
            SearchTextBox.Text = "";
            FindPW(); // Call find to reset the Data Grid
            ClearButton.Visible = false;
        }
            #endregion // End Search Text Box

        #endregion // End Form Event Handlers

        #region Form Exit and Save Events

        // Saves the data when button pressed
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Hide the options panel
            if (OptionPanel.Visible)
                OptionPanel.Visible = false;

            // If data is unsaved, save the temp, else save the pwList
            if (isUnsaved)
            {
                // Set the background to Green
                ColorStatusPanel.BackColor = SystemColors.Highlight;

                // Show Confirmation box to Save Over old data
                var confirmResult = MessageBox.Show("All old data will be overwritten" +
                                                   "\n\nConfirm Save Changes?",
                                                    "Confirm Save - Overwrite",
                                                    MessageBoxButtons.YesNo);

                // If the result was YES, Save the data
                if (confirmResult == DialogResult.Yes)
                {
                    // Save the temp list
                    SaveDataFile(Temp_pwList);

                    // Reset original list to the newly saved list
                    pwList = new List<PWEntry>(Temp_pwList);

                    isUnsaved = false; // reset the flag
                }
                else
                {
                    Console.WriteLine("Save canceled");
                }

                // Set the background to Green
                ColorStatusPanel.BackColor = Color.FromArgb(43, 242, 36);
            }
            else
            {
                // Resave what is already saved
                SaveDataFile(pwList);
            }

        }

        // This function saves the PW list passed in to the data file
        // Then encrypts the file
        private void SaveDataFile(List<PWEntry> tpw)
        {
            // Write to the file, it will be blank
            using (StreamWriter SW = new StreamWriter(TDataFile))
            {
                // Loop through the PW List and add all the entries to the file
                foreach (PWEntry x in tpw)
                    SW.WriteLine(x.Site + "   ;   " + x.User + "   ;   " + x.Pass);
            }

            // Encrypt the file
            EncryptFile(TDataFile, DataFile);

            // Delete temp file
            File.Delete(TDataFile); 
        }

        // Button pressed to exit, exits the app
        private void ExitButton_Click(object sender, EventArgs e)
        {
            // Exit the Application
            Application.Exit();
        }

        // Event for when the form closes
        // Prompt the User to Save any changes here
        private void PassMasterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If not waiting for enter
            // (Form closing event can be thrown if pressing the X in the top corner, this prevents the prompt in that case - although the close event gets cancled)
            if (!waitEnter)
            {
                // Hide the options panel
                if (OptionPanel.Visible)
                    OptionPanel.Visible = false;

                // If there are unsaved changes
                if (isUnsaved)
                {
                    // Set the background to Green
                    ColorStatusPanel.BackColor = Color.Red;

                    // Show Confirmation box to exit without saving, or cancel
                    var confirmResult = MessageBox.Show("There are UNSAVED changes" +
                                                       "\n\nAre you sure you want to exit without saving?",
                                                        "Exit with Unsaved Changes",
                                                        MessageBoxButtons.YesNo);

                    // If the result was YES, Exit without saving
                    if (confirmResult == DialogResult.Yes)
                    {
                        // nothing to do here yet
                        // exit
                    }
                    else
                    {
                        // Set the background to Green
                        ColorStatusPanel.BackColor = Color.FromArgb(43, 242, 36);
                        e.Cancel = true;
                    }

                }
                else
                {
                    // nothing to do here yet
                    // exit
                }
            }
        }

        #endregion

        #region General Functions - Search/Fill etc.

        // This function is used to store any changes that are made to the PWList
        // Original entries are left in the original list
        private void RefreshTempList()
        {
            // If not Using the Search List
            if (!UsingSrch)
            {
                // Empty the temp list
                Temp_pwList.Clear();

                // If the grid is not empty
                if (PWListGrid.Rows.Count != 0)
                {
                    // Loop and get all the values of the grid
                    for (int x = 0; x < PWListGrid.Rows.Count; x++)
                    {
                        // Loop through and set all the columns to the default color
                        if (PWListGrid.ColumnCount == 3)
                        {
                            PWEntry pwe = new PWEntry(); // Make a new entry

                            // Get the parts of the PW entry from the Grid
                            pwe.Site = PWListGrid.Rows[x].Cells[0].Value as String;
                            pwe.User = PWListGrid.Rows[x].Cells[1].Value as String;
                            pwe.Pass = PWListGrid.Rows[x].Cells[2].Value as String;
                            pwe.Pkey = PWListGrid.Rows[x].Tag as String;

                            // Add the entry
                            Temp_pwList.Add(pwe);
                        }
                        else
                        {
                            // This error should not be possible
                            // Here just in case
                            MessageBox.Show("Missing columns, there should be 3 but there are not - this shouldn't be possible", "Error");
                        }
                    }
                }
            }
            // Else if we ARE using the Search List, Update the values that changed
            else
            {
                // Search list must be updated before updating the Temp list
                // Empty the Search list
                SearchPWList.Clear();

                // If the grid is not empty
                if (PWListGrid.Rows.Count != 0)
                {
                    // Loop and get all the values of the grid
                    for (int x = 0; x < PWListGrid.Rows.Count; x++)
                    {
                        // Loop through and set all the columns to the default color
                        if (PWListGrid.ColumnCount == 3)
                        {
                            PWEntry pwe = new PWEntry(); // Make a new entry

                            // Get the parts of the PW entry from the Grid
                            pwe.Site = PWListGrid.Rows[x].Cells[0].Value as String;
                            pwe.User = PWListGrid.Rows[x].Cells[1].Value as String;
                            pwe.Pass = PWListGrid.Rows[x].Cells[2].Value as String;
                            pwe.Pkey = PWListGrid.Rows[x].Tag as String;

                            // Add the entry
                            SearchPWList.Add(pwe);
                        }
                        else
                        {
                            // This error should not be possible
                            // Here just in case
                            MessageBox.Show("Missing columns, there should be 3 but there are not - this shouldn't be possible", "Error");
                        }
                    }
                }

                // If the grid is not empty, and the search list is not empty
                if (PWListGrid.Rows.Count != 0 && SearchPWList.Any())
                {
                    // For all the entries in the Search list
                    foreach (PWEntry i in SearchPWList)
                    {
                        // Get the index of the matching entry in the TempList using the Pkey
                        int n = Temp_pwList.FindIndex(x => x.Pkey == i.Pkey);

                        // If index was found
                        if (n != -1)
                        {
                            // If any of the values do not match, replace them
                            // Updating the temp list
                            if (Temp_pwList[n].Site != i.Site)
                                Temp_pwList[n].Site = i.Site;

                            if (Temp_pwList[n].User != i.User)
                                Temp_pwList[n].User = i.User;

                            if (Temp_pwList[n].Pass != i.Pass)
                                Temp_pwList[n].Pass = i.Pass;
                        }
                        else
                        {
                            // If the index was NOT found, then it must be a new entry, so add it.
                            Temp_pwList.Add(i);
                        }

                        //PWEntry tmp = SearchPWList.Find(x => x.Pkey == i.Pkey);
                    }
                }

                // If there is anything in the delete list, remove it from the temp list
                if (DeletePWList.Any())
                {
                    // For all the entries in the Delete list
                    // Remove them from the Temp List
                    foreach (String i in DeletePWList)
                    {
                        // Get the index of the matching entry in the TempList using the Pkey
                        int n = Temp_pwList.FindIndex(x => x.Pkey == i);

                        // If index was found
                        if (n != -1)
                        {
                            // Remove the entry that was deleted
                            Temp_pwList.RemoveAt(n);
                        }
                        else
                        {
                            Console.WriteLine("Deleted Item Not Found in Temp list, ignoring");
                        }

                        //PWEntry tmp = SearchPWList.Find(x => x.Pkey == i.Pkey);
                    }

                    // Empty the Delete list
                    DeletePWList.Clear();
                }

            }

        }

        // Used to clear and Refill the data grid view with values
        private void FillDataGrid(List<PWEntry> entries)
        {
            // Clear the grid if it is not empty
            if (PWListGrid.Rows.Count != 0)
            {
                PWListGrid.Rows.Clear();
            }

            if (entries.Any())
            {
                // Loop to add items to data Grid
                foreach (PWEntry x in entries)
                {
                    // Put the Entry into the DataGrid
                    PWListGrid.Rows.Add(x.Site, x.User, x.Pass);

                    // Save the primary key for the row
                    PWListGrid.Rows[PWListGrid.Rows.Count - 1].Tag = x.Pkey;

                    // Make the new Row Read only
                    PWListGrid.Rows[PWListGrid.Rows.Count - 1].ReadOnly = true;
                }
            }

        }

        // Used to search for an Entry in the list
        // Will Search all 3 columns for the string if ShowPasswords is true
        // Else, only searches Site and User
        // Searches for ANY INSTANCE of the entered text in the entries
        // Search will be Case Sensitive
        private void FindPW()
        {
            // If there are rows to search
            //if (PWListGrid.Rows.Count != 0)

            // If there is stuff in the Temp list
            if (Temp_pwList.Any())
                {
                // If the search text is not blank
                if (SearchTextBox.Text != "")
                {
                    SearchPWList.Clear();

                    // Get the string to search for
                    String s = SearchTextBox.Text;

                    // Loop through and set all the columns to the default color
                    if (PWListGrid.ColumnCount == 3)
                    {
                        // Loop and get all the values of the grid
                        //for (int x = 0; x < PWListGrid.Rows.Count; x++)
                        // Loop through the Temp List 
                        foreach (PWEntry x in Temp_pwList)
                        {

                            PWEntry pwe = new PWEntry(); // Make a new entry

                            // Get the parts of the PW entry from the Grid
                            pwe.Site = x.Site;
                            pwe.User = x.User;
                            pwe.Pass = x.Pass;
                            pwe.Pkey = x.Pkey; // Primary Key used to identify the entry

                            // If we are showing Passwords, search all
                            if (ShowPasswords)
                            {
                                // Search all three columns for the entered string
                                if (pwe.Site.IndexOf(s) != -1 ||
                                    pwe.User.IndexOf(s) != -1 ||
                                    pwe.Pass.IndexOf(s) != -1)
                                {
                                    // Add the entry
                                    SearchPWList.Add(pwe);
                                }
                            }
                            else
                            {
                                // Search Site and User, if not showing passwords
                                if (pwe.Site.IndexOf(s) != -1 ||
                                    pwe.User.IndexOf(s) != -1)
                                {
                                    // Add the entry
                                    SearchPWList.Add(pwe);
                                }
                            }
                        }

                        // Add the Search list to the Data Grid
                        FillDataGrid(SearchPWList);
                        UsingSrch = true; // set the search flag for using the Search list
                    }
                    else
                    {
                        // This error should not be possible
                        // Here just in case
                        MessageBox.Show("Missing columns, there should be 3 but there are not - this shouldn't be possible", "Error");
                    }
                }
                else
                {
                    // If the search text is Empty
                    // Refill the grid with values from the temp list
                    FillDataGrid(Temp_pwList);
                    ClearButton.Visible = false; // hide the clear button
                    UsingSrch = false; // set the search flag
                }
            }
            else
            {
                // Show error if nothing in the list, but not using Search list
                if (!UsingSrch)
                    MessageBox.Show("There is nothing to search for, enter data before searching", "Error - No rows to search");
                else if (SearchTextBox.Text == "")
                {
                    // If still using the search list AND the search is blank, Reset the list to the temp

                    // Refill the grid with values from the temp list
                    FillDataGrid(Temp_pwList);
                    UsingSrch = false; // set the search flag
                }
            }
        }

        #region File Encrytion Functions

        // File Encryption Method, takes input file, and outputfile
        // edited from code online - credit to Steve Lydford, codeproject.com
        // Encrypts a file using Rijndael algorithm.
        private void EncryptFile(string inputFile, string outputFile)
        {
            // Try to encrypt
            try
            {
                // Create a key
                string password = @"myKey123";
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                // Make the output file stream
                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                // create Rijndael algorithm object
                RijndaelManaged RMCrypto = new RijndaelManaged();

                // make crypto stream
                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                // Make the input file stream
                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                // Write the encryption
                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);

                // Close the files // wont work in Finally
                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Encryption failed!\n\n" + ex, "Error");
            }
            finally
            {

               //// Close the files
               //fsIn.Close();
               //cs.Close();
               //fsCrypt.Close();
            }

        }

        // File Decryption Method, takes input file, and outputfile
        // edited from code online - credit to Steve Lydford, codeproject.com
        // Encrypts a file using Rijndael algorithm.
        private void DecryptFile(string inputFile, string outputFile)
        {
            // Try to decrypt the file
            try
            {
                // Create a key
                string password = @"myKey123";

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                // Make the input file stream
                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                // create Rijndael algorithm object
                RijndaelManaged RMCrypto = new RijndaelManaged();

                // make crypto stream
                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                // Make the output file stream
                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                // Write the decryption
                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                // Close the files // wont work in Finally
                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Decryption failed!\n\n" + ex, "Error");
            }
            finally
            {

                //// Close the files
                //fsOut.Close();
                //cs.Close();
                //fsCrypt.Close();
            }
        }

        #endregion // File Encryption Functions

        #endregion // End General Function

    }

    // Class to handle each password entry
    // Each instance should have a UNIQUE primary key assigned to it on creation
    public class PWEntry
    {
        String site = "";
        String user = "";
        String pass = "";
        String pkey;

        public string Site { get => site; set => site = value; }
        public string User { get => user; set => user = value; }
        public string Pass { get => pass; set => pass = value; }
        public string Pkey { get => pkey; set => pkey = value; }

        public PWEntry()
        {
            // Generate a Unqiue string for the primary key
            pkey = Guid.NewGuid().ToString("N");
            //Console.WriteLine(pkey);
        }
    }

}
