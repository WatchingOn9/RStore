using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RStore.Models {
    // No use
    public static class Level {
        public static Dictionary<int, string> Qty = new Dictionary<int, string> {
                { 10, "Below 10" },
                { 50, "11 to 50" },
                { 100, "51 to 100" },
                { 250, "101 to 250" },
                { 500, "251 to 500" },
                { 1000, "501 to 1000" },
                { 9999, "1000 and above" }
            };
    }
}
