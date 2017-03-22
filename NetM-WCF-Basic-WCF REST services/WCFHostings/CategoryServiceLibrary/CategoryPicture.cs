﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CategoryServiceLibrary
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Stream))]
    [KnownType(typeof(byte[]))]
    public class CategoryPicture
    {
        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        public MemoryStream PictureStream { get; set; }

        [DataMember]
        public byte[] PictureByteArray { get; set; }

        [DataMember]
        public string FileName { get; set; }
    }
}
