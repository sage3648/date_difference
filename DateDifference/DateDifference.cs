using System;
using System.Text.RegularExpressions;

//Author: Sage Stainsby
//Date: 25/08/2019

namespace DateDifference
{
  internal class DateDifference
  {
    public static Month[] Months = new Month[13]; 
    public struct Month
    {
      public double Days; 
    }

    public struct Date
    {
      public int Days;
      public int Months;
      public int Years; 
    }
    
    public static void Main(string[] args)
    {
      for (int i = 0; i < 13; i++)
      {
        double offset = i + 1;
        if (i < 13)
        {
          Months[i].Days = (28 + (offset + Math.Floor(offset / 8)) % 2 + 2 % offset +
                            2 * Math.Floor(1 / offset));
        }
        else
        {
          Months[i].Days = 0; 
        }
      }
      CompareDates();
    }
    
    public static void CompareDates()
    {
      Console.Write("Input Dates: ");
      String inputData = Console.ReadLine();
      
      if (inputData != null)
      {
        //remove whitespace
        inputData = Regex.Replace(inputData, @"\s", "");
      
          Date date1 = new Date();
          Date date2 = new Date();
          
          if (inputData.Length == 17)
          {
            
            date1.Days = Convert.ToInt32(inputData.Substring(0, 2));
            date1.Months = Convert.ToInt32(inputData.Substring(2, 2));
            date1.Years = Convert.ToInt32(inputData.Substring(4, 4));
          
            date2.Days = Convert.ToInt32(inputData.Substring(9, 2));
             date2.Months = Convert.ToInt32(inputData.Substring(11, 2));
            date2.Years = Convert.ToInt32(inputData.Substring(13, 4));
          
          CalculateDifference(date1, date2);
          }
          else
          {
            Console.WriteLine("Incorrect data entry, please enter dates in format DD MM YYYY, DD MM YYYY");
            CompareDates();
        }
      }
    }

    public static bool LeapYear(int year)
    {
      bool leapYear; 
      if (year % 4 == 0)
      {
        if (year % 100 == 0)
        {
          if (year % 400 == 0)
          {
            leapYear = true;
          }
          else
          {
            leapYear = false; 
          }
        }
        else
        {
          leapYear = true;
        }
      }
      else
      {
        leapYear = false; 
      }
      return leapYear; 
    }

    public static void CalculateDifference(Date date1, Date date2)
    {
      int totalDays = 0;
      
      //Reverse operations when date 2 is greater then date 1
      if (date1.Years > date2.Years)
      {
        Date temp = date2;
        date2 = date1; 
        date1 = temp;
      }

      //Check if current year is a leap year
      if (LeapYear(date1.Years))
      {
        Months[1].Days = 29; 
      }
      else
      {
        Months[1].Days = 28; 
      }
      
      //Add remaining days in the month
      totalDays +=  (int)Months[date1.Months - 1].Days - date1.Days;
      
      //Add remaining months in year
      for (int i = date1.Months; i < 12; i++)
      {
        totalDays += (int) Months[i].Days;
      }
      
      //Add remaining days up until final year
      for (int i = date1.Years; i < date2.Years - 1; i++)
      {
        int year = date1.Years + 1; 
        if (LeapYear(year))
        {
          totalDays += 366; 
        }
        else
        {
          totalDays += 365; 
        }
      }
      
      //take into account leap years 
      if (LeapYear(date2.Years))
      {
        Months[1].Days = 29; 
      }
      else
      {
        Months[1].Days = 28; 
      }
      
      //Add remaining days from remaining months 
      for (int i = 1; i < date2.Months; i++)
      {
        totalDays += (int) Months[i - 1].Days;
      }

      //Add remaining days from remaining year
      totalDays +=  date2.Days;
      
      Console.WriteLine(date1.Days + " " + date1.Months + " " + date1.Years + ", " + date2.Days + " " + date2.Months + " " + date2.Years + ", " + totalDays);
      
      //Add remaining months, days, from final year
      CompareDates();
    }
  }
}