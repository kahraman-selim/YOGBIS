﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class AutoHistoryVM
    {
        public int Id { get; set; }
        public string RowId { get; set; }
        public string TableName { get; set; }
        public string Changed { get; set; }
        public int Kind { get; set; }
        public DateTime Created { get; set; }
    }
}