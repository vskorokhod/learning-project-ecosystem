using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecosystem
{
    public struct SafeZone
    {
        public int first_row;
        public int first_column;
        public int last_row;
        public int last_column;
    }

    class Island
    {
        private List<Animal>[,] fields;
        private SafeZone safe_zone;

        public List<Animal>[,] Fields
        {
            get
            {
                return fields;
            }
            set
            {
                fields = value;
            }
        }

        public SafeZone Safe_zone
        {
            get
            {
                return safe_zone;
            }
            set
            {
                safe_zone = value;
            }
        }

        public Island(int n, int fr, int fc, int lr, int lc)
        {
            Fields = new List<Animal>[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Fields[i, j] = new List<Animal>();
                }
            }
            this.safe_zone.first_row = fr;
            this.safe_zone.first_column = fc;
            this.safe_zone.last_row = lr;
            this.safe_zone.last_column = lc;
        }
    }
}
