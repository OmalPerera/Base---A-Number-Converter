using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows_base.Properties;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace Windows_base
{
    public partial class Form1 : Form
    {

        private int activatedBase = 0;    // 0 - user haven choose any base
                                            // 2 - binary
                                            // 8 - octal
                                            // 10 - decimal
                                            // 16 - hexadecimal

        public bool clear_fields_on_off_status ;       // false - off
                                                       // true - on


        private bool exit_conf_on_off_status ;       // false - off
                                                     // true- on

        private bool show_tutorial_on_off;          // true - showing the tutorial
                                                    // false - not showing the tutorial

        public int BitNumber_for_binay = 8; // user defined bit Value for fraction (it will increase the acuracy)

        private bool decimal_Point_Typed = false; //when user entering any user input, user must input only one decimal point. this variable blocks the second decimal point type by the user


   
 
        

        

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {


            // blocking the tutorial if user configuration for show tutorial is false 
            show_tutorial_on_off = Convert.ToBoolean(Properties.Settings.Default.ShowTutorial);

            if (show_tutorial_on_off == true)
            {
                panel_tutorial.Visible = true;
            }
            else
            {
                panel_tutorial.Visible = false;
            }




            Disable_all_TextFields();
            Loading_UserConfiguredSetting();

            btn_binary_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_binary_selected;
            TextBox_binary.Enabled = true;
            activatedBase = 2;
            decimal_Point_Typed = false;


        }



        private void Loading_UserConfiguredSetting()
        {

            // methods to load user saved configurations (Clear fields) to the form.

            clear_fields_on_off_status = Convert.ToBoolean(Properties.Settings.Default.ClearFields);

            if (clear_fields_on_off_status == true)
            {
                pictureBox_switch_clear.Image = global::Windows_base.Properties.Resources.switch_on;
            }
            else
            {
                pictureBox_switch_clear.Image = global::Windows_base.Properties.Resources.switch_off2;
            }






            // methods to load user saved bit value for binary fractions.

            BitNumber_for_binay = Properties.Settings.Default.BitNumber;
            textBox_number_of_bits.Text = BitNumber_for_binay + "";







            // methods to load user saved configurations (Show tutorial) to the form.

            

            if (show_tutorial_on_off == true)
            {
                checkBox_tutorial_donotShow.Checked = false;
                pictureBox_switch_tutorial.Image = global::Windows_base.Properties.Resources.switch_on;
            }
            else
            {
                checkBox_tutorial_donotShow.Checked = true;
                pictureBox_switch_tutorial.Image = global::Windows_base.Properties.Resources.switch_off2;
            }




            // methods to load user saved configurations (Exit Configuration) to the form.

            exit_conf_on_off_status = Convert.ToBoolean(Properties.Settings.Default.ExitConfirm);

            if (exit_conf_on_off_status == true)
            {
                pictureBox_switch_exit.Image = global::Windows_base.Properties.Resources.switch_on;
            }
            else
            {
                pictureBox_switch_exit.Image = global::Windows_base.Properties.Resources.switch_off2;
            }
        }



        private bool m_isExiting;
        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {

            if (!m_isExiting && exit_conf_on_off_status)
            {
                DialogResult exitMsg = MessageBox.Show("Do you really want to exit ?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (exitMsg == DialogResult.Yes)
                {
                    m_isExiting = true;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }

            }

        }















/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ START : Tutorial +++++++++++++++++++++++++ */
        /*
         * this methods are used to handle tutorial at startup
         * Timer has been used to count up 
         */

        int timeCounter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timeCounter++;

            if (timeCounter == 10)
            {
                pictureBox_focus_ring.Visible = true;
            }

            if (timeCounter == 12)
            {
                pictureBox_tutorialHand.Visible = true;
            }

            if (timeCounter == 14)
            {


                int x = pictureBox_tutorialHand.Location.X;
                int y = pictureBox_tutorialHand.Location.Y;

                for (int m = 0; m <200; m++)
                {

                    pictureBox_tutorialHand.Location = new Point(x - m, y);
                    //pictureBox_tutorialHand.Margin = new Padding(0, 0, m, 0);
                    m=m+60;
                }

            }

            if (timeCounter == 18)
            {
                pictureBox_focus_ring.Visible = false;
            }

            if (timeCounter == 19)
            {
                pictureBox_focus_ring.Visible = true;
                pictureBox_tutorialHand.Visible = false;
                pictureBox_tutorial_enablingBinary.Visible = true;
                textBox_tutorial_binary_text.Visible = true;
                pictureBox_focus_ring.Visible = false;
            }

            if (timeCounter == 27)
            {
                textBox_tutorial_binary_text.Text = "100011100 . 101101 ";
            }

            if (timeCounter == 29)
            {
                button_tutorial_convert.Visible = true;
                checkBox_tutorial_donotShow.Visible = true;
            }

            

        }



        private void pictureBox5_Click(object sender, EventArgs e)
        {
            panel_tutorial.Visible = false;
        }



        private void checkBox_tutorial_donotShow_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_tutorial_donotShow.Checked == true)    //do not show is CHECKED NOW; so false
            {
                show_tutorial_on_off = false;
                pictureBox_switch_tutorial.Image = global::Windows_base.Properties.Resources.switch_off2;
                
            }
            else
            {
                show_tutorial_on_off = true;
                pictureBox_switch_tutorial.Image = global::Windows_base.Properties.Resources.switch_on;
            }

            Properties.Settings.Default.ShowTutorial = show_tutorial_on_off;
            Properties.Settings.Default.Save();
        }


/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ END : tutorial +++++++++++++++++++++++++ */











        private void Disable_all_TextFields()
        {
            TextBox_binary.Enabled = false;
            TextBox_octal.Enabled = false;
            TextBox_decimal.Enabled = false;
            TextBox_hexadecimal.Enabled = false;

        }


        private void Clear_all_TextFields()
        {
            if (clear_fields_on_off_status == true)
            {
                TextBox_binary.Text = "";
                TextBox_octal.Text = "";
                TextBox_decimal.Text = "";
                TextBox_hexadecimal.Text = "";
            }
            else
            {

            }

        }

        /*============================================================================================================

         * --------------------------------------------------------------------------------------- get the value in the text box at the start up andassign ith to the num_of_bits....
         * --------------------------------------------------------------------------------------- after change the number save it when pressing any button ath the menu
         * --------------------------------------------------------------------------------------- validate the txt boxes
         * help page
         * --------------------------------------------------------------------------------------- exit confirmation dialog box
         * --------------------------------------------------------------------------------------- how to put shared preference
         * 
         * 
         ===============================================================================================================*/













/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ START : Main Menu Buttons +++++++++++++++++++++++++ */
        
        /*
         * Following buttons are in the main panel as Tabs
         * Each button loads different panels while changing visbilities of each panels (panel.visibility = true|false)
         * 
         */

        private void btnConvert_Click(object sender, EventArgs e)
        {

            tableLayoutPanel_Convert.Visible = true;
            tableLayoutPanelAbout.Visible = false;
            tableLayoutPanel_settings.Visible = false;
            tableLayoutPanelHelp.Visible = false;


            btnConvert.BackgroundImage = global::Windows_base.Properties.Resources.menu_selection_indicator3;
            btnHelp.BackgroundImage = null;
            btnSettings.BackgroundImage = null;
            btnAbout.BackgroundImage = null;

            BitNumber_for_binay = Convert.ToInt32(textBox_number_of_bits.Text);
            Properties.Settings.Default.BitNumber = BitNumber_for_binay;
            Properties.Settings.Default.Save();


            
        }


        private void btnHelp_Click(object sender, EventArgs e)
        {
            tableLayoutPanelHelp.Visible = true;
            tableLayoutPanelAbout.Visible = false;
            tableLayoutPanel_Convert.Visible = false;
            tableLayoutPanel_settings.Visible = false;

            btnHelp.BackgroundImage = global::Windows_base.Properties.Resources.menu_selection_indicator3;
            btnConvert.BackgroundImage = null;
            btnSettings.BackgroundImage = null;
            btnAbout.BackgroundImage = null;

        }
        

        private void btnSettings_Click(object sender, EventArgs e)
        {
            tableLayoutPanel_settings.Visible = true;
            tableLayoutPanel_Convert.Visible = false;
            tableLayoutPanelAbout.Visible = false;
            tableLayoutPanelHelp.Visible = false;

            btnSettings.BackgroundImage = global::Windows_base.Properties.Resources.menu_selection_indicator3;
            btnConvert.BackgroundImage = null;
            btnHelp.BackgroundImage = null;
            btnAbout.BackgroundImage = null;
            
        }


        private void btnAbout_Click(object sender, EventArgs e)
        {

            tableLayoutPanelAbout.Visible = true;
            tableLayoutPanel_Convert.Visible = false;
            tableLayoutPanel_settings.Visible = false;
            tableLayoutPanelHelp.Visible = false;


            btnAbout.BackgroundImage = global::Windows_base.Properties.Resources.menu_selection_indicator3;
            btnConvert.BackgroundImage = null;
            btnHelp.BackgroundImage = null;
            btnSettings.BackgroundImage = null;
        }


/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ END : Main Menu Buttons +++++++++++++++++++++++++ */







        private void label_clear_fields_text_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox_clearfields_icon_Click(object sender, EventArgs e)
        {
            

        }


        private void pictureBox_switch_clear_fields_Click(object sender, EventArgs e)
        {


        }


        private void tableLayoutPanel_settings_Paint(object sender, PaintEventArgs e)
        {

        }


        private void textBox_number_of_bits_TextChanged(object sender, EventArgs e)
        {


        }


        private void tableLayoutPanel_Convert_Paint(object sender, PaintEventArgs e)
        {

        }







/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ START : Settings - switch +++++++++++++++++++++*/
        
        
        /*
         * These switches are in settings page.
         * switches are created based on PICTURE BOXES; when clicks background picture get changed and 
         *                                                                                              settings variables get saved
         */


        private void pictureBox_switch_clear_Click(object sender, EventArgs e)
        {
            
            if (clear_fields_on_off_status == true)
            {
                pictureBox_switch_clear.Image = global::Windows_base.Properties.Resources.switch_off2;
                clear_fields_on_off_status = false;
            }
            else
            {
                pictureBox_switch_clear.Image = global::Windows_base.Properties.Resources.switch_on;
                clear_fields_on_off_status = true;
            }

            Properties.Settings.Default.ClearFields = clear_fields_on_off_status;
            Properties.Settings.Default.Save();

        }


        private void pictureBox_switch_exit_Click(object sender, EventArgs e)
        {

            if (exit_conf_on_off_status == true)
            {
                pictureBox_switch_exit.Image = global::Windows_base.Properties.Resources.switch_off2;
                exit_conf_on_off_status = false;
            }
            else
            {
                pictureBox_switch_exit.Image = global::Windows_base.Properties.Resources.switch_on;
                exit_conf_on_off_status = true;
            }

            Properties.Settings.Default.ExitConfirm = exit_conf_on_off_status;
            Properties.Settings.Default.Save();

        }


        private void pictureBox_switch_tutorial_Click(object sender, EventArgs e)
        {
            if (show_tutorial_on_off == true)
            {
                pictureBox_switch_tutorial.Image = global::Windows_base.Properties.Resources.switch_off2;
                checkBox_tutorial_donotShow.Checked = true;
                show_tutorial_on_off = false;
                
            }
            else
            {
                pictureBox_switch_tutorial.Image = global::Windows_base.Properties.Resources.switch_on;
                checkBox_tutorial_donotShow.Checked = false;
                show_tutorial_on_off = true;
               
            }

            Properties.Settings.Default.ShowTutorial = show_tutorial_on_off;
            Properties.Settings.Default.Save();
        }

/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ END : Settings - switch +++++++++++++++++++++*/















/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ START : Validating Text Fields on KEY PRESS++++++++++++++++++ */


         private void TextBox_binary_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (Regex.IsMatch(e.KeyChar.ToString(), @"[0-1\.\b]"))
            {

                if(Regex.IsMatch(e.KeyChar.ToString(), @"[\.]"))
                {
                    if (decimal_Point_Typed == true)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        decimal_Point_Typed = true;
                    }
                }
                else
                {}

            }
            else
            {
                e.Handled = true;
            }
        }

         private void TextBox_octal_KeyPress(object sender, KeyPressEventArgs e)
         {

             if (Regex.IsMatch(e.KeyChar.ToString(), @"[0-7\.\b]"))
             {

                 if (Regex.IsMatch(e.KeyChar.ToString(), @"[\.]"))
                 {
                     if (decimal_Point_Typed == true)
                     {
                         e.Handled = true;
                     }
                     else
                     {
                         decimal_Point_Typed = true;
                     }
                 }
                 else
                 { }

             }
             else
             {
                 e.Handled = true;
             }

         }

         private void TextBox_decimal_KeyPress(object sender, KeyPressEventArgs e)
         {

             if (Regex.IsMatch(e.KeyChar.ToString(), @"[0-9\.\b]"))
             {

                 if (Regex.IsMatch(e.KeyChar.ToString(), @"[\.]"))
                 {
                     if (decimal_Point_Typed == true)
                     {
                         e.Handled = true;
                     }
                     else
                     {
                         decimal_Point_Typed = true;
                     }
                 }
                 else
                 { }

             }
             else
             {
                 e.Handled = true;
             }

         }

         private void TextBox_hexadecimal_KeyPress(object sender, KeyPressEventArgs e)
         {

             if (Regex.IsMatch(e.KeyChar.ToString(), @"[0-9A-Fa-f\.\b]"))
             {

                 if (Regex.IsMatch(e.KeyChar.ToString(), @"[\.]"))
                 {
                     if (decimal_Point_Typed == true)
                     {
                         e.Handled = true;
                     }
                     else
                     {
                         decimal_Point_Typed = true;
                     }
                 }
                 else
                 { }

             }
             else
             {
                 e.Handled = true;
             }

         }



/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ END : Validating Text Fields ++++++++++++++++++ */













/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ START : Bases selecting Icons ++++++++++++++++++ */
        
        // Before Entering the values, user must select a base (2, 8, 10, 16)
        // Following 4 methods executes when click on buttons
        /* when button clicks, background image will get change
         * first deactivates all text fields and only allows perticular text field
         * @ 'activatedBase' changes; it will determine the base when convert button hits
         * Change  the background of all other 3 buttons in to not selected mode
         * 
         */

        private void btn_binary_notClicked_Click(object sender, EventArgs e)
        {
            btn_binary_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_binary_selected;
            Disable_all_TextFields();
            TextBox_binary.Enabled = true;
            Clear_all_TextFields();
            activatedBase = 2;
            decimal_Point_Typed = false;

            btn_octal_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_octal;
            btn_decimal_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_decimal;
            btn_hexadecimal_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_hexadecimal;
                

        }

        private void btn_octal_notClicked_Click(object sender, EventArgs e)
        {
            btn_octal_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_octal_selected;
            Disable_all_TextFields();
            TextBox_octal.Enabled = true;
            Clear_all_TextFields();
            activatedBase = 8;
            decimal_Point_Typed = false;


            btn_binary_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_binary;
            btn_decimal_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_decimal;
            btn_hexadecimal_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_hexadecimal;


        }

        private void btn_decimal_notClicked_Click(object sender, EventArgs e)
        {
            btn_decimal_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_decimal_selected;
            Disable_all_TextFields();
            TextBox_decimal.Enabled = true;
            Clear_all_TextFields();
            activatedBase = 10;
            decimal_Point_Typed = false;

            btn_binary_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_binary;
            btn_octal_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_octal;
            btn_hexadecimal_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_hexadecimal;

        }

        private void btn_hexadecimal_notClicked_Click(object sender, EventArgs e)
        {
            btn_hexadecimal_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_hexadecimal_selected;
            Disable_all_TextFields();
            TextBox_hexadecimal.Enabled = true;
            Clear_all_TextFields();
            activatedBase = 16;
            decimal_Point_Typed = false;

            btn_binary_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_binary;
            btn_octal_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_octal;
            btn_decimal_notClicked.BackgroundImage = global::Windows_base.Properties.Resources.icon_decimal;

        }



/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ END : Bases selecting Icons ++++++++++++++++++ */




        private void btn_convert_Click(object sender, EventArgs e)
        {
            String valueEnteredByUser;

            switch (activatedBase)
            {
                case 2:

                    valueEnteredByUser = TextBox_binary.Text;

                    if (TextBox_binary.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Enter a Binary Value");
                    }
                    else
                    {
                        Convert_Binary_To_Others(valueEnteredByUser);
                    }
                    break;


                case 8:

                    valueEnteredByUser = TextBox_octal.Text;

                    if (TextBox_octal.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Enter an Octal Value");
                    }
                    else
                    {
                        Convert_Octal_To_Others(valueEnteredByUser);
                    }
                    break;


                case 10:

                    valueEnteredByUser = TextBox_decimal.Text;

                    if (TextBox_decimal.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Enter a Decimal Value");
                    }
                    else
                    {
                        Convert_Decimal_To_Others(valueEnteredByUser);
                    }
                    break;


                case 16:

                    valueEnteredByUser = TextBox_hexadecimal.Text;

                    if (TextBox_hexadecimal.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Enter a Hexadecimal Value");
                    }
                    else
                    {
                        Convert_Hexadecimal_To_Others(valueEnteredByUser);
                    }
                    break;


                case 0:
                    MessageBox.Show("Please Select one of bases & input a value to convert");
                    break;
                 
            }




        }




        private void TextBox_binary_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                btn_convert.PerformClick();
                //btn_convert_Click(this, new EventArgs());
               // e.SuppressKeyPress = true;
               // e.Handled = true;
            }
        }








        private string[] FullNumber_and_Fraction_Seperator(String inputValue)
        {
            String[] number_Fraction_divider;

            if (inputValue.Contains("."))
            {
                number_Fraction_divider = inputValue.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            }
            else
            {
                number_Fraction_divider = new string[5];
                number_Fraction_divider[0] = inputValue;
            }




            return number_Fraction_divider;
        }
        



        private String CommonConverter(long numberToBeConverted, int baseToBeConverted)
        {
           
                String converted_Result = Convert.ToString(numberToBeConverted, baseToBeConverted);
                return converted_Result;
            
        }



        private string CommonConverter_fractions(string numberToBeConverted, int currentbase, int baseToBeConverted)
        {
            
            String converted_Fractional_Result = "";
            
            if (string.IsNullOrWhiteSpace(numberToBeConverted))
            {
                return "";
            }
            else
            {
                switch (currentbase)
                {


                    case 2:

                        switch (baseToBeConverted)
                        {
                            case 8:
                                converted_Fractional_Result = Fraction_Binary_To_Octal(numberToBeConverted, 2);
                                break;

                            case 10:
                                converted_Fractional_Result = Fraction_Binary_Octal_To_Decimal(numberToBeConverted, 2);
                                break;

                            case 16:
                                converted_Fractional_Result = Fraction_Binary_To_Hexadecimal(numberToBeConverted, 2);
                                break;
                        }
                        break;




                    case 8:

                        switch (baseToBeConverted)
                        {
                            case 2:
                                converted_Fractional_Result = Fraction_Decimal_To_Binary(Fraction_Binary_Octal_To_Decimal(numberToBeConverted, 8));
                                break;

                            case 10:
                                converted_Fractional_Result = Fraction_Binary_Octal_To_Decimal(numberToBeConverted, 8);
                                break;

                            case 16:
                                converted_Fractional_Result = Fraction_Binary_To_Hexadecimal(Fraction_Decimal_To_Binary(Fraction_Binary_Octal_To_Decimal(numberToBeConverted, 8)), 2);
                                break;
                        }
                        break;




                    case 10:
                        switch (baseToBeConverted)
                        {
                            case 2:
                                converted_Fractional_Result = Fraction_Decimal_To_Binary(numberToBeConverted);
                                break;

                            case 8:
                                converted_Fractional_Result = Fraction_Binary_To_Octal(Fraction_Decimal_To_Binary(numberToBeConverted), 2);
                                break;

                            case 16:
                                converted_Fractional_Result = Fraction_Binary_To_Hexadecimal(Fraction_Decimal_To_Binary(numberToBeConverted), 2);
                                break;

                        }
                        break;




                    case 16:
                        switch (baseToBeConverted)
                        {
                            case 2:
                                converted_Fractional_Result = Fraction_Decimal_To_Binary(Fraction_Hexadecimal_To_Decimal(numberToBeConverted, 10));
                                break;

                            case 8:
                                converted_Fractional_Result = Fraction_Binary_To_Octal(Fraction_Decimal_To_Binary(Fraction_Hexadecimal_To_Decimal(numberToBeConverted, 10)), 2);
                                break;

                            case 10:
                                converted_Fractional_Result = Fraction_Hexadecimal_To_Decimal(numberToBeConverted, 10);
                                break;

                        }
                        break;




                    default:
                        MessageBox.Show("Unknown base");
                        break;


                }



                
                return " . " + converted_Fractional_Result;
            }
        }









/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ START : Whole portion Convertions ++++++++*/



        private void Convert_Binary_To_Others(String inputValue)
        {
            try
            {
                String[] seperatedNumbers_by_DecimalPoint = FullNumber_and_Fraction_Seperator(inputValue);

                String wholePortion = seperatedNumbers_by_DecimalPoint[0];
                String fractionPortion = seperatedNumbers_by_DecimalPoint[1];

                long inputValue_in_LONG = Convert.ToInt64(wholePortion, 2);
                


                // Updates DECIMAL value corresponds to binary
                TextBox_decimal.Text = CommonConverter(inputValue_in_LONG, 10) + CommonConverter_fractions(fractionPortion, 2, 10);

                // Updates OCTAL value corresponds to binary
                TextBox_octal.Text = CommonConverter(inputValue_in_LONG, 8) + CommonConverter_fractions(fractionPortion, 2, 8);

                // Updates HEXADECIMAL value corresponds to binary
                TextBox_hexadecimal.Text = CommonConverter(inputValue_in_LONG, 16) + CommonConverter_fractions(fractionPortion, 2, 16);

            }
            catch (Exception)
            {
                MessageBox.Show("base™ support only numbers below 64 bit binary numbers ");
                throw;
            }


        }


        private void Convert_Octal_To_Others(String inputValue)
        {

            try
            {
                String[] seperatedNumbers_by_DecimalPoint = FullNumber_and_Fraction_Seperator(inputValue);

                String wholePortion = seperatedNumbers_by_DecimalPoint[0];
                String fractionPortion = seperatedNumbers_by_DecimalPoint[1];

                long inputValue_in_LONG = Convert.ToInt64(wholePortion, 8);


                // Updates BINARY value corresponds to octal
                TextBox_binary.Text = CommonConverter(inputValue_in_LONG, 2) + CommonConverter_fractions(fractionPortion, 8, 2);

                // Updates DECIMAL value corresponds to octal
                TextBox_decimal.Text = CommonConverter(inputValue_in_LONG, 10) + CommonConverter_fractions(fractionPortion, 8, 10);

                // Updates HEXADECIMAL value corresponds to octal
                TextBox_hexadecimal.Text = CommonConverter(inputValue_in_LONG, 16) + CommonConverter_fractions(fractionPortion, 8, 16);
            }
            catch (Exception)
            {
                MessageBox.Show("base™ support only numbers below 777 777 777 777 777 777 777 ");
                throw;
            }



        }


        private void Convert_Decimal_To_Others(String inputValue)
        {


            try
            {
                String[] seperatedNumbers_by_DecimalPoint = FullNumber_and_Fraction_Seperator(inputValue);

                String wholePortion = seperatedNumbers_by_DecimalPoint[0];
                String fractionPortion = seperatedNumbers_by_DecimalPoint[1];

                long inputValue_in_LONG = Convert.ToInt64(wholePortion, 10);


                // Updates BINARY value corresponds to decimal
                TextBox_binary.Text = CommonConverter(inputValue_in_LONG, 2) + CommonConverter_fractions(fractionPortion, 10, 2);

                // Updates OCTAL value corresponds to decimal
                TextBox_octal.Text = CommonConverter(inputValue_in_LONG, 8) + CommonConverter_fractions(fractionPortion, 10, 8);

                // Updates HEXADECIMAL value corresponds to decimal
                TextBox_hexadecimal.Text = CommonConverter(inputValue_in_LONG, 16) + CommonConverter_fractions(fractionPortion, 10, 16);

            }
            catch (Exception)
            {
                MessageBox.Show("base™ support only numbers below 9 223 372 036 854 775 807 ");
                throw;
            }




        }

        
        private void Convert_Hexadecimal_To_Others(String inputValue)
        {

            try
            {
                String[] seperatedNumbers_by_DecimalPoint = FullNumber_and_Fraction_Seperator(inputValue);

                String wholePortion = seperatedNumbers_by_DecimalPoint[0];
                String fractionPortion = seperatedNumbers_by_DecimalPoint[1];

                long inputValue_in_LONG = Convert.ToInt64(wholePortion, 16);


                // Updates BINARY value corresponds to hexadecimal
                TextBox_binary.Text = CommonConverter(inputValue_in_LONG, 2) + CommonConverter_fractions(fractionPortion, 16, 2);

                // Updates OCTAL value corresponds to hexadecimal
                TextBox_octal.Text = CommonConverter(inputValue_in_LONG, 8) + CommonConverter_fractions(fractionPortion, 16, 8);

                // Updates DECIMAL value corresponds to hexadecimal
                TextBox_decimal.Text = CommonConverter(inputValue_in_LONG, 10) + CommonConverter_fractions(fractionPortion, 16, 10);

            }
            catch (Exception)
            {
                MessageBox.Show("base™ support only numbers below 7 FFF FFF FFF FFF FFF  ");
                throw;
            }

        }


/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ END : Whole portion Convertions ++++++++*/

















/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ START : Fractional portion Convertions ++++++++*/


        private String Fraction_Binary_Octal_To_Decimal(String numberToBeConverted, int currentBase)
        {

            String converted_Fractional_Result = "";
            double sum;
            int x = 0;
            double totalSum = 0;
            char[] char_NumberToBeConverted = numberToBeConverted.ToCharArray();
                        


            for (int i = 0; i < char_NumberToBeConverted.Length; i++)
            {
                x = (int)Char.GetNumericValue(char_NumberToBeConverted[i]);
                sum = x * (1 / Math.Pow(currentBase, i + 1));
                totalSum = totalSum + sum;
            }


            //result comes like '0.625' , but result should be sent as 625; so leading 0. have to cut off
            // here split will make 2 arrays 1st array will have 0. & secondarray will have 625
            String y = totalSum + "";
            String[] totalSum_without_leading_Zero = y.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            converted_Fractional_Result = totalSum_without_leading_Zero[1];

            return converted_Fractional_Result;
        }


        private String Fraction_Hexadecimal_To_Decimal(String numberToBeConverted, int expectedBase)
        {
            String converted_Fractional_Result = "";

            long inputValue_in_LONG = Convert.ToInt64(numberToBeConverted, 16);
            String binary_equivalent_to_hexa = CommonConverter(inputValue_in_LONG, 2);



            if (expectedBase == 2)
            {

            }
            else if (expectedBase == 8)
            {
                converted_Fractional_Result = Fraction_Binary_Octal_To_Decimal(binary_equivalent_to_hexa, 2);
            }
            else if (expectedBase == 10)
            {
                converted_Fractional_Result = Fraction_Binary_Octal_To_Decimal(binary_equivalent_to_hexa, 2);
            }
            else
            {
                converted_Fractional_Result = "";
            }

            
            return converted_Fractional_Result;
        }


        private String Fraction_Decimal_To_Binary(String numberToBeConverted)
        {
            String converted_Fractional_Result = "";
            double sum;
            int x = 0;
            String finalValue = "";

            x = Convert.ToInt32(numberToBeConverted);
            sum = x / (Math.Pow(10, numberToBeConverted.Length));


            for (int i = 0; i < BitNumber_for_binay; i++)
            {

                sum = sum * 2;

                if (sum >= 1)
                {
                    finalValue = finalValue + "1";
                    sum = sum - 1;
                }
                else
                {
                    finalValue = finalValue + "0";
                }

            }

            converted_Fractional_Result = finalValue;
            return converted_Fractional_Result;
        }


        private string Fraction_Binary_To_Octal(String numberToBeConverted, int currentBase)
        {

            char[] char_NumberToBeConverted = numberToBeConverted.ToCharArray();
            int x;
            int counter = 0;
            double y = 0;
            double sum = 0;
            String resultBuilder = "";
            int numberToBeConverted_length = numberToBeConverted.Length;
            int numberofThreeBits = numberToBeConverted_length / 3;
            int remainingBitEmpty = numberToBeConverted_length % 3;
            int loopCirculation = 0;



            if (remainingBitEmpty != 0)
            {
                loopCirculation = (numberofThreeBits + 1)*3;
            }
            else
            {
                loopCirculation = numberofThreeBits*3;
            }


            for (int i = 0; i <3 ; i++)
            {
                if (counter < numberToBeConverted_length)
                {
                    x = (int)Char.GetNumericValue(char_NumberToBeConverted[counter]);
                    y = x * (Math.Pow(2, (2 - i)));
                    //MessageBox.Show(i + "  " + x + "  " + y);
                    sum = sum + y;
                }
                else
                {

                }

                counter = counter + 1;

                if ((i == 2) && (counter <= loopCirculation))
                {
                    resultBuilder = resultBuilder + sum;
                    i = -1;
                    sum = 0;
                }


            }

            

            return resultBuilder;
        }


        private string Fraction_Binary_To_Hexadecimal(String numberToBeConverted, int currentBase)
        {

            char[] char_NumberToBeConverted = numberToBeConverted.ToCharArray();
            int x;
            int counter = 0;
            double y = 0;
            double sum = 0;
            String resultBuilder = "";
            int numberToBeConverted_length = numberToBeConverted.Length;
            int numberofThreeBits = numberToBeConverted_length / 4;
            int remainingBitEmpty = numberToBeConverted_length % 4;
            int loopCirculation = 0;
            String decimal_SUM_to_Hexa;



            if (remainingBitEmpty != 0)
            {
                loopCirculation = (numberofThreeBits + 1) * 4;
            }
            else
            {
                loopCirculation = numberofThreeBits * 4;
            }


            for (int i = 0; i < 4; i++)
            {
                if (counter < numberToBeConverted_length)
                {
                    x = (int)Char.GetNumericValue(char_NumberToBeConverted[counter]);
                    y = x * (Math.Pow(2, (3 - i)));
                    //MessageBox.Show(i + "  " + x + "  " + y);
                    sum = sum + y;
                }
                else
                {

                }

                counter = counter + 1;

                if ((i == 3) && (counter <= loopCirculation))
                {
                    long inputValue_in_LONG = Convert.ToInt64(sum+"", 10);
                    decimal_SUM_to_Hexa = CommonConverter(inputValue_in_LONG, 16);

                    resultBuilder = resultBuilder + decimal_SUM_to_Hexa;
                    i = -1;
                    sum = 0;
                }


            }



            return resultBuilder;
        }

        private void label_copy_right_Click(object sender, EventArgs e)
        {
            MessageBox.Show("P A O P Perera\t\tEP-1535 \nT L Senadheera\t\tEP-1545\nE A I L Edirisinghe\t\tEP-1515\nK K K S Kariyawasam\t\tEP-1526\nK T D S S Nihathamana\tEP-1534\nN D K G Dharmasiri\t\tEP-1513\nP D S M Premarathna\tEP-1536\nT L Thisarasinghe\t\tEp-1547");
        }

        private void linkLabel_linkedin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://lk.linkedin.com/in/omalperera");
            Process.Start(sInfo);
        }



/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ END : Fractional portion Convertions ++++++++*/
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
}
