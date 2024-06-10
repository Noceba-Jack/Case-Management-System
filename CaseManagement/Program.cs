using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement
{
    class Program
    {
        //declare global variables
        static List<int> caseNumber = new List<int>();
        static List<int> MASNumber = new List<int>();
        static List<string> investigatingOfficer = new List<string>();
        static List<string> chargeType = new List<string>();
        static List<string> postponementReason = new List<string>();
        static List<string> peopleInvolved = new List<string>();
        static List<DateTime> firstAppearanceDate = new List<DateTime>();
        static List<DateTime> postponementDate = new List<DateTime>();
        static List<DateTime> olderThan6Months = new List<DateTime>();
        static List<bool> isCompleted = new List<bool>();

        static void Main(string[] args)
        {
            //error handling
            try
            {
                //declare the variables
                int iOption = 0;

                //display the menu until user decides to exit program
                do
                {
                    //clear the console
                    Console.Clear();

                    //display the menu and collect user input
                    Console.WriteLine("---------------Case Management System---------------\n");
                    Console.Write("Select an action from the menu below:\n\t1.Add case\n\t2.View cases older than 6 months\n\t3.Display today's cases\n\t4.Display all cases\n\t5.Remove completed cases\n\t6.Search cases\n\t0.Exit program");

                    Console.Write("\n\nOption: ");
                    iOption = int.Parse(Console.ReadLine());

                    //clear the console
                    Console.Clear();

                    //carry put relevant method based on user selection
                    switch (iOption)
                    {
                        case 1:
                            AddCase();
                            break;

                        case 2:
                            ViewOldCases();
                            break;

                        case 3:
                            TodaysCases();
                            break;

                        case 4:
                            DisplayAllCases();
                            break;

                        case 5:
                            RemoveCompletedCases();
                            break;

                        case 6:
                            Console.WriteLine("What would you like to search by: \n\t1.Case Number\n\t2.MAS Number\n\t3.Charge Type\n\t4.Investigating Officer(s)\n\t5.People Involved\n\t6.Postponement Date");
                            Console.Write("Option: ");
                            int iCaseSearchOption = int.Parse(Console.ReadLine());
                            Console.Write("Enter search term: ");
                            SearchCases(iCaseSearchOption, Console.ReadLine());
                            break;

                        case 0:
                            Console.WriteLine("System closing...");
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please select an option from the menu.");
                            break;
                    }
                } while (iOption != 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
            }

            //allow user to view output
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }//Main()

        //allow user to add cases to system
        static void AddCase()
        {
            //error handling
            try
            {
                //declare the variables
                char cAddAnotherCase;
                int i = 0;

                //allow user to add cases
                do
                {
                    //collect case information and allocate it to corresponding array
                    isCompleted.Add(false);
                    Console.Write("Case number: ");
                    caseNumber.Add(int.Parse(Console.ReadLine()));
                    Console.Write("MAS Number: ");
                    MASNumber.Add(int.Parse(Console.ReadLine()));
                    Console.Write("People Involved: ");
                    peopleInvolved.Add(Console.ReadLine());
                    Console.Write("Charge: ");
                    chargeType.Add(Console.ReadLine());
                    Console.Write("Investigating Officer(s): ");
                    investigatingOfficer.Add(Console.ReadLine());
                    Console.Write("Postponement Reason: ");
                    postponementReason.Add(Console.ReadLine());

                    //validate user dates
                    Console.Write("Date of First Appearance (YYYY-MM-DD): ");
                    string sFirstAppearance = Console.ReadLine();
                    DateTime firstAppearance = DateTime.Parse(sFirstAppearance);
                    firstAppearanceDate.Add(firstAppearance);

                    Console.Write("Date of Postponement (YYYY-MM-DD): ");
                    string sPostponed = Console.ReadLine();
                    DateTime postponed = DateTime.Parse(sPostponed);
                    postponementDate.Add(postponed);

                    //ask user if they want to add another case
                    Console.Write("\nWould you like to add another case? (Y/N): ");
                    cAddAnotherCase = char.Parse(Console.ReadLine().ToUpper());

                    //increment the counter
                    i++;
                } while (cAddAnotherCase != 'N');
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to start again...");
                Console.ReadLine();
            }
        }//AddCase()

        //allow user to view cases older than 6 months
        static void ViewOldCases()
        {
            //error handling
            try
            {
                //declare the counter
                int i = 0;
                int iOlder = 0;
                int j = 1;

                //find current date
                DateTime todayDate = DateTime.Today;
                
                //get the date from six months ago
                DateTime sixMonthsAgo = DateTime.Today.AddMonths(-6);

                Console.WriteLine($"Today's date: {todayDate}, Date 6 months ago: {sixMonthsAgo}");
                //check if user entered date is older that 6 months
                for (; i < firstAppearanceDate.Count; i++)
                {
                    if (firstAppearanceDate[i] <= sixMonthsAgo)
                    {
                        //olderThan6Months[i] = firstAppearanceDate[i];
                        //postponementReasonOld[i] = postponementReason[i];
                        //chargeTypeOld[i] = chargeType[i];
                        //peopleInvolvedOld[i] = peopleInvolved[i];

                        //display the case details
                        Console.WriteLine($"Case {j}: ");
                        Console.WriteLine($"Date of first appearance: {firstAppearanceDate[i]}, Date of postponement: {postponementDate[i]}");
                        Console.WriteLine($"People Involved: {peopleInvolved[i]}, Charge: {chargeType[i]}, Postponement Reason: {postponementReason[i]}\n");
                        
                        iOlder++;
                    }
                }

                //if there are no cases older than 6 months
                if (iOlder == 0)
                {
                    Console.WriteLine("No cases older than 6 months.\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to start again...");
                Console.ReadLine();
            }

            //allow user opportunity to view output
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }//ViewOldCases()

        //view cases with a postponement date of today
        static void TodaysCases()
        {
            try
            {
                //declare variables
                int j = 1;
                int iCounter = 0;

                //find current date
                DateTime todayDate = DateTime.Today;

                //display cases with postponement date of today
                for (int i = 0; i < postponementDate.Count; i++)
                {
                    if (postponementDate[i] == todayDate)
                    {
                        Console.WriteLine($"Case {j}: ");
                        Console.WriteLine($"Case number: {caseNumber[i]}");
                        Console.WriteLine($"MAS Number: {MASNumber[i]}");
                        Console.WriteLine($"People Involved: {peopleInvolved[i]}");
                        Console.WriteLine($"Charge: {chargeType[i]}");
                        Console.WriteLine($"Investigating Officer(s): {investigatingOfficer[i]}");
                        Console.WriteLine($"Postponement Reason: {postponementReason[i]}");
                        Console.WriteLine($"Date of First Appearance (YYYY-MM-DD): {firstAppearanceDate[i]}\n");
                        Console.WriteLine($"Postponement Date: {postponementDate[i]}");

                        //increment counter
                        iCounter++;
                        j++;
                    }
                }

                //if there are no cases for today
                if (iCounter == 0)
                {
                    Console.WriteLine("No cases scheduled for today.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to start again...");
                Console.ReadLine();
            }

            //allow user opportunity to view output
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }//TodaysCases()

        //Display all cases in the system
        static void DisplayAllCases()
        {
            //show all cases and case information
            for (int i = 0; i < caseNumber.Count; i++)
            {
                Console.WriteLine($"Case {i+1}: ");
                Console.WriteLine($"Case number: {caseNumber[i]}");
                Console.WriteLine($"MAS Number: {MASNumber[i]}");
                Console.WriteLine($"People Involved: {peopleInvolved[i]}");
                Console.WriteLine($"Charge: {chargeType[i]}");
                Console.WriteLine($"Investigating Officer(s): {investigatingOfficer[i]}");
                Console.WriteLine($"Postponement Reason: {postponementReason[i]}");
                Console.WriteLine($"Date of First Appearance (YYYY-MM-DD): {firstAppearanceDate[i]}\n");
                Console.WriteLine($"Postponement Date: {postponementDate[i]}");
            }

            //allow user opportunity to view output
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }//DisplayAllCases()

        //remove completed cases
        static void RemoveCompletedCases()
        {
            //prompt user for case number of completed case
            Console.Write("Enter case number of completed case: ");
            int iCompletedCaseNumber = int.Parse(Console.ReadLine());

            //mark the index of the case number as completed in the isCompleted List
            for (int i = 0; i < caseNumber.Count; i++)
            {
                if (caseNumber[i] == iCompletedCaseNumber)
                {
                    isCompleted[i] = true;
                    Console.WriteLine($"Case No. {iCompletedCaseNumber} has been marked as completed.");
                }
            }

            //allow user opportunity to view output
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }//RemoveCompletedCases()

        //search for case information
        static void SearchCases(int _iCaseSearchOption, string sSearchTerm)
        {
            //declare counter
            int j = 1;

            //search the List corresponding to user selection and Display the case information of the corresponding cases that match the search
            switch (_iCaseSearchOption)
            {
                //search by case number
                case 1:
                    //declare the variables
                    int iCounter = 0;
                    int iCaseNumberSearch = int.Parse(sSearchTerm);

                    //inform use of search
                    Console.WriteLine($"Searching for case with case no. {iCaseNumberSearch}...");

                    //search for case with matching case number
                    for (int i = 0; i < caseNumber.Count; i++)
                    {
                        if (caseNumber[i] == iCaseNumberSearch)
                        {
                            Console.WriteLine($"Case number: {caseNumber[i]}");
                            Console.WriteLine($"MAS Number: {MASNumber[i]}");
                            Console.WriteLine($"People Involved: {peopleInvolved[i]}");
                            Console.WriteLine($"Charge: {chargeType[i]}");
                            Console.WriteLine($"Investigating Officer(s): {investigatingOfficer[i]}");
                            Console.WriteLine($"Postponement Reason: {postponementReason[i]}");
                            Console.WriteLine($"Date of First Appearance (YYYY-MM-DD): {firstAppearanceDate[i]}\n");
                            Console.WriteLine($"Postponement Date: {postponementDate[i]}");

                            //increment counter
                            iCounter++;
                        }
                    }

                    //if case is not found
                    if (iCounter == 0)
                    {
                        Console.WriteLine($"Case with case no. {iCaseNumberSearch} not found.");
                    }

                    //allow user opportunity to view output
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;

                //search by MAS Number
                case 2:
                    //declare the variables
                    iCounter = 0;
                    int iMASNumberSearch = int.Parse(sSearchTerm);

                    //inform use of search
                    Console.WriteLine($"Searching for case with MAS no. {iMASNumberSearch}...\n");

                    //search for case with matching case number
                    for (int i = 0; i < caseNumber.Count; i++)
                    {
                        if (MASNumber[i] == iMASNumberSearch)
                        {
                            Console.WriteLine($"Case number: {caseNumber[i]}");
                            Console.WriteLine($"MAS Number: {MASNumber[i]}");
                            Console.WriteLine($"People Involved: {peopleInvolved[i]}");
                            Console.WriteLine($"Charge: {chargeType[i]}");
                            Console.WriteLine($"Investigating Officer(s): {investigatingOfficer[i]}");
                            Console.WriteLine($"Postponement Reason: {postponementReason[i]}");
                            Console.WriteLine($"Date of First Appearance (YYYY-MM-DD): {firstAppearanceDate[i]}\n");
                            Console.WriteLine($"Postponement Date: {postponementDate[i]}");

                            //increment counter
                            iCounter++;
                        }
                    }

                    //if case is not found
                    if (iCounter == 0)
                    {
                        Console.WriteLine($"Case with MAS no. {iMASNumberSearch} not found.");
                    }

                    //allow user opportunity to view output
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;

                //search by charge type
                case 3:
                    //inform user of search
                    Console.WriteLine($"Searching for cases with charge type '{sSearchTerm}'...\n");

                    //declare the variables
                    iCounter = 0;
                    
                    //search and display case with the specific charge type
                    for (int i = 0; i < chargeType.Count; i++)
                    {
                        if (chargeType[i] == sSearchTerm)
                        {
                            Console.WriteLine($"Case {j}: ");
                            Console.WriteLine($"Case number: {caseNumber[i]}");
                            Console.WriteLine($"MAS Number: {MASNumber[i]}");
                            Console.WriteLine($"People Involved: {peopleInvolved[i]}");
                            Console.WriteLine($"Charge: {chargeType[i]}");
                            Console.WriteLine($"Investigating Officer(s): {investigatingOfficer[i]}");
                            Console.WriteLine($"Postponement Reason: {postponementReason[i]}");
                            Console.WriteLine($"Date of First Appearance (YYYY-MM-DD): {firstAppearanceDate[i]}\n");
                            Console.WriteLine($"Postponement Date: {postponementDate[i]}");

                            //increment counter
                            iCounter++;
                            j++;
                        }
                    }

                    //if case with charge type not found
                    if (iCounter == 0)
                    {
                        Console.WriteLine($"Case with charge type '{sSearchTerm}' not found.");
                    }

                    //allow user opportunity to view output
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;

                //search by investigating officer
                case 4:
                    Console.WriteLine($"Searching for case(s) with investigating officer(s) '{sSearchTerm}'...\n");

                    //declare the variables
                    iCounter = 0;

                    //search and display case with the specific investigating officer(s)
                    for (int i = 0; i < investigatingOfficer.Count; i++)
                    {
                        if (investigatingOfficer[i] == sSearchTerm)
                        {
                            Console.WriteLine($"Case {j}: ");
                            Console.WriteLine($"Case number: {caseNumber[i]}");
                            Console.WriteLine($"MAS Number: {MASNumber[i]}");
                            Console.WriteLine($"People Involved: {peopleInvolved[i]}");
                            Console.WriteLine($"Charge: {chargeType[i]}");
                            Console.WriteLine($"Investigating Officer(s): {investigatingOfficer[i]}");
                            Console.WriteLine($"Postponement Reason: {postponementReason[i]}");
                            Console.WriteLine($"Date of First Appearance (YYYY-MM-DD): {firstAppearanceDate[i]}\n");
                            Console.WriteLine($"Postponement Date: {postponementDate[i]}");

                            //increment counter
                            iCounter++;
                            j++;
                        }
                    }

                    //if case with investigating officer(s) not found
                    if (iCounter == 0)
                    {
                        Console.WriteLine($"Case with investigating officer(s) '{sSearchTerm}' not found.");
                    }

                    //allow user opportunity to view output
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                
                //search by people involved
                case 5:
                    Console.WriteLine($"Searching for case(s) with suspect(s) '{sSearchTerm}'...\n");

                    //declare the variables
                    iCounter = 0;

                    //search and display case with the specific charge type
                    for (int i = 0; i < peopleInvolved.Count; i++)
                    {
                        if (peopleInvolved[i] == sSearchTerm)
                        {
                            Console.WriteLine($"Case {j}: ");
                            Console.WriteLine($"Case number: {caseNumber[i]}");
                            Console.WriteLine($"MAS Number: {MASNumber[i]}");
                            Console.WriteLine($"People Involved: {peopleInvolved[i]}");
                            Console.WriteLine($"Charge: {chargeType[i]}");
                            Console.WriteLine($"Investigating Officer(s): {investigatingOfficer[i]}");
                            Console.WriteLine($"Postponement Reason: {postponementReason[i]}");
                            Console.WriteLine($"Date of First Appearance (YYYY-MM-DD): {firstAppearanceDate[i]}\n");
                            Console.WriteLine($"Postponement Date: {postponementDate[i]}");

                            //increment counter
                            iCounter++;
                            j++;
                        }
                    }

                    //if case with suspect(s) not found
                    if (iCounter == 0)
                    {
                        Console.WriteLine($"Case with suspect(s) '{sSearchTerm}' not found.");
                    }

                    //allow user opportunity to view output
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;

                //search ny postponement date
                case 6:
                    Console.WriteLine($"Searching for cases with postponement date '{sSearchTerm}'...");

                    //declare the variables
                    iCounter = 0;
                    DateTime postponementDateSearch = DateTime.Parse(sSearchTerm);

                    //search and display case with the specific postpinement date
                    for (int i = 0; i < postponementDate.Count; i++)
                    {
                        if (postponementDate[i] == postponementDateSearch)
                        {
                            Console.WriteLine($"Case {j}: ");
                            Console.WriteLine($"Case number: {caseNumber[i]}");
                            Console.WriteLine($"MAS Number: {MASNumber[i]}");
                            Console.WriteLine($"People Involved: {peopleInvolved[i]}");
                            Console.WriteLine($"Charge: {chargeType[i]}");
                            Console.WriteLine($"Investigating Officer(s): {investigatingOfficer[i]}");
                            Console.WriteLine($"Postponement Reason: {postponementReason[i]}");
                            Console.WriteLine($"Date of First Appearance (YYYY-MM-DD): {firstAppearanceDate[i]}\n");
                            Console.WriteLine($"Postponement Date: {postponementDate[i]}");

                            //increment counter
                            iCounter++;
                            j++;
                        }
                    }

                    //if case with postponement date not found
                    if (iCounter == 0)
                    {
                        Console.WriteLine($"Case with postponement date '{sSearchTerm}' not found.");
                    }

                    //allow user opportunity to view output
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;

                default:
                    Console.WriteLine("Invalid selection. Please select option from menu.");

                    //allow user opportunity to view output
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }//SearchCases()
    }//class
}//namespace
