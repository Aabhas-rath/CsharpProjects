﻿using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Image: IModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int Version { get; set; }
    }
}
