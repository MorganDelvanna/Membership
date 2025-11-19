using System;
using System.Text;

namespace PFGA_Membership
{
    internal class clsHelpers
    {
        public String Number2String(int number)
        {
            Char c;
            string retval;

            if (number <= 25)
            {
                c = (Char)(65 + (number - 1));
                retval = c.ToString();
            }
            else
            {
                c = (Char)(65 + (number - 26));
                retval = string.Concat("A", c.ToString());
            }

            return retval;
        }

        public int thisYear()
        {
            int retVal;

            if (DateTime.Today.Month >= 1 && DateTime.Today.Month < 8)
            {
                retVal = DateTime.Today.Year - 1;
            }
            else
            {
                retVal = DateTime.Today.Year;
            }

            return retVal;
        }

        public string getSectionLabels(string sectionFlag)
        {
            StringBuilder retVal = new StringBuilder();
            BitField section = new BitField();
            ulong sectionMask = 0;

            if (ulong.TryParse(sectionFlag, out sectionMask))
            {

                section.Mask = sectionMask;

                try
                {
                    if (section.AnyOn(BitField.Flag.f1)) //
                    {
                        retVal.Append("Archery, ");
                    }

                    if (section.AnyOn(BitField.Flag.f2)) //
                    {
                        retVal.Append("Handgun, ");
                    }

                    if (section.AnyOn(BitField.Flag.f3)) //
                    {
                        retVal.Append("Smallbore, ");
                    }

                    if (section.AnyOn(BitField.Flag.f4)) //
                    {
                        retVal.Append("SCA, ");
                    }

                    if (section.AnyOn(BitField.Flag.f5)) //
                    {
                        retVal.Append("Rifle, ");
                    }

                    if (section.AnyOn(BitField.Flag.f6)) //
                    {
                        retVal.Append("Action, ");
                    }

                    if (retVal.Length > 0)
                    {
                        retVal.Remove(retVal.Length - 2, 2);
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.Log("Error setting Section Checkboxes", ex, true);
                }
            }

            return retVal.ToString();
        }

        public string getParticipation(string participationFlag)
        {
            StringBuilder retVal = new StringBuilder();
            BitField participation = new BitField();
            ulong participationMask = 0;

            if (ulong.TryParse(participationFlag, out participationMask))
            {
                participation.Mask = participationMask;

                try
                {
                    if (participation.AnyOn(BitField.Flag.f1)) //
                    {
                        retVal.Append("Work Party, ");
                    }

                    if (participation.AnyOn(BitField.Flag.f2)) //
                    {
                        retVal.Append("Events, ");
                    }

                    if (participation.AnyOn(BitField.Flag.f3)) //
                    {
                        retVal.Append("Executive, ");
                    }

                    if (participation.AnyOn(BitField.Flag.f4)) //
                    {
                        retVal.Append("Range Officer, ");
                    }

                    if (participation.AnyOn(BitField.Flag.f5)) //
                    {
                        retVal.Append("Training Officer, ");
                    }

                    if (participation.AnyOn(BitField.Flag.f6)) //
                    {
                        retVal.Append("Other ");
                    }

                    if (retVal.Length > 0)
                    {
                        retVal.Remove(retVal.Length - 2, 2);
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.Log("Error setting Section Checkboxes", ex, true);
                }
            }

            return retVal.ToString();
        }
    }
}
