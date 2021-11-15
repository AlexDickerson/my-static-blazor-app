using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class DataContainer
    {
     
        public string sessionID { get; set; }   
        public List <Project> projects { get; set; }
        public DataContainer()
        {

        }
    }
}
