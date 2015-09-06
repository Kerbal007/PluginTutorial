using System.Collections.Generic;
using UnityEngine; //calls the unity engine

namespace PluginTutorial.Extensions
{
    public static class PartExtensions // Needs to be of type public static class
    {
        public static bool IsPrimary(this Part thisPart, List<Part> partsList, int moduleClassID) // IsPrimary function checks wether the current part being checked is the primary part
        {
            foreach (Part part in partsList) //loops thru every part in list looking for the moduleClassID
            {
                if (part.Modules.Contains(moduleClassID))
                {
                    if (part == thisPart) // Check if part is equal to our current part
                        return true;
                    else // If not then break out of the loop and return false
                        break;
                }
            }
            return false; // Default value returned
        }
    }
}
